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
    public partial class MedicineBill : Form
    {
        int GvisitID;
        public MedicineBill(int visitID)
        {
            InitializeComponent();
            GvisitID = visitID;
        }

        private void MedicineBill_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'MedicineBillDataSet.DataTable1' table. You can move, or remove it, as needed.
            this.DataTable1TableAdapter.Fill(this.MedicineBillDataSet.DataTable1, GvisitID);

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
