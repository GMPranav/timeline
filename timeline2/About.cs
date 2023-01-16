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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            this.Icon = timeline2.Properties.Resources.icon;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
