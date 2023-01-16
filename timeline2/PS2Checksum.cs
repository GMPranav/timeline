using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace timeline2
{
    public partial class PS2Checksum : Form
    {
        public PS2Checksum()
        {
            InitializeComponent();

            //0x01010101 or 0x00000101
            //0x00010101 or 0x00000001

            if (Global.SaveData[Global.PlatformOffsetHack + Global.sd_ps2checksum] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 1)] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 2)] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 3)] == 1 
                && Global.SaveData[Global.PlatformOffsetHack + Global.sd_ps2checksum_2] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 1)] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 2)] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 3)] == 1)
            {
                radioButton1.Select();
            }
            else if (Global.SaveData[Global.PlatformOffsetHack + Global.sd_ps2checksum] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 1)] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 2)] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 3)] == 1 
                && Global.SaveData[Global.PlatformOffsetHack + Global.sd_ps2checksum_2] == 0 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 1)] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 2)] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 3)] == 1)
            {
                radioButton2.Select();
            }
            else if (Global.SaveData[Global.PlatformOffsetHack + Global.sd_ps2checksum] == 0 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 1)] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 2)] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 3)] == 1 
                && Global.SaveData[Global.PlatformOffsetHack + Global.sd_ps2checksum_2] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 1)] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 2)] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 3)] == 1)
            {
                radioButton3.Select();
            }
            else if (Global.SaveData[Global.PlatformOffsetHack + Global.sd_ps2checksum] == 0 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 1)] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 2)] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 3)] == 1 
                && Global.SaveData[Global.PlatformOffsetHack + Global.sd_ps2checksum_2] == 0 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 1)] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 2)] == 1 && Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 3)] == 1)
            {
                radioButton4.Select();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CHECKSUM BYPASS-HACK, WORKS ONLY WITH SINGLE SAVE ON MEMORY CARD!

            //checksum-1, preventing to modify secondary weapon, location, storyline and cause profile loading error and corrupted save;
            //checksum-2, preventing to modify primary weapon and cause load error [or makes all stuff (weapons, life and sand powers) disappear with error bypass hack];
            if (radioButton1.Checked == true)
            {
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_ps2checksum] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 1)] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 2)] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 3)] = 1;

                Global.SaveData[Global.PlatformOffsetHack + Global.sd_ps2checksum_2] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 1)] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 2)] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 3)] = 1;
            }
            else if (radioButton2.Checked == true)
            {
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_ps2checksum] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 1)] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 2)] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 3)] = 1;

                Global.SaveData[Global.PlatformOffsetHack + Global.sd_ps2checksum_2] = 0;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 1)] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 2)] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 3)] = 1;
            }
            else if (radioButton3.Checked == true)
            {
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_ps2checksum] = 0;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 1)] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 2)] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 3)] = 1;

                Global.SaveData[Global.PlatformOffsetHack + Global.sd_ps2checksum_2] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 1)] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 2)] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 3)] = 1;
            }
            else if (radioButton4.Checked == true)
            {
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_ps2checksum] = 0;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 1)] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 2)] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum + 3)] = 1;

                Global.SaveData[Global.PlatformOffsetHack + Global.sd_ps2checksum_2] = 0;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 1)] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 2)] = 1;
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_ps2checksum_2 + 3)] = 1;
            }

            this.Close();
        }
    }
}
