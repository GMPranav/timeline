using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace timeline2
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public static class Global
    {
        public static byte[] SaveData;
        public static int PlatformOffsetHack;
        public static string SavePlatform;
        public static string selectedPath;
        public static string IsCharacterHacked;

        public static int[, ,] SecWpnHexData = new int[8, 18, 4];
        public static string[, ,] SecWpnParams = new string[8, 18, 8];
        public static string[] SecWpnCategory = new string[8];
        public static int CurrentSecWpnCategoryID;
        public static int CurrentSecWpnID;
        public static int[] CurrentSecWpnDurabity = new int[2];

        public static int[,] PriWpnHexData = new int[9, 4];
        public static string[] PriWpnParams = new string[9];
        public static int CurrentPriWpnID;

        public static int[,,] CheckpointHexData = new int[3, 210, 4];
        public static int[,,] LocationHexData = new int[3, 210, 4];
        public static string[,,] LocationParams = new string[3, 210, 1];
        public static string[] LocationCategory = new string[3];
        public static int CurrentLocationCategoryID;
        public static int CurrentLocationID;
        public static int category;
        public static int counter;

        public static int FilledSandTanks;

        public static int[] CurrentLife = new int[2];
        public static int[] MaxLife = new int[2];
        public static int[, ,] LifeHexData = new int[3, 10, 2];
        public static int[,] LifeData = new int[3, 10];

        public static int CurrentLifeID;
        public static int MaxLifeID;

        public static int CurrentDifficulty;
        public static string[] DifficultyParams = new string[3];

        public static int CurrentSandPowersTanks;
        public static string[] SandPowersTanksParams = new string[10];
        
        public static int CurrentCharacterID;
        public static int CurrentCharacterCategoryID;
        public static int CurrentStorylineID;
        public static int CurrentStorylineCategoryID;
        public static string[,] CharacterStorylineParams = new string[90, 2];
        public static int[,] CharacterStorylineCheckpointHexData = new int[90, 4];
        public static int[,] CharacterStorylineLocationHexData = new int[90, 4];

        //offsets
        public static int sd_char;
        public static int sd_storyline;
        public static int sd_sandpowerstanks;
        public static int sd_currentlife;
        public static int sd_maxlife;
        public static int sd_difficulty;
        public static int sd_filledsandtanks;
        public static int sd_secwpndurabity;
        public static int sd_priwpn;
        public static int sd_secwpn;
        public static int sd_checkpoint;
        public static int sd_location;
        public static int sd_ps2checksum;
        public static int sd_ps2checksum_2;

        public static int PS2SaveSlots = 0;
        public static int[,] PS2SaveSlotInfo = new int[20, 2];

        public static byte[] ReadFile(string pathSource)
        {


            try
            {

                using (FileStream fsSource = new FileStream(pathSource,
                    FileMode.Open, FileAccess.Read))
                {

                    // Read the source file into a byte array.
                    byte[] bytes = new byte[fsSource.Length];

                    int numBytesToRead = (int)fsSource.Length;
                    int numBytesRead = 0;
                    while (numBytesToRead > 0)
                    {
                        // Read may return anything from 0 to numBytesToRead.
                        int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                        // Break when the end of the file is reached.
                        if (n == 0)
                            break;

                        numBytesRead += n;
                        numBytesToRead -= n;
                    }
                    numBytesToRead = bytes.Length;
                    return bytes;
                }
            }
            catch (FileNotFoundException ioEx)
            {
                Console.WriteLine(ioEx.Message);
                byte[] placeholder = new byte[1];
                return placeholder;
            }

        }

        public static void SaveFile(string pathNew, byte[] bytes)
        {
            // Write the byte array to the other FileStream.
            using (FileStream fsNew = new FileStream(pathNew,
                FileMode.Create, FileAccess.Write))
            {
                fsNew.Write(bytes, 0, bytes.Length);
            }
        }

        public static void DetectCharacterStoryline()
        {
            //offset 44 - character; offset 48184 - storyline
            //DETECT CHARACTER
            for (int i = 0; i < CharacterStorylineCheckpointHexData.GetLength(0); i++)
            {
                if (SaveData[PlatformOffsetHack + sd_char] == CharacterStorylineCheckpointHexData[i, 0] && SaveData[PlatformOffsetHack + (sd_char + 1)] == CharacterStorylineCheckpointHexData[i, 1] && SaveData[PlatformOffsetHack + (sd_char + 2)] == CharacterStorylineCheckpointHexData[i, 2] && SaveData[PlatformOffsetHack + (sd_char + 3)] == CharacterStorylineCheckpointHexData[i, 3])
                {
                    CurrentCharacterID = i;
                    i = CharacterStorylineCheckpointHexData.GetLength(0);
                }
                else if (SaveData[PlatformOffsetHack + sd_char] != CharacterStorylineCheckpointHexData[i, 0] && SaveData[PlatformOffsetHack + (sd_char + 1)] != CharacterStorylineCheckpointHexData[i, 1] && SaveData[PlatformOffsetHack + (sd_char + 2)] != CharacterStorylineCheckpointHexData[i, 2] && SaveData[PlatformOffsetHack + (sd_char + 3)] != CharacterStorylineCheckpointHexData[i, 3])
                {
                    CurrentCharacterID = 0x100;
                }
            }


            //DETECT STORYLINE
            for (int j = 0; j < CharacterStorylineCheckpointHexData.GetLength(0); j++)
            {
                if (SaveData[PlatformOffsetHack + sd_storyline] == CharacterStorylineCheckpointHexData[j, 0] && SaveData[PlatformOffsetHack + (sd_storyline + 1)] == CharacterStorylineCheckpointHexData[j, 1] && SaveData[PlatformOffsetHack + (sd_storyline + 2)] == CharacterStorylineCheckpointHexData[j, 2] && SaveData[PlatformOffsetHack + (sd_storyline + 3)] == CharacterStorylineCheckpointHexData[j, 3])
                {
                    CurrentStorylineID = j;
                    j = CharacterStorylineCheckpointHexData.GetLength(0);
                }
                else if (SaveData[PlatformOffsetHack + sd_storyline] != CharacterStorylineCheckpointHexData[j, 0] && SaveData[PlatformOffsetHack + (sd_storyline + 1)] != CharacterStorylineCheckpointHexData[j, 1] && SaveData[PlatformOffsetHack + (sd_storyline + 2)] != CharacterStorylineCheckpointHexData[j, 2] && SaveData[PlatformOffsetHack + (sd_storyline + 3)] != CharacterStorylineCheckpointHexData[j, 3])
                {
                    CurrentStorylineID = 0x100;
                }
            }



            //DETECT CHARACTER HACK
            if (SaveData[PlatformOffsetHack + sd_char] == SaveData[PlatformOffsetHack + sd_storyline] && SaveData[PlatformOffsetHack + (sd_char + 1)] == SaveData[PlatformOffsetHack + (sd_storyline + 1)] && SaveData[PlatformOffsetHack + (sd_char + 2)] == SaveData[PlatformOffsetHack + (sd_storyline + 2)] && SaveData[PlatformOffsetHack + (sd_char + 3)] == SaveData[PlatformOffsetHack + (sd_storyline + 3)])
            {
                IsCharacterHacked = "No";
            }
            else if (SaveData[PlatformOffsetHack + sd_char] != SaveData[PlatformOffsetHack + sd_storyline] && SaveData[PlatformOffsetHack + (sd_char + 1)] != SaveData[PlatformOffsetHack + (sd_storyline + 1)] && SaveData[PlatformOffsetHack + (sd_char + 2)] != SaveData[PlatformOffsetHack + (sd_storyline + 2)] && SaveData[PlatformOffsetHack + (sd_char + 3)] != SaveData[PlatformOffsetHack + (sd_storyline + 3)])
            {
                IsCharacterHacked = "Yes";
            }
        }

        public static void DetectSandPowersTanks()
        {
            SandPowersTanksParams[0] = "None";
            SandPowersTanksParams[1] = "Recall";
            SandPowersTanksParams[2] = "Eye of the Storm";
            SandPowersTanksParams[3] = "Breath of Fate";
            SandPowersTanksParams[4] = "4 Sand Tanks";
            SandPowersTanksParams[5] = "Ravages of Time";
            SandPowersTanksParams[6] = "5 Sand Tanks";
            SandPowersTanksParams[7] = "Wind of Fate";
            SandPowersTanksParams[8] = "6 Sand Tanks";
            SandPowersTanksParams[9] = "Cyclone of Fate";
            CurrentSandPowersTanks = SaveData[PlatformOffsetHack + sd_sandpowerstanks];
        }

        public static void DetectLife()
        {
            if (SavePlatform != "PS3")
            {
                CurrentLife[0] = SaveData[PlatformOffsetHack + sd_currentlife];
                CurrentLife[1] = SaveData[PlatformOffsetHack + (sd_currentlife + 1)];
                MaxLife[0] = SaveData[PlatformOffsetHack + sd_maxlife];
                MaxLife[1] = SaveData[PlatformOffsetHack + (sd_maxlife + 1)];
            }
            else if (SavePlatform == "PS3")
            {
                CurrentLife[1] = SaveData[PlatformOffsetHack + sd_currentlife];
                CurrentLife[0] = SaveData[PlatformOffsetHack + (sd_currentlife + 1)];
                MaxLife[1] = SaveData[PlatformOffsetHack + sd_maxlife];
                MaxLife[0] = SaveData[PlatformOffsetHack + (sd_maxlife + 1)];
            }

            for (int i = 0; i < 10; i++)
            {
                if (CurrentLife[0] == LifeHexData[CurrentDifficulty, i, 0] && CurrentLife[1] == LifeHexData[CurrentDifficulty, i, 1])
                {
                    CurrentLifeID = i;
                    i = 10;
                }
                else if (i == 9 && CurrentLife[0] != LifeHexData[CurrentDifficulty, i, 0] && CurrentLife[1] != LifeHexData[CurrentDifficulty, i, 1])
                {
                    CurrentLifeID = 0x100;
                }
            }
            for (int j = 0; j < 10; j++)
            {
                if (MaxLife[0] == LifeHexData[CurrentDifficulty, j, 0] && MaxLife[1] == LifeHexData[CurrentDifficulty, j, 1])
                {
                    MaxLifeID = j;
                    j = 10;
                }
                else if (j == 9 && MaxLife[0] != LifeHexData[CurrentDifficulty, j, 0] && MaxLife[1] != LifeHexData[CurrentDifficulty, j, 1])
                {
                    MaxLifeID = 0x100;
                }
            }
        }

        public static void DetectDifficulty()
        {
            DifficultyParams[0] = "Easy";
            DifficultyParams[1] = "Normal";
            DifficultyParams[2] = "Hard";
            CurrentDifficulty = SaveData[PlatformOffsetHack + sd_difficulty];
        }

        public static void DetectFilledSandTanks()
        {
            FilledSandTanks = SaveData[PlatformOffsetHack + sd_filledsandtanks];
        }

        public static void DetectCurrentSecWpnDurabity()
        {
            if (SavePlatform != "PS3")
            {
                CurrentSecWpnDurabity[0] = SaveData[PlatformOffsetHack + sd_secwpndurabity];
                CurrentSecWpnDurabity[1] = SaveData[PlatformOffsetHack + (sd_secwpndurabity + 1)];
            }
            else if (SavePlatform == "PS3")
            {
                CurrentSecWpnDurabity[1] = SaveData[PlatformOffsetHack + sd_secwpndurabity];
                CurrentSecWpnDurabity[0] = SaveData[PlatformOffsetHack + (sd_secwpndurabity + 1)];
            }
        }

        public static void DetectPriWpn()
        {

            for (int i = 0; i < 9; i++)
            {
                if (SaveData[PlatformOffsetHack + sd_priwpn] == PriWpnHexData[i, 0] && SaveData[PlatformOffsetHack + (sd_priwpn + 1)] == PriWpnHexData[i, 1] && SaveData[PlatformOffsetHack + (sd_priwpn + 2)] == PriWpnHexData[i, 2] && SaveData[PlatformOffsetHack + (sd_priwpn + 3)] == PriWpnHexData[i, 3])
                {
                    CurrentPriWpnID = i;
                    i = 9;
                }
                else if (i == 8 && SaveData[PlatformOffsetHack + sd_priwpn] != PriWpnHexData[i, 0] && SaveData[PlatformOffsetHack + (sd_priwpn + 1)] != PriWpnHexData[i, 1] && SaveData[PlatformOffsetHack + (sd_priwpn + 2)] != PriWpnHexData[i, 2] && SaveData[PlatformOffsetHack + (sd_priwpn + 3)] != PriWpnHexData[i, 3])
                {
                    CurrentPriWpnID = 0x100;
                }
            }
        }

        public static void DetectSecWpn()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 18; j++)
                    if (SaveData[PlatformOffsetHack + sd_secwpn] == SecWpnHexData[i, j, 0] && SaveData[PlatformOffsetHack + (sd_secwpn + 1)] == SecWpnHexData[i, j, 1] && SaveData[PlatformOffsetHack + (sd_secwpn + 2)] == SecWpnHexData[i, j, 2] && SaveData[PlatformOffsetHack + (sd_secwpn + 3)] == SecWpnHexData[i, j, 3])
                    {
                        CurrentSecWpnCategoryID = i;
                        CurrentSecWpnID = j;
                        i = 8;
                        j = 18;
                    }
                    else if (i == 7 && j == 17 && SaveData[PlatformOffsetHack + sd_secwpn] != SecWpnHexData[i, j, 0] && SaveData[PlatformOffsetHack + (sd_secwpn + 1)] != SecWpnHexData[i, j, 1] && SaveData[PlatformOffsetHack + (sd_secwpn + 2)] != SecWpnHexData[i, j, 2] && SaveData[PlatformOffsetHack + (sd_secwpn + 3)] != SecWpnHexData[i, j, 3])
                    {
                        CurrentSecWpnCategoryID = 0x100;
                        CurrentSecWpnID = 0x100;
                    }
            }
        }

        public static void DetectLocation()
        {
            for (int i = 0; i < CheckpointHexData.GetLength(0); i++)
            {
                for (int j = 0; j < CheckpointHexData.GetLength(1); j++)
                    if (SaveData[PlatformOffsetHack + sd_checkpoint] == CheckpointHexData[i, j, 0] && SaveData[PlatformOffsetHack + (sd_checkpoint + 1)] == CheckpointHexData[i, j, 1] && SaveData[PlatformOffsetHack + (sd_checkpoint + 2)] == CheckpointHexData[i, j, 2] && SaveData[PlatformOffsetHack + (sd_checkpoint + 3)] == CheckpointHexData[i, j, 3] && SaveData[PlatformOffsetHack + sd_location] == LocationHexData[i, j, 0] && SaveData[PlatformOffsetHack + (sd_location + 1)] == LocationHexData[i, j, 1] && SaveData[PlatformOffsetHack + (sd_location + 2)] == LocationHexData[i, j, 2] && SaveData[PlatformOffsetHack + (sd_location + 3)] == LocationHexData[i, j, 3])
                    {
                        CurrentLocationCategoryID = i;
                        CurrentLocationID = j;
                        i = CheckpointHexData.GetLength(0);
                        j = CheckpointHexData.GetLength(1);
                    }
                    else if (SaveData[PlatformOffsetHack + sd_checkpoint] != CheckpointHexData[i, j, 0] && SaveData[PlatformOffsetHack + (sd_checkpoint + 1)] != CheckpointHexData[i, j, 1] && SaveData[PlatformOffsetHack + (sd_checkpoint + 2)] != CheckpointHexData[i, j, 2] && SaveData[PlatformOffsetHack + (sd_checkpoint + 3)] != CheckpointHexData[i, j, 3] && SaveData[PlatformOffsetHack + sd_location] != LocationHexData[i, j, 0] && SaveData[PlatformOffsetHack + (sd_location + 1)] != LocationHexData[i, j, 1] && SaveData[PlatformOffsetHack + (sd_location + 2)] != LocationHexData[i, j, 2] && SaveData[PlatformOffsetHack + (sd_location + 3)] != LocationHexData[i, j, 3])
                    {
                        CurrentLocationCategoryID = 0x100;
                        CurrentLocationID = 0x100;
                    }
            }
        }

        public static void AddLocation(string LocationName, string LocationPlatform, uint CheckpointData, uint LocationData)
        {
            if (LocationPlatform == "ALL" || LocationPlatform == SavePlatform)
            {
                LocationParams[category, counter, 0] = LocationName;

                if (SavePlatform != "PS3")
                {
                    CheckpointHexData[category, counter, 0] = Convert.ToInt32(CheckpointData / 0x1000000);
                    CheckpointHexData[category, counter, 1] = Convert.ToInt32((CheckpointData % 0x1000000) / 0x10000);
                    CheckpointHexData[category, counter, 2] = Convert.ToInt32((CheckpointData % 0x10000) / 0x100);
                    CheckpointHexData[category, counter, 3] = Convert.ToInt32(CheckpointData % 0x100);
                    LocationHexData[category, counter, 0] = Convert.ToInt32(LocationData / 0x1000000);
                    LocationHexData[category, counter, 1] = Convert.ToInt32((LocationData % 0x1000000) / 0x10000);
                    LocationHexData[category, counter, 2] = Convert.ToInt32((LocationData % 0x10000) / 0x100);
                    LocationHexData[category, counter, 3] = Convert.ToInt32(LocationData % 0x100);
                }
                else if (SavePlatform == "PS3")
                {
                    CheckpointHexData[category, counter, 3] = Convert.ToInt32(CheckpointData / 0x1000000);
                    CheckpointHexData[category, counter, 2] = Convert.ToInt32((CheckpointData % 0x1000000) / 0x10000);
                    CheckpointHexData[category, counter, 1] = Convert.ToInt32((CheckpointData % 0x10000) / 0x100);
                    CheckpointHexData[category, counter, 0] = Convert.ToInt32(CheckpointData % 0x100);
                    LocationHexData[category, counter, 3] = Convert.ToInt32(LocationData / 0x1000000);
                    LocationHexData[category, counter, 2] = Convert.ToInt32((LocationData % 0x1000000) / 0x10000);
                    LocationHexData[category, counter, 1] = Convert.ToInt32((LocationData % 0x10000) / 0x100);
                    LocationHexData[category, counter, 0] = Convert.ToInt32(LocationData % 0x100);
                }
                counter++;
            }
        }

        public static void AddCharacterStoryline(string StorylineName, string Character, uint CharacterStorylineCheckpointData, uint CharacterStorylineLocationData)
        {
            CharacterStorylineParams[counter, 0] = StorylineName;
            CharacterStorylineParams[counter, 1] = Character;

            if (SavePlatform != "PS3")
            {
                CharacterStorylineCheckpointHexData[counter, 0] = Convert.ToInt32(CharacterStorylineCheckpointData / 0x1000000);
                CharacterStorylineCheckpointHexData[counter, 1] = Convert.ToInt32((CharacterStorylineCheckpointData % 0x1000000) / 0x10000);
                CharacterStorylineCheckpointHexData[counter, 2] = Convert.ToInt32((CharacterStorylineCheckpointData % 0x10000) / 0x100);
                CharacterStorylineCheckpointHexData[counter, 3] = Convert.ToInt32(CharacterStorylineCheckpointData % 0x100);
                CharacterStorylineLocationHexData[counter, 0] = Convert.ToInt32(CharacterStorylineLocationData / 0x1000000);
                CharacterStorylineLocationHexData[counter, 1] = Convert.ToInt32((CharacterStorylineLocationData % 0x1000000) / 0x10000);
                CharacterStorylineLocationHexData[counter, 2] = Convert.ToInt32((CharacterStorylineLocationData % 0x10000) / 0x100);
                CharacterStorylineLocationHexData[counter, 3] = Convert.ToInt32(CharacterStorylineLocationData % 0x100);
            }
            else if (SavePlatform == "PS3")
            {
                CharacterStorylineCheckpointHexData[counter, 3] = Convert.ToInt32(CharacterStorylineCheckpointData / 0x1000000);
                CharacterStorylineCheckpointHexData[counter, 2] = Convert.ToInt32((CharacterStorylineCheckpointData % 0x1000000) / 0x10000);
                CharacterStorylineCheckpointHexData[counter, 1] = Convert.ToInt32((CharacterStorylineCheckpointData % 0x10000) / 0x100);
                CharacterStorylineCheckpointHexData[counter, 0] = Convert.ToInt32(CharacterStorylineCheckpointData % 0x100);
                CharacterStorylineLocationHexData[counter, 3] = Convert.ToInt32(CharacterStorylineLocationData / 0x1000000);
                CharacterStorylineLocationHexData[counter, 2] = Convert.ToInt32((CharacterStorylineLocationData % 0x1000000) / 0x10000);
                CharacterStorylineLocationHexData[counter, 1] = Convert.ToInt32((CharacterStorylineLocationData % 0x10000) / 0x100);
                CharacterStorylineLocationHexData[counter, 0] = Convert.ToInt32(CharacterStorylineLocationData % 0x100);
            }
            counter++;
        }

        public static void InitializeSceneDB()
        {
            for (int i = 0; i < CheckpointHexData.GetLength(0); i++)
            {
                for (int j = 0; j < CheckpointHexData.GetLength(1); j++)
                {
                    CheckpointHexData[i, j, 0] = 0x100;
                    CheckpointHexData[i, j, 1] = 0x100;
                    CheckpointHexData[i, j, 2] = 0x100;
                    CheckpointHexData[i, j, 3] = 0x100;

                    LocationHexData[i, j, 0] = 0x100;
                    LocationHexData[i, j, 1] = 0x100;
                    LocationHexData[i, j, 2] = 0x100;
                    LocationHexData[i, j, 3] = 0x100;
                }
            }

            LocationCategory[0] = "Standart";
            LocationCategory[1] = "Advanced";
            LocationCategory[2] = "Advanced-Storygate";

            //------------------STANDART MODE, SAVEPOINTS ONLY------------------------
            category = 0;
            counter = 0;

            AddLocation("Beach - Present", "ALL", 0xBB0F0133, 0x0328082B);
            AddLocation("Fortress Entrance - Present", "ALL", 0x1FF21308, 0xB0FE1208);
            AddLocation("Fortress Entrance - Past", "ALL", 0x4E850011, 0xC14A0011);
            AddLocation("Fortress Entrance - Past", "ALL", 0xF5650192, 0xD7021308);
            AddLocation("Central Hall - Past", "ALL", 0x892100A6, 0x4E0E0004);
            AddLocation("Southern Passage - Past", "ALL", 0x2EE5013C, 0x2A12003B);
            AddLocation("Sacrificial Altar - Past", "ALL", 0x5396023C, 0x89310011);
            AddLocation("Sacrificial Altar - Past", "ALL", 0x5C96023C, 0xCB92023C);
            AddLocation("Southern Passage - Present", "ALL", 0x85850011, 0x63580011);
            AddLocation("Southern Passage - Present", "ALL", 0x28CF013C, 0xD671003B);
            AddLocation("Courtyard - Past", "PSP", 0x1412009C, 0x6101009C);
            AddLocation("Guard Tower - Past", "PSP", 0x4012009C, 0xC402009C);
            AddLocation("Hourglass Chamber - Past", "ALL", 0x7B1A0044, 0x2D17013C);
            AddLocation("Underground Spring - Past", "PSP", 0x7C160093, 0x7103009B);
            AddLocation("Underground River - Past", "PSP", 0x85160093, 0x3504009B);
            AddLocation("Underground Cave - Past", "PSP", 0xC30000F9, 0x3704009B);
            AddLocation("Garden Hall - Past", "ALL", 0xA0311408, 0xBB7D1308);
            AddLocation("Garden - Past", "ALL", 0x13B0023C, 0x5DC5003C);
            AddLocation("Garden Hall - Present", "ALL", 0xA6850011, 0xBA941308);
            AddLocation("Garden - Present", "ALL", 0x20B0023C, 0x66D7003C);
            AddLocation("Garden - Present", "ALL", 0x51D4013C, 0x68D7003C);
            AddLocation("Garden Waterworks - Present", "ALL", 0x64790029, 0x6AD7003C);
            AddLocation("Garden Hall - Past", "ALL", 0xC6850011, 0xF5580011);
            AddLocation("Garden Waterworks - Past", "ALL", 0x1E790029, 0xB82B0029);
            AddLocation("Puzzle Chamber - Past", "PSP", 0x9D030093, 0x7F090097);
            AddLocation("Mechanical Tower - Past", "ALL", 0x410E01A6, 0x80290029);
            AddLocation("Mechanical Tower - Past", "ALL", 0xFD790029, 0x81290029);
            AddLocation("Mechanical Pit - Past", "ALL", 0x547A0029, 0xA6090004);
            AddLocation("Mechanical Pit - Present", "ALL", 0xC9930011, 0xC6580011);
            AddLocation("Activation Room - Present", "ALL", 0x547B0029, 0xD78E0004);
            AddLocation("Activation Room - Past", "ALL", 0xFA930011, 0xAB580011);
            AddLocation("Activation Room - Past", "ALL", 0x88840029, 0xEF8E0004);
            AddLocation("Beach - Present", "ALL", 0x724700A6, 0x0428082B);
            AddLocation("Catacombs - Present", "ALL", 0xA4FC1308, 0x37051308);
            AddLocation("Prison - Present", "ALL", 0x787D1408, 0xCD3B1308);
            AddLocation("Prison - Past", "ALL", 0xF0850011, 0x88580011);
            AddLocation("Prison - Past", "ALL", 0xFBFC1308, 0x38521308);
            AddLocation("Library - Past", "ALL", 0x820C023C, 0x101F0004);
            AddLocation("Library - Past", "ALL", 0xB80C023C, 0x0C1F0004);
            AddLocation("Sacred Caves - Present", "ALL", 0x75850011, 0x5E510011);
            AddLocation("Sacred Caves - Present", "ALL", 0x812C013B, 0x6E480011);
            AddLocation("Sacred Caves - Past", "ALL", 0xB9930011, 0x79510011);
            AddLocation("Sacred Caves - Past", "ALL", 0xAF32013B, 0xCF490011);
            AddLocation("Garden Hall - Present", "ALL", 0xD6850011, 0xF1580011);
            AddLocation("Sacrificial Altar - Past", "ALL", 0x96850011, 0x65580011);
            AddLocation("Cliff - Past", "ALL", 0x27CB013C, 0x28AC1308);
            AddLocation("Fortress Entrance - Past", "ALL", 0xC5F21308, 0x84971308);
            AddLocation("Foundry - Past", "ALL", 0x3A1B0044, 0xCDA51308);
            AddLocation("Behind the Walls - Past", "PSP", 0x092E009C, 0x7F00009C);
            AddLocation("Fire Temple - Past", "PSP", 0xFE29009C, 0x96010095);
            AddLocation("Water Temple - Past", "PSP", 0x062A009C, 0xCF000091);
            AddLocation("Statue Chamber - Past", "PSP", 0x442A009C, 0x7F000095);
            AddLocation("Mystic Caves - Past", "ALL", 0x17E5013C, 0x26030023);
            AddLocation("Mystic Caves - Past", "ALL", 0x357B0029, 0xA8090004);
            AddLocation("Babylon - Present", "ALL", 0x0123023B, 0x14B2013B);

            //-----------------ADVANCED MODE, ALL CHECKPOINTS------------------------
            category = 1;
            counter = 0;

            AddLocation("010_Cale_TEC_Checkpoint01", "ALL", 0x60080004, 0x4A96074D);
            AddLocation("010_Deck_TEC_Checkpoint01", "ALL", 0x324800A6, 0x4B96074D);
            AddLocation("020_Beach_TEC_Savepoint", "ALL", 0xBB0F0133, 0x03280B2B);
            AddLocation("020_Cliff_TEC_Checkpoint01", "ALL", 0x724700A6, 0x0428082B);
            AddLocation("020_PassageTo040_TEC_Checkpoint02", "ALL", 0x1C5C0129, 0x0728082B);
            AddLocation("035_Cliff_TEC_SavePointRef01", "ALL", 0x27CB013C, 0x28AC1308);
            AddLocation("035_Grotte_TEC_Checkpoint01", "ALL", 0x44850029, 0x2EAC1308);
            AddLocation("040_couloirA1HallToGuerite_TEC_Savepoint01", "ALL", 0x1FF21308, 0xB0FE1208);
            AddLocation("040_couloirTo070_TEC_Checkpoint01", "ALL", 0x3CF21308, 0xA6FE1208);
            AddLocation("040_GueriteINT_TEC_Checkpoint01", "ALL", 0x39F21308, 0xAEFE1208);
            AddLocation("040_WARPGATE_TEC_SCT_SavePoint01@Sct_Checkpoint01", "ALL", 0x3E850011, 0xBF4A0011);
            AddLocation("045_couloirA1HallToGuerite_TEC_Savepoint01", "ALL", 0xF5650192, 0xD7021308);
            AddLocation("045_couloirB2HallToGuerite_TEC_SavePoint01", "ALL", 0xC5F21308, 0x84971308);
            AddLocation("045_ExitToBonus_TEC_Checkpoint01", "ALL", 0x31191408, 0x6F971308);
            AddLocation("045_WARPGATE_TEC_SCT_Savepoint01@Sct_Checkpoint01", "ALL", 0x4E850011, 0xC14A0011);
            AddLocation("055_AccessRoom_TEC_Checkpoint01v9", "PSP", 0x8BC902A6, 0x4E0E0004);
            AddLocation("055_AccessRoom_TEC_Savepoint_01", "ALL", 0x892100A6, 0x4E0E0004);
            AddLocation("055_AccessRoom_PostSW_Checkpoint", "ALL", 0x68520133, 0x4E0E0004);
            AddLocation("055_JctTo045_TEC_Checkpoint01", "ALL", 0xD8F21308, 0x440E0004);
            AddLocation("055_JctToBonus_TEC_Checkpoint01", "ALL", 0x15C100A6, 0xD0890004);
            AddLocation("055_JctTGT_Checkpoint_Easy_01", "PSP", 0xB70D0096, 0x3604009C);
            AddLocation("055_LinkTo205B_TEC_Savepoint_01", "ALL", 0x410E01A6, 0x80290029);
            AddLocation("065_HourglassRoom_TEC_Savepoint_01", "ALL", 0x7B1A0044, 0x2D17013C);
            AddLocation("065_WARPGATE_TEC_SCT_Savepoint01@Sct_Checkpoint01", "ALL", 0x64850011, 0x50510011);
            AddLocation("070_Corridor330B_TEC_checkpoint_01", "ALL", 0x0DCF013C, 0x1C12003B);
            AddLocation("070_MainRoom_TEC_checkpoint_01", "ALL", 0x1ECF013C, 0x2612003B);
            AddLocation("070_To040A_TEC_SavePoint_01", "ALL", 0x28CF013C, 0xD671003B);
            AddLocation("070_WARPGATE_TEC_SCT_Savepoint01@Sct_Checkpoint01", "ALL", 0x85850011, 0x63580011);
            AddLocation("075_Corridor055B_TEC_SavePoint001", "ALL", 0x2EE5013C, 0x2A12003B);
            AddLocation("075_CorridorGauntletB_TEC_checkpoint_01", "ALL", 0x6692023C, 0x2834013C);
            AddLocation("075_MainRoom_TEC_CheckPoint_01", "ALL", 0x7BD4013C, 0x3012003B);
            AddLocation("085_Linker2CliffA_TEC_Checkpoint01", "ALL", 0x24CB013C, 0x7F310011);
            AddLocation("085_Linker2UpgradeB_TEC_SCT_checkpoint_01", "ALL", 0xDA87023C, 0x6F49013C);
            AddLocation("085_Linker2WarpA_TEC_CheckPoint_001", "ALL", 0xFD3E023C, 0x85310011);
            AddLocation("085_LinkerTo075_TEC_SCT_savepoint01", "ALL", 0x5396023C, 0x89310011);
            AddLocation("085_SacrificeRoom_LowerSection_TEC_Checkpoint01", "ALL", 0x13CB013C, 0xCB92023C);
            AddLocation("085_SacrificeRoom_TEC_SCT_savepoint_01", "ALL", 0x5C96023C, 0xCB92023C);
            AddLocation("085_WARPGATE_TEC_SCT_Savepoint01@Sct_Checkpoint01", "ALL", 0x96850011, 0x65580011);
            AddLocation("100_WARPGATE_A_TEC_SCT_Savepoint01@Sct_Checkpoint01", "ALL", 0xA6850011, 0xBA941308);
            AddLocation("100_WARPGATE_B_TEC_SCT_Savepoint01@Sct_Checkpoint01", "ALL", 0xD6850011, 0xF1580011);
            AddLocation("105_Hall_TEC_Checkpoint01@Sct_Checkpoint01", "ALL", 0x77F21308, 0xB97D1308);
            AddLocation("105_JonctionTo055_TEC_Savepoint01@Sct_Checkpoint01", "ALL", 0xA0311408, 0xBB7D1308);
            AddLocation("105_WARPGATE_A_TEC_SCT_Savepoint01@Sct_Checkpoint01", "ALL", 0xB6850011, 0xF3580011);
            AddLocation("105_WARPGATE_B_TEC_SCT_Savepoint01@Sct_Checkpoint01", "ALL", 0xC6850011, 0xF5580011);
            AddLocation("120_To110_TEC_Savepoint01", "ALL", 0x64790029, 0x6AD7003C);
            AddLocation("110_LoadingBalcony_TEC_SCT_savepoint01", "ALL", 0x20B0023C, 0x66D7003C);
            AddLocation("110_LowerGarden_TEC_SCT_SavePointRef_01", "ALL", 0x51D4013C, 0x68D7003C);
            AddLocation("115_ArmorRoom_TEC_SCT_checkpoint_01", "ALL", 0xD687023C, 0xC3E1003C);
            AddLocation("115_LoadingBalcony_TEC_SCT_savepoint_01", "ALL", 0x13B0023C, 0x5DC5003C);
            AddLocation("115_LowerGarden_TEC_SCT_CheckPoint01Trapeze", "ALL", 0xD0CE013C, 0x5FC5003C);
            AddLocation("120_Main_TEC_Checkpoint01", "ALL", 0x562201A6, 0xC52C0029);
            AddLocation("125_To105_TEC_Savepoint_01", "ALL", 0x1E790029, 0xB82B0029);
            AddLocation("125_ToUpgrade01_SCT_CheckPoint", "ALL", 0xCEA30029, 0x712D0029);
            AddLocation("125_VegGoal_TEC_Checkpoint01", "ALL", 0x1B790029, 0xB62B0029);
            AddLocation("205_Entrance_TEC_Savepoint01", "ALL", 0xFD790029, 0x81290029);
            AddLocation("205_JctTo215_TEC_Checkpoint01", "ALL", 0xFEDA0029, 0x5E2A0029);
            AddLocation("205_MechTower_TEC_Checkpoint01", "ALL", 0x087A0029, 0x2C2A0029);
            AddLocation("205_MechTower_TEC_Checkpoint02", "ALL", 0x0B7A0029, 0x2C2A0029);
            AddLocation("210_TowerMec1_TEC_Checkpoint01", "ALL", 0xA1850029, 0xE30A0004);
            AddLocation("210_WARPGATE_TEC_SCT_Savepoint01@Sct_Checkpoint01", "ALL", 0xC9930011, 0xC6580011);
            //AddLocation("215_E3_TEC_Checkpoint01", "ALL", 0x275A0004, 0x77290004); //DEMO LOCATION, WORKING ONLY THROUGH "START NEW GAME" (checked on PC version)
            AddLocation("215_ExitTo205_TEC_Savepoint01", "ALL", 0x547A0029, 0xA6090004);
            AddLocation("215_ExitTo605_TEC_Savepoint01", "ALL", 0x357B0029, 0xA8090004);
            AddLocation("215_TowerMec1_TEC_Checkpoint01", "ALL", 0xAA7A0029, 0xB2090004);
            AddLocation("215_TowerMec1_TEC_Checkpoint02", "ALL", 0xBB7A0029, 0xB2090004);
            AddLocation("215_WARPGATE_TEC_SCT_Savepoint01@Sct_Checkpoint01", "ALL", 0xD9930011, 0xC8580011);
            AddLocation("230_ExitToMecpit_TEC_Checkpoint01", "ALL", 0x22900004, 0xD78E0004);
            AddLocation("230_ExitToMecpit_TEC_Savepoint01", "ALL", 0x547B0029, 0xD78E0004);
            AddLocation("230_TowerMec2_TEC_Checkpoint01", "ALL", 0x837B0029, 0xDD8E0004);
            AddLocation("230_TowerMec2_TEC_Checkpoint02", "ALL", 0xB37B0029, 0xDD8E0004);
            AddLocation("230_WARPGATE_TEC_SCT_Savepoint01@Sct_Checkpoint01", "ALL", 0xE9930011, 0xA9580011);
            AddLocation("235_JctToBonus_SCT_Checkpoint", "ALL", 0xCAA30029, 0xE58E0004);
            AddLocation("235_TowerMec2_TEC_Checkpoint01", "ALL", 0xCB840029, 0xEF8E0004);
            AddLocation("235_TowerMec2_TEC_Checkpoint02", "ALL", 0xED840029, 0xEF8E0004);
            AddLocation("235_TowerMec2_TEC_Checkpoint03", "ALL", 0x1C270129, 0xEF8E0004);
            AddLocation("235_TowerMec2_TEC_Savepoint01", "ALL", 0x88840029, 0xEF8E0004);
            AddLocation("235_WARPGATE_TEC_SCT_Savepoint01@Sct_Checkpoint01", "ALL", 0xFA930011, 0xAB580011);
            AddLocation("330_CorrFrom100_SCT_Checkpoint_Easy_01", "PSP", 0x89DD019C, 0x0CFB003C);
            AddLocation("330_CorrFrom100_SCT_Checkpoint_01", "PSP", 0xA5DD019C, 0x0CFB003C);
            AddLocation("330_CorrFrom100_TEC_Checkpoint_01", "PSP", 0x25FC003C, 0x4127009C);
            AddLocation("330_InsideA_SCT_Checkpoint_01", "PSP", 0xABDD019C, 0x0EFB003C);
            AddLocation("330_OutsideA_SCT_Checkpoint_Easy_02", "PSP", 0xA8DD019C, 0x10FB003C);
            AddLocation("330_OutsideB_SCT_Checkpoint_Easy_01", "PSP", 0xAEDD019C, 0x12FB003C);
            AddLocation("400_catacombINT_TEC_Checkpoint01", "ALL", 0x97FC1308, 0x37051308);
            //AddLocation("400_catacombINT_TEC_Checkpoint02", "ALL", 0x9BFC1308, 0x37051308); //NOT WORKING?
            //AddLocation("400_catacombINT_TEC_Checkpoint03", "ALL", 0x9EFC1308, 0x37051308); //NOT WORKING?
            //AddLocation("400_catacombINT_TEC_Checkpoint04", "ALL", 0xA1FC1308, 0x37051308); //NOT WORKING?
            AddLocation("400_catacombINT_TEC_SavePoint01", "ALL", 0xA4FC1308, 0x37051308);
            AddLocation("410_Rc01_TEC_Checkpoint01", "ALL", 0xD9010048, 0x310D0067);
            AddLocation("410_Rc01_TEC_SavePoint01@Sct_Checkpoint01", "ALL", 0x787D1408, 0xCD3B1308);
            AddLocation("410_WARPGATE_TEC_SCT_Savepoint01@Sct_Checkpoint01", "ALL", 0x00860011, 0x86580011);
            AddLocation("415_ExitTo615_TEC_Savepoint", "ALL", 0x3A1B0044, 0xCDA51308);
            AddLocation("415_ExitToBonus_TEC_Checkpoint01", "ALL", 0xB10900AD, 0xB17E0001);
            AddLocation("415_Rc01_B_TEC_CheckPoint01", "ALL", 0xADFC1308, 0xE9D01308);
            AddLocation("415_Rc01_B_TEC_Checkpoint01@Sct_Checkpoint01", "ALL", 0xCFA01408, 0xE9D01308);
            AddLocation("415_Rc02_TEC_CheckPoint01", "ALL", 0xB0FC1308, 0x34521308);
            AddLocation("415_Ss01_TEC_SavePoint01", "ALL", 0xFBFC1308, 0x38521308);
            AddLocation("415_WARPGATE_TEC_SCT_Savepoint01@Sct_Checkpoint01", "ALL", 0xF0850011, 0x88580011);
            AddLocation("425_BiblioHall_TEC_CheckPoint_01", "ALL", 0x860C023C, 0x151F0004);
            AddLocation("425_ExitTo415_TEC_SavePoint_01", "ALL", 0x820C023C, 0x101F0004);
            AddLocation("425_ExitTo435_TEC_CheckPoint_01", "ALL", 0xAE0C023C, 0x0E1F0004);
            AddLocation("425_JctTo205_TEC_SavePoint_01", "ALL", 0xB80C023C, 0x0C1F0004);
            AddLocation("425_JctToBonus_TEC_SCT_checkpoint_01", "ALL", 0xD387023C, 0x94890004);
            AddLocation("605_Bridge_TEC_SavePoint_01", "ALL", 0x17E5013C, 0x26030023);
            AddLocation("605_SanctEpe_Pont_TEC_CheckPoint01", "ALL", 0x25E5013C, 0x41030023);
            AddLocation("615_Forge_TEC_Checkpoint01", "ALL", 0x3E1B0044, 0xCDA51308);
            AddLocation("710_CorridorT09A_TEC_CheckpointV01_04", "ALL", 0x64EF003B, 0x6E480011);
            AddLocation("710_CorridorT09A_TEC_SavePoint_Ftn01", "ALL", 0x812C013B, 0x6E480011);
            AddLocation("710_MainRoom_TEC_CheckpointV01_02", "ALL", 0x65FE003B, 0x07B2013B);
            AddLocation("710_MainRoom_TEC_CheckpointV02_02", "ALL", 0xFE22023B, 0x12B2013B);
            AddLocation("710_MainRoom_TEC_CheckpointV03_02", "ALL", 0x0123023B, 0x14B2013B);
            AddLocation("710_WARPGATE_715_TEC_SCT_Savepoint01@Sct_Checkpoint01", "ALL", 0xAA930011, 0x7B510011);
            AddLocation("710_WARPGATE_THRONE_TEC_SCT_Savepoint01", "ALL", 0x75850011, 0x5E510011);
            AddLocation("715_CorridorT09A_TEC_SavePoint_Ftn01", "ALL", 0xAF32013B, 0xCF490011);
            AddLocation("715_MainRoom_TEC_Checkpointv01_02", "ALL", 0x54E5003B, 0xD6490011);
            AddLocation("715_MainRoom_TEC_TmpCheckpoint03_v01@Sct_Checkpoint01", "ALL", 0x5EE5003B, 0xD6490011);
            AddLocation("715_WARPGATE_TEC_SCT_Savepoint01@Sct_Checkpoint01", "ALL", 0xB9930011, 0x79510011);
            AddLocation("805_Hallway02_SCT_Checkpoint@Sct_Checkpoint01", "PSP", 0x4A02009C, 0x7B00009C);
            AddLocation("805_Hallway04_SCT_Checkpoint", "PSP", 0x4E02009C, 0x7F00009C);
            AddLocation("805_Hallway04_TEC_SavePoint", "PSP", 0x092E009C, 0x7F00009C);
            AddLocation("GGT_ExitHall_SCT_Checkpoint", "PSP", 0x990300F9, 0x8F03009B);
            AddLocation("GGT_PW_Top_Checkpoint_01@Sct_Checkpoint01", "PSP", 0x7C160093, 0x7103009B);
            AddLocation("GGT_PW_TOP_SCT_Checkpoint_Easy_01", "PSP", 0x67DD019C, 0x7103009B);
            AddLocation("GGT_PW_TOP_SCT_Checkpoint_Easy_02", "PSP", 0x6ADD019C, 0x7103009B);
            AddLocation("GGT_River_B_Checkpoint_01@Sct_Checkpoint01", "PSP", 0x85160093, 0x3504009B);
            AddLocation("GGT_River_B_SCT_Checkpoint_Easy_01", "PSP", 0x73DD019C, 0x3504009B);
            AddLocation("GGT_River_B_SCT_Checkpoint_Easy_02", "PSP", 0x76DD019C, 0x3504009B);
            AddLocation("GGT_River_B_SCT_Checkpoint_Easy_03", "PSP", 0x79DD019C, 0x3504009B);
            AddLocation("GGT_River_B_SCT_Checkpoint_Easy_04", "PSP", 0x7CDD019C, 0x3504009B);
            AddLocation("GGT_StartHall_Checkpoint", "PSP", 0x342B009C, 0x7303009B);
            AddLocation("MGT_Hub_TestStartpoint@Sct_Checkpoint01", "PSP", 0xC100009C, 0x7B090097);
            AddLocation("MGT_Levers_Test_Startpoint@Sct_Checkpoint01", "PSP", 0x9601009C, 0x7C090097);
            AddLocation("MGT_Passage_Main_Checkpoint_Easy_01", "PSP", 0xD10D0096, 0xB7000095);
            //AddLocation("MGT_Passage_Main_Checkpoint_Easy_02", "PSP", 0xD50D0096, 0xB7000095); //NOT WORKING?
            AddLocation("MGT_SCT_Checkpoint_Easy_01", "PSP", 0xDE0D0096, 0x7F090097);
            AddLocation("MGT_SCT_Checkpoint_Easy_02", "PSP", 0xE20D0096, 0x7F090097);
            AddLocation("TGT_Court_SCT_Checkpoint_01", "PSP", 0xC40D0096, 0x6101009C);
            AddLocation("TGT_Court_SCT_Checkpoint@Sct_Checkpoint01", "PSP", 0x1412009C, 0x6101009C);
            AddLocation("TGT_CourtEntrance_Checkpoint_Easy_01", "PSP", 0xBB0D0096, 0x7920009C);
            AddLocation("TGT_CourtExit_Checkpoint_Easy_01", "PSP", 0xC70D0096, 0x7720009C);
            //AddLocation("TGT_CourtToBalcony_Checkpoint_Easy_01", "PSP", 0xBE0D0096, 0xBF02009C); //NOT WORKING?
            AddLocation("TGT_CourtToBalcony_SCT_Checkpoint@Sct_Checkpoint01", "PSP", 0x3C12009C, 0xBF02009C);
            AddLocation("TGT_ExitSToCourt_SCT_Checkpoint@Sct_Checkpoint01", "PSP", 0x4A12009C, 0x9304009C);
            AddLocation("TGT_TowerRoom02_SCT_Checkpoint@Sct_Checkpoint01", "PSP", 0x4012009C, 0xC402009C);

            //-----------------STORYGATE-ADVANCED MODE, ALL STORYGATES------------------------
            category = 2;
            counter = 0;

            AddLocation("010_Deck_TEC_StoryGate01", "ALL", 0x7A27082B, 0x4B96074D);
            AddLocation("020_Beach_TEC_StoryGate01", "ALL", 0x4828082B, 0x0328082B);
            AddLocation("020_PassageTo040_TEC_StoryGateV02", "ALL", 0x3DC40033, 0x0728082B);
            AddLocation("035_Cliff_TEC_StoryGateV01", "ALL", 0xCEAC1308, 0x28AC1308);
            AddLocation("040_couloirTo070_TEC_StoryGateV02", "ALL", 0x5BC40033, 0xA6FE1208);
            AddLocation("040_cWARPT01A_TEC_StoryGateV03", "ALL", 0x54C40033, 0xA8FE1208);
            AddLocation("040_hall_TEC_StoryGateV01", "ALL", 0x32FF1208, 0xB0FE1208);
            AddLocation("045_cWARPT01_TEC_StoryGateV01", "ALL", 0xDE021308, 0xD0021308);
            AddLocation("045_cWARPT01_TEC_StoryGateV02", "ALL", 0xA9C80033, 0xD0021308);
            AddLocation("045_ExitTo035_TEC_StoryGateV04", "ALL", 0xC3C80033, 0x84971308);
            AddLocation("045_Hall_TEC_StoryGate_V03", "ALL", 0xBFC80033, 0xD7021308);
            AddLocation("055_AccessRoom_TEC_StoryGateV01", "ALL", 0xE30E0004, 0x4E0E0004);
            AddLocation("055_AccessRoom_TEC_StoryGateV03", "ALL", 0x06C40033, 0x4E0E0004);
            AddLocation("055_AccessRoom_TEC_StoryGateV02", "ALL", 0x09C40033, 0x4E0E0004);
            AddLocation("055_AccessRoom_TEC_StoryGateV04", "ALL", 0x0DC40033, 0x4E0E0004);
            AddLocation("055_AccessRoom_TEC_StoryGateV07", "ALL", 0x10C40033, 0x4E0E0004);
            AddLocation("055_AccessRoom_TEC_StoryGateV06", "ALL", 0x13C40033, 0x4E0E0004);
            AddLocation("055_AccessRoom_TEC_StoryGateV09", "ALL", 0x17C40033, 0x4E0E0004);
            AddLocation("055_AccessRoom_TEC_StoryGateV05", "ALL", 0x1AC40033, 0x4E0E0004);
            AddLocation("055_AccessRoom_TEC_StoryGateV08", "ALL", 0x20C40033, 0x4E0E0004);
            AddLocation("055_LinkTo205B_TEC_StoryGateV10", "ALL", 0x1DC40033, 0x80290029);
            AddLocation("065_ExitTo055_TEC_StoryGateV02", "ALL", 0xFBC90033, 0x2B17013C);
            AddLocation("065_ExitTo055_TEC_StoryGateV03", "ALL", 0xFEC90033, 0x2B17013C);
            AddLocation("065_ExitTo055_TEC_StoryGateV04", "ALL", 0x01CA0033, 0x2B17013C);
            AddLocation("065_ExitTo055_TEC_StoryGateV05", "ALL", 0x04CA0033, 0x2B17013C);
            AddLocation("065_ExitTo055_TEC_StoryGateV01", "ALL", 0xD017013C, 0x2B17013C);
            AddLocation("070_Corridor330B_TEC_StoryGateV02", "ALL", 0xA2C80033, 0x1C12003B);
            AddLocation("070_CorridorT02B_TEC_StoryGateV01", "ALL", 0xD212003B, 0x2412003B);
            AddLocation("075_MainRoom_TEC_StoryGateV01", "ALL", 0x8312003B, 0x3012003B);
            AddLocation("085_Linker2WarpC_TEC_StoryGateV02", "ALL", 0x88C80033, 0x34470011);
            AddLocation("085_LinkerTo075_TEC_StoryGate_V01", "ALL", 0x40320011, 0x89310011);
            AddLocation("100_ExitTo120_TEC_StoryGateV02", "ALL", 0x54C90033, 0xB6941308);
            AddLocation("100_ToWarpT05_TEC_StoryGateV01", "ALL", 0xE5941308, 0xBA941308);
            AddLocation("100_ToWarpT06_TEC_StoryGateV03", "ALL", 0x58C90033, 0xBC941308);
            AddLocation("105_ExitTo115_B_TEC_StoryGateV02", "ALL", 0x2DC80033, 0x81D31308);
            AddLocation("105_Hall_TEC_StoryGateV04", "ALL", 0x38C80033, 0xB97D1308);
            AddLocation("105_Hall_TEC_StoryGateV05", "ALL", 0x3CC80033, 0xB97D1308);
            AddLocation("105_JonctionTo055_TEC_StoryGate01", "ALL", 0x607E1308, 0xBB7D1308);
            AddLocation("105_JonctionTo055_TEC_StoryGateV06", "ALL", 0x2AC80033, 0xBB7D1308);
            AddLocation("105_ToWarpT06_TEC_StoryGateV03", "ALL", 0x34C80033, 0xC17D1308);
            AddLocation("110_CorrTo120_SCT_StoryGateV01", "ALL", 0xE72C0029, 0x6AD7003C);
            AddLocation("110_LoadingBalcony_TEC_StoryGateV01", "ALL", 0xECD7003C, 0x66D7003C);
            AddLocation("115_LoadingBalcony_TEC_StoryGateV01", "ALL", 0x6DC6003C, 0x5DC5003C);
            AddLocation("115_LowerGarden_TEC_StoryGate_Goal01", "ALL", 0xF8C90033, 0x5FC5003C);
            AddLocation("125_VegGoal_TEC_StoryGateV01", "ALL", 0xDB2B0029, 0xB62B0029);
            AddLocation("125_VegGoal_TEC_StoryGateV02", "ALL", 0x0DC90033, 0xB62B0029);
            AddLocation("205_Bridge_TEC_StoryGateV01", "ALL", 0xB92A0029, 0x80290029);
            AddLocation("205_JctTo215_TEC_StoryGateV04", "ALL", 0xCEC80033, 0x5E2A0029);
            AddLocation("205_JctTo235_TEC_StoryGateV02", "ALL", 0xD2C80033, 0x5F2A0029);
            AddLocation("205_JctTo425_TEC_StoryGateV03", "ALL", 0xCAC80033, 0x602A0029);
            AddLocation("210_WarpExit_TEC_StoryGateV01", "ALL", 0x530B0004, 0xE70A0004);
            AddLocation("215_ExitTo605_TEC_StoryGateV02", "ALL", 0x26C40033, 0xA8090004);
            AddLocation("215_TowerMec1_TEC_StoryGateV01", "ALL", 0x570B0004, 0xA6090004);
            AddLocation("235_WarpExit_TEC_StoryGate01", "ALL", 0xC35E0029, 0xF38E0004);
            AddLocation("400_couloirBTo020_TEC_StoryGateV01", "ALL", 0x97051308, 0x3B051308);
            AddLocation("410_ExitTo400_TEC_StoryGateV01", "ALL", 0x443C1308, 0xC93B1308);
            AddLocation("415_ExitTo615_TEC_StoryGateV02", "ALL", 0xBDCA0033, 0xCDA51308);
            AddLocation("415_ToWarpExit_TEC_StoryGateV01", "ALL", 0x60521308, 0x52CA0001);
            AddLocation("425_ExitTo415_TEC_StoryGateV01", "ALL", 0x7A1F0004, 0x101F0004);
            AddLocation("425_ExitTo415_TEC_StoryGateV02", "ALL", 0xEFC80033, 0x101F0004);
            AddLocation("605_Bridge_TEC_StoryGateV01", "ALL", 0xDF000023, 0x26030023);
            AddLocation("615_Forge_TEC_StoryGateV01", "ALL", 0x4F0A0044, 0xCDA51308);
            AddLocation("710_CorridorT08B_TEC_StoryGateV02", "ALL", 0x9EC90033, 0x6C480011);
            AddLocation("710_CorridorT08B_TEC_StoryGateV01", "ALL", 0xB856003B, 0x6C480011);
            AddLocation("710_CorridorT08B_TEC_StoryGateV03", "ALL", 0xB641013B, 0x6C480011);
            AddLocation("715_CorridorT09C_TEC_StoryGateV01", "ALL", 0x4956003B, 0xD3490011);

        }

        public static void InitializeCharacterStorylineDB()
        {
            for (int i = 0; i < CharacterStorylineCheckpointHexData.GetLength(0); i++)
            {
                CharacterStorylineCheckpointHexData[i, 0] = 0x100;
                CharacterStorylineCheckpointHexData[i, 1] = 0x100;
                CharacterStorylineCheckpointHexData[i, 2] = 0x100;
                CharacterStorylineCheckpointHexData[i, 3] = 0x100;
            }

            counter = 0;

            AddCharacterStoryline("Wreckage", "Prince", 0x4828082B, 0x0328082B);
            AddCharacterStoryline("The Ruined Fortress / First Steps in the Past", "Prince", 0x32FF1208, 0xB0FE1208);
            AddCharacterStoryline("First Steps in the Past", "Prince", 0xDE021308, 0xD0021308);
            AddCharacterStoryline("The Fortress Rebuilt / Chasing the Girl in black", "Prince", 0xE30E0004, 0x4E0E0004);
            AddCharacterStoryline("A Damsel in Distress / Fate's Dark Hand", "Prince", 0x40320011, 0x89310011);
            AddCharacterStoryline("Fate's Dark Hand", "Prince", 0xD212003B, 0x2412003B);
            AddCharacterStoryline("A Helping Hand", "Prince", 0x5BC40033, 0xA6FE1208);
            AddCharacterStoryline("A Helping Hand", "Prince", 0xA9C80033, 0xD0021308);
            AddCharacterStoryline("A Helping Hand", "Prince", 0x06C40033, 0x4E0E0004);
            AddCharacterStoryline("The Key and the Lock", "Prince", 0xFBC90033, 0x2B17013C);
            AddCharacterStoryline("The Towers / The Second Tower", "Prince", 0x0DC40033, 0x4E0E0004); //todo-check, if that two storylines is really same
            AddCharacterStoryline("The Water Maiden", "Prince", 0x607E1308, 0xBB7D1308);
            AddCharacterStoryline("Water and Gardens", "Prince", 0x2DC80033, 0x81D31308);
            AddCharacterStoryline("Water and Gardens", "Prince", 0xE5941308, 0xBA941308);
            AddCharacterStoryline("Water and Gardens", "Prince", 0xECD7003C, 0x66D7003C);
            AddCharacterStoryline("Water and Gardens", "Prince", 0x54C90033, 0xB6941308);
            AddCharacterStoryline("Water and Gardens", "Prince", 0x34C80033, 0xC17D1308);
            AddCharacterStoryline("The Second Tower", "Prince", 0xDB2B0029, 0xB62B0029);
            AddCharacterStoryline("The Second Tower", "Prince", 0x38C80033, 0xB97D1308);
            AddCharacterStoryline("The Second Tower", "Prince", 0x1AC40033, 0x4E0E0004);
            AddCharacterStoryline("Clockworks and Gears", "Prince", 0xB92A0029, 0x80290029);
            AddCharacterStoryline("Clockworks and Gears", "Prince", 0x570B0004, 0xA6090004);
            AddCharacterStoryline("Clockworks and Gears", "Prince", 0x22900004, 0xD78E0004);
            AddCharacterStoryline("Clockworks and Gears", "Prince", 0xC35E0029, 0xF38E0004);
            AddCharacterStoryline("The Door is Open", "Prince", 0xD2C80033, 0x5F2A0029);
            AddCharacterStoryline("The Door is Open / The Empress", "Prince", 0x13C40033, 0x4E0E0004);
            AddCharacterStoryline("The Empress", "Prince", 0xFEC90033, 0x2B17013C);
            AddCharacterStoryline("The Long Way Home", "Prince", 0x3DC40033, 0x0728082B);
            AddCharacterStoryline("You Cannot Change Your Fate", "Prince", 0x97051308, 0x3B051308);
            AddCharacterStoryline("A Throne and a Mask", "Prince", 0x443C1308, 0xC93B1308);
            AddCharacterStoryline("A Throne and a Mask", "Prince", 0x60521308, 0x52CA0001);
            AddCharacterStoryline("A Throne and a Mask", "Prince", 0x7A1F0004, 0x101F0004);
            AddCharacterStoryline("A Throne and a Mask", "Prince", 0xCAC80033, 0x602A0029);
            AddCharacterStoryline("A Throne and a Mask", "Prince", 0x20C40033, 0x4E0E0004);
            AddCharacterStoryline("A Throne and a Mask / The Face of Time", "Prince", 0x01CA0033, 0x2B17013C);
            AddCharacterStoryline("The Face of Time", "Prince", 0xB856003B, 0x6C480011);
            AddCharacterStoryline("A Second Chance", "Sand Wraith", 0x64EF003B, 0x6E480011);
            AddCharacterStoryline("A Second Chance", "Sand Wraith", 0x4956003B, 0xD3490011);
            AddCharacterStoryline("A Second Chance", "Sand Wraith", 0x0DC90033, 0xB62B0029);
            AddCharacterStoryline("A Second Chance", "Sand Wraith", 0x3CC80033, 0xB97D1308);
            AddCharacterStoryline("A Second Chance", "Sand Wraith", 0xA2C80033, 0x1C12003B);
            AddCharacterStoryline("A Second Chance", "Sand Wraith", 0x88C80033, 0x34470011);
            AddCharacterStoryline("Mirrored Fates", "Sand Wraith", 0xC3C80033, 0x84971308);
            AddCharacterStoryline("Mirrored Fates", "Sand Wraith", 0xBDCA0033, 0xCDA51308);
            AddCharacterStoryline("Mirrored Fates", "Sand Wraith", 0xEFC80033, 0x101F0004);
            AddCharacterStoryline("Mirrored Fates", "Sand Wraith", 0xDF000023, 0x26030023);
            AddCharacterStoryline("The Race to the Throne", "Sand Wraith", 0x26C40033, 0xA8090004);
            AddCharacterStoryline("The Race to the Throne", "Sand Wraith", 0xCEC80033, 0x5E2A0029);
            AddCharacterStoryline("The Race to the Throne", "Sand Wraith", 0x1DC40033, 0x80290029);
            AddCharacterStoryline("The Death of a Prince", "Prince", 0x68520133, 0x4E0E0004);
            AddCharacterStoryline("The Death of a Prince / The Warrior Within", "Prince", 0x04CA0033, 0x2B17013C);
            AddCharacterStoryline("Dawning of a New Fate", "Prince", 0xB641013B, 0x6C480011);

            AddCharacterStoryline("010_Deck_TEC_StoryGate01", "Prince", 0x7A27082B, 0x4B96074D); //new game
            AddCharacterStoryline("035_Cliff_TEC_StoryGateV01", "Sand Wraith", 0xCEAC1308, 0x28AC1308); //Past. cliff (Mirrored Fates?)
            AddCharacterStoryline("040_cWARPT01A_TEC_StoryGateV03", "Prince", 0x54C40033, 0xA8FE1208); //Present. 1st sand portal in past
            AddCharacterStoryline("045_Hall_TEC_StoryGate_V03", "Prince", 0xBFC80033, 0xD7021308); //??? Past. way into hourglass room opened, towers switch activated, backdoor into throneroom opened
            AddCharacterStoryline("055_AccessRoom_TEC_StoryGateV02", "Prince", 0x09C40033, 0x4E0E0004); //Past. The Fortress Rebuilt / Chasing the Girl in black
            AddCharacterStoryline("055_AccessRoom_TEC_StoryGateV07", "Prince", 0x10C40033, 0x4E0E0004); //??? Past. way into hourglass room opened, towers switch activated, backdoor into throneroom opened
            AddCharacterStoryline("055_AccessRoom_TEC_StoryGateV09", "Sand Wraith", 0x17C40033, 0x4E0E0004); //Past. first Kaileena and Shahdee dialog scene
            AddCharacterStoryline("065_ExitTo055_TEC_StoryGateV01", "Prince", 0xD017013C, 0x2B17013C); //??? Past. empty and closed hourglass room
            AddCharacterStoryline("075_MainRoom_TEC_StoryGateV01", "Prince", 0x8312003B, 0x3012003B); //Past. The Fortress Rebuilt / Chasing the Girl in black
            AddCharacterStoryline("100_ToWarpT06_TEC_StoryGateV03", "Sand Wraith", 0x58C90033, 0xBC941308); //Present. first Sand Wraith's meet with Dahaka and wall into well. doesn't trigger until you jump into time portal
            AddCharacterStoryline("105_JonctionTo055_TEC_StoryGateV06", "Sand Wraith", 0x2AC80033, 0xBB7D1308);//??? Past. in garden past with ability to go back to throne room. teleporting in present triggers Dahaka well scene.
            AddCharacterStoryline("110_CorrTo120_SCT_StoryGateV01", "Prince", 0xE72C0029, 0x6AD7003C); //Present. water tower in present (before Dahaka chase)
            AddCharacterStoryline("115_LoadingBalcony_TEC_StoryGateV01", "Prince", 0x6DC6003C, 0x5DC5003C); //garden-past. first puzzle room to open door into present time-portal
            AddCharacterStoryline("115_LowerGarden_TEC_StoryGate_Goal01", "Prince", 0xF8C90033, 0x5FC5003C); //garden-past. same puzzle room with trigger activated (which activate triggers at time-portal door)
            AddCharacterStoryline("210_WarpExit_TEC_StoryGateV01", "Prince", 0x530B0004, 0xE70A0004); //mechanical tower-present. first portal, which leads us into present.
            AddCharacterStoryline("615_Forge_TEC_StoryGateV01", "Sand Wraith", 0x4F0A0044, 0xCDA51308); //Past. forge puzzle with Sand Wraith
            AddCharacterStoryline("710_CorridorT08B_TEC_StoryGateV02", "Prince", 0x9EC90033, 0x6C480011); //sacred caves-present. final battle
        }

        public static void AddPriWpn(string PriWpnName, uint PriWpnData)
        {
            PriWpnParams[counter] = PriWpnName;

            if (SavePlatform != "PS3")
            {
                PriWpnHexData[counter, 0] = Convert.ToInt32(PriWpnData / 0x1000000);
                PriWpnHexData[counter, 1] = Convert.ToInt32((PriWpnData % 0x1000000) / 0x10000);
                PriWpnHexData[counter, 2] = Convert.ToInt32((PriWpnData % 0x10000) / 0x100);
                PriWpnHexData[counter, 3] = Convert.ToInt32(PriWpnData % 0x100);
            }
            else if (SavePlatform == "PS3")
            {
                PriWpnHexData[counter, 3] = Convert.ToInt32(PriWpnData / 0x1000000);
                PriWpnHexData[counter, 2] = Convert.ToInt32((PriWpnData % 0x1000000) / 0x10000);
                PriWpnHexData[counter, 1] = Convert.ToInt32((PriWpnData % 0x10000) / 0x100);
                PriWpnHexData[counter, 0] = Convert.ToInt32(PriWpnData % 0x100);
            }
            counter++;
        }

        public static void AddSecWpn(string SecWpnName, uint SecWpnData, string param1, string param2, string param3, string durabity, string sparam1, string sparam2)
        {
            if (SavePlatform != "PS3")
            {
                SecWpnHexData[category, counter, 0] = Convert.ToInt32(SecWpnData / 0x1000000);
                SecWpnHexData[category, counter, 1] = Convert.ToInt32((SecWpnData % 0x1000000) / 0x10000);
                SecWpnHexData[category, counter, 2] = Convert.ToInt32((SecWpnData % 0x10000) / 0x100);
                SecWpnHexData[category, counter, 3] = Convert.ToInt32(SecWpnData % 0x100);
            }
            else if (SavePlatform == "PS3")
            {
                SecWpnHexData[category, counter, 3] = Convert.ToInt32(SecWpnData / 0x1000000);
                SecWpnHexData[category, counter, 2] = Convert.ToInt32((SecWpnData % 0x1000000) / 0x10000);
                SecWpnHexData[category, counter, 1] = Convert.ToInt32((SecWpnData % 0x10000) / 0x100);
                SecWpnHexData[category, counter, 0] = Convert.ToInt32(SecWpnData % 0x100);
            }

            SecWpnParams[category, counter, 0] = SecWpnName;
            SecWpnParams[category, counter, 1] = param1;
            SecWpnParams[category, counter, 2] = param2;
            SecWpnParams[category, counter, 3] = param3;
            SecWpnParams[category, counter, 4] = durabity;
            SecWpnParams[category, counter, 5] = sparam1;
            SecWpnParams[category, counter, 6] = sparam2;
            counter++;
        }

        public static void InitializeLifeDB()
        {
            //-----------EASY----------
            //00 - No Life Upgrades
            LifeHexData[0, 0, 0] = 0x96;
            LifeHexData[0, 0, 1] = 0x00;
            LifeData[0, 0] = 0x96;

            //01 - 1 Life Upgrade
            LifeHexData[0, 1, 0] = 0xB0;
            LifeHexData[0, 1, 1] = 0x00;
            LifeData[0, 1] = 0xB0;

            //02 - 2 Life Upgrade
            LifeHexData[0, 2, 0] = 0xC9;
            LifeHexData[0, 2, 1] = 0x00;
            LifeData[0, 2] = 0xC9;

            //03 - 3 Life Upgrade
            LifeHexData[0, 3, 0] = 0xE2;
            LifeHexData[0, 3, 1] = 0x00;
            LifeData[0, 3] = 0xE2;

            //04 - 4 Life Upgrade
            LifeHexData[0, 4, 0] = 0xFA;
            LifeHexData[0, 4, 1] = 0x00;
            LifeData[0, 4] = 0xFA;

            //05 - 5 Life Upgrade
            LifeHexData[0, 5, 0] = 0x14;
            LifeHexData[0, 5, 1] = 0x01;
            LifeData[0, 5] = 0x114;

            //06 - 6 Life Upgrade
            LifeHexData[0, 6, 0] = 0x2D;
            LifeHexData[0, 6, 1] = 0x01;
            LifeData[0, 6] = 0x12D;

            //07 - 7 Life Upgrade
            LifeHexData[0, 7, 0] = 0x46;
            LifeHexData[0, 7, 1] = 0x01;
            LifeData[0, 7] = 0x146;

            //08 - 8 Life Upgrade
            LifeHexData[0, 8, 0] = 0x5F;
            LifeHexData[0, 8, 1] = 0x01;
            LifeData[0, 8] = 0x15F;

            //09 - 9 Life Upgrade
            LifeHexData[0, 9, 0] = 0x78;
            LifeHexData[0, 9, 1] = 0x01;
            LifeData[0, 9] = 0x178;

            //-----------NORMAL----------
            //00 - No Life Upgrades
            LifeHexData[1, 0, 0] = 0x64;
            LifeHexData[1, 0, 1] = 0x00;

            //01 - 1 Life Upgrade
            LifeHexData[1, 1, 0] = 0x75;
            LifeHexData[1, 1, 1] = 0x00;

            //02 - 2 Life Upgrade
            LifeHexData[1, 2, 0] = 0x86;
            LifeHexData[1, 2, 1] = 0x00;

            //03 - 3 Life Upgrade
            LifeHexData[1, 3, 0] = 0x97;
            LifeHexData[1, 3, 1] = 0x00;

            //04 - 4 Life Upgrade
            LifeHexData[1, 4, 0] = 0xA7;
            LifeHexData[1, 4, 1] = 0x00;

            //05 - 5 Life Upgrade
            LifeHexData[1, 5, 0] = 0xB8;
            LifeHexData[1, 5, 1] = 0x00;

            //06 - 6 Life Upgrade
            LifeHexData[1, 6, 0] = 0xC9;
            LifeHexData[1, 6, 1] = 0x00;

            //07 - 7 Life Upgrade
            LifeHexData[1, 7, 0] = 0xD9;
            LifeHexData[1, 7, 1] = 0x00;

            //08 - 8 Life Upgrade
            LifeHexData[1, 8, 0] = 0xEA;
            LifeHexData[1, 8, 1] = 0x00;

            //09 - 9 Life Upgrade
            LifeHexData[1, 9, 0] = 0xFB;
            LifeHexData[1, 9, 1] = 0x00;

            //-----------HARD----------
            //00 - No Life Upgrades
            LifeHexData[2, 0, 0] = 0x42;
            LifeHexData[2, 0, 1] = 0x00;

            //01 - 1 Life Upgrade
            LifeHexData[2, 1, 0] = 0x4E;
            LifeHexData[2, 1, 1] = 0x00;

            //02 - 2 Life Upgrade
            LifeHexData[2, 2, 0] = 0x59;
            LifeHexData[2, 2, 1] = 0x00;

            //03 - 3 Life Upgrade
            LifeHexData[2, 3, 0] = 0x64;
            LifeHexData[2, 3, 1] = 0x00;

            //04 - 4 Life Upgrade
            LifeHexData[2, 4, 0] = 0x6F;
            LifeHexData[2, 4, 1] = 0x00;

            //05 - 5 Life Upgrade
            LifeHexData[2, 5, 0] = 0x7A;
            LifeHexData[2, 5, 1] = 0x00;

            //06 - 6 Life Upgrade
            LifeHexData[2, 6, 0] = 0x85;
            LifeHexData[2, 6, 1] = 0x00;

            //07 - 7 Life Upgrade
            LifeHexData[2, 7, 0] = 0x90;
            LifeHexData[2, 7, 1] = 0x00;

            //08 - 8 Life Upgrade
            LifeHexData[2, 8, 0] = 0x9B;
            LifeHexData[2, 8, 1] = 0x00;

            //09 - 9 Life Upgrade
            LifeHexData[2, 9, 0] = 0xA6;
            LifeHexData[2, 9, 1] = 0x00;
        }

        public static void InitializeWeaponDB()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    SecWpnHexData[i, j, 0] = 0x100;
                    SecWpnHexData[i, j, 1] = 0x100;
                    SecWpnHexData[i, j, 2] = 0x100;
                    SecWpnHexData[i, j, 3] = 0x100;
                }
            }

            //------------------PRIMARY WEAPONS------------------------
            counter = 0;
            
            //00 - None
            AddPriWpn("None", 0xFFFFFFFF);

            //01 - Eagle Sword
            AddPriWpn("Eagle Sword", 0x00000000);

            //02 - Wooden Stick
            AddPriWpn("Wooden Stick", 0x01000000);

            //03 - Spider Sword
            AddPriWpn("Spider Sword", 0x02000000);

            //04 - Serpent Sword
            AddPriWpn("Serpent Sword", 0x03000000);

            //05 - Lion Sword
            AddPriWpn("Lion Sword", 0x04000000);

            //06 - Lion Sword (broken)
            AddPriWpn("Lion Sword (broken)", 0x05000000);

            //07 - Scorpion Sword
            AddPriWpn("Scorpion Sword", 0x06000000);

            //08 - Water Sword
            AddPriWpn("Water Sword", 0x07000000);

            //----------SECONDARY WEAPONS-----------------
            //------------------NoWeapon Reserved------------------------
            SecWpnCategory[0] = "None";
            category = 0;
            counter = 0;

            //00 - None
            AddSecWpn("None", 0xFFFFFFFF, null, null, null, null, null, null);

            //------------------SWORDS------------------------
            SecWpnCategory[1] = "Sword";
            category = 1;
            counter = 0;

            //01 - Buyasta
            AddSecWpn("Buyasta", 0xDB100016, "2", "3", "6", "80", null, null);

            //02 - Zarich
            AddSecWpn("Zarich", 0x4EE60033, "3", "3", "3", "80", null, null);

            //03 - Haoma
            AddSecWpn("Haoma", 0x4AE60033, "3", "7", "7", "200", null, null);

            //04 - Spenta
            AddSecWpn("Spenta", 0x4BE60033, "4", "4", "3", "80", null, null);

            //05 - Yasht
            AddSecWpn("Yasht", 0x3C5B0113, "5", "7", "6", "200", null, null);

            //06 - Vanant
            AddSecWpn("Vanant", 0xDD100016, "7", "4", "1", "80", null, null);

            //07 - Kerena
            AddSecWpn("Kerena", 0x3CE60033, "8", "1", "4", "60", "powerful", null);

            //08 - Camros
            AddSecWpn("Camros", 0x3E5B0113, "6", "4", "5", "80", "cursed", null);

            //09 - Fravashis
            AddSecWpn("Fravashis", 0x51E60033, "5", "7", "5", "200", "cursed", null);

            //10 - Tasan
            AddSecWpn("Tasan", 0x49E60033, "2", "3", "7", "90", "vampiric", null);

            //11 - Asto
            AddSecWpn("Asto", 0x43E60033, "4", "2", "7", "60", "vampiric", null);

            //12 - Agas
            AddSecWpn("Agas", 0x48E60033, "4", "4", "8", "120", "vampiric", null);

            //13 - Srosh
            AddSecWpn("Srosh", 0x405B0113, "4", "8", "5", "100", "indestructible", null);

            //14 - Rustam
            AddSecWpn("Rustam", 0x4CE60033, "4", "4", "5", "120", null, null);

            //15 - Mainyu
            AddSecWpn("Mainyu", 0x45E60033, "5", "3", "3", "100", null, null);

            //16 - Mahre
            AddSecWpn("Mahre", 0x385B0113, "5", "7", "3", "200", null, null);

            //17 - Dena
            AddSecWpn("Dena", 0x3A5B0113, "4", "2", "6", "60", "vampiric", null);

            //18 - Asman
            AddSecWpn("Asman", 0x47E60033, "8", "2", "5", "30", "powerful", null);

            //------------------AXES------------------------
            SecWpnCategory[2] = "Axe";
            category = 2;
            counter = 0;

            //01 - Airyaman
            AddSecWpn("Airyaman", 0x83E50033, "4", "3", "2", "60", null, null);

            //02 - Allatum
            AddSecWpn("Allatum", 0xDF100016, "5", "3", "2", "60", null, null);

            //03 - Natat
            AddSecWpn("Natat", 0x72E50033, "5", "3", "4", "60", null, null);

            //04 - Apaosa
            AddSecWpn("Apaosa", 0x91E50033, "6", "2", "3", "30", "powerthrow", null);

            //05 - Vahishta
            AddSecWpn("Vahishta", 0x90E50033, "5", "4", "4", "100", null, null);

            //06 - Vidatu
            AddSecWpn("Vidatu", 0x79E50033, "6", "3", "2", "60", null, null);

            //07 - Ahura
            AddSecWpn("Ahura", 0x43380113, "3", "3", "3", "60", null, null);

            //08 - Drvaspa
            AddSecWpn("Drvaspa", 0x3DE60033, "4", "3", "2", "60", null, null);

            //09 - Apam
            AddSecWpn("Apam", 0x62E50033, "5", "3", "3", "60", null, null);

            //10 - Ereta
            AddSecWpn("Ereta", 0x78E50033, "5", "3", "6", "60", null, null);

            //11 - Mainyu
            AddSecWpn("Mainyu", 0x305B0113, "5", "6", "4", "150", null, null);

            //12 - Ahura
            AddSecWpn("Ahura", 0x93E50033, "4", "3", "1", "60", "knockdown", null);

            //13 - Bahram
            AddSecWpn("Bahram", 0x7BE50033, "8", "1", "1", "30", "powerful", null);

            //14 - Spentas
            AddSecWpn("Spentas", 0x92E50033, "5", "8", "1", "100", "indestructible", "cursed");

            //------------------MACES------------------------
            SecWpnCategory[3] = "Mace";
            category = 3;
            counter = 0;

            //01 - Peris
            AddSecWpn("Peris", 0x3AE60033, "2", "3", "1", "100", null, null);

            //02 - Zend
            AddSecWpn("Zend", 0x50E60033, "3", "4", "2", "120", null, null);

            //03 - Vata
            AddSecWpn("Vata", 0x46E60033, "3", "7", "2", "200", null, null);

            //04 - Sraosa
            AddSecWpn("Sraosa", 0x4FE60033, "4", "3", "1", "80", "knockdown", null);

            //05 - Menog
            AddSecWpn("Menog", 0x8F5B0113, "6", "4", "1", "100", "knockdown", null);

            //06 - Baga
            AddSecWpn("Baga", 0x1A380113, "3", "4", "1", "120", null, null);

            //07 - Yima
            AddSecWpn("Yima", 0x44E60033, "4", "3", "1", "80", null, null);

            //08 - Izha
            AddSecWpn("Izha", 0x39E60033, "3", "8", "1", "300", "knockdown", null);

            //------------------DAGGERS------------------------
            SecWpnCategory[4] = "Dagger";
            category = 4;
            counter = 0;

            //01 - Khara
            AddSecWpn("Khara", 0x325B0113, "1", "3", "8", "60", "powerthrow", null);

            //02 - Indra
            AddSecWpn("Indra", 0x345B0113, "1", "4", "8", "80", "powerthrow", null);

            //03 - Abathur
            AddSecWpn("Abathur", 0x36E60033, "2", "4", "8", "100", "powerthrow", null);

            //04 - Armaiti
            AddSecWpn("Armaiti", 0x3BE60033, "5", "3", "7", "60", null, null);

            //05 - Yazata
            AddSecWpn("Yazata", 0x37E60033, "2", "4", "7", "100", "powerthrow", null);

            //06 - Vahishta
            AddSecWpn("Vahishta", 0x3FE60033, "6", "4", "8", "120", null, null);

            //------------------SECRET------------------------
            SecWpnCategory[5] = "Secret";
            category = 5;
            counter = 0;

            //01 - Teddy Bear
            AddSecWpn("Teddy Bear", 0x3EE60033, "1", "8", "5", "100", "indestructible", "vampiric");

            //02 - Rayman Fist
            AddSecWpn("Rayman Fist", 0x35E60033, "2", "8", "7", "100", "indestructible", "knockdown");

            //03 - Pink Flamingo
            AddSecWpn("Pink Flamingo", 0x42E60033, "4", "8", "1", "100", "indestructible", "knockdown");

            //04 - Hockey Stick
            AddSecWpn("Hockey Stick", 0x38E60033, "4", "8", "3", "100", "indestructible", "powerful");

            //05 - Light Sword
            AddSecWpn("Light Sword", 0x41E60033, "8", "8", "8", "100", "indestructible", "cursed");

            //------------------UNDOCUMENTED------------------------
            SecWpnCategory[6] = "Undocumented";
            category = 6;
            counter = 0;

            //01 - Mace
            AddSecWpn("Mace", 0x4DE60033, "?", "?", "?", "?", null, null);

            //02 - Mace
            AddSecWpn("Mace", 0x40E60033, "?", "?", "?", "?", null, null);

            //03 - Shahdee's Sword
            AddSecWpn("Shahdee's Sword", 0x33E60033, "?", "?", "?", "100", "indestructible", "cursed");

            //04 - Kaileena's Sword
            AddSecWpn("Kaileena's Sword", 0x31E60033, "?", "?", "?", "100", "indestructible", "cursed");

            //05 - Wooden Stick
            AddSecWpn("Wooden Stick", 0x0F350133, "?", "?", "?", "?", null, null);

            //06 - Lion Sword
            AddSecWpn("Lion Sword", 0xB65B0113, "?", "?", "?", "?", null, null);


            //------------------BAD/USELESS------------------------
            /*if (SavePlatform == "PSP") //was loading on Jpcsp only with memory errors skip; crashes on PPSSPP and PC version
            {
                SecWpnCategory[7] = "Bad/Useless";
                category = 7;
                counter = 0;

                //01 - Eagle Sword
                AddSecWpn("Eagle Sword", 0x30E60033, "?", "?", "?", "?", "indestructible", null);

                //02 - Wooden Stick
                AddSecWpn("Wooden Stick", 0x41380113, "?", "?", "?", "?", "indestructible", null);

                //03 - Spider Sword
                AddSecWpn("Spider Sword", 0x2DE60033, "?", "?", "?", "?", "indestructible", null);

                //04 - Serpent Sword
                AddSecWpn("Serpent Sword", 0x2EE60033, "?", "?", "?", "?", "indestructible", null);

                //05 - Lion Sword
                AddSecWpn("Lion Sword", 0x1C380113, "?", "?", "?", "?", "indestructible", null);

                //06 - Lion Sword (broken)
                AddSecWpn("Lion Sword (broken)", 0x2FE60033, "?", "?", "?", "?", "indestructible", null);

                //07 - Scorpion Sword
                AddSecWpn("Scorpion Sword", 0x32E60033, "?", "?", "?", "?", "indestructible", null);

                //08 - Water Sword
                AddSecWpn("Water Sword", 0x34E60033, "?", "?", "?", "?", "indestructible", null);
            }*/

        }
    }
}
