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

        private void button16_Click(object sender, RoutedEventArgs e)
        {
            String userName = txtAddUserName.Text;
            String password = txtAddPassword1.Password;
            String password2 = txtAddPassword2.Password;
            if (password.Equals(password2))
            {
                Shripada.Code.Admin.addNewUser(userName, password);
                txtAddUserName.Text = "";
                txtAddPassword1.Password = "";
                txtAddPassword2.Password = "";
            }

            else
            {
                System.Windows.MessageBox.Show("Password doesnot match! Please check");
                txtAddPassword2.Password = "";
            }
        }

        private void bttnAddCancel_Click(object sender, RoutedEventArgs e)
        {
            txtAddUserName.Text = "";
            txtAddPassword1.Password = "";
            txtAddPassword2.Password = "";
        }

        private void button15_Click(object sender, RoutedEventArgs e)
        {
            String userName = txtResetUserName.Text;
            String password = txtResetOldPassword.Password;
            if (Shripada.Code.LoginCode.login(userName, password))
            {
                Window w = new ResetPassword(userName);
                w.Show();
            }
                        

        }

        //Ward Methods
        private void bttnWardSubmit_Click(object sender, RoutedEventArgs e)
        {
            
            String wardName = txtWardName.Text;
            decimal stayCharges = Convert.ToDecimal(txtHospitalStay.Text);
            decimal operationdelivery = Convert.ToDecimal(txtOperationalDelivery.Text);
            decimal anaesthesia= Convert.ToDecimal(txtAnaesthesia.Text);
            decimal OTCharge=Convert.ToDecimal(txtOTCharge.Text);
            decimal assistantCharge=Convert.ToDecimal(txtAssistantCharge.Text);
            decimal nursing=Convert.ToDecimal(txtNursing.Text);
            decimal consultantCharge=Convert.ToDecimal(txtPaediatricianCharge.Text);
            decimal roundCharge=Convert.ToDecimal(txtRoundCharge.Text);
            decimal miscellaneousCharge = Convert.ToDecimal(txtMiscellaneousCharge.Text);

            Shripada.Code.Wards.addWard(wardName,stayCharges,operationdelivery,anaesthesia,OTCharge,assistantCharge,nursing,consultantCharge,roundCharge,miscellaneousCharge);
            refreshWards();
        }

        public void refreshWards()
        {
            txtSearchWard.Text = "";
            txtWardName.Text = "";
            txtHospitalStay.Text = "";
            txtOperationalDelivery.Text = "";
            txtAnaesthesia.Text="";
            txtOTCharge.Text="";
            txtAssistantCharge.Text="";
            txtNursing.Text="";
            txtPaediatricianCharge.Text="";
            txtRoundCharge.Text="";
            txtMiscellaneousCharge.Text="";
        }

        private void bttnWardCancel_Click(object sender, RoutedEventArgs e)
        {
            refreshWards();
        }

        private void bttnWardUpdate_Click(object sender, RoutedEventArgs e)
        {
            String wardName = txtWardName.Text;
            decimal stayCharges = Convert.ToDecimal(txtHospitalStay.Text);
            decimal operationdelivery = Convert.ToDecimal(txtOperationalDelivery.Text);
            decimal anaesthesia = Convert.ToDecimal(txtAnaesthesia.Text);
            decimal OTCharge = Convert.ToDecimal(txtOTCharge.Text);
            decimal assistantCharge = Convert.ToDecimal(txtAssistantCharge.Text);
            decimal nursing = Convert.ToDecimal(txtNursing.Text);
            decimal consultantCharge = Convert.ToDecimal(txtPaediatricianCharge.Text);
            decimal roundCharge = Convert.ToDecimal(txtRoundCharge.Text);
            decimal miscellaneousCharge = Convert.ToDecimal(txtMiscellaneousCharge.Text);

            Shripada.Code.Wards.updateWard(wardName, stayCharges, operationdelivery, anaesthesia, OTCharge, assistantCharge, nursing, consultantCharge, roundCharge, miscellaneousCharge);
            refreshWards();
        }

        private void bttnSearchWard_Click(object sender, RoutedEventArgs e)
        {
            String wardName = txtSearchWard.Text;
            List<String> wardDetails = Shripada.Code.Wards.searchWard(wardName);
            
            txtWardName.Text = wardDetails.ElementAt(0);
            txtHospitalStay.Text = wardDetails.ElementAt(1);
            txtOperationalDelivery.Text = wardDetails.ElementAt(2);
            txtAnaesthesia.Text = wardDetails.ElementAt(3);
            txtOTCharge.Text = wardDetails.ElementAt(4);
            txtAssistantCharge.Text = wardDetails.ElementAt(5);
            txtNursing.Text = wardDetails.ElementAt(6);
            txtPaediatricianCharge.Text = wardDetails.ElementAt(7);
            txtRoundCharge.Text = wardDetails.ElementAt(8);
            txtMiscellaneousCharge.Text = wardDetails.ElementAt(9);


        }

        private void bttnWardDelete_Click(object sender, RoutedEventArgs e)
        {

            String wardName = txtWardName.Text;
            Shripada.Code.Wards.deleteWard(wardName);
            refreshWards();
        }

        
        
        
       
 

      
    }
}
