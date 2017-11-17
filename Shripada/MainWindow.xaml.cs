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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;



namespace Shripada
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        List<String> reminderList = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void bttnLogin_Click(object sender, RoutedEventArgs e)
        {
            String userName = txtUserName.Text;
            String password = passwordBox1.Password;
            if (Shripada.Code.LoginCode.login(userName, password))
            {
                generateReminderByQuantity();
                generateReminderByDate();
                //System.Windows.Forms.MessageBox.Show(reminderList.Count.ToString());
                Window w = new Dashboard(reminderList);
                w.Show();
                this.Close();

            }
            

        }

        public void generateReminderByQuantity()
        {
            Hashtable ht = Shripada.Code.Reminder.getReminderByQuantity();
            int noOfRows = ht.Count / 3;

            for (int i = 1; i <= noOfRows; i++)
            {
                
                int presentStock = Convert.ToInt32(ht["presentQuantity" + i]);
                int thresholdStock = Convert.ToInt32(ht["threshold" + i]);
                //System.Windows.Forms.MessageBox.Show(ht["medicineName" + i].ToString());
                String medicineName = Convert.ToString(ht["MedicineName" + i]);
                if (presentStock < thresholdStock)
                {
                    reminderList.Add(Shripada.Code.Reminder.generateReminderByQuantity(medicineName, presentStock, thresholdStock));
                }
            }

        }

        public void generateReminderByDate()
        {
            Hashtable ht = Shripada.Code.Reminder.getReminderByDate();
            int noOfRows = ht.Count / 2;

            for (int i = 1; i <= noOfRows; i++)
            {
                String medicineName = Convert.ToString(ht["MedicineName" + i]);
                DateTime currentDate = DateTime.Now;
                DateTime reminderDate = Convert.ToDateTime(ht["ReminderDate" + i]);
                if (currentDate >= reminderDate)
                {
                    reminderList.Add(Shripada.Code.Reminder.generateReminderByDate(medicineName, reminderDate));
                }
            }
        }

        private void bttnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
