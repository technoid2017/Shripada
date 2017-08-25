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
           
            setPatientNames(patientID, patientName);
            dtDateOfAdmission.SelectedDate = DateTime.Now;
            setDoctors();
            getAllVisitData();
            
        }

        public void setPatientNames(String patientID, String patientName)
        {
            txtVisitPatientID.Text = patientID;
            txtVisitPatientName.Text = patientName;
            txtExamPatientID.Text = patientID;
            txtExamPatientName.Text = patientName;
            txtTreatPatientID.Text = patientID;
            txtTreatPatientName.Text = patientName;
            txtMedPatientID.Text = patientID;
            txtMedPatientName.Text = patientName;
            txtServicesPatientID.Text = patientID;
            txtServicesPatientName.Text = patientName;
            txtDischargePatientID.Text = patientID;
            txtDischargePatientName.Text = patientName;
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

        public void getAllVisitData()
        {
            List<String> visitDetails = Shripada.Code.Visit.getVisitDetails(txtVisitPatientID.Text);

            
            if (!visitDetails.ElementAt(2).ToString().TrimEnd().Equals("00:00 AM"))
            {
                txtVisitTimeOfAdmission.Text = visitDetails.ElementAt(2).ToString();
            }

            if (!visitDetails.ElementAt(3).ToString().TrimEnd().Equals("broughtby"))
            {
                txtVisitBroughtBy.Text = visitDetails.ElementAt(3).ToString();
            }

            if (!visitDetails.ElementAt(4).ToString().TrimEnd().Equals("relation"))
            {
                txtVisitRelationship.Text = visitDetails.ElementAt(4).ToString();
            }

            if (!visitDetails.ElementAt(5).ToString().TrimEnd().Equals("incharge"))
            {
                drpDoctorIncharge.Visibility = Visibility.Hidden;
                txtVisitDoctor.Visibility = Visibility.Visible;
                bttnEditDoctor.Visibility = Visibility.Visible;
                txtVisitDoctor.Text = visitDetails.ElementAt(5).ToString();
            }

            if (!visitDetails.ElementAt(6).ToString().TrimEnd().Equals("0"))
            {
                txtVisitDeposit.Text = visitDetails.ElementAt(6).ToString();
            }

            if (!visitDetails.ElementAt(7).ToString().TrimEnd().Equals("medicalhistory"))
            {
                txtVisitMedicalHistory.Text = visitDetails.ElementAt(7).ToString();
            }

           
            if (!visitDetails.ElementAt(9).ToString().TrimEnd().Equals("pulse"))
            {
                txtExamPulse.Text = visitDetails.ElementAt(9).ToString();
            }

            if (!visitDetails.ElementAt(10).ToString().TrimEnd().Equals("bp"))
            {
                txtExamBloodPressure.Text = visitDetails.ElementAt(10).ToString();
            }

            if (!visitDetails.ElementAt(11).ToString().TrimEnd().Equals("temp"))
            {
                txtExamTemperature.Text = visitDetails.ElementAt(11).ToString();
            }

            if (!visitDetails.ElementAt(12).ToString().TrimEnd().Equals("weight"))
            {
                txtExamWeight.Text = visitDetails.ElementAt(12).ToString();
            }

            if (!visitDetails.ElementAt(13).ToString().TrimEnd().Equals("custom1"))
            {
                txtExamCustom1.Text = visitDetails.ElementAt(13).ToString();
            }

            if (!visitDetails.ElementAt(14).ToString().TrimEnd().Equals("custom2"))
            {
                txtExamCustom2.Text = visitDetails.ElementAt(14).ToString();
            }

            if (!visitDetails.ElementAt(15).ToString().TrimEnd().Equals("wardtype"))
            {
                drpTreatWardType.Visibility = Visibility.Hidden;
                txtWardType.Visibility = Visibility.Visible;
                bttnEditWardType.Visibility = Visibility.Visible;
                txtWardType.Text = visitDetails.ElementAt(15).ToString();
            }

            if (!visitDetails.ElementAt(16).ToString().TrimEnd().Equals("course"))
            {
                txtTreatCourseInWard.Text = visitDetails.ElementAt(16).ToString();
            }

            if (!visitDetails.ElementAt(17).ToString().TrimEnd().Equals("treatmentgiven"))
            {
                txtTreatTreatmentGiven.Text = visitDetails.ElementAt(17).ToString();
            }
            if (!visitDetails.ElementAt(18).ToString().TrimEnd().Equals("treatmentadvanced"))
            {
                txtTreatAdviced.Text = visitDetails.ElementAt(18).ToString();
            }
            if (!visitDetails.ElementAt(19).ToString().TrimEnd().Equals("0"))
            {
                txtMedicineAmount.Text = visitDetails.ElementAt(19).ToString();
            }
            if (!visitDetails.ElementAt(20).ToString().TrimEnd().Equals("0"))
            {
                txtServicesTotalAmount.Text = visitDetails.ElementAt(20).ToString();
            }

            /*Code for Date & Time of Discharge
            if (!visitDetails.ElementAt(21).ToString().TrimEnd().Equals("2025-12-31"))
            {
                txtVisitTimeOfAdmission.Text = visitDetails.ElementAt(21).ToString();
            }
            if (!visitDetails.ElementAt(22).ToString().TrimEnd().Equals("00:00 AM"))
            {
                txtVisitTimeOfAdmission.Text = visitDetails.ElementAt(22).ToString();
            }*/
            
            //Total Charges
            //txtVisitTimeOfAdmission.Text = visitDetails.ElementAt(23).ToString();
            //Discounts
            //txtVisitTimeOfAdmission.Text = visitDetails.ElementAt(24).ToString();
            //Total Payable
            //txtVisitTimeOfAdmission.Text = visitDetails.ElementAt(25).ToString();
           
        }

        private void bttnEditDoctor_Click(object sender, RoutedEventArgs e)
        {
            txtVisitDoctor.Visibility = Visibility.Hidden;
            bttnEditDoctor.Visibility = Visibility.Hidden;
            drpDoctorIncharge.Visibility = Visibility.Visible;
        }

        private void bttnEditWardType_Click(object sender, RoutedEventArgs e)
        {
            txtWardType.Visibility = Visibility.Hidden;
            bttnEditWardType.Visibility = Visibility.Hidden;
            drpTreatWardType.Visibility = Visibility.Visible;
        }

    }
}
