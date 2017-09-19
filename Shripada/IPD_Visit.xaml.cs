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
using System.Data;

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
            setWards();
            setMedicines();
            setServices();
            getAllVisitData();
            loadDefaultGrid(patientID);
            
            
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
            List<String> doctors = Shripada.Code.Doctors.getDoctors();
            foreach (String s in doctors)
            {
                drpDoctorIncharge.Items.Add(s);
            }

        }

        public void setWards()
        {
            List<String> wards = Shripada.Code.Wards.getWards();
            foreach (String s in wards)
            {
                drpTreatWardType.Items.Add(s);
            }

        }

        public void setMedicines()
        {
            List<String> medicines = Shripada.Code.Medicine.getMedicines();
            foreach (String s in medicines)
            {
                drpMedicine.Items.Add(s);
            }

        }

        public void setServices()
        {
            List<String> services = Shripada.Code.Services.getServices();
            foreach (String s in services)
            {
                drpServices.Items.Add(s);
            }

        }

        public void loadDefaultGrid(String patientID)
        {

            DataTable dtMedicine = Shripada.Code.Medicine.addMedicineToGrid(patientID);
            dataGrid1.ItemsSource = dtMedicine.DefaultView;
            decimal previousAmount = Shripada.Code.Medicine.getCurrentMedicineBill(patientID);
            //System.Windows.MessageBox.Show(previousAmount.ToString());
            txtMedicineAmount.Text = previousAmount.ToString();

            DataTable dtServices = Shripada.Code.Services.addServiceToGrid(patientID);
            dataGrid2.ItemsSource = dtServices.DefaultView;
            decimal previousAmountServices = Shripada.Code.Services.getCurrentServiceBill(patientID);
            //System.Windows.MessageBox.Show(previousAmountServices.ToString());
            txtServicesTotalAmount.Text = previousAmountServices.ToString();
            
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
            //refreshAdditionalDetails();
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

        
        private void bttnVisitCancel_Click(object sender, RoutedEventArgs e)
        {
            txtVisitBroughtBy.Text = "";
            txtVisitRelationship.Text = "";
            txtVisitMedicalHistory.Text = "";
            txtVisitTimeOfAdmission.Text = "";
            txtVisitDeposit.Text = "";
            drpDoctorIncharge.SelectedIndex = -1;
            dtDateOfAdmission.SelectedDate = DateTime.Now;
        }

        private void bttnVisitExamine_Click(object sender, RoutedEventArgs e)
        {
            tabVisitExam.IsSelected = true;
        }

        private void bttnExamSubmit_Click(object sender, RoutedEventArgs e)
        {
            String pulse = txtExamPulse.Text;
            String bp = txtExamTemperature.Text;
            String temp = txtExamTemperature.Text;
            String weight = txtExamWeight.Text;
            String custom1 = txtExamCustom1.Text;
            String custom2 = txtExamCustom2.Text;
            String patientID = txtExamPatientID.Text;
            Shripada.Code.Visit.submitVisitExamDetails(pulse,bp,temp,weight,custom1,custom2,patientID);
            //clearExamForm();
        }

        private void bttnExamPrevious_Click(object sender, RoutedEventArgs e)
        {
            tabVisitDetails.IsSelected = true;
        }

        private void bttnExamNext_Click(object sender, RoutedEventArgs e)
        {
            tabVisitTreatment.IsSelected = true;
        }

        private void bttnExamCancel_Click(object sender, RoutedEventArgs e)
        {
            clearExamForm();
        }
        public void clearExamForm()
        {
            txtExamBloodPressure.Text = "";
            txtExamPulse.Text = "";
            txtExamTemperature.Text = "";
            txtExamWeight.Text = "";
            txtExamCustom1.Text = "";
            txtExamCustom2.Text = "";
        }

        //Treatment Tab
        private void bttnTreatPrevious_Click(object sender, RoutedEventArgs e)
        {
            tabVisitExam.IsSelected = true;
        }

        private void bttnTreatNext_Click(object sender, RoutedEventArgs e)
        {
            tabVisitMedicines.IsSelected = true;
        }

        private void bttnTreatCancel_Click(object sender, RoutedEventArgs e)
        {
            cancelTreatmentForm();
        }

        void cancelTreatmentForm()
        {
            drpTreatWardType.SelectedIndex = -1;
            txtTreatAdviced.Text = "";
            txtTreatCourseInWard.Text = "";
            txtTreatTreatmentGiven.Text = "";
        }

        private void bttnTreatSubmit_Click(object sender, RoutedEventArgs e)
        {
            String patientID = txtTreatPatientID.Text;
            String wardType = drpTreatWardType.SelectedItem.ToString();
            String advicedTreatment = txtTreatAdviced.Text;
            String givenTreatment = txtTreatTreatmentGiven.Text;
            String courseinWard = txtTreatCourseInWard.Text;
            Shripada.Code.Visit.submitVisitTreatmentDetails(wardType, advicedTreatment, givenTreatment, courseinWard, patientID);
            //cancelTreatmentForm();
        }

        private void bttnEditWardType_Click(object sender, RoutedEventArgs e)
        {
            txtWardType.Visibility = Visibility.Hidden;
            bttnEditWardType.Visibility = Visibility.Hidden;
            drpTreatWardType.Visibility = Visibility.Visible;
        }

        private void bttnMedAddNew_Click(object sender, RoutedEventArgs e)
        {
            decimal grandTotalAmount = 0;
            int quantity = Convert.ToInt32(txtMedQuantity.Text);
            decimal availableStock = Shripada.Code.Medicine.getAvailableStock(drpMedicine.SelectedItem.ToString());
            if (availableStock == 0)
            {
                System.Windows.Forms.MessageBox.Show("Sorry, You have no stock available!");
            }

            else if (availableStock < quantity)
            {
                System.Windows.Forms.MessageBox.Show("Sorry! You have only" +availableStock +" stock remaining!");
            }

            else if (availableStock >= quantity)
            {
                decimal totalAmount = Shripada.Code.Medicine.calculateMedicinePrice(drpMedicine.SelectedItem.ToString(), quantity);

                Shripada.Code.Medicine.placeMedicineOrder(txtMedPatientID.Text, drpMedicine.SelectedItem.ToString(), quantity, totalAmount);

                DataTable dt = Shripada.Code.Medicine.addMedicineToGrid(txtMedPatientID.Text);
                dataGrid1.ItemsSource = dt.DefaultView;

                decimal previousAmount = Shripada.Code.Medicine.getCurrentMedicineBill(txtMedPatientID.Text);
                grandTotalAmount = previousAmount + totalAmount;

                Shripada.Code.Medicine.addTotalBillToMain(txtMedPatientID.Text, grandTotalAmount);
                txtMedicineAmount.Text = grandTotalAmount.ToString();
                
                decimal remainingStock = availableStock - quantity;
                //System.Windows.Forms.MessageBox.Show(remainingStock.ToString());
                Shripada.Code.Medicine.updateRemainingStock(drpMedicine.SelectedItem.ToString(), remainingStock);

                drpMedicine.SelectedIndex = -1;
                txtMedQuantity.Text = "";
            }
        }

        private void bttnServicesAddNew_Click(object sender, RoutedEventArgs e)
        {

            decimal grandTotalAmount = 0;
            int noOfDays = Convert.ToInt32(txtServicesNoOfDays.Text);

            decimal totalAmount = Shripada.Code.Services.calculateServicePrice(drpServices.SelectedItem.ToString(), noOfDays);

            Shripada.Code.Services.placeServiceOrder(txtServicesPatientID.Text, drpServices.SelectedItem.ToString(), noOfDays, totalAmount);

            DataTable dtServices = Shripada.Code.Services.addServiceToGrid(txtServicesPatientID.Text);
            dataGrid2.ItemsSource = dtServices.DefaultView;

            decimal previousAmount = Shripada.Code.Services.getCurrentServiceBill(txtServicesPatientID.Text);
            grandTotalAmount = previousAmount + totalAmount;

            Shripada.Code.Services.addTotalBillToMain(txtServicesPatientID.Text, grandTotalAmount);
            txtServicesTotalAmount.Text = grandTotalAmount.ToString();

            Shripada.Code.Services.addServiceToGrid(txtServicesPatientID.Text);

            drpServices.SelectedIndex = -1;
            txtServicesNoOfDays.Text = "";
        }

        private void bttnDischargeGenerateBill_Click(object sender, RoutedEventArgs e)
        {
            List<String> DischargeDetails = Shripada.Code.Visit.getVisitDetails(txtVisitPatientID.Text);

            lbDischargeDOA.Content = DischargeDetails.ElementAt(1).ToString();
            lblDischargeTime.Content = DischargeDetails.ElementAt(2).ToString();
            txtDischargeDeposit.Text = DischargeDetails.ElementAt(6).ToString();
            txtDischargeWardType.Text = DischargeDetails.ElementAt(15).ToString();
            txtDischargeMedicineCharges.Text = DischargeDetails.ElementAt(19).ToString();
            txtDischargeServiceCharges.Text = DischargeDetails.ElementAt(20).ToString();
            dtDischargeDate.SelectedDate = DateTime.Now;
            

        }




    }
}
