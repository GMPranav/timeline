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
    public partial class CurrentSecWpnDurabity : Form
    {
        public CurrentSecWpnDurabity()
        {
            InitializeComponent();

            this.Icon = timeline2.Properties.Resources.icon;

            textBox1.Text = (Global.CurrentSecWpnDurabity[0] + Global.CurrentSecWpnDurabity[1] * 256).ToString();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (Global.SavePlatform != "PS3")
            {
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_secwpndurabity] = Convert.ToByte(Convert.ToInt32(textBox1.Text) % 256);
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_secwpndurabity + 1)] = Convert.ToByte(Convert.ToInt32(textBox1.Text) / 256);
            }
            else if (Global.SavePlatform == "PS3")
            {
                Global.SaveData[Global.PlatformOffsetHack + (Global.sd_secwpndurabity + 1)] = Convert.ToByte(Convert.ToInt32(textBox1.Text) % 256);
                Global.SaveData[Global.PlatformOffsetHack + Global.sd_secwpndurabity] = Convert.ToByte(Convert.ToInt32(textBox1.Text) / 256);
            }

            Global.DetectCurrentSecWpnDurabity();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
