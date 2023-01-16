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
    public partial class PS2Slot : Form
    {
        public PS2Slot()
        {
            InitializeComponent();

            if (Global.SavePlatform == "PS3")
            {
                this.Text = "Load PS3 SaveGame...";
                label1.Text = "Multiple PS3 Saves";
            }

            for (int j = 0; j < Global.PS2SaveSlots; j++)
            {
                comboBox1.Items.Add(Global.PS2SaveSlotInfo[j, 0] + 1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Global.PlatformOffsetHack = Global.PS2SaveSlotInfo[comboBox1.SelectedIndex, 1];
            this.Close();
        }
    }
}
