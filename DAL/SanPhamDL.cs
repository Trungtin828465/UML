
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{

    public class SanPhamDL
    {
        private static SanPhamDL Instance;
        public static SanPhamDL GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new SanPhamDL();
                }
                return Instance;
            }
        }

        #region Lấy Danh Sách Sản Phẩm
        public DataTable GetDanhSachSanPham()
        {
            try
            {
                string sql = @"
                SELECT
                  [ProductID], [ProductName], [BrandID], [Price], 
                  [Description], [StockQuantity], [CategoryID], 
                  [ImageUrl], [SupplierID], [CreatedAt], [CreatedBy], 
                  [Discount], [Check_Remove], [AverageRating], 
                  [ImportPrice], [Profit], [ProfitMargin]              
                FROM [WatchStoreC#].[dbo].[Product]
                WHERE Check_Remove = 1";

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

        #region Ngừng Kinh Doanh Sản Phẩm
        public bool NgungKinhDoanhSanPham(string MASP)
        {
            try
            {
                string sql = " UPDATE Product SET Check_Remove = 0 WHERE [ProductID] ='" + MASP + "'";
                
                int rows = DataProvider.JustExcuteNoParameter(sql);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Lỗi database: " + ex.Message);
               Console.WriteLine("Lỗi database: " + ex.Message);
                return false;
            }
        }
        #endregion

        #region Sửa Sản Phẩm
        public bool SuaSanPham(SanPhamDTO spDTO)
        {
            try
            {
                string sql = @"
            UPDATE [WatchStoreC#].[dbo].[Product]
            SET 
                [ProductName] = @ProductName,
                [BrandID] = @BrandID,
                [Price] = @Price,
                [StockQuantity] = @StockQuantity,
                [CategoryID] = @CategoryID,
                [ImageUrl] = @ImageUrl,
                [SupplierID] = @SupplierID,
                [CreatedAt] = @CreatedAt,
                [Discount] = @Discount,
                [AverageRating] = @AverageRating,
                [ImportPrice] = @ImportPrice,
                [ProfitMargin] = @ProfitMargin
            WHERE [ProductID] = @ProductID";

                using (SqlConnection con = DataProvider.Openconnect())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Kiểm tra và xử lý giá trị NULL
                        cmd.Parameters.AddWithValue("@ProductName", string.IsNullOrEmpty(spDTO.ProductName) ? (object)DBNull.Value : spDTO.ProductName);
                        cmd.Parameters.AddWithValue("@BrandID", spDTO.BrandID);
                        cmd.Parameters.AddWithValue("@Price", spDTO.Price);
                        cmd.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(spDTO.Description) ? (object)DBNull.Value : spDTO.Description);
                        cmd.Parameters.AddWithValue("@StockQuantity", spDTO.StockQuantity);
                        cmd.Parameters.AddWithValue("@CategoryID", spDTO.CategoryID);
                        cmd.Parameters.AddWithValue("@ImageUrl", string.IsNullOrEmpty(spDTO.ImageUrl) ? (object)DBNull.Value : spDTO.ImageUrl);
                        cmd.Parameters.AddWithValue("@SupplierID", spDTO.SupplierID);
                        cmd.Parameters.AddWithValue("@CreatedAt", spDTO.CreatedAt == DateTime.MinValue ? (object)DBNull.Value : spDTO.CreatedAt);
                        cmd.Parameters.AddWithValue("@Discount", spDTO.Discount);
                        cmd.Parameters.AddWithValue("@AverageRating", spDTO.AverageRating);
                        cmd.Parameters.AddWithValue("@ImportPrice", spDTO.ImportPrice);
                        cmd.Parameters.AddWithValue("@Profit", spDTO.Profit);
                        cmd.Parameters.AddWithValue("@ProfitMargin", spDTO.ProfitMargin);
                        cmd.Parameters.AddWithValue("@ProductID", spDTO.ProductID);

                        // Thực thi câu lệnh SQL
                        int rows = cmd.ExecuteNonQuery();

                        // Kiểm tra số dòng bị ảnh hưởng (cập nhật thành công)
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log lỗi chi tiết để gỡ lỗi
                Console.WriteLine("Lỗi khi sửa sản phẩm: " + ex.Message);
                return false;
            }
        }
        #endregion

        #region Thêm Sản Phẩm
        public bool ThemSanPham(SanPhamDTO spDTO)
        {
            try
            {


                string sql = @"
        INSERT INTO Product (
            ProductName, BrandID, Price, Description, StockQuantity, 
            CategoryID, ImageUrl, SupplierID, CreatedAt, Discount, 
            Check_Remove, AverageRating, ImportPrice, Profit, ProfitMargin
        ) 
        VALUES (
            @ProductName, @BrandID, @Price, @Description, @StockQuantity, 
            @CategoryID, @ImageUrl, @SupplierID, @CreatedAt, @Discount, 
            @Check_Remove, @AverageRating, @ImportPrice, @Profit, @ProfitMargin
        )";


                SqlConnection con = DataProvider.Openconnect();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;


                cmd.Parameters.AddWithValue("@ProductName", spDTO.ProductName);
                cmd.Parameters.AddWithValue("@BrandID", spDTO.BrandID);
                cmd.Parameters.AddWithValue("@Price", spDTO.Price);
                cmd.Parameters.AddWithValue("@Description", "Đẹp");
                cmd.Parameters.AddWithValue("@StockQuantity", spDTO.StockQuantity);
                cmd.Parameters.AddWithValue("@CategoryID", spDTO.CategoryID);
                cmd.Parameters.AddWithValue("@ImageUrl", spDTO.ImageUrl);
                cmd.Parameters.AddWithValue("@SupplierID", spDTO.SupplierID);
                cmd.Parameters.AddWithValue("@CreatedAt", spDTO.CreatedAt);
                cmd.Parameters.AddWithValue("@Discount", spDTO.Discount);
                cmd.Parameters.AddWithValue("@Check_Remove", 1);
                cmd.Parameters.AddWithValue("@AverageRating", 4.5);
                cmd.Parameters.AddWithValue("@ImportPrice", spDTO.ImportPrice);
                cmd.Parameters.AddWithValue("@Profit", 0);
                cmd.Parameters.AddWithValue("@ProfitMargin", spDTO.ProfitMargin);

               





                cmd.Connection = con;
                int rows = cmd.ExecuteNonQuery();
                DataProvider.Disconnect(con);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi database: " + ex.Message);
                return false;
            }
        }
        #endregion

        #region Lấy Danh Sách Sản Phẩm Theo Bộ Lọc
        public DataTable GetDanhSachSanPhamTheoBoLoc(string TENSP, string MALOAISP, string MANCC)
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "";
                if (TENSP != "")
                {
                    if (MALOAISP == "-1" && MANCC == "-1")
                    {
                        sql = "SELECT [ProductID], [ProductName], [BrandID], [Price], [Description], [StockQuantity], [CategoryID], [ImageUrl], [SupplierID], [CreatedAt], [CreatedBy], [Discount], [Check_Remove], [AverageRating], [ImportPrice], [Profit], [ProfitMargin] FROM Product WHERE ProductName LIKE N'%" + TENSP + "%' AND Check_Remove = '1'";
                        dt = new DataTable();
                        dt = DataProvider.GetTable(sql);
                    }
                    else if (MALOAISP != "-1" && MANCC == "-1")
                    {
                        sql = "SELECT [ProductID], [ProductName], [BrandID], [Price], [Description], [StockQuantity], [CategoryID], [ImageUrl], [SupplierID], [CreatedAt], [CreatedBy], [Discount], [Check_Remove], [AverageRating], [ImportPrice], [Profit], [ProfitMargin] FROM Product WHERE ProductName LIKE N'%" + TENSP + "%' AND CategoryID LIKE '%" + MALOAISP + "%' AND Check_Remove = '1'";
                        dt = new DataTable();
                        dt = DataProvider.GetTable(sql);
                    }
                    else if (MALOAISP == "-1" && MANCC != "-1")
                    {

                        sql = "SELECT [ProductID], [ProductName], [BrandID], [Price], [Description], [StockQuantity], [CategoryID], [ImageUrl], [SupplierID], [CreatedAt], [CreatedBy], [Discount], [Check_Remove], [AverageRating], [ImportPrice], [Profit], [ProfitMargin] FROM Product WHERE ProductName LIKE N'%" + TENSP + "%' AND SupplierID LIKE '%" + MANCC + "%' AND Check_Remove = '1'";
                        dt = new DataTable();
                        dt = DataProvider.GetTable(sql);
                    }
                    else if (MALOAISP != "-1" && MANCC != "-1")
                    {


                        sql = "SELECT [ProductID], [ProductName], [BrandID], [Price], [Description], [StockQuantity], [CategoryID], [ImageUrl], [SupplierID], [CreatedAt], [CreatedBy], [Discount], [Check_Remove], [AverageRating], [ImportPrice], [Profit], [ProfitMargin] FROM Product WHERE ProductName LIKE N'%" + TENSP + "%' AND SupplierID LIKE '%" + MANCC + "%' AND CategoryID LIKE '%" + MALOAISP + "%' AND Check_Remove = '1'";
                        dt = new DataTable();
                        dt = DataProvider.GetTable(sql);
                    }
                }
                else
                {
                    if (MALOAISP == "-1" && MANCC == "-1")
                    {
                         sql = @"
                                        SELECT
                                          [ProductID], [ProductName], [BrandID], [Price], 
                                          [Description], [StockQuantity], [CategoryID], 
                                          [ImageUrl], [SupplierID], [CreatedAt], [CreatedBy], 
                                          [Discount], [Check_Remove], [AverageRating], 
                                          [ImportPrice], [Profit], [ProfitMargin]              
                                        FROM [WatchStoreC#].[dbo].[Product]
                                        WHERE Check_Remove = 1";


                        dt = new DataTable();
                        dt = DataProvider.GetTable(sql);
                    }
                    else if (MALOAISP != "-1" && MANCC == "-1")
                    {

                        
                        
                        sql = "SELECT [ProductID], [ProductName], [BrandID], [Price], [Description], [StockQuantity], [CategoryID], [ImageUrl], [SupplierID], [CreatedAt], [CreatedBy], [Discount], [Check_Remove], [AverageRating], [ImportPrice], [Profit], [ProfitMargin] FROM Product WHERE  CategoryID LIKE '%" + MALOAISP + "%' AND Check_Remove = '1'";

                        dt = new DataTable();
                        dt = DataProvider.GetTable(sql);
                    }
                    else if (MALOAISP == "-1" && MANCC != "-1")
                    {
                       
                            sql = "SELECT [ProductID], [ProductName], [BrandID], [Price], [Description], [StockQuantity], [CategoryID], [ImageUrl], [SupplierID], [CreatedAt], [CreatedBy], [Discount], [Check_Remove], [AverageRating], [ImportPrice], [Profit], [ProfitMargin] FROM Product WHERE SupplierID LIKE '%" + MANCC + "%' AND Check_Remove = '1'";
                            dt = new DataTable();
                        dt = DataProvider.GetTable(sql);
                    }
                    else if (MALOAISP != "-1" && MANCC != "-1")
                    {
        
                        sql = "SELECT [ProductID], [ProductName], [BrandID], [Price], [Description], [StockQuantity], [CategoryID], [ImageUrl], [SupplierID], [CreatedAt], [CreatedBy], [Discount], [Check_Remove], [AverageRating], [ImportPrice], [Profit], [ProfitMargin] FROM Product WHERE SupplierID LIKE '%" + MANCC + "%' AND CategoryID LIKE '%" + MALOAISP + "%' AND Check_Remove = '1'";
                        dt = new DataTable();
                        dt = DataProvider.GetTable(sql);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
              //  MessageBox.Show("Lỗi database: " + ex.Message);
                return null;
            }
        }
        #endregion

        

    }
}
