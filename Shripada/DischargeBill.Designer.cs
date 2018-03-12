namespace Shripada
{
    partial class DischargeBill
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
            this.DataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DischargeBillDataSet = new Shripada.DischargeBillDataSet();
            this.DataTable1TableAdapter = new Shripada.DischargeBillDataSetTableAdapters.DataTable1TableAdapter();
            this.NewDischargeBill = new Shripada.NewDischargeBill();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DischargeBillDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewDischargeBill)).BeginInit();
            this.SuspendLayout();
            // 
            // DataTable1BindingSource
            // 
            this.DataTable1BindingSource.DataMember = "DataTable1";
            this.DataTable1BindingSource.DataSource = this.DischargeBillDataSet;
            // 
            // DischargeBillDataSet
            // 
            this.DischargeBillDataSet.DataSetName = "DischargeBillDataSet";
            this.DischargeBillDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DataTable1TableAdapter
            // 
            this.DataTable1TableAdapter.ClearBeforeFill = true;
            // 
            // NewDischargeBill
            // 
            this.NewDischargeBill.DataSetName = "NewDischargeBill";
            this.NewDischargeBill.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.DataTable1BindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Shripada.NewDischargeBillReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(582, 745);
            this.reportViewer1.TabIndex = 0;
            // 
            // DischargeBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 741);
            this.Controls.Add(this.reportViewer1);
            this.Name = "DischargeBill";
            this.Text = "DischargeBill";
            this.Load += new System.EventHandler(this.DischargeBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataTable1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DischargeBillDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewDischargeBill)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource DataTable1BindingSource;
        private DischargeBillDataSet DischargeBillDataSet;
        private DischargeBillDataSetTableAdapters.DataTable1TableAdapter DataTable1TableAdapter;
        private NewDischargeBill NewDischargeBill;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}