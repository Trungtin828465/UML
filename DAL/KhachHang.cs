using System;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public class KhachHang
    {
        private static KhachHang Instance;

        public static KhachHang GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new KhachHang();
                }
                return Instance;
            }
        }

        #region Lấy Danh Sách Khách Hàng
        public DataTable GetDanhSachKhachHang()
        {
            try
            {
                string sql = @"
                    SELECT TOP (1000) [CustomerID]
                          ,[FullName]
                          ,[ImgCustomer]
                          ,[Email]
                          ,[CreatedAt]
                          ,[Password]
                          ,[Phone]
                          ,[Gender]
                          ,[Address]
                      FROM [Customer] where check_Remove = 1";



                DataTable dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product list: " + ex.Message);
            }
        }
        #endregion
        #region Tìm Kiếm Khách Hàng
        public DataTable SearchCustomer(string searchText)
        {
            try
            {
                string sql = "SELECT * FROM Customer WHERE check_Remove = 1 and FullName LIKE N'%" + searchText + "%'";

                DataTable dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching customer: " + ex.Message);
            }
        }
        #endregion
        #region Thêm nhân viên
        public int InsertCustomer(string customerName, string customerEmail, string customerPhone,
                             string customerPassword, string customerAddress,
                             string customerGender, string imageFileName)
        {
            string query = "INSERT INTO Customer (FullName, Email, Phone, Password, Address, Gender, ImgCustomer, check_Remove) " +
                          "VALUES (@FullName, @Email, @Phone, @Password, @Address, @Gender, @ImgCustomer, 1)";
            SqlParameter[] parameters =
            {

            new SqlParameter("@FullName", customerName),
            new SqlParameter("@Email", customerEmail),
            new SqlParameter("@Phone", customerPhone),
            new SqlParameter("@Password", customerPassword),
            new SqlParameter("@Address", customerAddress),
            new SqlParameter("@Gender", customerGender),
            new SqlParameter("@ImgCustomer", imageFileName),
        };
            return DataProvider.ExecuteCommand(query, parameters);
        }
        #endregion
        #region Xóa khách hàng
        public int DeleteCustomer(int CustomerID)
        {
            try
            {
                string query = "Update Customer set check_Remove = 0 WHERE check_Remove = 1 and CustomerID = @CustomerID";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@CustomerID", CustomerID),
                };

                return DataProvider.ExecuteCommand(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Không thể xóa, khách hàng có nhiều lịch sử mua hàng");
            }
        }
        #endregion
        #region update nhân viên
        public int UpdateCustomer(int customerid, string customerName, string customerEmail, string customerPhone,
                             string customerPassword, string customerAddress,
                             string customerGender, string imageFileName)
        {

            string query = "UPDATE Customer SET FullName = @FullName, Email=@Email, Phone=@Phone, Password=@Password, Address=@Address, Gender=@Gender, ImgCustomer=@ImgCustomer WHERE CustomerID = @CustomerID ";



            SqlParameter[] parameters =
                            {
             new SqlParameter("@FullName", customerName),
            new SqlParameter("@Email", customerEmail),
            new SqlParameter("@Phone", customerPhone),
            new SqlParameter("@Password", customerPassword),
            new SqlParameter("@Address", customerAddress),
            new SqlParameter("@Gender", customerGender),
            new SqlParameter("@ImgCustomer", imageFileName),
             new SqlParameter("@CustomerID",customerid ),

            };
            return DataProvider.ExecuteCommand(query, parameters);
        }
        #endregion

        public DataTable GetCustomerById(int id)
        {
            string sql = @"SELECT [CustomerID]
                                  ,[FullName]
                                  ,[ImgCustomer]
                                  ,[Email]
                                  ,[CreatedAt]
                                  ,[Password]
                                  ,[Phone]
                                  ,[Gender]
                                  ,[Address]
                           FROM [WatchStoreC#].[dbo].[Customer]
                           Where check_Remove = 1 and CustomerID = " + id;
            DataTable dt = DataProvider.GetTable(sql);
            return dt;
        }
    }
}
