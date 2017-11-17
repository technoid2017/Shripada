using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;


namespace Shripada.Code
{
    class Visit
    {        

        public static int generateSrNo()
        {
            int srNo = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand(" select isnull(MAX(srNo),0) from visitData", con);

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


        public static int addNewVisit(String patientID, String patientName)
        {

            int serial = generateSrNo();
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("insert into visitData values(@srNo,@patientID,@DOA,'00:00 AM', 'broughtby', 'relation', 'incharge', 0, 'medicalhistory', 'diagnosis', 'pulse', 'bp', 'temp', 'weight', 'custom1', 'custom2', 'wardtype', 'course', 'treatmentgiven', 'treatmentadvanced', 0,0, '2025-12-31', '00:00 AM', 0,0,0, 'Incomplete',0)", con);

                    cmd.Parameters.AddWithValue("@srNo", serial);
                    cmd.Parameters.AddWithValue("@patientID", patientID);
                    cmd.Parameters.AddWithValue("@DOA", DateTime.Now.ToString());

                    con.Open();

                    int i = cmd.ExecuteNonQuery();


                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Visit Successfully for Patient:" +patientName);

                    }
                    
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            return serial;

         }

        public static void submitVisitBasicDetails(int visitID, String broughtBy, String relation, String doctor, String medicalHistory, String admissionDate, string admissionTime, decimal deposit)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("update visitData set dateOfAdmission = @dateOfAdmission, timeOfAdmission = @timeOfAdmission, broughtBy = @broughtBy, relation= @relation, inchargeDoctor= @inchargeDoctor, medicalHistory = @medicalHistory, deposit = @deposit where srNo = @visitID", con);

                    cmd.Parameters.AddWithValue("@broughtBy", broughtBy);
                    cmd.Parameters.AddWithValue("@relation", relation);
                    cmd.Parameters.AddWithValue("@inchargeDoctor", doctor);
                    cmd.Parameters.AddWithValue("@medicalHistory", medicalHistory);
                    cmd.Parameters.AddWithValue("@deposit", Convert.ToDecimal(deposit));
                    cmd.Parameters.AddWithValue("@timeOfAdmission", admissionTime);
                    cmd.Parameters.AddWithValue("@dateOfAdmission", Convert.ToDateTime(admissionDate));
                    cmd.Parameters.AddWithValue("@visitID", visitID);

                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Details saved Successfully");

                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

        }


        public static List<String> getVisitDetails(String patientID, String visitStatus)
        {
            List<String> visitDetails = new List<string>();

            try
            {

                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("select * from visitData where patientID = @patientID and visitStatus = @visitStatus", con);

                    cmd.Parameters.Add(new SqlParameter("@patientID", patientID));

                    cmd.Parameters.Add(new SqlParameter("@visitStatus", visitStatus));
                                      

                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        visitDetails.Add((String)rdr["patientID"]);
                        DateTime DOA = (DateTime)rdr["dateOfAdmission"];
                        visitDetails.Add(DOA.ToString("yyyy-MM-dd"));
                        
                        visitDetails.Add((String)rdr["timeOfAdmission"]);
                        visitDetails.Add((String)rdr["broughtBy"]);
                        visitDetails.Add((String)rdr["relation"]);
                        visitDetails.Add((String)rdr["inchargeDoctor"]);
                        decimal deposit = (decimal)rdr["deposit"];
                        visitDetails.Add(deposit.ToString());
                                               
                        visitDetails.Add((String)rdr["medicalHistory"]);
                        visitDetails.Add((String)rdr["diagnosis"]);
                        visitDetails.Add((String)rdr["pulse"]);
                        visitDetails.Add((String)rdr["bp"]);
                        visitDetails.Add((String)rdr["temperature"]);
                        visitDetails.Add((String)rdr["pWeight"]);
                        visitDetails.Add((String)rdr["examCustom1"]);
                        visitDetails.Add((String)rdr["examCustom2"]);
                        visitDetails.Add((String)rdr["wardType"]);
                        visitDetails.Add((String)rdr["courseInWard"]);
                        visitDetails.Add((String)rdr["treatmentGiven"]);
                        visitDetails.Add((String)rdr["treatmentAdvanced"]);
                        
                        decimal medicineBill = (decimal)rdr["medicineBill"];
                        visitDetails.Add(medicineBill.ToString());
                        decimal serviceBill = (decimal)rdr["serviceBill"];
                        visitDetails.Add(serviceBill.ToString());
                        decimal totalCharges = (decimal)rdr["totalCharges"];
                        visitDetails.Add(totalCharges.ToString());
                        decimal discounts = (decimal)rdr["discounts"];
                        visitDetails.Add(discounts.ToString());
                        decimal payable = (decimal)rdr["payable"];
                        visitDetails.Add(payable.ToString());

                        DateTime DODischarge = (DateTime)rdr["dateOfDischarge"];
                        visitDetails.Add(DODischarge.ToString("yyyy-MM-dd"));

                        visitDetails.Add((String)rdr["timeOfDischarge"]);
                        visitDetails.Add((String)rdr["visitStatus"]);
                        visitDetails.Add(Convert.ToString((int)rdr["srNo"]));
                        visitDetails.Add(Convert.ToString((decimal)rdr["wardCharges"]));
                    }

                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            return visitDetails;
        }


