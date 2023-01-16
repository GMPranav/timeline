using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace timeline2
{
    public partial class main : Form
    {
        public void UpdateTextInfo()
        {
            while (true)
            {
                label1.Text = "Secondary Weapon: " + Global.SecWpnParams[Global.CurrentSecWpnCategoryID, Global.CurrentSecWpnID, 0] + " (" + Global.SecWpnCategory[Global.CurrentSecWpnCategoryID] + ")";
                label2.Text = "Primary Weapon: " + Global.PriWpnParams[Global.CurrentPriWpnID];
                label3.Text = "Current Secondary Weapon Durabity: " + (Global.CurrentSecWpnDurabity[0] + Global.CurrentSecWpnDurabity[1] * 256).ToString() + "/" + Global.SecWpnParams[Global.CurrentSecWpnCategoryID, Global.CurrentSecWpnID, 4];
                label4.Text = "Filled Sand Tanks: " + Global.FilledSandTanks.ToString();
                label5.Text = "Current/Max Life: " + (Global.CurrentLife[0] + Global.CurrentLife[1] * 256).ToString() + "/" + (Global.MaxLife[0] + Global.MaxLife[1] * 256).ToString();
                label6.Text = "Unlocked Sand Powers/Tanks: " + Global.SandPowersTanksParams[Global.CurrentSandPowersTanks];
                label7.Text = "Hack Character: " + Global.IsCharacterHacked + " (" + Global.CharacterStorylineParams[Global.CurrentCharacterID, 1] + ")";
                label8.Text = "Game Difficulty: " + Global.DifficultyParams[Global.CurrentDifficulty];
                label9.Text = "Current Location: " + Global.LocationParams[Global.CurrentLocationCategoryID, Global.CurrentLocationID, 0];
                label10.Text = "Storyline: " + Global.CharacterStorylineParams[Global.CurrentStorylineID, 0];
                if (Global.SavePlatform != "PSP")
                {
                    label11.Text = "Warrior Within SaveGame (" + Global.SavePlatform + ")";
                }
                else if (Global.SavePlatform == "PSP")
                {
                    label11.Text = "Revelations SaveGame (" + Global.SavePlatform + ")";
                }
                Thread.Sleep(50);
            }
        }
        public main()
        {
            InitializeComponent();

            this.Icon = timeline2.Properties.Resources.icon;

            if (Global.SavePlatform == "PS2")
            {
                MessageBox.Show("Experimental PS2 saves support, check ReadMe for details!", "Timeline", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Global.InitializeSceneDB();
            Global.InitializeWeaponDB();
            Global.DetectSecWpn();
            Global.DetectPriWpn();
            Global.DetectCurrentSecWpnDurabity();
            Global.DetectFilledSandTanks();
            Global.InitializeLifeDB();
            Global.DetectDifficulty();
            Global.DetectLife();
            Global.DetectSandPowersTanks();
            Global.InitializeCharacterStorylineDB();
            Global.DetectCharacterStoryline();
            Global.DetectLocation();

            //

            Thread newThread;
            newThread = new Thread(UpdateTextInfo);
            newThread.Start();
            newThread.IsBackground = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SecWpn secwpn_dialog = new SecWpn();
            secwpn_dialog.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PriWpn priwpn_dialog = new PriWpn();
            priwpn_dialog.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CurrentSecWpnDurabity currentsecwpndurabity_dialog = new CurrentSecWpnDurabity();
            currentsecwpndurabity_dialog.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FilledSandTanks filledsandtanks_dialog = new FilledSandTanks();
            filledsandtanks_dialog.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Life life_dialog = new Life();
            life_dialog.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SandPowersTanks sandpowerstanks_dialog = new SandPowersTanks();
            sandpowerstanks_dialog.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Character character_dialog = new Character();
            character_dialog.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Difficulty difficulty_dialog = new Difficulty();
            difficulty_dialog.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Location location_dialog = new Location();
            location_dialog.ShowDialog();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (Global.SavePlatform == "PS2" && Global.PS2SaveSlots == 1)
            {
                PS2Checksum ps2checksum_dialog = new PS2Checksum();
                ps2checksum_dialog.ShowDialog();
            }
            Global.SaveFile(Global.selectedPath, Global.SaveData);
            MessageBox.Show("Saved!", "Timeline", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Storyline storyline_dialog = new Storyline();
            storyline_dialog.ShowDialog();
        }
    }
}
