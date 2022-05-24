using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Launcher
{
    public partial class SnakeLauncher : Form
    {
        public SnakeLauncher()
        {
            InitializeComponent();
            cmbScelta.SelectedIndex = 0;

            this.BackgroundImage = Image.FromFile("launcher_bg.jpg");
            btnInfo.BackgroundImage = Image.FromFile("info_logo.jpg");
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            using (var dialog = new About())
            {
                dialog.ShowDialog();
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "Snake.exe";
            startInfo.Arguments = cmbScelta.SelectedItem.ToString();
            Process.Start(startInfo);
        }
    }
}
