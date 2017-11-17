using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace Shripada.Code
{
    class Reminder
    {
        public static Hashtable getReminderByQuantity()
        {
            Hashtable reminderByQuantityList = new Hashtable();
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand(" select * from MedicineData where reminderType = 'Quantity'order by medicineName", con);

                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    int i = 1;
                    while (rdr.Read())
                    {                        
                            reminderByQuantityList.Add("MedicineName"+i, (String)rdr["MedicineName"]);
                            reminderByQuantityList.Add("presentQuantity"+i, (decimal)rdr["quantity"]);
                            reminderByQuantityList.Add("threshold"+i, (decimal)rdr["quantityThreshold"]);
                            i++;
                    }

                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }


            return reminderByQuantityList;
        }

        public static String generateReminderByQuantity(String medicineName, int availableQuantity, int thresholdQuantity)
        {
            String Msg1 = "REMINDER! You need to restock Medicine : " + medicineName.Trim() + " as current Quantity is : " + availableQuantity.ToString().Trim() + " and Minimum required quantity is : " + thresholdQuantity.ToString().Trim();
            return Msg1;
        }

        public static Hashtable getReminderByDate()
        {
            Hashtable reminderByDateList = new Hashtable();
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand(" select * from MedicineData where reminderType = 'Date'order by medicineName", con);

                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    int i = 1;
                    while (rdr.Read())
                    {
                        reminderByDateList.Add("MedicineName" + i, (String)rdr["MedicineName"]);
                        reminderByDateList.Add("ReminderDate" + i, (DateTime)rdr["reminderDate"]);
                        
                        i++;
                    }

                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }


            return reminderByDateList;
        }

        public static String generateReminderByDate(String medicineName, DateTime reminderDate)
        {
            String Msg2 = "URGENT REMINDER! You need to restock Medicine : " + medicineName.Trim() + " as it expires on : " + reminderDate.ToString().Trim();
            return Msg2;
        }
    }
}
