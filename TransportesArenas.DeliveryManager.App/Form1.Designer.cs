namespace TransportesArenas.DeliveryManager.App
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBoxExcel = new MetroFramework.Controls.MetroTextBox();
            this.textBoxOutput = new MetroFramework.Controls.MetroTextBox();
            this.textBoxPdf = new MetroFramework.Controls.MetroTextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonProcess = new MetroFramework.Controls.MetroButton();
            this.labelProcess = new MetroFramework.Controls.MetroLabel();
            this.buttonPdf = new MetroFramework.Controls.MetroButton();
            this.buttonOutput = new MetroFramework.Controls.MetroButton();
            this.buttonExcel = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBoxExcel
            // 
            this.textBoxExcel.Location = new System.Drawing.Point(67, 94);
            this.textBoxExcel.Name = "textBoxExcel";
            this.textBoxExcel.Size = new System.Drawing.Size(355, 20);
            this.textBoxExcel.TabIndex = 3;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(67, 171);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(355, 20);
            this.textBoxOutput.TabIndex = 4;
            // 
            // textBoxPdf
            // 
            this.textBoxPdf.Location = new System.Drawing.Point(67, 131);
            this.textBoxPdf.Name = "textBoxPdf";
            this.textBoxPdf.Size = new System.Drawing.Size(355, 20);
            this.textBoxPdf.TabIndex = 5;
            // 
            // buttonProcess
            // 
            this.buttonProcess.Location = new System.Drawing.Point(198, 210);
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Size = new System.Drawing.Size(76, 31);
            this.buttonProcess.TabIndex = 9;
            this.buttonProcess.Text = "Procesar";
            this.buttonProcess.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // labelProcess
            // 
            this.labelProcess.AutoSize = true;
            this.labelProcess.Location = new System.Drawing.Point(280, 220);
            this.labelProcess.Name = "labelProcess";
            this.labelProcess.Size = new System.Drawing.Size(0, 0);
            this.labelProcess.TabIndex = 11;
            // 
            // buttonPdf
            // 
            this.buttonPdf.Location = new System.Drawing.Point(428, 125);
            this.buttonPdf.Name = "buttonPdf";
            this.buttonPdf.Size = new System.Drawing.Size(43, 31);
            this.buttonPdf.TabIndex = 8;
            this.buttonPdf.Click += new System.EventHandler(this.buttonPdf_Click);
            // 
            // buttonOutput
            // 
            this.buttonOutput.Location = new System.Drawing.Point(428, 165);
            this.buttonOutput.Name = "buttonOutput";
            this.buttonOutput.Size = new System.Drawing.Size(43, 31);
            this.buttonOutput.TabIndex = 7;
            this.buttonOutput.Click += new System.EventHandler(this.buttonOutput_Click);
            // 
            // buttonExcel
            // 
            this.buttonExcel.Location = new System.Drawing.Point(428, 88);
            this.buttonExcel.Name = "buttonExcel";
            this.buttonExcel.Size = new System.Drawing.Size(43, 31);
            this.buttonExcel.TabIndex = 6;
            this.buttonExcel.Click += new System.EventHandler(this.buttonExcel_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 94);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(38, 19);
            this.metroLabel1.TabIndex = 12;
            this.metroLabel1.Text = "Excel";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(28, 132);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(33, 19);
            this.metroLabel2.TabIndex = 13;
            this.metroLabel2.Text = "PDF";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(9, 171);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(52, 19);
            this.metroLabel3.TabIndex = 14;
            this.metroLabel3.Text = "Destino";
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Location = new System.Drawing.Point(126, 197);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(45, 45);
            this.metroProgressSpinner1.TabIndex = 15;
            this.metroProgressSpinner1.Value = 100;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 253);
            this.Controls.Add(this.metroProgressSpinner1);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.labelProcess);
            this.Controls.Add(this.buttonProcess);
            this.Controls.Add(this.buttonPdf);
            this.Controls.Add(this.buttonOutput);
            this.Controls.Add(this.buttonExcel);
            this.Controls.Add(this.textBoxPdf);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.textBoxExcel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "TransportesArenas.DeliveryManager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private MetroFramework.Controls.MetroTextBox textBoxExcel;
        private MetroFramework.Controls.MetroTextBox textBoxOutput;
        private MetroFramework.Controls.MetroTextBox textBoxPdf;
        private MetroFramework.Controls.MetroButton buttonExcel;
        private MetroFramework.Controls.MetroButton buttonOutput;
        private MetroFramework.Controls.MetroButton buttonPdf;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private MetroFramework.Controls.MetroButton buttonProcess;
        private MetroFramework.Controls.MetroLabel labelProcess;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
    }
}

