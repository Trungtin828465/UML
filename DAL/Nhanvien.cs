using System;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{

    public class Nhanvien
    {
        private static Nhanvien Instance;

        public static Nhanvien GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new Nhanvien();
                }
                return Instance;
            }
        }

        #region Lấy Danh Sách Khách Hàng
        public DataTable GetDanhSachNhanVien()
        {
            try
            {
                string sql = @"
                SELECT TOP (1000) [AdminID]
                      ,[Adminname]
                      ,[Password]
                      ,[ImgAdmin]
                      ,[FullName]
                      ,[Email]
                      ,[Role]
                      ,[CreatedAt]
                      ,[Phone]
                      ,[Gender],[Luong]
                  FROM [Admin] where check_Remove = 1 and Role != N'Quản trị viên' ";

                DataTable dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product list: " + ex.Message);
            }
        }
        #endregion

        #region Tìm Kiếm Nhân Viên
        public DataTable SearchEmployees(string searchText)
        {
            try
            {
                string sql = "SELECT * FROM Admin WHERE check_Remove = 1 and ( Adminname LIKE '%" + searchText + "%')";

                DataTable dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching employees: " + ex.Message);
            }
        }
        #endregion


        #region Lọc nhân viên
        public DataTable FilterEmployeesByRole(string role)
        {
            try
            {
                string sql = "SELECT * FROM Admin WHERE Role = N'" + role + "' and check_Remove = 1";
                DataTable dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error filtering employees by role: " + ex.Message);
            }
        }
        #endregion
        #region Thêm nhân viên
        public int InsertAdmin(string adminName, string adminPassword, string adminFullName,
                             string adminEmail, string adminRole, string adminPhone,
                             string adminGender, string imageFileName)
        {
            string query = "INSERT INTO Admin (Adminname, Password, FullName, Email, Role, Phone, Gender, ImgAdmin, check_Remove) " +
                           "VALUES (@Adminname, @Password, @FullName, @Email, @Role, @Phone, @Gender, @ImgAdmin, 1)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Adminname", adminName),
                new SqlParameter("@Password", adminPassword),
                new SqlParameter("@FullName", adminFullName),
                new SqlParameter("@Email", adminEmail),
                new SqlParameter("@Role", adminRole),
                new SqlParameter("@Phone", adminPhone),
                new SqlParameter("@Gender", adminGender),
                new SqlParameter("@ImgAdmin", imageFileName),
            };
            return DataProvider.ExecuteCommand(query, parameters);
        }
        #endregion
        #region Xóa nhân viên
        public int DeleteAdmin(int adminId)
        {
            try
            {
                string query = "Update Admin set check_Remove = 0  WHERE check_Remove = 1 and AdminID = @AdminID";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@AdminID", adminId),
                };

                return DataProvider.ExecuteCommand(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa nhân viên: " + ex.Message);
            }
        }
        #endregion
        #region update nhân viên
        public int UpdateAdmin(int adminId, string adminName, string adminPassword, string adminFullName,
                                string adminEmail, string adminRole, string adminPhone, string adminGender, string imageFileName)
        {

            string query = "UPDATE Admin SET Adminname = @Adminname, Password = @Password, FullName = @FullName, " +
                         "Email = @Email, Role = @Role, Phone = @Phone, Gender = @Gender, ImgAdmin=@ImgAdmin WHERE AdminId = @AdminId";

            SqlParameter[] parameters =
                            {
         new SqlParameter("@Adminname", adminName),
         new SqlParameter("@Password", adminPassword),
         new SqlParameter("@FullName", adminFullName),
         new SqlParameter("@Email", adminEmail),
         new SqlParameter("@Role", adminRole),
         new SqlParameter("@Phone", adminPhone),
        new SqlParameter("@Gender", adminGender),
        new SqlParameter("@ImgAdmin", imageFileName),
        new SqlParameter("@AdminId", adminId),

            };
            return DataProvider.ExecuteCommand(query, parameters);
        }
        #endregion
    }
}