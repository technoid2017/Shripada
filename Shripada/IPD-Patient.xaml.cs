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
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Navigation;
using System.Text.RegularExpressions;
using Shripada.Code;

namespace Shripada
{
    /// <summary>
    /// Interaction logic for IPD_Patient.xaml
    /// </summary>
    public partial class IPD_Patient : Window
    {
        public IPD_Patient()
        {
            InitializeComponent();
            txtPatientID.Text = Shripada.Code.Patient.generatePatientID();
            dtRegisterDate.SelectedDate = DateTime.Now;


        }

       private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

       

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Shripada.Code.Patient.addVisitNumber(txtPatientID.Text);
            Shripada.Code.Visit.addNewVisit(txtPatientID.Text, txtPatientName.Text);
            Window w = new IPD_View(txtPatientID.Text, txtPatientName.Text);
            w.Show();
            //this.Close();
           



        }

        public String getSex()
        {
            String sex = "";
            if (radioMale.IsChecked.Equals(true) || dradioMale.IsChecked.Equals(true))
            {
                sex = radioMale.Content.ToString();
            }

            else if (radioFemale.IsChecked.Equals(true) || dradioFemale.IsChecked.Equals(true))
            {
                sex = radioFemale.Content.ToString();
            }

            else
            {
                sex = radioOther.Content.ToString();
            }

            return sex;
        }

        public String getMediclaim()
        {
            String mediclaim = "";
            if (radioCashless.IsChecked.Equals(true) || drdCheckBox.IsChecked.Equals(true))
            {
                mediclaim = radioCashless.Content.ToString();
            }

            else if (radioNone.IsChecked.Equals(true) || dradioNone.IsChecked.Equals(true))
            {
                mediclaim = radioNone.Content.ToString();
            }

            else
            {
                mediclaim = radioReinburse.Content.ToString();
            }

            return mediclaim;
        }


        public void pageAddPatientRefresh()
        {
            txtAddress.Text = "";
            txtAge.Text = "";
            txtCelNo.Text = "";
            txtPatientID.Text = "";
            txtPatientName.Text = "";
            dtRegisterDate.SelectedDate = DateTime.Now;
            radioFemale.IsChecked = false;
            radioMale.IsChecked = false;
            radioOther.IsChecked = false;
            radioReinburse.IsChecked = false;
            radioNone.IsChecked = false;
            radioCashless.IsChecked = false;


                    
        }

        
        private void bttnSumbit_Click(object sender, RoutedEventArgs e)
        {
            string patientID = txtPatientID.Text;
            string patientName = txtPatientName.Text;
            string registerDate = dtRegisterDate.SelectedDate.ToString();
            string address = txtAddress.Text;
            string celNo = txtCelNo.Text;
            int age = Convert.ToInt32(txtAge.Text);
            string sex = getSex();
            string mediclaim = getMediclaim();
            int noOfVisit = 0;

            Shripada.Code.Patient.addPatient(patientID, patientName, registerDate, address, celNo, age, sex, mediclaim, noOfVisit);
            bttnAddNew.Visibility = Visibility.Visible;
            bttnAddVisit.IsEnabled = true;
            bttnSumbit.IsEnabled = false;
            bttnCancel.IsEnabled = false;

        }

        private void bttnAddNew_Click(object sender, RoutedEventArgs e)
        {
            pageAddPatientRefresh();
            txtPatientID.Text = Shripada.Code.Patient.generatePatientID();
            bttnAddNew.Visibility = Visibility.Hidden;
            bttnSumbit.IsEnabled = true;
            bttnCancel.IsEnabled = true;
            bttnAddVisit.IsEnabled = false;
        }

        private void bttnCancel_Click(object sender, RoutedEventArgs e)
        {
            pageAddPatientRefresh();
        }

        //---------------------------------------------Search Patient------------------------------

        private void bbtnSearchByID_Click(object sender, RoutedEventArgs e)
        {
            String patientID = txtSearchById.Text;
            DataTable dt1 = Shripada.Code.Patient.searchPatient("byID",patientID);
            dataGrid1.ItemsSource = dt1.DefaultView;
            dataGrid1.Visibility = Visibility.Visible;
            bttnClearSearch.Visibility = Visibility.Visible;

        }

        private void bttnSearchByName_Click(object sender, RoutedEventArgs e)
        {
            String patientName = txtSearchByName.Text;
            DataTable dt1 = Shripada.Code.Patient.searchPatient("byName", patientName);
            dataGrid1.ItemsSource = dt1.DefaultView;
            dataGrid1.Visibility = Visibility.Visible;
            bttnClearSearch.Visibility = Visibility.Visible;

        }

