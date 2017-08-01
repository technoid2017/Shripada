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

                    newSrNo = Convert.ToInt32(id) + 1;
                    DateTime current = DateTime.Now;
                    String year = current.Year.ToString();
                    patientID = newSrNo.ToString() + '/' + year;
                   
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
                    SqlCommand cmd = new SqlCommand("insert into dbo.Patients (patientID,patientName, address, celnumber, age, sex, mediclaim, dateOfRegister, noOfVisits, currentStatus, srNo) values (@patientID, @patientName, @address, @celNo, @age, @sex, @mediclaim, @registerDate, @noOfVisit, @currentStatus, @srNo)", con);
                   
                    cmd.Parameters.AddWithValue("@patientID", patientID);
                    cmd.Parameters.AddWithValue("@patientName", patientName);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@celNo", celNo);
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@sex", sex);
                    cmd.Parameters.AddWithValue("@mediclaim", mediclaim);
                    cmd.Parameters.AddWithValue("@registerDate", registerDate);
                    cmd.Parameters.AddWithValue("@noOfVisit", noOfVisit);
                    cmd.Parameters.AddWithValue("@currentStatus", "Registered");
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

        public static void addVisitNumber(String patientID)
        {
            int visitNumber = 0;
            System.Windows.MessageBox.Show(patientID);
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("select noOfVisits from Patients where patientID = @patientID", con);
                    cmd.Parameters.AddWithValue("@patientID", patientID);

                    con.Open();

                    String oldValue = cmd.ExecuteScalar().ToString();

                    visitNumber = Convert.ToInt32(oldValue) + 1;
                    //System.Windows.MessageBox.Show(visitNumber.ToString());
                    con.Close();

                    SqlCommand cmd2 = new SqlCommand("update Patients set noOfVisits = @visitNumber, currentStatus = @currentStatus where patientID = @patientID", con);
                    
                    cmd2.Parameters.AddWithValue("@visitNumber", visitNumber);
                    cmd2.Parameters.AddWithValue("@patientID", patientID.Trim());
                    cmd2.Parameters.AddWithValue("@currentStatus", "Admitted");

                    con.Open();

                    int i = cmd2.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("New Visit Added");

                    }


                }

               
            }

            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

        }

        public static DataTable searchPatient(String searchType, String searchValue)
        {
            DataTable dt = new DataTable("Patients");

            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("selectAllProcedure", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@tableName", "Patients"));
                    if(searchType.Equals("byID"))
                    {
                    cmd.Parameters.Add(new SqlParameter("@columnName", "patientID"));
                    cmd.Parameters.Add(new SqlParameter("@columnValue", searchValue));
                    }

                    else if(searchType.Equals("byName"))
                    {                        
                    cmd.Parameters.Add(new SqlParameter("@columnName", "patientName"));
                    cmd.Parameters.Add(new SqlParameter("@columnValue", searchValue));

                    }

                    con.Open();
            
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    //dt = new DataTable("Patients");
                    sda.Fill(dt);
                    //dataGridSearchPatient.ItemsSource = dt.DefaultView;

                }

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            return dt;
        }

        public static List<String> getPatientDetails(String searchValue)
        {
            List<String> patientDetails = new List<string>();

            try
            {

                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("selectAllProcedure", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@tableName", "Patients"));
                    
                        cmd.Parameters.Add(new SqlParameter("@columnName", "patientID"));
                        cmd.Parameters.Add(new SqlParameter("@columnValue", searchValue));
                   

                    
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        patientDetails.Add((String)rdr["patientID"]);
                        patientDetails.Add((String)rdr["patientName"]);
                        patientDetails.Add((String)rdr["address"]);
                        patientDetails.Add((String)rdr["celnumber"]);
                        int age = (int)rdr["age"];
                        patientDetails.Add(age.ToString());
                        patientDetails.Add((String)rdr["sex"]);
                        patientDetails.Add((String)rdr["mediclaim"]);
                        DateTime DOR = (DateTime)rdr["dateOfRegister"];
                        patientDetails.Add(DOR.ToString("dd-MM-yyyy"));
                        int visits = (int)rdr["noOfVisits"];
                        patientDetails.Add(visits.ToString());
                        patientDetails.Add((String)rdr["currentStatus"]);

                    }

                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            return patientDetails;
        }

       
    }
}
