using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BestRestaurants
{
    public class Cuisine
    {
        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM tasks;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
