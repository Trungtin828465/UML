using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SignupDL
    {
        public static string ReceivedData_id { get; set; }


        private static SignupDL Instance;
        public static SignupDL GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new SignupDL();
                }
                return Instance;
            }
        }
        private SignupDL() { }

        private string connectionString = @"Data Source=LAPTOP-BJKTHL97\SQLEXPRESS;Initial Catalog=WatchStoreC#;Integrated Security=True;TrustServerCertificate=True";

        // Kiểm tra đăng nhập
        public bool CheckLogin(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(1) FROM Admin WHERE AdminName = @username AND Password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                conn.Open();
                int result = (int)cmd.ExecuteScalar();
                return result > 0;
            }
        }

        // Lấy quyền của người dùng
        public string GetRole(string username)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Role FROM Admin WHERE AdminName = @username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                conn.Open();
                return (string)cmd.ExecuteScalar();
            }
        }
        // Lấy ten của người dùng
        public string GetName(string name)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Adminname FROM Admin WHERE AdminName = @username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", name);

                conn.Open();
                return (string)cmd.ExecuteScalar();
            }
        }
        // Lấy  của người dùng
        public string GETid(string name)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT AdminID FROM Admin WHERE AdminName = @username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", name);

                conn.Open();

                // ExecuteScalar() trả về giá trị của cột đầu tiên trong dòng đầu tiên, hoặc null nếu không có kết quả
                var result = cmd.ExecuteScalar();

                // Kiểm tra nếu result không phải là null và trả về giá trị AdminID dưới dạng chuỗi
                if (result != DBNull.Value && result != null)
                {
                    return result.ToString();  // Trả về AdminID dưới dạng chuỗi
                }
                else
                {
                    return null;  // Trả về null nếu không tìm thấy AdminID
                }
            }
        }



        // Đổi mật khẩu
        public bool ChangePassword(string username, string newPassword)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Admin SET Password = @newPassword WHERE AdminName = @username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@newPassword", newPassword);
                    cmd.Parameters.AddWithValue("@username", username);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while changing the password in the database.", ex);
            }
        }
        #region Lấy Danh sach admin
        public DataTable GetDanhSachAdmin()
        {
            try
            {
                string sql = @"SELECT TOP (1000) [AdminID]
                              ,[Adminname]
                              ,[Password]
                              ,[ImgAdmin]
                              ,[FullName]
                              ,[Email]
                              ,[Role]
                              ,[CreatedAt]
                              ,[Phone]
                              ,[Gender]
                              ,[check_Remove]
                          FROM [WatchStoreC#].[dbo].[Admin]";

                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                //   MessageBox.Show("Lỗi database: " + ex.Message);
                return null;
            }
        }
        #endregion

    }
}
