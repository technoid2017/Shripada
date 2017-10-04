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

        //Methods for Calculating ward charges

        //public static void submitWardDetails(int visitID, String patientID, String wardType, int hospitalStay, int operationalDelivery, int aneasthesia, int OTCharge, int assistantCharge, int nursing, int padiatricianCharge, int roundCharge, int miscellaneousCharge, DateTime admissionDate, DateTime dischargeDate,  int noOfdays)
        public static void addWardDetails(int visitID, String patientID)    
            {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("insert into WardOrder values(@visitID, @patientID, @wardType, @hospitalStay, @operationalDelivery, @aneasthesia, @OTCharge, @assistantCharge, @nursing, @padiatricianCharge, @roundCharge, @miscellaneousCharge, @admissionDate, @dischargeDate, @noOfdays, @stayAmount, @operationAmount, @aneasthesiaAmount, @oTChargeAmount, @assistantAmount, @nursingAmount, @paediatricianAmount, @roundAmount, @miscellaneousAmount, @totalWardAmount)", con);
                  
                    
                    cmd.Parameters.AddWithValue("@visitID", visitID);    
                    cmd.Parameters.AddWithValue("@patientID", patientID);
                    cmd.Parameters.AddWithValue("@wardType", "Default");
                    cmd.Parameters.AddWithValue("@hospitalStay", 0);
                    cmd.Parameters.AddWithValue("@operationalDelivery", 0);
                    cmd.Parameters.AddWithValue("@aneasthesia", 0);
                    cmd.Parameters.AddWithValue("@OTCharge", 0);
                    cmd.Parameters.AddWithValue("@assistantCharge", 0);
                    cmd.Parameters.AddWithValue("@nursing", 0);
                    cmd.Parameters.AddWithValue("@padiatricianCharge", 0);
                    cmd.Parameters.AddWithValue("@roundCharge", 0);
                    cmd.Parameters.AddWithValue("@miscellaneousCharge", 0);
                    cmd.Parameters.AddWithValue("@admissionDate", "1/1/2010");
                    cmd.Parameters.AddWithValue("@dischargeDate", "1/12/2025");
                    cmd.Parameters.AddWithValue("@noOfdays", 0);
                    cmd.Parameters.AddWithValue("@stayAmount", 0);
                    cmd.Parameters.AddWithValue("@operationAmount", 0);
                    cmd.Parameters.AddWithValue("@aneasthesiaAmount", 0);
                    cmd.Parameters.AddWithValue("@oTChargeAmount", 0);
                    cmd.Parameters.AddWithValue("@assistantAmount", 0);
                    cmd.Parameters.AddWithValue("@nursingAmount", 0);
                    cmd.Parameters.AddWithValue("@paediatricianAmount", 0);
                    cmd.Parameters.AddWithValue("@roundAmount", 0);
                    cmd.Parameters.AddWithValue("@miscellaneousAmount", 0);
                    cmd.Parameters.AddWithValue("@totalWardAmount", 0);
                              
                    
                    con.Open();
                    int i = cmd.ExecuteNonQuery();


                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Ward Details added Successfully");

                    }
                }
            }

            catch (Exception cs)
            {
                System.Windows.MessageBox.Show(cs.Message);
            }

        }

        public static void updateWardData(int visitID, String patientID, String wardType, int hospitalStay, int operationalDelivery, int aneasthesia, int OTCharge, int assistantCharge, int nursing, int padiatricianCharge, int roundCharge, int miscellaneousCharge, DateTime admissionDate, DateTime dischargeDate, int noOfdays)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("update WardOrder set visitID =  @visitID, patientID =  @patientID, wardType=@wardType, hospitalStay=@hospitalStay, operationalDelivery=@operationalDelivery, aneasthesia=@aneasthesia, OTCharge=@OTCharge, assistantCharge=@assistantCharge, nursing=@nursing, padiatricianCharge=@padiatricianCharge, roundCharge=@roundCharge, miscellaneousCharge=@miscellaneousCharge, admissionDate = @admissionDate, dischargeDate = @dischargeDate, noOfdays = @noOfdays where visitID = @visitID", con);


                    cmd.Parameters.AddWithValue("@visitID", visitID);
                    cmd.Parameters.AddWithValue("@patientID", patientID);
                    cmd.Parameters.AddWithValue("@wardType", wardType);
                    cmd.Parameters.AddWithValue("@hospitalStay", hospitalStay);
                    cmd.Parameters.AddWithValue("@operationalDelivery", operationalDelivery);
                    cmd.Parameters.AddWithValue("@aneasthesia", aneasthesia);
                    cmd.Parameters.AddWithValue("@OTCharge", OTCharge);
                    cmd.Parameters.AddWithValue("@assistantCharge", assistantCharge);
                    cmd.Parameters.AddWithValue("@nursing", nursing);
                    cmd.Parameters.AddWithValue("@padiatricianCharge", padiatricianCharge);
                    cmd.Parameters.AddWithValue("@roundCharge", roundCharge);
                    cmd.Parameters.AddWithValue("@miscellaneousCharge", miscellaneousCharge);
                    cmd.Parameters.AddWithValue("@admissionDate", admissionDate);
                    cmd.Parameters.AddWithValue("@dischargeDate", dischargeDate);
                    cmd.Parameters.AddWithValue("@noOfdays", noOfdays);

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

        public static void setDischargeDate(DateTime date, int visitID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("update WardOrder set dischargeDate = @dischargeDate where visitID = @visitID", con);

                    cmd.Parameters.AddWithValue("@dischargeDate", date);
                    cmd.Parameters.AddWithValue("@visitID", visitID);

                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        //System.Windows.MessageBox.Show("Examination Details saved Successfully");

                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
        }

        public static List<int> getWardCharges(int visitID)
        {
            List<int> wardServices = new List<int>();

            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("select * from WardOrder where visitID = @visitID", con);

                    cmd.Parameters.AddWithValue("@visitID", visitID);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        wardServices.Add((int)rdr["hospitalStay"]);
                        wardServices.Add((int)rdr["operationalDelivery"]);
                        wardServices.Add((int)rdr["aneasthesia"]);
                        wardServices.Add((int)rdr["OTCharge"]);
                        wardServices.Add((int)rdr["assistantCharge"]);
                        wardServices.Add((int)rdr["nursing"]);
                        wardServices.Add((int)rdr["padiatricianCharge"]);
                        wardServices.Add((int)rdr["roundCharge"]);
                        wardServices.Add((int)rdr["miscellaneousCharge"]);
                      
                    }

                }

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            return wardServices;
        }


        public static String getWardTypeOfVisit(int visitID)
        {
            String wardType = "";

            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("select * from WardOrder where visitID = @visitID", con);

                    cmd.Parameters.AddWithValue("@visitID", visitID);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        wardType = (String)rdr["wardType"];
                        
                    }

                }

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
         

            return wardType;
        }

        public static List<decimal> getWardTypeWiseRates(String wardType)
        {
            List<decimal> wardTypeRates = new List<decimal>();

               try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("select * from WardData where wardType = @wardType", con);

                    cmd.Parameters.AddWithValue("@wardType", wardType);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        wardTypeRates.Add((decimal)rdr["stayCharges"]);
                        wardTypeRates.Add((decimal)rdr["operationdelivery"]);
                        wardTypeRates.Add((decimal)rdr["anaesthesia"]);
                        wardTypeRates.Add((decimal)rdr["OTCharge"]);
                        wardTypeRates.Add((decimal)rdr["assistantCharge"]);
                        wardTypeRates.Add((decimal)rdr["nursing"]);
                        wardTypeRates.Add((decimal)rdr["consultantCharge"]);
                        wardTypeRates.Add((decimal)rdr["roundCharge"]);
                        wardTypeRates.Add((decimal)rdr["miscellaneousCharge"]);
                    }

                }

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
         
            return wardTypeRates;
        }


        public static void setWardChargesForVisit(int noOfdays, decimal stayAmount,decimal operationAmount, decimal aneasthesiaAmount, decimal oTChargeAmount, decimal assistantAmount, decimal nursingAmount, decimal paediatricianAmount, decimal roundAmount, decimal miscellaneousAmount, decimal totalWardAmount, int visitID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("update WardOrder set noOfdays =  @noOfdays, stayAmount =  @stayAmount, operationAmount=@operationAmount, aneasthesiaAmount=@aneasthesiaAmount, oTChargeAmount=@oTChargeAmount, assistantAmount=@assistantAmount, nursingAmount=@nursingAmount, paediatricianAmount=@paediatricianAmount, roundAmount=@roundAmount, miscellaneousAmount=@miscellaneousAmount, totalWardAmount=@totalWardAmount where visitID = @visitID", con);

                    cmd.Parameters.AddWithValue("@noOfdays", noOfdays);
                    cmd.Parameters.AddWithValue("@stayAmount", stayAmount);
                    cmd.Parameters.AddWithValue("@operationAmount", operationAmount);
                    cmd.Parameters.AddWithValue("@aneasthesiaAmount", aneasthesiaAmount);
                    cmd.Parameters.AddWithValue("@oTChargeAmount", oTChargeAmount);
                    cmd.Parameters.AddWithValue("@assistantAmount", assistantAmount);
                    cmd.Parameters.AddWithValue("@nursingAmount", nursingAmount);
                    cmd.Parameters.AddWithValue("@paediatricianAmount", paediatricianAmount);
                    cmd.Parameters.AddWithValue("@roundAmount", roundAmount);
                    cmd.Parameters.AddWithValue("@miscellaneousAmount", miscellaneousAmount);
                    cmd.Parameters.AddWithValue("@totalWardAmount", totalWardAmount);
                    cmd.Parameters.AddWithValue("@visitID", visitID);
                    
                    con.Open();
                    int i = cmd.ExecuteNonQuery();


                    if (i >= 1)
                    {
                        //System.Windows.MessageBox.Show("Ward details updated Successfully");

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
