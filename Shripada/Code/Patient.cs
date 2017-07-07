using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Shripada.Code
{
    class Patient
    {
        static int newSrNo;

        public static String generatePatientID()
        {
            String patientID = "";
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand(" select isnull(MAX(srNo),0) from Patients", con);

                    con.Open();

                    String id = cmd.ExecuteScalar().ToString();

                    //String id1 = cmd.ExecuteScalar().ToString();


                    newSrNo = Convert.ToInt32(id) + 1;
                   // System.Windows.Forms.MessageBox.Show(srNo.ToString());
                    DateTime current = DateTime.Now;
                    String year = current.Year.ToString();
                   // System.Windows.Forms.MessageBox.Show(year);
                    patientID = newSrNo.ToString() + '/' + year;
                   // System.Windows.Forms.MessageBox.Show(patientID);

                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

            return patientID;
        }

        public static void addPatient(String patientID, String patientName, String registerDate, String address, String celNo, int age, String sex, String mediclaim, int noOfVisit)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("insert into dbo.Patients (patientID,patientName, address, celnumber, age, sex, mediclaim, dateOfRegister, noOfVisits, isDischarged, srNo) values (@patientID, @patientName, @address, @celNo, @age, @sex, @mediclaim, @registerDate, @noOfVisit, 0, @srNo)", con);
                   
                    cmd.Parameters.AddWithValue("@patientID", patientID);
                    cmd.Parameters.AddWithValue("@patientName", patientName);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@celNo", celNo);
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@sex", sex);
                    cmd.Parameters.AddWithValue("@mediclaim", mediclaim);
                    cmd.Parameters.AddWithValue("@registerDate", registerDate);
                    cmd.Parameters.AddWithValue("@noOfVisit", noOfVisit);
                    cmd.Parameters.AddWithValue("@srNo", newSrNo);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();


                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Patient registered Successfully");

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
