using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Shripada.Code
{
    class Services
    {
        public static List<String> getServices()
        {
            List<String> serviceList = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand(" select * from ServiceData order by serviceName", con);

                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        serviceList.Add((String)rdr["serviceName"]);

                    }

                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            return serviceList;

        }

        public static decimal calculateServicePrice(String serviceName, int noOfDays)
        {
            //decimal quant = Convert.ToDecimal(quantity);
            decimal amount = 0;
            decimal totalAmount = 0;
            try
            {

                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("select * from ServiceData where serviceName = @serviceName", con);

                    cmd.Parameters.Add(new SqlParameter("@serviceName", serviceName));


                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        amount = (decimal)rdr["servicePrice"];

                    }
                }
                totalAmount = amount * noOfDays;

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
                    SqlCommand cmd = new SqlCommand("select isnull(MAX(srNo),0) from ServiceOrder", con);

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



        public static void placeServiceOrder(String patientID, String serviceName, int noOfDays, decimal totalAmount)
        {
            int serial = generateSrNo();
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("insert into ServiceOrder values(@srNo, @serviceName, @noOfDays, @totalAmount, @patientID)", con);

                    cmd.Parameters.AddWithValue("@srNo", serial);
                    cmd.Parameters.AddWithValue("@serviceName", serviceName);
                    cmd.Parameters.AddWithValue("@noOfDays", noOfDays);
                    cmd.Parameters.AddWithValue("@totalAmount", totalAmount);
                    cmd.Parameters.AddWithValue("@patientID", patientID);

                    con.Open();

                    int i = cmd.ExecuteNonQuery();

                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Service added successfully");

                    }

                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

        }

        public static DataTable addServiceToGrid(String patientID)
        {
            DataTable dt = new DataTable();
            try
            {

                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("select serviceName as 'Service Name', noOfDays as 'No of Days', totalAmount as 'Cost' from ServiceOrder where patientID = @patientID", con);
                    cmd.Parameters.AddWithValue("@patientID", patientID);
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("ServiceOrder");
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

        public static decimal getCurrentServiceBill(String patientID)
        {
            decimal currentServiceBill = 0;
            try
            {

                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("select serviceBill from visitData where patientID = @patientID and visitStatus = @visitStatus", con);

                    cmd.Parameters.Add(new SqlParameter("@patientID", patientID));

                    cmd.Parameters.Add(new SqlParameter("@visitStatus", "Incomplete"));


                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        currentServiceBill = (decimal)rdr["serviceBill"];
                    }
                }
            }


            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }


            return currentServiceBill;
        }

        public static void addTotalBillToMain(String patientID, decimal totalAmount)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("update visitData set serviceBill = @serviceBill where patientID = @patientID", con);

                    cmd.Parameters.AddWithValue("@serviceBill", totalAmount);
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
