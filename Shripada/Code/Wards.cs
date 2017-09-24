using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Shripada.Code
{
    class Wards
    {
        public static List<String> getWards()
        {
            List<String> wardList = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand(" select * from WardData order by wardType", con);

                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        wardList.Add((String)rdr["wardType"]);

                    }

                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            return wardList;

        }

        public static void addWard(String wardType, decimal stayCharges, decimal operationdelivery, decimal anaesthesia, decimal OTCharge, decimal assistantCharge, decimal nursing, decimal consultantCharge, decimal roundCharge, decimal miscellaneousCharge)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("insert into WardData values(@wardType, @stayCharges, @operationdelivery, @anaesthesia, @OTCharge, @assistantCharge, @nursing, @consultantCharge, @roundCharge, @miscellaneousCharge)", con);

                    cmd.Parameters.AddWithValue("@wardType", wardType);
                    cmd.Parameters.AddWithValue("@stayCharges", stayCharges);
                    cmd.Parameters.AddWithValue("@operationdelivery", operationdelivery);
                    cmd.Parameters.AddWithValue("@anaesthesia", anaesthesia);
                    cmd.Parameters.AddWithValue("@OTCharge", OTCharge);
                    cmd.Parameters.AddWithValue("@assistantCharge", assistantCharge);
                    cmd.Parameters.AddWithValue("@nursing", nursing);
                    cmd.Parameters.AddWithValue("@consultantCharge", consultantCharge);
                    cmd.Parameters.AddWithValue("@roundCharge", roundCharge);
                    cmd.Parameters.AddWithValue("@miscellaneousCharge", miscellaneousCharge);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();


                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Ward added Successfully");

                    }
                }
            }

            catch (Exception cs)
            {
                System.Windows.MessageBox.Show(cs.Message);
            }

        }

        public static void updateWard(String wardType, decimal stayCharges, decimal operationdelivery, decimal anaesthesia, decimal OTCharge, decimal assistantCharge, decimal nursing, decimal consultantCharge, decimal roundCharge, decimal miscellaneousCharge)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("update WardData set wardType=@wardType, stayCharges=@stayCharges, operationdelivery=@operationdelivery, anaesthesia=@anaesthesia, OTCharge=@OTCharge, assistantCharge=@assistantCharge, nursing=@nursing, consultantCharge=@consultantCharge, roundCharge=@roundCharge, miscellaneousCharge=@miscellaneousCharge where wardType = @wardType", con);

                    cmd.Parameters.AddWithValue("@wardType", wardType);
                    cmd.Parameters.AddWithValue("@stayCharges", stayCharges);
                    cmd.Parameters.AddWithValue("@operationdelivery", operationdelivery);
                    cmd.Parameters.AddWithValue("@anaesthesia", anaesthesia);
                    cmd.Parameters.AddWithValue("@OTCharge", OTCharge);
                    cmd.Parameters.AddWithValue("@assistantCharge", assistantCharge);
                    cmd.Parameters.AddWithValue("@nursing", nursing);
                    cmd.Parameters.AddWithValue("@consultantCharge", consultantCharge);
                    cmd.Parameters.AddWithValue("@roundCharge", roundCharge);
                    cmd.Parameters.AddWithValue("@miscellaneousCharge", miscellaneousCharge);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();


                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Ward details updated Successfully");

                    }
                }
            }

            catch (Exception cs)
            {
                System.Windows.MessageBox.Show(cs.Message);
            }

        }

        public static List<String> searchWard(String wardType)
        {
            List<String> wardDetails = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("select * from WardData where wardType like  '%'+ @wardType +  '%'", con);
                    cmd.Parameters.AddWithValue("@wardType", wardType);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        wardDetails.Add((String)rdr["wardType"]);
                        wardDetails.Add(Convert.ToString((decimal)rdr["stayCharges"]));
                        wardDetails.Add(Convert.ToString((decimal)rdr["operationdelivery"]));
                        wardDetails.Add(Convert.ToString((decimal)rdr["anaesthesia"]));
                        wardDetails.Add(Convert.ToString((decimal)rdr["OTCharge"]));
                        wardDetails.Add(Convert.ToString((decimal)rdr["assistantCharge"]));
                        wardDetails.Add(Convert.ToString((decimal)rdr["nursing"]));
                        wardDetails.Add(Convert.ToString((decimal)rdr["consultantCharge"]));
                        wardDetails.Add(Convert.ToString((decimal)rdr["roundCharge"]));
                        wardDetails.Add(Convert.ToString((decimal)rdr["miscellaneousCharge"]));

                    }
                }
            }

            catch (Exception cs)
            {
                System.Windows.MessageBox.Show(cs.Message);
            }
            return wardDetails;
        }

        public static void deleteWard(String wardType)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("delete from WardData where wardType=@wardType", con);
                    cmd.Parameters.AddWithValue("@wardType", wardType);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("ward Deleted Successfully");

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
