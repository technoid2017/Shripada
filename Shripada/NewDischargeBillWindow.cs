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
    public partial class NewDischargeBillWindow : Form
    {
        int GvisitID;
        public NewDischargeBillWindow(int visitID)
        {
            InitializeComponent();
            GvisitID = visitID;
        }

        private void NewDischargeBillWindow_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'NewDischargeBill.DataTable1' table. You can move, or remove it, as needed.
            this.DataTable1TableAdapter.Fill(this.NewDischargeBill.DataTable1, GvisitID);

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
