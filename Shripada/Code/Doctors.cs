using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Shripada.Code
{
    class Doctors
    {
        public static List<String> getDoctors()
        {
            List<String> doctorList = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand(" select * from DoctorsData order by doctorName", con);

                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        doctorList.Add((String)rdr["doctorName"]);

                    }

                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            return doctorList;

        }

        public static void addDoctor(String name, String specialization)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("insert into DoctorsData values(@doc, @specialization)", con);
                    cmd.Parameters.AddWithValue("@doc", name);
                    cmd.Parameters.AddWithValue("@specialization", specialization);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();


                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Docter added Successfully");

                    }
                }
            }

            catch (Exception cs)
            {
                System.Windows.MessageBox.Show(cs.Message);
            }
        }

        public static List<String> searchDoctor(String doctorName)
        {
            List<String> doctorDetails = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("select * from DoctorsData where doctorName like  '%'+ @doc +  '%'", con);
                    cmd.Parameters.AddWithValue("@doc", doctorName);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        doctorDetails.Add((String)rdr["doctorName"]);
                        doctorDetails.Add((String)rdr["specialization"]);
                    }
                }
            }

            catch (Exception cs)
            {
                System.Windows.MessageBox.Show(cs.Message);
            }
            return doctorDetails;
        }

        public static void updateDoctor(String name, String specialization)
        {
            try
            {
              using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("update DoctorsData set doctorName = @doc, specialization = @specialization where doctorName = @doc", con);
                    cmd.Parameters.AddWithValue("@doc", name);
                    cmd.Parameters.AddWithValue("@specialization", specialization);
                    
                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Details Updated Successfully");

                    }

                }
            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        public static void deleteDoctor(String doctorName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("delete from DoctorsData where doctorName=@doc", con);
                    cmd.Parameters.AddWithValue("@doc", doctorName);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Doctor Deleted Successfully");

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
