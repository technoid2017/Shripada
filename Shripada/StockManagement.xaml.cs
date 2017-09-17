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
    /// Interaction logic for StockManagement.xaml
    /// </summary>
    public partial class StockManagement : Window
    {
        public StockManagement()
        {
            InitializeComponent();
            dtMedRegisterDate.SelectedDate = DateTime.Now;
            dtMedExpireDate.SelectedDate = DateTime.Now;
            dtMedReminder.SelectedDate = Convert.ToDateTime("12/31/2020");
            txtMedQuantityThrashhold.Text = "0";
            dtMedEditCurrentDate.SelectedDate = DateTime.Now;
        }

        private void bttnMedSumbit_Click(object sender, RoutedEventArgs e)
        {
            String medicineName = txtMedName.Text;
            String medicineDescription = txtMedDesc.Text;
            Decimal medicinePrice = Convert.ToDecimal(txtMedRate.Text);
            String manufacturer = txtMedManufacturer.Text;
            String batchNo = txtMedBatchNo.Text;
            DateTime registerDate = Convert.ToDateTime(dtMedRegisterDate.SelectedDate);
            DateTime expiryDate = Convert.ToDateTime(dtMedExpireDate.SelectedDate);
            Decimal quantity = Convert.ToDecimal(txtMedQuantity.Text);
            Decimal amount = Convert.ToDecimal(txtMedAmount.Text);
            String reminderType = getReminderType();
            DateTime reminderDate = Convert.ToDateTime(dtMedReminder.SelectedDate);
            Decimal quantityThreshold = Convert.ToDecimal(txtMedQuantityThrashhold.Text);

            Shripada.Code.Stock.addNewMedicine(medicineName, medicineDescription, medicinePrice, manufacturer, batchNo, registerDate, expiryDate, quantity, amount, reminderType, reminderDate, quantityThreshold);
            refreshMedicine();
        }

        private void bttnMedCancel_Click(object sender, RoutedEventArgs e)
        {
            refreshMedicine();
        }

        public void refreshMedicine()
        {
            txtMedName.Text="";
            txtMedDesc.Text="";
            txtMedRate.Text="";
            txtMedManufacturer.Text="";
            txtMedBatchNo.Text="";
            dtMedRegisterDate.SelectedDate=DateTime.Now;
            dtMedExpireDate.SelectedDate=DateTime.Now;
            txtMedQuantity.Text="";
            txtMedAmount.Text="";
            rdAddMedNone.IsChecked = true;
            dtMedReminder.SelectedDate = Convert.ToDateTime("12/31/2020");
            txtMedQuantityThrashhold.Text="0";

        }
        public String getReminderType()
        {
            String remindertype = "";
            if (rdAddMedNone.IsChecked.Equals(true))
            {
                remindertype = rdAddMedNone.Content.ToString();
            }

            else if (rdAddMedDate.IsChecked.Equals(true))
            {
                remindertype = rdAddMedDate.Content.ToString();
            }

            else
            {
                remindertype = rdAddMedQuantity.Content.ToString();
            }

            return remindertype;
        }

        private void rdAddMedDate_Checked(object sender, RoutedEventArgs e)
        {
            dtMedReminder.Visibility = Visibility.Visible;
            txtMedQuantityThrashhold.Visibility = Visibility.Hidden;
        }

        private void rdAddMedDate_Unchecked(object sender, RoutedEventArgs e)
        {
            dtMedReminder.Visibility = Visibility.Hidden;
        }

        private void rdAddMedQuantity_Unchecked(object sender, RoutedEventArgs e)
        {
            txtMedQuantityThrashhold.Visibility = Visibility.Hidden;
        }

        private void rdAddMedQuantity_Checked(object sender, RoutedEventArgs e)
        {
            dtMedReminder.Visibility = Visibility.Hidden;
            txtMedQuantityThrashhold.Visibility = Visibility.Visible;
        }

        

       

    }
}
