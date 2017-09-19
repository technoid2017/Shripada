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

        public static Boolean isMedicinePresent(String newMedicineName)
        {
            Boolean isDuplicate = false;

            List<String> medicines = Shripada.Code.Medicine.getMedicines();
            foreach (String s in medicines)
            {
                if (newMedicineName.Equals(s.Trim()))
                {
                    isDuplicate = true;
                }
                                    
            }
            return isDuplicate;
        }


        public static List<String> getMedicineDetails(String searchValue)
        {
            List<String> medicineDetails = new List<string>();

            try
            {

                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("selectAllProcedure", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@tableName", "MedicineData"));

                    cmd.Parameters.Add(new SqlParameter("@columnName", "medicineName"));
                    cmd.Parameters.Add(new SqlParameter("@columnValue", searchValue));



                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        medicineDetails.Add((String)rdr["medicineName"]);
                        medicineDetails.Add((String)rdr["medicineDescription"]);
                        medicineDetails.Add(Convert.ToString((decimal)rdr["medicinePrice"]));
                        medicineDetails.Add((String)rdr["manufacturer"]);
                        medicineDetails.Add((String)rdr["batchNo"]);
                        DateTime DOR = (DateTime)rdr["registerDate"];
                        medicineDetails.Add(DOR.ToString("yyyy-MM-dd"));
                        DateTime DOE = (DateTime)rdr["expiryDate"];
                        medicineDetails.Add(DOE.ToString("yyyy-MM-dd"));

                        medicineDetails.Add(Convert.ToString((decimal)rdr["quantity"]));
                        medicineDetails.Add(Convert.ToString((decimal)rdr["amount"]));
                        
                        medicineDetails.Add((String)rdr["reminderType"]);
                        DateTime ReminderDate = (DateTime)rdr["reminderDate"];
                        medicineDetails.Add(ReminderDate.ToString("yyyy-MM-dd"));
                        medicineDetails.Add(Convert.ToString((decimal)rdr["quantityThreshold"]));
                        DateTime lastUpdateDate = (DateTime)rdr["lastStockUpdate"];
                        medicineDetails.Add(lastUpdateDate.ToString("yyyy-MM-dd"));

                    }

                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            return medicineDetails;
        }


        public static void editMedicine(String medicineName, String medicineDescription, Decimal medicinePrice, String manufacturer, String batchNo, DateTime expiryDate, Decimal quantity, Decimal amount, String reminderType, DateTime reminderDate, Decimal quantityThreshold, DateTime lastStockUpdate)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("update MedicineData set medicineDescription = @medicineDescription, medicinePrice=@medicinePrice, manufacturer=@manufacturer, batchNo=@batchNo, expiryDate=@expiryDate, quantity=@quantity, amount=@amount, reminderType=@reminderType, reminderDate=@reminderDate, quantityThreshold=@quantityThreshold, lastStockUpdate=@lastStockUpdate where medicineName = @medicineName", con);

                    cmd.Parameters.AddWithValue("@medicineName", medicineName);
                    cmd.Parameters.AddWithValue("@medicineDescription", medicineDescription);
                    cmd.Parameters.AddWithValue("@medicinePrice", medicinePrice);
                    cmd.Parameters.AddWithValue("@manufacturer", manufacturer);
                    cmd.Parameters.AddWithValue("@batchNo", batchNo);
                    cmd.Parameters.AddWithValue("@expiryDate", expiryDate);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@reminderType", reminderType);
                    cmd.Parameters.AddWithValue("@reminderDate", reminderDate);
                    cmd.Parameters.AddWithValue("@quantityThreshold", quantityThreshold);
                    cmd.Parameters.AddWithValue("@lastStockUpdate", lastStockUpdate);

                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Stock updated Successfully");

                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
        }

        public static void deleteMedicine(String medicineName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("delete from MedicineData where medicineName=@medicineName", con);
                    cmd.Parameters.AddWithValue("@medicineName", medicineName);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Medicine Deleted Successfully");

                    }
                }
            }

            catch (Exception cs)
            {
                System.Windows.MessageBox.Show(cs.Message);
            }
        }
    }
}
