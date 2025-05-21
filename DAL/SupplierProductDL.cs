using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DAL
{
    public class SupplierProductDL
    {
        public static int SelectedSupplierID { get; set; }
        public static String img { get; set; }
        public static String fileImg { get; set; }


        private static SupplierProductDL Instance;
        public static SupplierProductDL GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new SupplierProductDL();
                }
                return Instance;
            }
        }


        #region Lấy Danh sách sản phẩm trong kho
        public DataTable GetProductInStock()
        {
            try
            {
                string sql = @"
                
                SELECT
                    ProductID,
                    SupplierID,
                    ProductName,
                    Price,
                    Status,
                    Quantity,
                    CreatedAt,
                    Img,
                    CategoryID,
                    BrandID
                 FROM SupplierProduct "
;


                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                //MessasgeBox.Show("Lỗi database: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region Lấy Danh Sách Sản Phẩm trong cửa hanng
        public DataTable GetProduct()
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
                FROM [Product]
                WHERE Check_Remove = 1";

                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        #endregion


        public int AddProduct(string productName, int supplierID, decimal price, DateTime createdAt, int stockQuantity, string imageFileName)
        {
            try
            {
                // SQL kiểm tra sản phẩm đã tồn tại
                string sqlCheckExist = @"
                SELECT COUNT(*) 
                FROM Product 
                WHERE ProductName = @ProductName AND SupplierID = @SupplierID 
               ";

                // SQL cập nhật sản phẩm nếu đã tồn tại
                string sqlUpdate = @"
                UPDATE Product 
                SET ImportPrice = @ImportPrice, 
                    StockQuantity = StockQuantity + @StockQuantity ,
                    Price = 0,
                    ProfitMargin = 0,
                    Profit=0

                WHERE ProductName = @ProductName AND SupplierID = @SupplierID 
                ";

                // SQL thêm mới sản phẩm
                string sqlInsert = @"
                INSERT INTO Product (ProductName, SupplierID, ImportPrice, CreatedAt, StockQuantity, ImageUrl) 
                VALUES (@ProductName, @SupplierID, @ImportPrice, @CreatedAt, @StockQuantity, @Img)";

                // Tạo mảng tham số dùng chung
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@ProductName", SqlDbType.NVarChar) { Value = productName },
            new SqlParameter("@SupplierID", SqlDbType.Int) { Value = supplierID },
            new SqlParameter("@ImportPrice", SqlDbType.Decimal) { Value = price },
            new SqlParameter("@CreatedAt", SqlDbType.DateTime) { Value = createdAt },
            new SqlParameter("@StockQuantity", SqlDbType.Int) { Value = stockQuantity },
            new SqlParameter("@Img", SqlDbType.NVarChar) { Value = imageFileName }
            //new SqlParameter("@BrandID", SqlDbType.Int) { Value = brandID },
            //new SqlParameter("@CategoryID", SqlDbType.Int) { Value = categoryID }
                };

                // Kiểm tra sản phẩm đã tồn tại chưa
                int count = Convert.ToInt32(DataProvider.ExecuteScalar(sqlCheckExist, parameters));

                if (count > 0)
                {
                    // Tạo mảng tham số mới để cập nhật (không dùng lại tham số cũ)
                    SqlParameter[] updateParams = parameters.Select(p => new SqlParameter(p.ParameterName, p.SqlDbType) { Value = p.Value }).ToArray();
                    MessageBox.Show("CapNhatSP");
                    return DataProvider.JustExcuteWithParameter(sqlUpdate, updateParams);
                }
                else
                {
                    // Tạo mảng tham số mới để thêm mới
                    SqlParameter[] insertParams = parameters.Select(p => new SqlParameter(p.ParameterName, p.SqlDbType) { Value = p.Value }).ToArray();
                    MessageBox.Show("ThemMoiSP");

                    return DataProvider.JustExcuteWithParameter(sqlInsert, insertParams);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                //MessageBox.Show($"Lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }


        // update lại status
        public bool UpdateProductStatus(int productId, int newStatus)
        {
            string sql = @"
                UPDATE SupplierProduct
                SET Status = @Status
                WHERE ProductID = @ProductID;
                  ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ProductID", SqlDbType.Int) { Value = productId },
                new SqlParameter("@Status", SqlDbType.Int) { Value = newStatus }
            };

            return DataProvider.JustExcuteWithParameter(sql, parameters) > 0;
        }
        //brand NAme
        public string GetBrandNameById(int brandID)
        {
            try
            {
                string sql = "SELECT BrandName FROM [Brand] WHERE BrandID = @BrandID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("@BrandID", SqlDbType.Int) { Value = brandID }
                };

                DataTable dt = DataProvider.GetTableWithParameters(sql, parameters);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["BrandName"].ToString();
                }
                else
                {
                    return "Không tìm thấy thương hiệu";
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Lỗi khi truy vấn dữ liệu DL: " + ex.Message);
                return null;
            }
        }

        //The loai
        public string GetCategoryNameById(int categoryID)
        {
            try
            {

                string sql = "SELECT CategoryName FROM [Category] WHERE CategoryID = @CategoryID";


                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@CategoryID", SqlDbType.Int) { Value = categoryID }
                };


                DataTable dt = DataProvider.GetTableWithParameters(sql, parameters);


                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["CategoryName"].ToString();
                }
                else
                {
                    return "Không tìm thấy danh mục";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi truy vấn dữ liệu DL: " + ex.Message);
                return null;
            }
        }
        // ncc
        public string GetSupplierNameById(int supplierID)
        {
            try
            {

                string sql = "SELECT ContactName FROM [dbo].[Supplier] WHERE SupplierID = @SupplierID";


                SqlParameter[] parameters = new SqlParameter[]
                {
                  new SqlParameter("@SupplierID", SqlDbType.Int) { Value = supplierID }
                };


                DataTable dt = DataProvider.GetTableWithParameters(sql, parameters);


                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["ContactName"].ToString();
                }
                else
                {
                    return "Không tìm thấy nhà cung cấp";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi truy vấn dữ liệu DL: " + ex.Message);
                return null;
            }
        }


        // xoa ncc
        public bool UpdateSupplierRemove(int SupplierID, int check_Remove)
        {
            string sql = @"
                UPDATE Supplier
                SET check_Remove = @check_Remove
                WHERE SupplierID = @SupplierID;
                  ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SupplierID", SqlDbType.Int) { Value = SupplierID },
                new SqlParameter("@check_Remove", SqlDbType.Int) { Value = check_Remove }
            };

            return DataProvider.JustExcuteWithParameter(sql, parameters) > 0;
        }

        // update ncc
        public bool UpdateSupplier(int SupplierID, string ContactName, string Phone, string Email, string Address)
        {
            try
            {
                string sql = @"
            UPDATE Supplier
            SET
                ContactName = @ContactName,
                Phone = @Phone,
                Email = @Email,
                Address = @Address
            WHERE SupplierID = @SupplierID;
              ";


                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SupplierID", SqlDbType.Int) { Value = SupplierID },
                    new SqlParameter("@ContactName", SqlDbType.NVarChar, 100) { Value = ContactName },
                    new SqlParameter("@Phone", SqlDbType.NVarChar, 20) { Value = Phone },
                    new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = Email },
                    new SqlParameter("@Address", SqlDbType.NVarChar, 255) { Value = Address },
                };


                int result = DataProvider.JustExcuteWithParameter(sql, parameters);


                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật nhà cung cấp: " + ex.Message);
                //Console.WriteLine("Lỗi khi cập nhật nhà cung cấp: " + ex.Message);
                return false;
            }
        }
        // add ncc
        public bool AddSupplier(string ContactName, string Phone, string Email, string Address)
        {
            try
            {

                string checkEmailSql = "SELECT COUNT(*) FROM Supplier WHERE Email = @Email";
                SqlParameter[] checkEmailParams = new SqlParameter[]
                {
                  new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = Email }
                };


                int emailCount = Convert.ToInt32(DataProvider.ExecuteScalar(checkEmailSql, checkEmailParams));

                if (emailCount > 0)
                {
                    // Nếu email đã tồn tại
                    return false;
                }

                // Nếu email chưa tồn tại, thực hiện thêm nhà cung cấp mới
                string sql = @"
                    INSERT INTO Supplier (ContactName, Phone, Email, Address, CreatedAt, check_Remove)
                    VALUES (@ContactName, @Phone, @Email, @Address, @CreatedAt, 1);
                    ";

                DateTime currentDate = DateTime.Now;

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ContactName", SqlDbType.NVarChar, 100) { Value = ContactName },
                    new SqlParameter("@Phone", SqlDbType.NVarChar, 20) { Value = Phone },
                    new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = Email },
                    new SqlParameter("@Address", SqlDbType.NVarChar, 255) { Value = Address },
                    new SqlParameter("@CreatedAt",  SqlDbType.DateTime) { Value = currentDate }
                };

                int result = DataProvider.JustExcuteWithParameter(sql, parameters);


                return result > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Lỗi khi thêm nhà cung cấp: " + ex.Message);
                return false;
            }
        }
        // find
        public int AddSupplierProduct(int supplierID, string productName, decimal price, int status, int quantity, string img)
        {
            try
            {
                string sql = @"
        INSERT INTO SupplierProduct (
            SupplierID,
            ProductName,
            Price,
            Status,
            Quantity,
            Img
            
        ) VALUES (
            @SupplierID,
            @ProductName,
            @Price,
            @Status,
            @Quantity,
            @Img
          
        )";

                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@SupplierID", SqlDbType.Int) { Value = supplierID },
            new SqlParameter("@ProductName", SqlDbType.NVarChar) { Value = productName },
            new SqlParameter("@Price", SqlDbType.Decimal) { Value = price },
            new SqlParameter("@Status", SqlDbType.Int) { Value = status },
            new SqlParameter("@Quantity", SqlDbType.Int) { Value = quantity },
            new SqlParameter("@Img", SqlDbType.NVarChar) { Value = img ?? (object)DBNull.Value },
         
                };

                Console.WriteLine("Executing SQL with parameters:");
                foreach (var param in parameters)
                {
                    Console.WriteLine($"{param.ParameterName}: {param.Value}");
                }

                return DataProvider.JustExcuteWithParameter(sql, parameters); // Ensure this returns rows affected or a success code
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in AddSupplierProduct: " + ex.Message);
                throw; // Optionally rethrow to handle higher up
            }
        }
    }
}
