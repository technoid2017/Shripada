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


        public static void addNewVisit(String patientID, String patientName)
        {

            int serial = generateSrNo();
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("insert into visitData values(@srNo,@patientID,@DOA,'00:00 AM', 'broughtby', 'relation', 'incharge', 0, 'medicalhistory', 'diagnosis', 'pulse', 'bp', 'temp', 'weight', 'custom1', 'custom2', 'wardtype', 'course', 'treatmentgiven', 'treatmentadvanced', 0,0, '2025-12-31', '00:00 AM', 0,0,0)", con);

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

         }

        public static void submitVisitBasicDetails(String patientID, String broughtBy, String relation,String doctor, String medicalHistory,String admissionDate, string admissionTime, decimal deposit)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("update visitData set dateOfAdmission = @dateOfAdmission, timeOfAdmission = @timeOfAdmission, broughtBy = @broughtBy, relation= @relation, inchargeDoctor= @inchargeDoctor, medicalHistory = @medicalHistory, deposit = @deposit where patientId = @patientID", con);

                    cmd.Parameters.AddWithValue("@broughtBy", broughtBy);
                    cmd.Parameters.AddWithValue("@relation", relation);
                    cmd.Parameters.AddWithValue("@inchargeDoctor", doctor);
                    cmd.Parameters.AddWithValue("@medicalHistory", medicalHistory);
                    cmd.Parameters.AddWithValue("@deposit", Convert.ToDecimal(deposit));
                    cmd.Parameters.AddWithValue("@timeOfAdmission", admissionTime);
                    cmd.Parameters.AddWithValue("@dateOfAdmission", Convert.ToDateTime(admissionDate));
                    cmd.Parameters.AddWithValue("@patientID", patientID);

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

    }

   
}
