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
    public partial class SandPowersTanks : Form
    {
        public SandPowersTanks()
        {
            InitializeComponent();

            this.Icon = timeline2.Properties.Resources.icon;

            for (int i = 0; i < 10; i++)
            {
                comboBox1.Items.Add(Global.SandPowersTanksParams[i]);
            }

            if (Global.CurrentSandPowersTanks != 256)
            {
                comboBox1.SelectedIndex = Global.CurrentSandPowersTanks;
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Global.SaveData[Global.PlatformOffsetHack + Global.sd_sandpowerstanks] = Convert.ToByte(comboBox1.SelectedIndex);

            Global.DetectSandPowersTanks();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
