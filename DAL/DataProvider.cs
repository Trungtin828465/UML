using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows.Forms;

namespace DAL
{
    public class DataProvider
    {

        public static SqlConnection Openconnect()
        {
            string connectionString = @"Data Source=LAPTOP-BJKTHL97\SQLEXPRESS;Initial Catalog=WatchStoreC#;Integrated Security=True;TrustServerCertificate=True";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
        public static void Disconnect(SqlConnection con)
        {
            con.Close();
        }

        public static string getConnectionString()
        {
            return @"Data Source=LAPTOP-BJKTHL97\SQLEXPRESS;Initial Catalog=WatchStoreC#;Integrated Security=True;TrustServerCertificate=True";
        }

        public static int JustExcuteNoParameter(string sql)
        {
            SqlConnection con = Openconnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = con;
            int rows = cmd.ExecuteNonQuery();
            Disconnect(con);
            if (rows > 0)
            {
                return rows;
            }
            else
            {
                return -1;
            }
        }
        // này
        public static int JustExcuteWithParameter(string sql, params SqlParameter[] parameters)
        {
            string connectionString = @"Data Source=LAPTOP-BJKTHL97\SQLEXPRESS;Initial Catalog=WatchStoreC#;Integrated Security=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddRange(parameters);
                        connection.Open();
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error: {ex.Message}");
                return -1; // Trả về -1 để biểu thị lỗi
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return -1; // Trả về -1 để biểu thị lỗi
            }
        }


        // gióng
        public static DataTable GetTable(string sql)
        {
            SqlConnection con = Openconnect();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Disconnect(con);
            return dt;
        }


        // này
        public static DataTable GetTableWithParameters(string sql, SqlParameter[] parameters)
        {
            string connectionString = @"Data Source=LAPTOP-BJKTHL97\SQLEXPRESS;Initial Catalog=WatchStoreC#;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        //nay

        public static DataTable GetTable1(string sql, SqlParameter[] parameters = null)
        {
            SqlConnection con = null;
            try
            {
                con = Openconnect();
                SqlCommand cmd = new SqlCommand(sql, con);

                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thực thi GetTable: {ex.Message}");
                return null;
            }
            finally
            {
                if (con != null)
                {
                    Disconnect(con);
                }
            }
        }
        public static object ExecuteScalar(string sql, SqlParameter[] parameters)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentException("Câu lệnh SQL không được để trống.", nameof(sql));
            }

            string connectionString = @"Data Source=LAPTOP-BJKTHL97\SQLEXPRESS;Initial Catalog=WatchStoreC#;Integrated Security=True;TrustServerCertificate=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (parameters != null && parameters.Length > 0)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc xử lý tùy thuộc vào yêu cầu
                Console.WriteLine("Lỗi khi thực thi câu lệnh SQL: " + ex.Message);

                // Tùy chọn: Ném lại lỗi nếu cần thiết
                throw new InvalidOperationException("Không thể thực thi câu lệnh SQL.", ex);
            }
        }

        public static int ExecuteCommand(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = Openconnect())
            {
                //conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

    }
}