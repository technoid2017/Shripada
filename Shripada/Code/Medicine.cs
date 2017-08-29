using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Shripada.Code
{
    class Medicine
    {

        public static List <String> getMedicines()
        {
            List<String> medicineList = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand(" select * from MedicineData order by medicineName", con);

                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                                               
                        medicineList.Add((String)rdr["medicineName"]);
                        
                    }
                    
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            return medicineList;

            }

        public static decimal calculateMedicinePrice(String medicineName, int quantity)
        {
            //decimal quant = Convert.ToDecimal(quantity);
            decimal amount = 0;
            decimal totalAmount = 0;
            try
            {

                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("select * from MedicineData where medicineName = @medicineName", con);

                    cmd.Parameters.Add(new SqlParameter("@medicineName", medicineName));


                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        amount = (decimal)rdr["medicinePrice"];

                    }
                }
                totalAmount = amount * quantity;

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

            return totalAmount;
        }

        public static int generateSrNo()
        {
            int srNo = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("select isnull(MAX(srNo),0) from MedicineOrder", con);

                    con.Open();

                    String id = cmd.ExecuteScalar().ToString();

                    srNo = Convert.ToInt32(id) + 1;

                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

            return srNo;
        }



        public static void placeMedicineOrder(String patientID, String medicineName, int quantity, decimal totalAmount)
        {
            int serial = generateSrNo();
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("insert into MedicineOrder values(@srNo, @medicineName, @quantity, @totalAmount, @patientID)", con);

                    cmd.Parameters.AddWithValue("@srNo", serial);
                    cmd.Parameters.AddWithValue("@medicineName", medicineName);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters.AddWithValue("@totalAmount", totalAmount);
                    cmd.Parameters.AddWithValue("@patientID", patientID);

                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    
                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Medicine inserted successfully");

                    }

                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

        }

        public static DataTable addMedicineToGrid(String patientID)
        {
            DataTable dt = new DataTable();
            try
            {

                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("select medicineName as 'Medicine Name', quantity as 'Quantity', totalAmount as 'Price' from MedicineOrder where patientID = @patientID", con);
                    cmd.Parameters.AddWithValue("@patientID", patientID);
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("MedicineOrder");
                    sda.Fill(dt);
                    //dataGrid1.ItemsSource = dt.DefaultView;
                }

            }
            catch (Exception cs)
            {
                System.Windows.MessageBox.Show(cs.Message);
            }
            return dt;
        }

        public static decimal getCurrentMedicineBill(String patientID)
        {
            decimal currentMedicineBill = 0;
            try
            {

                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("select medicineBill from visitData where patientID = @patientID and visitStatus = @visitStatus", con);

                    cmd.Parameters.Add(new SqlParameter("@patientID", patientID));

                    cmd.Parameters.Add(new SqlParameter("@visitStatus", "Incomplete"));


                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        currentMedicineBill = (decimal)rdr["medicineBill"];
                    }
                }
            }


            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }


            return currentMedicineBill;
        }

        public static void addTotalBillToMain(String patientID, decimal totalAmount)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("update visitData set medicineBill = @medicineBill where patientID = @patientID", con);

                    cmd.Parameters.AddWithValue("@medicineBill", totalAmount);
                    cmd.Parameters.AddWithValue("@patientID", patientID);

                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        //System.Windows.MessageBox.Show("Details saved Successfully");

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

