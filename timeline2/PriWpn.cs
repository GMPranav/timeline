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
    public partial class PriWpn : Form
    {
        public PriWpn()
        {
            InitializeComponent();

            this.Icon = timeline2.Properties.Resources.icon;

            for (int i = 0; i < 9; i++)
            {
                comboBox1.Items.Add(Global.PriWpnParams[i]);
            }

            if (Global.CurrentPriWpnID != 256)
            {
                comboBox1.SelectedIndex = Global.CurrentPriWpnID;
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Global.SaveData[Global.PlatformOffsetHack + Global.sd_priwpn] = Convert.ToByte(Global.PriWpnHexData[comboBox1.SelectedIndex, 0]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_priwpn + 1)] = Convert.ToByte(Global.PriWpnHexData[comboBox1.SelectedIndex, 1]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_priwpn + 2)] = Convert.ToByte(Global.PriWpnHexData[comboBox1.SelectedIndex, 2]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_priwpn + 3)] = Convert.ToByte(Global.PriWpnHexData[comboBox1.SelectedIndex, 3]);

            Global.DetectPriWpn();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