        public static List<String> getVisitDetailsForViewOnly(String patientID, String visitStatus, DateTime admissionDate, DateTime dischargeDate)
        {
            List<String> visitDetails = new List<string>();

            try
            {

                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("select * from visitData where patientID = @patientID and visitStatus = @visitStatus and dateOfAdmission = @admissionDate and dateOfDischarge = @dischargeDate", con);

                    cmd.Parameters.Add(new SqlParameter("@patientID", patientID));

                    cmd.Parameters.Add(new SqlParameter("@visitStatus", visitStatus));
                    cmd.Parameters.Add(new SqlParameter("@admissionDate", admissionDate));
                    cmd.Parameters.Add(new SqlParameter("@dischargeDate", dischargeDate));
                   

                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        visitDetails.Add((String)rdr["patientID"]);
                        DateTime DOA = (DateTime)rdr["dateOfAdmission"];
                        visitDetails.Add(DOA.ToString("yyyy-MM-dd"));
                        
                        visitDetails.Add((String)rdr["timeOfAdmission"]);
                        visitDetails.Add((String)rdr["broughtBy"]);
                        visitDetails.Add((String)rdr["relation"]);
                        visitDetails.Add((String)rdr["inchargeDoctor"]);
                        decimal deposit = (decimal)rdr["deposit"];
                        visitDetails.Add(deposit.ToString());
                                               
                        visitDetails.Add((String)rdr["medicalHistory"]);
                        visitDetails.Add((String)rdr["diagnosis"]);
                        visitDetails.Add((String)rdr["pulse"]);
                        visitDetails.Add((String)rdr["bp"]);
                        visitDetails.Add((String)rdr["temperature"]);
                        visitDetails.Add((String)rdr["pWeight"]);
                        visitDetails.Add((String)rdr["examCustom1"]);
                        visitDetails.Add((String)rdr["examCustom2"]);
                        visitDetails.Add((String)rdr["wardType"]);
                        visitDetails.Add((String)rdr["courseInWard"]);
                        visitDetails.Add((String)rdr["treatmentGiven"]);
                        visitDetails.Add((String)rdr["treatmentAdvanced"]);
                        
                        decimal medicineBill = (decimal)rdr["medicineBill"];
                        visitDetails.Add(medicineBill.ToString());
                        decimal serviceBill = (decimal)rdr["serviceBill"];
                        visitDetails.Add(serviceBill.ToString());
                        decimal totalCharges = (decimal)rdr["totalCharges"];
                        visitDetails.Add(totalCharges.ToString());
                        decimal discounts = (decimal)rdr["discounts"];
                        visitDetails.Add(discounts.ToString());
                        decimal payable = (decimal)rdr["payable"];
                        visitDetails.Add(payable.ToString());

                        DateTime DODischarge = (DateTime)rdr["dateOfDischarge"];
                        visitDetails.Add(DODischarge.ToString("yyyy-MM-dd"));

                        visitDetails.Add((String)rdr["timeOfDischarge"]);
                        visitDetails.Add((String)rdr["visitStatus"]);
                        visitDetails.Add(Convert.ToString((int)rdr["srNo"]));
                        visitDetails.Add(Convert.ToString((decimal)rdr["wardCharges"]));
                    }

                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            return visitDetails;
        }


