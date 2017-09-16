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
    /// Interaction logic for ResetPassword.xaml
    /// </summary>
    public partial class ResetPassword : Window
    {
        String user = "";
        public ResetPassword(String userName)
        {
            InitializeComponent();
            user = userName;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            String password1 = passwordBox1.Password;
            String password2 = passwordBox2.Password;
            if (password1.Equals(password2))
            {
                Shripada.Code.Admin.resetPassword(user, password1);
                this.Close();
            }

            else
            {
                System.Windows.MessageBox.Show("Password doesnot match! Please try again...");
            }
        }

        private void button15_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
