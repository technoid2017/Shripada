using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Shripada.Code
{
    class Stock
    {
        public static void addNewMedicine(String medicineName, String medicineDescription, Decimal medicinePrice, String manufacturer, String batchNo, DateTime registerDate, DateTime expiryDate, Decimal quantity, Decimal amount, String reminderType, DateTime reminderDate, Decimal quantityThreshold)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("insert into MedicineData values (@medicineName, @medicineDescription, @medicinePrice, @manufacturer, @batchNo, @registerDate, @expiryDate, @quantity, @amount, @reminderType, @reminderDate, @quantityThreshold, @lastStockUpdate)", con);
                   
                    cmd.Parameters.AddWithValue("@medicineName", medicineName);
                    cmd.Parameters.AddWithValue("@medicineDescription", medicineDescription);
                    cmd.Parameters.AddWithValue("@medicinePrice", medicinePrice);
                    cmd.Parameters.AddWithValue("@manufacturer", manufacturer);
                    cmd.Parameters.AddWithValue("@batchNo", batchNo);
                    cmd.Parameters.AddWithValue("@registerDate", registerDate);
                    cmd.Parameters.AddWithValue("@expiryDate", expiryDate);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@reminderType", reminderType);
                    cmd.Parameters.AddWithValue("@reminderDate", reminderDate);
                    cmd.Parameters.AddWithValue("@quantityThreshold", quantityThreshold);
                    cmd.Parameters.AddWithValue("@lastStockUpdate", registerDate);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();


                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Medicine Added Successfully");

                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            
        }

    }
}
