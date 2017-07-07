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
    /// Interaction logic for IPD_Patient.xaml
    /// </summary>
    public partial class IPD_Patient : Window
    {
        public IPD_Patient()
        {
            InitializeComponent();
            txtPatientID.Text = Shripada.Code.Patient.generatePatientID();
        }

        private void bbtnSearchByID_Click(object sender, RoutedEventArgs e)
        {

        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Window w = new IPD_View();
            w.Show();
            this.Close();
        }

        public String getSex()
        {
            String sex = "";
            if (radioMale.IsChecked.Equals(true))
            {
                sex = radioMale.Content.ToString();
            }

            else if (radioFemale.IsChecked.Equals(true))
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
            if (radioCashless.IsChecked.Equals(true))
            {
                mediclaim = radioCashless.Content.ToString();
            }

            else if (radioNone.IsChecked.Equals(true))
            {
                mediclaim = radioNone.Content.ToString();
            }

            else
            {
                mediclaim = radioReinburse.Content.ToString();
            }

            return mediclaim;
        }


        public void pageRefresh()
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

        }

        private void bttnAddNew_Click(object sender, RoutedEventArgs e)
        {
            pageRefresh();
            txtPatientID.Text = Shripada.Code.Patient.generatePatientID();
            bttnAddNew.Visibility = Visibility.Hidden;
        }

    }
}
