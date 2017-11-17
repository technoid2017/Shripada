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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public List<String> GReminderList = new List<string>();
        public Dashboard(List<String> reminderList)
        {
            InitializeComponent();
            GReminderList = reminderList;
            if (GReminderList.Count != 0)
            {
                System.Windows.Forms.MessageBox.Show("You have " + GReminderList.Count + " new reminders. Check now!");
            }
            labelUserName.Content = Shripada.Code.LoginCode.getLoggedInUser();
            labelCurrentDate.Content = Shripada.Code.LoginCode.getCurrentDate();
        }

        private void bttnIPD_Click(object sender, RoutedEventArgs e)
        {
            Window W = new IPD_Patient();
            W.Show();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {

            Window w = new Reminder(GReminderList);
            w.Show();
        }

        private void bttnSetting_Click(object sender, RoutedEventArgs e)
        {
            Window w = new Setting();
            w.Show();
        }

        private void bttnStock_Click(object sender, RoutedEventArgs e)
        {
            Window w = new StockManagement();
            w.Show();
        }



       


    
    }
}
