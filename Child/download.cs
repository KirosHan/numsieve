using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Numsieve
{
    public partial class download : Form
    {
        private string version = "0.0.0.0";
        private string date = "";
        public download(string s_version,string s_date)
        {
            InitializeComponent();
            version = s_version;
            date = s_date;
        }

        private void download_Load(object sender, EventArgs e)
        {
            label1.Text = "Version "+version+"已经发布";
            label2.Text = "发布时间: " + date;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/KirosHan/numsieve/releases");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
