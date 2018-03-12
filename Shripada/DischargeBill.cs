using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shripada
{
    public partial class DischargeBill : Form
    {
        int GvisitID;
        public DischargeBill(int visitID)
        {
            InitializeComponent();
            GvisitID = visitID;
        }

        private void DischargeBill_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DischargeBillDataSet.DataTable1' table. You can move, or remove it, as needed.
           // this.DataTable1TableAdapter.Fill(this.NewDischargeBill.DataTable1, GvisitID);
            this.DataTable1TableAdapter.Fill(this.NewDischargeBill.DataTable1, GvisitID);
            this.reportViewer1.RefreshReport();
            //this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