        public static void submitVisitExamDetails(String pulse, String bp, String temperature, String pWeight, String examCustom1, String examCustom2, int visitID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("update visitData set pulse = @pulse, bp = @bp, temperature = @temperature, pWeight= @pWeight, examCustom1 = @examCustom1, examCustom2= @examCustom2 where srNo = @visitID", con);

                    cmd.Parameters.AddWithValue("@pulse", pulse);
                    cmd.Parameters.AddWithValue("@bp", bp);
                    cmd.Parameters.AddWithValue("@temperature", temperature);
                    cmd.Parameters.AddWithValue("@pWeight", pWeight);

                    cmd.Parameters.AddWithValue("@examCustom1", examCustom1);
                    cmd.Parameters.AddWithValue("@examCustom2", examCustom2);
                    cmd.Parameters.AddWithValue("@visitID", visitID);

                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Examination Details saved Successfully");

                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

        }

        public static void submitVisitTreatmentDetails(String wardType, String treatmentAdvanced, String treatmentGiven, String courseInWard, int visitID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("update visitData set wardType = @wardType, courseInWard = @courseInWard, treatmentGiven = @treatmentGiven, treatmentAdvanced= @treatmentAdvanced where srNo = @visitID", con);

                    cmd.Parameters.AddWithValue("@wardType", wardType);
                    cmd.Parameters.AddWithValue("@treatmentAdvanced", treatmentAdvanced);
                    cmd.Parameters.AddWithValue("@treatmentGiven", treatmentGiven);
                    cmd.Parameters.AddWithValue("@courseInWard", courseInWard);
                    cmd.Parameters.AddWithValue("@visitID", visitID);

                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Examination Details saved Successfully");

                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

        }

        public static void setDischargeDateAndTime(DateTime date, String time, int visitID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("update visitData set dateOfDischarge = @dateOfDischarge, timeOfDischarge = @timeOfDischarge where srNo = @visitID", con);

                    cmd.Parameters.AddWithValue("@dateOfDischarge", date);
                    cmd.Parameters.AddWithValue("@timeOfDischarge", time);
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


        public static void setWardChargesToVisitData(int visitID, decimal wardCharges)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("update visitData set wardCharges = @wardCharges where srNo = @visitID", con);

                    cmd.Parameters.AddWithValue("@wardCharges", wardCharges);
                    cmd.Parameters.AddWithValue("@visitID", visitID);
                    
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

        public static void completeVisit(decimal totalCharges, decimal discounts, decimal payable, int visitID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("update visitData set totalCharges = @totalCharges, discounts = @discounts, payable= @payable, visitStatus = @visitStatus where srNo = @visitID", con);

                    cmd.Parameters.AddWithValue("@totalCharges", Convert.ToDecimal(totalCharges));
                    cmd.Parameters.AddWithValue("@discounts", Convert.ToDecimal(discounts));
                    cmd.Parameters.AddWithValue("@payable", Convert.ToDecimal(payable));
                    cmd.Parameters.AddWithValue("@visitStatus", "Complete");
                    cmd.Parameters.AddWithValue("@visitID", visitID);
                    

                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Visit Completed Successfully");

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
