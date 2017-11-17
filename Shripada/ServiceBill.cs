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
    public partial class ServiceBill : Form
    {
        int GvisitID;
        public ServiceBill(int visitID)
        {
            InitializeComponent();
            GvisitID = visitID;
        }

        private void ServiceBill_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'ServiceBillDataSet.DataTable1' table. You can move, or remove it, as needed.
            this.DataTable1TableAdapter.Fill(this.ServiceBillDataSet.DataTable1, GvisitID);

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
