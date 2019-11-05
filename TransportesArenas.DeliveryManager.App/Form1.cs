using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac;
using MetroFramework.Forms;
using TransportesArenas.DeliveryManager.Backend.Implementations;
using TransportesArenas.DeliveryManager.Backend.Interfaces;
using IContainer = Autofac.IContainer;

namespace TransportesArenas.DeliveryManager.App
{
    public partial class Form1 : MetroForm
    {
        private delegate void SafeCallDelegate(string text);
        private delegate void EmptyDelegate();
        private int totalDeliveries;
        private int deliveriesProcessed;
        private ILifetimeScope scope;
        private readonly IContainer iocContainer;

        public Form1()
        {
            this.iocContainer = IoCBackendBuilder.Build();

            InitializeComponent();
            this.Text = $@"T.A Delivery Manager {Assembly.GetExecutingAssembly().GetName().Version}";
            this.openFileDialog1 = new OpenFileDialog();
            this.folderBrowserDialog1 = new FolderBrowserDialog();
            this.metroProgressSpinner1.Visible = false;
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
            {
                MessageBox.Show("Informa los campos");
                return;
            }

            this.buttonProcess.Enabled = false;
            this.metroProgressSpinner1.Visible = true;
            deliveriesProcessed = 0;

            using (this.scope = iocContainer.BeginLifetimeScope())
            {
                var resquest = this.scope.Resolve<IDelivaryManagerProcessRequest>();
                resquest.ExcelFile = this.textBoxExcel.Text;
                resquest.PdfFile = this.textBoxPdf.Text;
                resquest.OutputFolder = this.textBoxOutput.Text;

                var deliveryProcessManager = this.scope.Resolve<IDeliveryProcessManager>();
                deliveryProcessManager.TotalDeliveriesEvent += this.DeliveryProcessManager_TotalDeliveriesEvent;
                deliveryProcessManager.StepEvent += this.DeliveryProcessManagerOnStepEvent;
                deliveryProcessManager.ProcessEndedEvent += this.DeliveryProcessManagerOnProcessEndedEvent;


                Task.Run(() =>
                {
                    deliveryProcessManager.RunAsync(resquest).ConfigureAwait(true);
                }).ConfigureAwait(true);
            }

        }

        private void DeliveryProcessManagerOnProcessEndedEvent()
        {
            if (this.buttonProcess.InvokeRequired)
            {
                var d = new EmptyDelegate(DeliveryProcessManagerOnProcessEndedEvent);
                buttonProcess.Invoke(d);
            }
            else
            {
                this.buttonProcess.Enabled = true;
            }

            if (this.metroProgressSpinner1.InvokeRequired)
            {
                var d = new EmptyDelegate(DeliveryProcessManagerOnProcessEndedEvent);
                metroProgressSpinner1.Invoke(d);
            }
            else
            {
                this.metroProgressSpinner1.Visible = false;
            }

        }

        private void DeliveryProcessManagerOnStepEvent()
        {
            deliveriesProcessed++;
            this.WriteTextSafe($"{deliveriesProcessed} / {totalDeliveries}");
        }

        private void WriteTextSafe(string text)
        {
            if (labelProcess.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                labelProcess.Invoke(d, text);
            }
            else
            {
                labelProcess.Text = text;
            }
        }

        private void DeliveryProcessManager_TotalDeliveriesEvent(int deliveries)
        {
            this.totalDeliveries = deliveries;
        }

        private bool IsValid()
        {
            return !string.IsNullOrEmpty(this.textBoxExcel.Text) &&
                !string.IsNullOrEmpty(this.textBoxPdf.Text) &&
                    !string.IsNullOrEmpty(this.textBoxOutput.Text);
        }
    }
}
