using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Shripada
{
    /// <summary>
    /// Interaction logic for IPD_View.xaml
    /// </summary>
    public partial class IPD_View : Window
    {
        public IPD_View(String patientID, String patientName)
        {
            InitializeComponent();
            txtVisitPatientID.Text = patientID;
            txtVisitPatientName.Text = patientName;
            dtDateOfAdmission.SelectedDate = DateTime.Now;
            setDoctors();
        }

        public void setDoctors()
        {
            drpDoctorIncharge.Items.Add("Doc1");
            drpDoctorIncharge.Items.Add("Doc2");

        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            String patientID = txtVisitPatientID.Text;
            String broughtBy = txtVisitBroughtBy.Text;
            String relation = txtVisitRelationship.Text;
            String doctor = drpDoctorIncharge.SelectedItem.ToString();
            String medicalHistory = txtVisitMedicalHistory.Text;
            DateTime admissionDt = Convert.ToDateTime(dtDateOfAdmission.SelectedDate);
            String admissionDate = admissionDt.ToString("yyyy-MM-dd");
            System.Windows.MessageBox.Show(admissionDate);
            string admissionTime = txtVisitTimeOfAdmission.Text;
            decimal deposit = Convert.ToDecimal(txtVisitDeposit.Text);

            Shripada.Code.Visit.submitVisitBasicDetails(patientID, broughtBy, relation, doctor, medicalHistory, admissionDate, admissionTime, deposit);
            refreshAdditionalDetails();
        }

        public void refreshAdditionalDetails()
        {
            txtVisitBroughtBy.Text = "";
            txtVisitRelationship.Text = "";
            drpDoctorIncharge.SelectedIndex = -1;
            txtVisitMedicalHistory.Text = "";
            txtVisitTimeOfAdmission.Text = "";
            dtDateOfAdmission.SelectedDate = DateTime.Now;
            txtVisitDeposit.Text = "";

        }
    }
}
