using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransportesArenas.DeliveryManager.Backend.Implementations;
using TransportesArenas.DeliveryManager.Backend.Interfaces;

namespace TransportesArenas.DeliveryManager.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.openFileDialog1 = new OpenFileDialog();
            this.folderBrowserDialog1 = new FolderBrowserDialog();
        }

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            BuildFileDialogForExcel();
            this.openFileDialog1.ShowDialog(this);
            this.textBoxExcel.Text = openFileDialog1.FileName;
        }

        private void buttonPdf_Click(object sender, EventArgs e)
        {
            BuildFileDialogForPdf();
            this.openFileDialog1.ShowDialog(this);
            this.textBoxPdf.Text = openFileDialog1.FileName;
        }
        private void buttonOutput_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.ShowNewFolderButton = true;
            var result = this.folderBrowserDialog1.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                textBoxOutput.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void BuildFileDialogForExcel()
        {
            this.openFileDialog1.InitialDirectory = @"C:\";
            this.openFileDialog1.DefaultExt = "xlsx";
            this.openFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx";
        }

        private void BuildFileDialogForPdf()
        {
            this.openFileDialog1.InitialDirectory = @"C:\";
            this.openFileDialog1.DefaultExt = "pdf";
            this.openFileDialog1.Filter = "pdf files (*.pdf)|*.pdf";
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            if (!IsValid())
                MessageBox.Show("Informa los campos");

            var resquest = new DelivaryManagerProcessRequest
            {
                ExcelFile = this.textBoxExcel.Text,
                PdfFile = this.textBoxPdf.Text,
                OutputFolder = this.textBoxOutput.Text
            };

            IDeliveryProcessManager deliveryProcessManager = new DeliveryProcessManager();
            Task.Run(() => { deliveryProcessManager.RunAsync(resquest).ConfigureAwait(true); });

        }

        private bool IsValid()
        {
            return !string.IsNullOrEmpty(this.textBoxExcel.Text) &&
                !string.IsNullOrEmpty(this.textBoxPdf.Text) &&
                    !string.IsNullOrEmpty(this.textBoxOutput.Text);
        }
    }
}
