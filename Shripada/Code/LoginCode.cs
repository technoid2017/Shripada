using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows;


namespace Shripada.Code
{
    public class LoginCode
    {
        public static String welcome = "Welcome";

        public static String loggedInUser = "";
       
        public static bool login(String userName, String password)
        {
            bool result = false;
            loggedInUser = userName;
            String pass = " ";
            try{
                using (SqlConnection con = new SqlConnection(utils.cons))
                {
                    SqlCommand cmd = new SqlCommand("Select [password] from login where [username] = '" + userName + "'", con);
                    con.Open();
                    pass = cmd.ExecuteScalar().ToString();

                    if (pass == "")
                    {
                        System.Windows.Forms.MessageBox.Show("Please enter correct User Name ");
                    }
                    else if (pass.Trim() == password)
                    {
                        result = true;
                                              
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Enter Correct Password ");
                    }
                }
            }
                catch(Exception e) {
                   System.Windows.Forms.MessageBox.Show("Please enter correct User Name");
                }
            return result;

        }

        public static String getLoggedInUser()
        {
            String user = loggedInUser;
            return user;
        }

        public static String getCurrentDate()
        {
            String currentDate = DateTime.Now.ToString("d/M/yyyy");
            return currentDate;
        }

    }
}
