using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Shripada.Code
{
    class Admin
    {

        public static void addNewUser(String userName, String password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("insert into Login values(@username, @password)", con);
                    cmd.Parameters.AddWithValue("@username", userName);
                    cmd.Parameters.AddWithValue("@password", password);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("New User Added Successfully");

                    }
                }
            }
            catch (Exception cs)
            {
                System.Windows.MessageBox.Show(cs.Message);
            }
        }

        public static void resetPassword(String userName, String password)
        {
            try
            {


                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("update Login set password = @password where username = @username", con);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@username", userName);


                    con.Open();

                    int i = cmd.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        System.Windows.MessageBox.Show("Password changed Successfully");

                    }

                }
            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

        }


    }
}
