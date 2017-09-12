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
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
        }

        private void button15_Click(object sender, RoutedEventArgs e)
        {
            Window w = new ResetPassword();
            w.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            String doctorName = txtDoctorName.Text;
            String specialization = txtDoctorSpecialization.Text;
            Shripada.Code.Doctors.addDoctor(doctorName, specialization);
            txtDoctorName.Text = "";
            txtDoctorSpecialization.Text = "";
        }

        private void bttnDoctorSearch_Click(object sender, RoutedEventArgs e)
        {
            String doctorName = txtDoctorSearchByName.Text;
            List<String> doctorDetails = Shripada.Code.Doctors.searchDoctor(doctorName);
            txtDoctorName.Text = doctorDetails.ElementAt(0);
            txtDoctorSpecialization.Text = doctorDetails.ElementAt(1);
            txtDoctorSearchByName.Text = "";
        }


        private void bttnDoctorUpdate_Click(object sender, RoutedEventArgs e)
        {
            String searchBoxValue = txtDoctorSearchByName.Text;
            String doctorName = txtDoctorName.Text;
            String specialization = txtDoctorSpecialization.Text;
            Shripada.Code.Doctors.updateDoctor(doctorName, specialization);
            txtDoctorSearchByName.Text = "";
            txtDoctorName.Text = "";
            txtDoctorSpecialization.Text = "";
        }

        private void bttnDoctorCancel_Click(object sender, RoutedEventArgs e)
        {
            txtDoctorSearchByName.Text = "";
            txtDoctorName.Text = "";
            txtDoctorSpecialization.Text = "";
        }

        private void bttnDoctorDelete_Click(object sender, RoutedEventArgs e)
        {

            String doctorName = txtDoctorName.Text;
            Shripada.Code.Doctors.deleteDoctor(doctorName);
            txtDoctorSearchByName.Text = "";
            txtDoctorName.Text = "";
            txtDoctorSpecialization.Text = "";
        }
// Methods for Services
        private void bttnServicesSubmit_Click(object sender, RoutedEventArgs e)
        {
            String serviceName = txtServiceName.Text;
            String description = txtServiceDescription.Text;
            decimal amount = Convert.ToDecimal(txtServiceAmount.Text);
            Shripada.Code.Services.addService(serviceName, description, amount);
            txtServiceName.Text = "";
            txtServiceDescription.Text = "";
            txtServiceAmount.Text = "";
        }

        private void bttnServicesCancel_Click(object sender, RoutedEventArgs e)
        {
            txtServiceSearch.Text = "";
            txtServiceName.Text = "";
            txtServiceDescription.Text = "";
            txtServiceAmount.Text = "";
        }

        private void bttnServicesUpdate_Click(object sender, RoutedEventArgs e)
        {
            String searchValue = txtServiceSearch.Text;
            String serviceName = txtServiceName.Text;
            String description = txtServiceDescription.Text;
            decimal amount = Convert.ToDecimal(txtServiceAmount.Text);
            Shripada.Code.Services.updateService(serviceName, description, amount);
            txtServiceSearch.Text = "";
            txtServiceName.Text = "";
            txtServiceDescription.Text = "";
            txtServiceAmount.Text = "";
        }

        private void bttnServicesDelete_Click(object sender, RoutedEventArgs e)
        {

            String serviceName = txtServiceName.Text;
            decimal amount = Convert.ToDecimal(txtServiceAmount.Text);
            Shripada.Code.Services.deleteService(serviceName);
            txtServiceSearch.Text = "";
            txtServiceName.Text = "";
            txtServiceDescription.Text = "";
            txtServiceAmount.Text = "";
        }

        private void bttnServiceSearchByName_Click(object sender, RoutedEventArgs e)
        {

            String serviceName = txtServiceSearch.Text;
            List<String> servieDetails = Shripada.Code.Services.searchService(serviceName);
            txtServiceName.Text = servieDetails.ElementAt(0);
            txtServiceDescription.Text = servieDetails.ElementAt(1);
            txtServiceAmount.Text = servieDetails.ElementAt(2);
            txtServiceSearch.Text = "";
        }

        

      
    }
}
