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
    public partial class FilledSandTanks : Form
    {
        public FilledSandTanks()
        {
            InitializeComponent();

            this.Icon = timeline2.Properties.Resources.icon;

            for (int i = 0; i < 7; i++)
            {
                comboBox1.Items.Add(i.ToString());
            }

            comboBox1.SelectedIndex = Global.FilledSandTanks;
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Global.SaveData[Global.PlatformOffsetHack + Global.sd_filledsandtanks] = Convert.ToByte(comboBox1.SelectedIndex);

            Global.DetectFilledSandTanks();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
