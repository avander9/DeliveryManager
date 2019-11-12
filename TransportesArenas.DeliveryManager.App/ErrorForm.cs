using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace TransportesArenas.DeliveryManager.App
{
    public partial class ErrorForm : MetroForm
    {
        private string errorMessage;
        public ErrorForm(string errorMessage)
        {
            this.errorMessage = errorMessage;
            InitializeComponent();
            this.Text = "Error";
        }

        private void openLogButton_Click(object sender, EventArgs e)
        {
            var logFile = Path.Combine(Environment.CurrentDirectory, "log.txt");
            Process.Start(logFile);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
