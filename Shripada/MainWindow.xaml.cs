﻿using System;
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


namespace Shripada
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
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
                this.Close();
            }
            

        }

        private void bttnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
