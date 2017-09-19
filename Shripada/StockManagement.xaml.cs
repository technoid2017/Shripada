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
            Boolean isDuplicate = Shripada.Code.Stock.isMedicinePresent(medicineName);
            if (isDuplicate)
            {
                System.Windows.Forms.MessageBox.Show("This Medicine is already Registered!");
            }

            else
            {
                Shripada.Code.Stock.addNewMedicine(medicineName, medicineDescription, medicinePrice, manufacturer, batchNo, registerDate, expiryDate, quantity, amount, reminderType, reminderDate, quantityThreshold);
                refreshMedicine();
            }
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
            if (rdAddMedNone.IsChecked.Equals(true) || rdMedRemNone.IsChecked.Equals(true))
            {
                remindertype = rdAddMedNone.Content.ToString();
            }

            else if (rdAddMedDate.IsChecked.Equals(true) || rdMedRemDate.IsChecked.Equals(true))
            {
                remindertype = rdAddMedDate.Content.ToString();
            }

            else
            {
                remindertype = rdAddMedQuantity.Content.ToString();
            }

            return remindertype;
        }

        private void rdAddMedNone_Checked(object sender, RoutedEventArgs e)
        {
            txtMedQuantityThrashhold.Visibility = Visibility.Hidden;
            dtMedReminder.Visibility = Visibility.Hidden;

        }
        private void rdAddMedDate_Checked(object sender, RoutedEventArgs e)
        {
            dtMedReminder.Visibility = Visibility.Visible;
            txtMedQuantityThrashhold.Visibility = Visibility.Hidden;
        }

        private void rdAddMedQuantity_Checked(object sender, RoutedEventArgs e)
        {
            dtMedReminder.Visibility = Visibility.Hidden;
            txtMedQuantityThrashhold.Visibility = Visibility.Visible;
        }
    //Update Stock

        private void bttnMedSearch_Click(object sender, RoutedEventArgs e)
        {
            String searchMedicine = txtMedSearchByName.Text;
            if (Shripada.Code.Stock.isMedicinePresent(searchMedicine))
            {
                List<String> medicineDetails = Shripada.Code.Stock.getMedicineDetails(searchMedicine);

                txtMedEditName.Text = medicineDetails.ElementAt(0).ToString();
                txtMedEditDesc.Text = medicineDetails.ElementAt(1);
                txtMedEditRate.Text = medicineDetails.ElementAt(2);
                txtMedEditManufacturer.Text = medicineDetails.ElementAt(3);
                txtMedEditBatchNo.Text = medicineDetails.ElementAt(4);

                txtMedEditExpireDate.Text = medicineDetails.ElementAt(6);
                txtMedEditQuantity.Text = medicineDetails.ElementAt(7);
                txtMedEditAmount.Text = medicineDetails.ElementAt(8);
                String reminderType = medicineDetails.ElementAt(9);
                if (reminderType.Trim().Equals("None"))
                {
                    rdMedRemNone.IsChecked = true;
                    rdMedRemDate.IsChecked = false;
                    rdMedRemQuantity.IsChecked = false;
                    txtMedEditReminderDate.Visibility = Visibility.Hidden;
                    txtMedEditQauntityThrashhold.Visibility = Visibility.Hidden;
                }

                else if (reminderType.Trim().Equals("Date"))
                {
                    rdMedRemNone.IsChecked = false;
                    rdMedRemDate.IsChecked = true;
                    rdMedRemQuantity.IsChecked = false;
                    txtMedEditReminderDate.Visibility = Visibility.Visible;
                    txtMedEditQauntityThrashhold.Visibility = Visibility.Hidden;
                }

                else
                {
                    rdMedRemNone.IsChecked = false;
                    rdMedRemDate.IsChecked = false;
                    rdMedRemQuantity.IsChecked = true;
                    txtMedEditReminderDate.Visibility = Visibility.Hidden;
                    txtMedEditQauntityThrashhold.Visibility = Visibility.Visible;
                }


                txtMedEditReminderDate.Text = medicineDetails.ElementAt(10);
                txtMedEditQauntityThrashhold.Text = medicineDetails.ElementAt(11);
                txtMedEditLastUpdateDate.Text = medicineDetails.ElementAt(12);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No such Medicine present");
            }
        }

        private void rdMedRemNone_Checked(object sender, RoutedEventArgs e)
        {
            txtMedEditReminderDate.Visibility = Visibility.Hidden;
            txtMedEditQauntityThrashhold.Visibility = Visibility.Hidden;
        }

        private void rdMedRemDate_Checked(object sender, RoutedEventArgs e)
        {
            txtMedEditReminderDate.Visibility = Visibility.Visible;
            txtMedEditQauntityThrashhold.Visibility = Visibility.Hidden;
        }

       
        private void rdMedRemQuantity_Checked(object sender, RoutedEventArgs e)
        {
            txtMedEditReminderDate.Visibility = Visibility.Hidden;
            txtMedEditQauntityThrashhold.Visibility = Visibility.Visible;
        }

       
        private void bttnMedEditSave_Click(object sender, RoutedEventArgs e)
        {
            String medicineName = txtMedEditName.Text;
            String medicineDescription=txtMedEditDesc.Text;
            Decimal medicinePrice = Convert.ToDecimal(txtMedEditRate.Text);
            String manufacturer= txtMedEditManufacturer.Text;
            String batchNo = txtMedEditBatchNo.Text;
            DateTime expiryDate = Convert.ToDateTime(txtMedEditExpireDate.Text);
            Decimal quantity = Convert.ToDecimal(txtMedEditQuantity.Text);
            Decimal amount = Convert.ToDecimal(txtMedEditAmount.Text);
            String reminderType = getReminderType();
            DateTime reminderDate = Convert.ToDateTime(txtMedEditReminderDate.Text);
            Decimal quantityThreshold = Convert.ToDecimal(txtMedQuantityThrashhold.Text);
            DateTime lastStockUpdate = Convert.ToDateTime(dtMedEditCurrentDate.SelectedDate);

            Shripada.Code.Stock.editMedicine(medicineName, medicineDescription, medicinePrice, manufacturer, batchNo, expiryDate, quantity, amount, reminderType, reminderDate, quantityThreshold, lastStockUpdate);
            refreshUpdateStock();
        }

        public void refreshUpdateStock()
        {
            txtMedSearchByName.Text = "";
            txtMedEditName.Text = "";
            txtMedEditDesc.Text = "";
            txtMedEditRate.Text = "";
            txtMedEditManufacturer.Text = "";
            txtMedEditBatchNo.Text = "";
            txtMedEditExpireDate.Text = "";
            txtMedEditQuantity.Text = "";
            txtMedEditAmount.Text = "";
            rdMedRemNone.IsChecked = true;
            txtMedEditReminderDate.Text = "";
            txtMedQuantityThrashhold.Text = "";
            dtMedEditCurrentDate.SelectedDate = DateTime.Now;
        }

        private void bttnMedEditCancel_Click(object sender, RoutedEventArgs e)
        {
            refreshUpdateStock();
        }

        private void bttnMedDelete_Click(object sender, RoutedEventArgs e)
        {
            String medicineName = txtMedEditName.Text;
            Shripada.Code.Stock.deleteMedicine(medicineName);
            refreshUpdateStock();
        }

        
              

    }
}
