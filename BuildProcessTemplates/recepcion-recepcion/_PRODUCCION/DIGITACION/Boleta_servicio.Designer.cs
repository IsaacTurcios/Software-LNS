namespace LND
{
    partial class Boleta_servicio
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Boleta_servicio));
            this.PED_ENCBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.recepciondb = new LND.recepciondb();
            this.pEDCOMPLEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pEDCOMPLEBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pEDENCBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PED_COMPLEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PED_ENCBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recepciondb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pEDCOMPLEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pEDCOMPLEBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pEDENCBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PED_COMPLEBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // PED_ENCBindingSource
            // 
            this.PED_ENCBindingSource.DataMember = "PED_ENC";
            this.PED_ENCBindingSource.DataSource = this.recepciondb;
            this.PED_ENCBindingSource.CurrentChanged += new System.EventHandler(this.PED_ENCBindingSource_CurrentChanged);
            // 
            // recepciondb
            // 
            this.recepciondb.DataSetName = "Recepciondb";
            this.recepciondb.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pEDCOMPLEBindingSource
            // 
            this.pEDCOMPLEBindingSource.DataMember = "PED_COMPLE";
            this.pEDCOMPLEBindingSource.DataSource = this.recepciondb;
            // 
            // pEDCOMPLEBindingSource1
            // 
            this.pEDCOMPLEBindingSource1.DataMember = "PED_COMPLE";
            this.pEDCOMPLEBindingSource1.DataSource = this.recepciondb;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "PED_ENC";
            reportDataSource1.Value = this.PED_ENCBindingSource;
            reportDataSource2.Name = "PED_COMPLE";
            reportDataSource2.Value = this.PED_COMPLEBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "LND.Boleta_Recepcion.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(838, 494);
            this.reportViewer1.TabIndex = 0;
            // 
            // pEDENCBindingSource
            // 
            this.pEDENCBindingSource.DataMember = "PED_ENC";
            this.pEDENCBindingSource.DataSource = this.recepciondb;
            // 
            // PED_COMPLEBindingSource
            // 
            this.PED_COMPLEBindingSource.DataMember = "PED_COMPLE";
            this.PED_COMPLEBindingSource.DataSource = this.recepciondb;
            // 
            // Boleta_servicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 494);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Boleta_servicio";
            this.Text = "Boleta_servicio";
            this.Load += new System.EventHandler(this.Boleta_servicio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PED_ENCBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recepciondb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pEDCOMPLEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pEDCOMPLEBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pEDENCBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PED_COMPLEBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource PED_ENCBindingSource;
        private LND.recepciondb recepciondb;
        private System.Windows.Forms.BindingSource pEDENCBindingSource;
        private System.Windows.Forms.BindingSource pEDCOMPLEBindingSource1;
        private System.Windows.Forms.BindingSource pEDCOMPLEBindingSource;
        private System.Windows.Forms.BindingSource PED_COMPLEBindingSource;
    }
}