        public void viewDetails(object sender, RoutedEventArgs e)
        {
           // System.Windows.Forms.MessageBox.Show("I am coming soon!");
            String patientID = txtSearchById.Text;
            List<String> patientDetails = new List<string>();
               patientDetails =  Shripada.Code.Patient.getPatientDetails(patientID);

            dPatientID.Text = patientDetails.ElementAt(0).ToString();
            dtxtPatientName.Text = patientDetails.ElementAt(1);
            dtxtAddress.Text = patientDetails.ElementAt(2);
            dtxtCelNo.Text = patientDetails.ElementAt(3);
            dtxtAge.Text = patientDetails.ElementAt(4);
            String sex = patientDetails.ElementAt(5);
            //System.Windows.Forms.MessageBox.Show(sex);
            if(sex.Trim().Equals("Male"))
            {
                dradioMale.IsChecked = true;
                dradioFemale.IsChecked = false;
                dradioOther.IsChecked = false;
            }

            else if (sex.Trim().Equals("Female"))
            {
                dradioFemale.IsChecked = true;
                dradioMale.IsChecked = false;
                dradioOther.IsChecked = false;
            }

            else
            {
                dradioOther.IsChecked = true;
                dradioFemale.IsChecked = false;
                dradioMale.IsChecked = false;
            }

            String mediclaim = patientDetails.ElementAt(6);
            if (mediclaim.Trim().Equals("None"))
            {
                dradioNone.IsChecked = true;
            }

            else if (mediclaim.Trim().Equals("Reinburse"))
            {
                dradioReinburse.IsChecked = true;
            }

            else
            {
                drdCheckBox.IsChecked = true;
            }

            dtxtRegDate.Text = patientDetails.ElementAt(7);
            
            dtxtNoOfVisits.Text = patientDetails.ElementAt(8);

            String currentStatus = patientDetails.ElementAt(9);

            tabViewPatient.IsSelected = true;
            patientEditVisitsLogic(currentStatus);
            patientViewMode();
        }

        private void bttnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.Visibility = Visibility.Hidden;
            bttnClearSearch.Visibility = Visibility.Hidden;
            txtSearchByName.Text = "";
            txtSearchById.Text = "";
        }

        //---------------------------------------------View Patient------------------------------
        public void patientViewMode()
        {
            dPatientID.IsEnabled = false;
            dtxtAddress.IsEnabled = false;
            dtxtAge.IsEnabled = false;
            dtxtCelNo.IsEnabled = false;
            dtxtNoOfVisits.IsEnabled = false;
            dtxtPatientName.IsEnabled = false;
            dtxtRegDate.IsEnabled = false;
            dradioFemale.IsEnabled = false;
            dradioMale.IsEnabled = false;
            dradioOther.IsEnabled = false;
            dradioReinburse.IsEnabled = false;
            dradioNone.IsEnabled = false;
            drdCheckBox.IsEnabled = false;

            dbttnUpdate.Visibility = Visibility.Hidden;
            dbttnCancel.Visibility = Visibility.Hidden;


        }

        public void patientEditMode()
        {
            //dPatientID.IsEnabled = true;
            dtxtAddress.IsEnabled = true;
            dtxtAge.IsEnabled = true;
            dtxtCelNo.IsEnabled = true;
            //dtxtNoOfVisits.IsEnabled = true;
            dtxtPatientName.IsEnabled = true;
            dtxtRegDate.IsEnabled = true;
            dradioFemale.IsEnabled = true;
            dradioMale.IsEnabled = true;
            dradioOther.IsEnabled = true;
            dradioReinburse.IsEnabled = true;
            dradioNone.IsEnabled = true;
            drdCheckBox.IsEnabled = true;

            dbttnUpdate.Visibility = Visibility.Visible;
            dbttnDelete.IsEnabled = false;
            dbttnCancel.Visibility = Visibility.Visible;
            dbttnEditDetails.Visibility = Visibility.Hidden;

        }

        public void patientEditVisitsLogic(String currentStatus)
        {
            if (currentStatus.Equals("Admitted"))
            {
                dbttnAddVisit.IsEnabled = false;
                //dbttnCompleteVisit.IsEnabled = true;
            }
            else
            {
                dbttnAddVisit.IsEnabled = true;
                //dbttnCompleteVisit.IsEnabled = false;
            }
        }

        private void dbttnEditDetails_Click(object sender, RoutedEventArgs e)
        {
            patientEditMode();
        }

        private void dbttnCancel_Click(object sender, RoutedEventArgs e)
        {
            patientViewMode();
        }

        private void dbttnUpdate_Click(object sender, RoutedEventArgs e)
        {
            String patientID = dPatientID.Text;
            String patientName = dtxtPatientName.Text;
            DateTime registerDate = Convert.ToDateTime(dtxtRegDate.Text);
            String address = dtxtAddress.Text;
            String celNo = dtxtCelNo.Text;
            int age = Convert.ToInt32(dtxtAge.Text);
            String sex = getSex();
            String mediclaim = getMediclaim();
            
            Shripada.Code.Patient.updatePatient(patientID, patientName, registerDate, address, celNo, age, sex, mediclaim);
            patientViewMode();
        }

        private void dbttnAddVisit_Click(object sender, RoutedEventArgs e)
        {
            Shripada.Code.Patient.addVisitNumber(dPatientID.Text);
            Shripada.Code.Visit.addNewVisit(dPatientID.Text, dtxtPatientName.Text);
            Window w = new IPD_View(dPatientID.Text, dtxtPatientName.Text);
            w.Show();
           
            
           // dPatientID.Text = w.
            
        }

        private void dbttnCompleteVisit_Click(object sender, RoutedEventArgs e)
        {
            Window w = new IPD_View(dPatientID.Text, dtxtPatientName.Text);
            w.Show();
        }

    

        

    }
}
