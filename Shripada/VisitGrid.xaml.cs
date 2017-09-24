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
using System.Data;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Windows.Controls;

namespace Shripada
{
    /// <summary>
    /// Interaction logic for VisitGrid.xaml
    /// </summary>
    public partial class VisitGrid : Window
    {
        String GpatientID;
        String visitStatus;
        String GpatientName;
      
        public VisitGrid(String patientID, String patientName)
        {
            InitializeComponent();
            GpatientID = patientID;
            GpatientName = patientName;
            DataTable dt1 = Shripada.Code.Patient.showVisitdataInGrid(GpatientID);
            dataGrid1.ItemsSource = dt1.DefaultView;
        }

        
        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                System.Windows.Controls.DataGrid dg = (System.Windows.Controls.DataGrid)sender;
                DataRowView row_selected = dg.SelectedItem as DataRowView;
                if (row_selected != null)
                {
                    visitStatus = row_selected["visitStatus"].ToString();
                    
                }
               
        }

        private void visitDetails(object sender, RoutedEventArgs e)
        {
            Window w = new IPD_View(GpatientName, GpatientID, visitStatus);
            w.Show();

        }

           
    }
}
