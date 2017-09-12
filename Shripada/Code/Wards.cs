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
    }
}
