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

    }
}
