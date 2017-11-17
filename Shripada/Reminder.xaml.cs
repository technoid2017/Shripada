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
    /// Interaction logic for Reminder.xaml
    /// </summary>
    public partial class Reminder : Window
    {
        public List<String> GReminderList = new List<string>();
        public Reminder(List<String> reminders)
        {
            InitializeComponent();
            GReminderList = reminders;
            displayReminders();
        }

        public void displayReminders()
        {
            int count = GReminderList.Count;
            //System.Windows.Forms.MessageBox.Show(count.ToString());
            //Label[] labels = new Label[count];

            for (int i = 0; i < count; i++)
            {
                   // System.Windows.Forms.MessageBox.Show(GReminderList[i]);
                        Label dynamicLabel = new Label();
                        dynamicLabel.Name = "Reminder"+i;
                        //dynamicLabel.Width = auto;
                        dynamicLabel.Height = 40;
                        dynamicLabel.Foreground = new SolidColorBrush(Colors.Red);
                        dynamicLabel.Content = i+1 +". "+ GReminderList[i];
                        //dynamicLabel.Background = new SolidColorBrush(Colors.Black);

                        stackPanel1.Children.Add(dynamicLabel);

                //labels[i] = new Label();
                //abels[i].Content = GReminderList[i];
                //this.Controls.Add(labels[i]);
                //Reminder.Controls.Add(new Label { Text = "I am lable", Height = 20, Width = 100, Name = "lable" + i, BackColor = Color.White, Location = new Point(x, y) });

            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
