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
    public partial class SecWpn : Form
    {
        public SecWpn()
        {
            InitializeComponent();

            this.Icon = timeline2.Properties.Resources.icon;

            for (int i = 0; i < 8; i++)
            {
                if (Global.SecWpnCategory[i] != null)
                {
                    comboBox1.Items.Add(Global.SecWpnCategory[i] + "s");
                }
            }

            if (Global.CurrentSecWpnCategoryID != 256 && Global.CurrentSecWpnID != 256)
            {
                if (Global.CurrentSecWpnCategoryID >= 0 && Global.CurrentSecWpnCategoryID <= 7)
                {
                    comboBox1.SelectedIndex = Global.CurrentSecWpnCategoryID;
                    comboBox2.SelectedIndex = Global.CurrentSecWpnID;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";

            label_atk_pow.Text = "";
            label_blk_end.Text = "";
            label_atk_spd.Text = "";
            label_wpn_durabity.Text = "";
            label_wpn_special.Text = "";

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 18; j++)
                    if (comboBox1.SelectedIndex == i && Global.SecWpnParams[i, j, 0] != null)
                    {
                        if (j < 9) comboBox2.Items.Add("0" + (j + 1) + " - " + Global.SecWpnParams[i, j, 0]);
                        else if (j > 8) comboBox2.Items.Add((j + 1) + " - " + Global.SecWpnParams[i, j, 0]);
                    }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label_atk_pow.Text = "";
            label_blk_end.Text = "";
            label_atk_spd.Text = "";
            label_wpn_durabity.Text = "";
            label_wpn_special.Text = "";

            if (Global.SecWpnParams[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 1] != null)
            {
                label_atk_pow.Text = "Attack Power: " + Global.SecWpnParams[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 1] + @"/8";
            }
            if (Global.SecWpnParams[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 2] != null)
            {
                label_blk_end.Text = "Block/Endurance: " + Global.SecWpnParams[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 2] + @"/8";
            }
            if (Global.SecWpnParams[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 3] != null)
            {
                label_atk_spd.Text = "Attack Speed: " + Global.SecWpnParams[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 3] + @"/8";
            }
            if (Global.SecWpnParams[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 4] != null)
            {
                label_wpn_durabity.Text = "Durabity: " + Global.SecWpnParams[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 4];
            }
            if (Global.SecWpnParams[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 5] != null)
            {
                label_wpn_special.Text = "Special: " + Global.SecWpnParams[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 5];
            }
            if (Global.SecWpnParams[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 6] != null)
            {
                label_wpn_special.Text += ", " + Global.SecWpnParams[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 6];
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Global.SaveData[Global.PlatformOffsetHack + Global.sd_secwpn] = Convert.ToByte(Global.SecWpnHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 0]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_secwpn + 1)] = Convert.ToByte(Global.SecWpnHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 1]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_secwpn + 2)] = Convert.ToByte(Global.SecWpnHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 2]);
            Global.SaveData[Global.PlatformOffsetHack + (Global.sd_secwpn + 3)] = Convert.ToByte(Global.SecWpnHexData[comboBox1.SelectedIndex, comboBox2.SelectedIndex, 3]);

            Global.DetectSecWpn();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
