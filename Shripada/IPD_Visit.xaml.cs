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
        String GvisitStatus;
        int GvisitID;
        
        public IPD_View(String patientID, String patientName)
        {
            InitializeComponent();
            setPatientNames(patientID, patientName);
            dtDateOfAdmission.SelectedDate = DateTime.Now;
            dtDischargeDate.SelectedDate = DateTime.Now;
            setDoctors();
            setWards();
            setMedicines();
            setServices();
            GvisitStatus = "Incomplete";
            getAllVisitData(GvisitStatus);
            loadDefaultGrid(GvisitID);
            
        }

        public IPD_View(String patientName, String patientID, String visitStatus, DateTime admissionDate, DateTime dischargeDate)
        {
            InitializeComponent();
            setPatientNames(patientID, patientName);
            viewOnlyMode();
           
            GvisitStatus = visitStatus;
            getAllVisitDataForViewOnly(GvisitStatus, admissionDate, dischargeDate);
            loadDefaultGrid(GvisitID);
        }

        public void viewOnlyMode()
        {
            bttnVisitSubmit.Visibility = Visibility.Hidden;
            bttnVisitCancel.Visibility = Visibility.Hidden;
            bttnEditDoctor.Visibility = Visibility.Hidden;
            
            bttnExamCancel.Visibility = Visibility.Hidden;
            bttnExamSubmit.Visibility = Visibility.Hidden;

            bttnTreatCancel.Visibility = Visibility.Hidden;
            bttnTreatSubmit.Visibility = Visibility.Hidden;
            bttnEditWardType.Visibility = Visibility.Hidden;

            bttnServicesAddNew.Visibility = Visibility.Hidden;
            bttnMedAddNew.Visibility = Visibility.Hidden;

            bttnDischargeCancel.Visibility = Visibility.Hidden;
            bttnDischargeGenerateBill.Visibility = Visibility.Hidden;
            bttnDischargeSubmit.Visibility = Visibility.Hidden;
           
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

        public void loadDefaultGrid(int visitID)
        {

            DataTable dtMedicine = Shripada.Code.Medicine.addMedicineToGrid(visitID);
            dataGrid1.ItemsSource = dtMedicine.DefaultView;
            decimal previousAmount = Shripada.Code.Medicine.getCurrentMedicineBill(visitID);
            //System.Windows.MessageBox.Show(previousAmount.ToString());
            txtMedicineAmount.Text = previousAmount.ToString();

            DataTable dtServices = Shripada.Code.Services.addServiceToGrid(visitID);
            dataGrid2.ItemsSource = dtServices.DefaultView;
            decimal previousAmountServices = Shripada.Code.Services.getCurrentServiceBill(visitID);
          
            txtServicesTotalAmount.Text = previousAmountServices.ToString();
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            String patientID = txtVisitPatientID.Text;
            String broughtBy = txtVisitBroughtBy.Text;
            String relation = txtVisitRelationship.Text;
            String doctor = txtVisitDoctor.Text;
            if (drpDoctorIncharge.IsVisible)
            {
                doctor = drpDoctorIncharge.SelectedItem.ToString();
            }
            String medicalHistory = txtVisitMedicalHistory.Text;
            
            String admissionDate = txtDateOfAdmission.Text;
            if (dtDateOfAdmission.IsVisible)
            {
                DateTime admissionDt = Convert.ToDateTime(dtDateOfAdmission.SelectedDate);
                admissionDate = admissionDt.ToString("yyyy-MM-dd");
            }
            //System.Windows.MessageBox.Show(admissionDate);
            string admissionTime = txtVisitTimeOfAdmission.Text;
            decimal deposit = Convert.ToDecimal(txtVisitDeposit.Text);

            Shripada.Code.Visit.submitVisitBasicDetails(GvisitID, broughtBy, relation, doctor, medicalHistory, admissionDate, admissionTime, deposit);
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

        public void getAllVisitData(String visitStatus)
        {
            List<String> visitDetails = Shripada.Code.Visit.getVisitDetails(txtVisitPatientID.Text, visitStatus);

            dtDateOfAdmission.Visibility = Visibility.Hidden;
            txtDateOfAdmission.Visibility = Visibility.Visible;
            txtDateOfAdmission.Text = visitDetails.ElementAt(1).ToString();
            GvisitID = Convert.ToInt32(visitDetails.ElementAt(27));

            if (!visitDetails.ElementAt(2).ToString().TrimEnd().Equals("00:00 AM"))
            {
                txtVisitTimeOfAdmission.Text = visitDetails.ElementAt(2).ToString();
                lbDischargeDOA.Content = visitDetails.ElementAt(2).ToString();
            }

            if (!visitDetails.ElementAt(3).ToString().TrimEnd().Equals("broughtby"))
            {
                txtVisitBroughtBy.Text = visitDetails.ElementAt(3).ToString();
                lblDischargeTime.Content = visitDetails.ElementAt(3).ToString();
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
                txtDischargeDeposit.Text = visitDetails.ElementAt(6).ToString();
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
                txtDischargeWardType.Text = visitDetails.ElementAt(15).ToString();
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
                txtDischargeMedicineCharges.Text = visitDetails.ElementAt(19).ToString(); 
            }
            if (!visitDetails.ElementAt(20).ToString().TrimEnd().Equals("0"))
            {
                txtServicesTotalAmount.Text = visitDetails.ElementAt(20).ToString();
                txtDischargeServiceCharges.Text = visitDetails.ElementAt(20).ToString();
            }

            //Code for Date & Time of Discharge
            if (!visitDetails.ElementAt(24).ToString().TrimEnd().Equals("2025-12-31"))
            {
                txtDischargeDateOfDischarge.Text = visitDetails.ElementAt(24).ToString();
            }
            if (!visitDetails.ElementAt(25).ToString().TrimEnd().Equals("00:00 AM"))
            {
                txtDischargeTOD.Text = visitDetails.ElementAt(25).ToString();
            }
            
            //Total Charges
            txtDischargeTotalCharges.Text = visitDetails.ElementAt(21).ToString();
            //Discounts
            txtDischargeDiscount.Text = visitDetails.ElementAt(22).ToString();
            //Total Payable
            txtDsichargePayable.Text = visitDetails.ElementAt(23).ToString();
            //Ward Charges
            txtDischargeWardCharges.Text = visitDetails.ElementAt(27).ToString();
           
        }


        public void getAllVisitDataForViewOnly(String visitStatus, DateTime admissionDate, DateTime dischargeDate)
        {
            List<String> visitDetails = Shripada.Code.Visit.getVisitDetailsForViewOnly(txtVisitPatientID.Text, visitStatus, admissionDate, dischargeDate);

            dtDateOfAdmission.Visibility = Visibility.Hidden;
            txtDateOfAdmission.Visibility = Visibility.Visible;
            txtDateOfAdmission.Text = visitDetails.ElementAt(1).ToString();
            GvisitID = Convert.ToInt32(visitDetails.ElementAt(27));

            if (!visitDetails.ElementAt(2).ToString().TrimEnd().Equals("00:00 AM"))
            {
                txtVisitTimeOfAdmission.Text = visitDetails.ElementAt(2).ToString();
                lbDischargeDOA.Content = visitDetails.ElementAt(2).ToString();
            }

            if (!visitDetails.ElementAt(3).ToString().TrimEnd().Equals("broughtby"))
            {
                txtVisitBroughtBy.Text = visitDetails.ElementAt(3).ToString();
                lblDischargeTime.Content = visitDetails.ElementAt(3).ToString();
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
                txtDischargeDeposit.Text = visitDetails.ElementAt(6).ToString();
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
                txtDischargeWardType.Text = visitDetails.ElementAt(15).ToString();
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
                txtDischargeMedicineCharges.Text = visitDetails.ElementAt(19).ToString();
            }
            if (!visitDetails.ElementAt(20).ToString().TrimEnd().Equals("0"))
            {
                txtServicesTotalAmount.Text = visitDetails.ElementAt(20).ToString();
                txtDischargeServiceCharges.Text = visitDetails.ElementAt(20).ToString();
            }

            //Code for Date & Time of Discharge
            if (!visitDetails.ElementAt(24).ToString().TrimEnd().Equals("2025-12-31"))
            {
                dtDischargeDate.Visibility = Visibility.Hidden;
                txtDischargeDateOfDischarge.Visibility = Visibility.Visible;
                txtDischargeDateOfDischarge.Text = visitDetails.ElementAt(24).ToString();
            }
            if (!visitDetails.ElementAt(25).ToString().TrimEnd().Equals("00:00 AM"))
            {
                txtDischargeTOD.Text = visitDetails.ElementAt(25).ToString();
            }

            //Total Charges
            txtDischargeTotalCharges.Text = visitDetails.ElementAt(21).ToString();
            //Discounts
            txtDischargeDiscount.Text = visitDetails.ElementAt(22).ToString();
            //Total Payable
            txtDsichargePayable.Text = visitDetails.ElementAt(23).ToString();
            //Ward Charges
            txtDischargeWardCharges.Text = visitDetails.ElementAt(27).ToString();

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
            Shripada.Code.Visit.submitVisitExamDetails(pulse,bp,temp,weight,custom1,custom2,GvisitID);
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
            submitWardDetailsToWardOrder();
            String patientID = txtTreatPatientID.Text;
            String wardType = drpTreatWardType.SelectedItem.ToString();
            String advicedTreatment = txtTreatAdviced.Text;
            String givenTreatment = txtTreatTreatmentGiven.Text;
            String courseinWard = txtTreatCourseInWard.Text;
            Shripada.Code.Visit.submitVisitTreatmentDetails(wardType, advicedTreatment, givenTreatment, courseinWard, GvisitID);
            //cancelTreatmentForm();
        }

        public void submitWardDetailsToWardOrder()
        {
            String patientID = txtTreatPatientID.Text;
            String wardType = drpTreatWardType.SelectedItem.ToString();
            DateTime admissionDate = Convert.ToDateTime(txtDateOfAdmission.Text);
            DateTime dischargeDate = admissionDate;
            int noOfDays = 0;
            int hospitalStay = 0;
            int operationalDelivery = 0;
            int aneasthesia = 0;
            int OTCharge = 0;
            int assistantCharge = 0;
            int nursing = 0;
            int padiatricianCharge = 0;
            int roundCharge = 0;
            int miscellaneousCharge = 0;

            if (chkHospitalStay.IsChecked == true)
            {
                hospitalStay = 1;
            }
            if (chkOperationalDelivery.IsChecked == true)
            {
                operationalDelivery = 1;
            }
            if (chkAneasthesia.IsChecked == true)
            {
                aneasthesia = 1;
            }
            if (chkOTCharge.IsChecked == true)
            {
                OTCharge = 1;
            }
            if (chkAssistantCharge.IsChecked == true)
            {
                assistantCharge = 1;
            }
            if (chkNursing.IsChecked == true)
            {
                nursing = 1;
            }
            if (chkPaediatricianCharge.IsChecked == true)
            {
                padiatricianCharge = 1;
            }
            if (chkRoundCharge.IsChecked == true)
            {
                roundCharge = 1;
            }
            if (chkMiscellaneousCharge.IsChecked == true)
            {
                miscellaneousCharge = 1;
            }

            Shripada.Code.Wards.updateWardData(GvisitID, patientID, wardType, hospitalStay, operationalDelivery, aneasthesia, OTCharge, assistantCharge, nursing, padiatricianCharge, roundCharge, miscellaneousCharge, admissionDate, dischargeDate, noOfDays);
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

                Shripada.Code.Medicine.placeMedicineOrder(txtMedPatientID.Text, drpMedicine.SelectedItem.ToString(), quantity, totalAmount, GvisitID);

                DataTable dt = Shripada.Code.Medicine.addMedicineToGrid(GvisitID);
                dataGrid1.ItemsSource = dt.DefaultView;

                decimal previousAmount = Shripada.Code.Medicine.getCurrentMedicineBill(GvisitID);
                grandTotalAmount = previousAmount + totalAmount;

                Shripada.Code.Medicine.addTotalBillToMain(GvisitID, grandTotalAmount);
                txtMedicineAmount.Text = grandTotalAmount.ToString();
                
                decimal remainingStock = availableStock - quantity;
                //System.Windows.Forms.MessageBox.Show(remainingStock.ToString());
                Shripada.Code.Medicine.updateRemainingStock(drpMedicine.SelectedItem.ToString(), remainingStock);

                drpMedicine.SelectedIndex = -1;
                txtMedQuantity.Text = "";
            }
        }

        private void bttnMedGenerateBill_Click(object sender, RoutedEventArgs e)
        {
            MedicineBill window = new MedicineBill(GvisitID);
            window.Show();
        }

        private void bttnServicesAddNew_Click(object sender, RoutedEventArgs e)
        {

            decimal grandTotalAmount = 0;
            int noOfDays = Convert.ToInt32(txtServicesNoOfDays.Text);

            decimal totalAmount = Shripada.Code.Services.calculateServicePrice(drpServices.SelectedItem.ToString(), noOfDays);

            Shripada.Code.Services.placeServiceOrder(txtServicesPatientID.Text, drpServices.SelectedItem.ToString(), noOfDays, totalAmount, GvisitID);

            DataTable dtServices = Shripada.Code.Services.addServiceToGrid(GvisitID);
            dataGrid2.ItemsSource = dtServices.DefaultView;

            decimal previousAmount = Shripada.Code.Services.getCurrentServiceBill(GvisitID);
            grandTotalAmount = previousAmount + totalAmount;

            Shripada.Code.Services.addTotalBillToMain(GvisitID, grandTotalAmount);
            txtServicesTotalAmount.Text = grandTotalAmount.ToString();

            Shripada.Code.Services.addServiceToGrid(GvisitID);

            drpServices.SelectedIndex = -1;
            txtServicesNoOfDays.Text = "";
        }

        private void bttnServicesGenerateBill_Click(object sender, RoutedEventArgs e)
        {
            ServiceBill s = new ServiceBill(GvisitID);
            s.Show();
        }

        private void bttnDischargeGenerateBill_Click(object sender, RoutedEventArgs e)
        {
            //txtDischargeTotalCharges.Text = "";
            //txtDischargeWardCharges.Text = "";
            //txtDischargeDeposit.Text = "";
            DateTime disChargeDate = Convert.ToDateTime(dtDischargeDate.SelectedDate);
            String dischargeTime = txtDischargeTOD.Text;
            decimal totalCharges = 0;
            decimal totalPayable = 0;

            Shripada.Code.Visit.setDischargeDateAndTime(disChargeDate, dischargeTime, GvisitID);
            Shripada.Code.Wards.setDischargeDate(disChargeDate,GvisitID);

            List<String> admitDetails = Shripada.Code.Visit.getVisitDetails(txtVisitPatientID.Text, GvisitStatus);
            DateTime admitDate = Convert.ToDateTime(admitDetails.ElementAt(1));

            calculateWardCharges(admitDate, disChargeDate);

            List<String> DischargeDetails = Shripada.Code.Visit.getVisitDetails(txtVisitPatientID.Text, GvisitStatus);
            lbDischargeDOA.Content = DischargeDetails.ElementAt(1);
            lblDischargeTime.Content = DischargeDetails.ElementAt(2);
            txtDischargeDeposit.Text = DischargeDetails.ElementAt(6);
            txtDischargeWardType.Text = DischargeDetails.ElementAt(15);
            txtDischargeMedicineCharges.Text = DischargeDetails.ElementAt(19);
            txtDischargeServiceCharges.Text = DischargeDetails.ElementAt(20);
            txtDischargeWardCharges.Text = DischargeDetails.ElementAt(28);

            totalCharges = Convert.ToDecimal(txtDischargeMedicineCharges.Text) + Convert.ToDecimal(txtDischargeServiceCharges.Text) + Convert.ToDecimal(txtDischargeWardCharges.Text);
            txtDischargeTotalCharges.Text = totalCharges.ToString();

            totalPayable = totalCharges - Convert.ToDecimal(DischargeDetails.ElementAt(6)) - Convert.ToDecimal(txtDischargeDiscount.Text);
            txtDsichargePayable.Text = totalPayable.ToString();
        }

        void calculateWardCharges(DateTime admitDate, DateTime disChargeDate)
        {
            decimal totalWardCharges = 0;
            int noOfDays = disChargeDate.Subtract((admitDate)).Days;
            String wardType = Shripada.Code.Wards.getWardTypeOfVisit(GvisitID);
            decimal hospitalStay = 0;
            decimal operationdelivery = 0;
            decimal anaesthesia = 0;
            decimal OTCharge = 0;
            decimal assistantCharge = 0;
            decimal nursing = 0;
            decimal consultantCharge = 0;
            decimal roundCharge = 0;
            decimal miscellaneousCharge = 0;

            List<decimal> wardRates = Shripada.Code.Wards.getWardTypeWiseRates(wardType);


            List<int> wardCharges = Shripada.Code.Wards.getWardCharges(GvisitID);

            if (wardCharges.ElementAt(0) == 1)
            {
                hospitalStay = wardRates.ElementAt(0) * noOfDays;
            }

            if (wardCharges.ElementAt(1) == 1)
            {
                operationdelivery = wardRates.ElementAt(1);
            }

            if (wardCharges.ElementAt(2) == 1)
            {
                anaesthesia = wardRates.ElementAt(2);
            }

            if (wardCharges.ElementAt(3) == 1)
            {
                OTCharge = wardRates.ElementAt(3) * noOfDays;
            }

            if (wardCharges.ElementAt(4) == 1)
            {
                assistantCharge = wardRates.ElementAt(4) * noOfDays;
            }

            if (wardCharges.ElementAt(5) == 1)
            {
                nursing = wardRates.ElementAt(5) * noOfDays;
            }

            if (wardCharges.ElementAt(6) == 1)
            {
                consultantCharge = wardRates.ElementAt(6) * noOfDays;
            }

            if (wardCharges.ElementAt(7) == 1)
            {
                roundCharge = wardRates.ElementAt(7) * noOfDays;
            }

            if (wardCharges.ElementAt(8) == 1)
            {
                miscellaneousCharge = wardRates.ElementAt(8);
            }

            totalWardCharges = hospitalStay + operationdelivery + anaesthesia + OTCharge + assistantCharge + nursing + consultantCharge + roundCharge + miscellaneousCharge;
            Shripada.Code.Wards.setWardChargesForVisit(noOfDays,hospitalStay,operationdelivery,anaesthesia,OTCharge,assistantCharge,nursing,consultantCharge,roundCharge,miscellaneousCharge,totalWardCharges, GvisitID);
            Shripada.Code.Visit.setWardChargesToVisitData(GvisitID, totalWardCharges);
        
        }

        private void drpTreatWardType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            expander1.Visibility = Visibility.Visible;
        }

        private void bttnDischargeSubmit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Are you sure you want to proceed?");
            decimal discounts = Convert.ToDecimal(txtDischargeDiscount.Text);
            decimal totalCharges = Convert.ToDecimal(txtDischargeTotalCharges.Text);
            decimal payable = Convert.ToDecimal(txtDsichargePayable.Text);
                        
            Shripada.Code.Visit.completeVisit(totalCharges, discounts, payable, GvisitID);
            Shripada.Code.Patient.dischargePatient(txtDischargePatientID.Text);
            //refreshDischargePage();
            bttnDischargeSubmit.IsEnabled = false;
        }

        void refreshDischargePage()
        {
            txtDischargeTOD.Text = "";
            txtDischargeWardType.Text = "";
            txtDischargeWardCharges.Text = "";
            txtDischargeServiceCharges.Text = "";
            txtDischargeMedicineCharges.Text = "";
            txtDischargeDiscount.Text = "";
            txtDischargeTotalCharges.Text = "";
            txtDsichargePayable.Text = "";
        }

        private void bttnDischargeCancel_Click(object sender, RoutedEventArgs e)
        {
            refreshDischargePage();
        }

        private void bttnDischargePrint_Click(object sender, RoutedEventArgs e)
        {
            NewDischargeBillWindow d = new NewDischargeBillWindow(GvisitID);
            d.Show();
        }

        private void bttnDischargeCard_Click(object sender, RoutedEventArgs e)
        {
            DischargeCard d = new DischargeCard(GvisitID);
            d.Show();

        }

        

        

        

       




    }
}
