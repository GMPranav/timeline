using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace timeline2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Icon = timeline2.Properties.Resources.icon;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //диалог открытия файла
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();

            if (dialog.FileName == "")
            {
                MessageBox.Show("No file was selected!", "Load Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dialog.FileName != "")
            {
                Global.selectedPath = dialog.FileName;

                //MessageBox.Show(selectedPath);

                Global.SaveData = Global.ReadFile(Global.selectedPath);

                //Check, if PC version save loaded, header SAV4; otherwise loading PSP save
                if (Path.GetExtension(Global.selectedPath) == ".SAV" && Global.SaveData.Length == 51780 || Path.GetExtension(Global.selectedPath) == ".sav" && Global.SaveData.Length == 51780)
                {
                    Global.SavePlatform = "PC";
                    Global.PlatformOffsetHack = 40;

                    Global.sd_char = 44;
                    Global.sd_storyline = 48184;
                    Global.sd_sandpowerstanks = 48158;
                    Global.sd_currentlife = 48152;
                    Global.sd_maxlife = 48154;
                    Global.sd_difficulty = 120;
                    Global.sd_filledsandtanks = 48156;
                    Global.sd_secwpndurabity = 48162;
                    Global.sd_priwpn = 48164;
                    Global.sd_secwpn = 36;
                    Global.sd_checkpoint = 32;
                    Global.sd_location = 124;
                }
                else if (Path.GetExtension(Global.selectedPath) == ".BIN" && Global.SaveData.Length == 51740 || Path.GetExtension(Global.selectedPath) == ".bin" && Global.SaveData.Length == 51740)
                {
                    Global.SavePlatform = "PSP";
                    Global.PlatformOffsetHack = 0;

                    Global.sd_char = 44;
                    Global.sd_storyline = 48184;
                    Global.sd_sandpowerstanks = 48158;
                    Global.sd_currentlife = 48152;
                    Global.sd_maxlife = 48154;
                    Global.sd_difficulty = 120;
                    Global.sd_filledsandtanks = 48156;
                    Global.sd_secwpndurabity = 48162;
                    Global.sd_priwpn = 48164;
                    Global.sd_secwpn = 36;
                    Global.sd_checkpoint = 32;
                    Global.sd_location = 124;
                }
                else if (Path.GetExtension(Global.selectedPath) == ".PS2" && Global.SaveData.Length == 8650752 || Path.GetExtension(Global.selectedPath) == ".ps2" && Global.SaveData.Length == 8650752)
                {
                    Global.SavePlatform = "PS2";

                    Global.sd_char = 40;
                    Global.sd_storyline = 49684;
                    Global.sd_sandpowerstanks = 49658;
                    Global.sd_currentlife = 49652;
                    Global.sd_maxlife = 49654;
                    Global.sd_difficulty = 116;
                    Global.sd_filledsandtanks = 49656;
                    Global.sd_secwpndurabity = 49662;
                    Global.sd_priwpn = 49664;
                    Global.sd_secwpn = 36;
                    Global.sd_checkpoint = 32;
                    Global.sd_location = 120;

                    Global.sd_ps2checksum = 512; //checksum, preventing to modify secondary weapon, location, storyline and cause profile loading error and corrupted save;
                    Global.sd_ps2checksum_2 = 50144; //checksum, preventing to modify primary weapon and cause load error [or makes all stuff (weapons, life and sand powers) disappear with error bypass hack];

                    //check for multiple saves
                    for (int i = 0; i < Global.SaveData.Length; i++)
                    {
                        if (Global.SaveData[i] == 0x42 && Global.SaveData[i + 1] == 0x41 && Global.SaveData[i + 2] == 0x53 && Global.SaveData[i + 3] == 0x4C && Global.SaveData[i + 4] == 0x55 && Global.SaveData[i + 5] == 0x53 && Global.SaveData[i + 6] == 0x2D && Global.SaveData[i + 7] == 0x32 && Global.SaveData[i + 8] == 0x31 && Global.SaveData[i + 9] == 0x30 && Global.SaveData[i + 10] == 0x32 && Global.SaveData[i + 11] == 0x32 && Global.SaveData[i + 12] == 0x53)

                        {
                            if ((i + 49700) < Global.SaveData.Length && Global.SaveData[i + 528] == 0x76 && Global.SaveData[i + 529] == 0x69 && Global.SaveData[i + 530] == 0x65 && Global.SaveData[i + 531] == 0x77)
                            {
                                Global.PS2SaveSlotInfo[Global.PS2SaveSlots, 0] = ((Convert.ToInt32(Global.SaveData[i + 13]) - 48) * 10) + (Convert.ToInt32(Global.SaveData[i + 14] - 48));
                                Global.PS2SaveSlotInfo[Global.PS2SaveSlots, 1] = i + 992;
                                Global.PS2SaveSlots++;
                            }
                        }
                    }

                    if (Global.PS2SaveSlots == 1)
                    {
                        Global.PlatformOffsetHack = Global.PS2SaveSlotInfo[0, 1];
                    }
                    else if (Global.PS2SaveSlots > 1)
                    {
                        PS2Slot ps2slot_dialog = new PS2Slot();
                        ps2slot_dialog.ShowDialog();
                    }
                }

                else if (Path.GetExtension(Global.selectedPath) == "" && Global.SaveData.Length == 2536436)
                {
                    Global.SavePlatform = "PS3";

                    Global.sd_char = 68;
                    Global.sd_storyline = 48208;
                    Global.sd_sandpowerstanks = 48181;
                    Global.sd_currentlife = 48176;
                    Global.sd_maxlife = 48178;
                    Global.sd_difficulty = 147;
                    Global.sd_filledsandtanks = 48183;
                    Global.sd_secwpndurabity = 48184;
                    Global.sd_priwpn = 48188;
                    Global.sd_secwpn = 60;
                    Global.sd_checkpoint = 56;
                    Global.sd_location = 148;

                    //check for multiple saves
                    for (int i = 0; i < Global.SaveData.Length; i++)
                    {
                        if (Global.SaveData[i] == 0xFF && Global.SaveData[i + 1] == 0xFF && Global.SaveData[i + 2] == 0xFF && Global.SaveData[i + 3] == 0xFF && Global.SaveData[i + 4] == 0xFF && Global.SaveData[i + 5] == 0xFF && Global.SaveData[i + 6] == 0xFF && Global.SaveData[i + 7] == 0xFF && Global.SaveData[i + 8] >= 0x30 && Global.SaveData[i + 9] >= 0x30 && Global.SaveData[i + 10] == 0x00 && Global.SaveData[i + 11] == 0x01)
                        {
                            Global.PS2SaveSlotInfo[Global.PS2SaveSlots, 0] = ((Convert.ToInt32(Global.SaveData[i + 8]) - 48) * 10) + (Convert.ToInt32(Global.SaveData[i + 9] - 48));
                            Global.PS2SaveSlotInfo[Global.PS2SaveSlots, 1] = i;
                            Global.PS2SaveSlots++;
                        }
                    }

                    if (Global.PS2SaveSlots == 1)
                    {
                        Global.PlatformOffsetHack = Global.PS2SaveSlotInfo[0, 1];
                    }
                    else if (Global.PS2SaveSlots > 1)
                    {
                        PS2Slot ps2slot_dialog = new PS2Slot();
                        ps2slot_dialog.ShowDialog();
                    }
                }

                main main_dialog = new main();
                main_dialog.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            About about_dialog = new About();
            about_dialog.ShowDialog();
        }
    }
}
