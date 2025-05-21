using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

    public class ReportDL
    {
        private static ReportDL Instance;
        public static ReportDL GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new ReportDL();
                }
                return Instance;
            }
        }
        //doanh thu tb
        public DataTable GetHoaDon2023()
        {
            try
            {
                string sql = @"
               
                     SELECT 
                        MONTH(OrderDate) AS Month,  
                        CAST(SUM(TotalPrice) / COUNT(OrderID) AS INT) AS TotalRevenue -- Loại bỏ phần thập phân
                    FROM 
                        [Order]
                    WHERE 
                        YEAR(OrderDate) = 2023 
                    GROUP BY 
                        MONTH(OrderDate)  
                    ORDER BY 
                        Month;

                    ";

                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dữ liệu: " + ex.Message);
                return null;
            }
        }
        public DataTable GetHoaDon2024()
        {
            try
            {
                string sql = @"
               
                    SELECT 
                        MONTH(OrderDate) AS Month,  
                        CAST(SUM(TotalPrice) / COUNT(OrderID) AS INT) AS TotalRevenue -- Loại bỏ phần thập phân
                    FROM 
                        [Order]
                    WHERE 
                        YEAR(OrderDate) = 2024
                    GROUP BY 
                        MONTH(OrderDate)  
                    ORDER BY 
                        Month;
                    ";

                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dữ liệu: " + ex.Message);
                return null;
            }
        }
        // daonh thu 2 biểu đồ kết hợp
        public DataTable GetDoanhThu2023()
        {
            try
            {
                string sql = @"
               
                   SELECT 
                        YEAR(OrderDate) AS Year,
                        MONTH(OrderDate) AS Month,                          
                        COUNT(OrderID) AS [Tổng hóa đơn],                  
                        CAST(SUM(TotalPrice) AS BIGINT) AS [Tổng tiền]
                    FROM 
                        [Order]
                    WHERE 
                        YEAR(OrderDate) = 2023
                    GROUP BY 
                        YEAR(OrderDate), MONTH(OrderDate)
                    ORDER BY 
                        Year, Month;

           
                    ";

                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dữ liệu: " + ex.Message);
                return null;
            }
        }
        public DataTable GetDoanhThu2024()
        {
            try
            {
                string sql = @"
                SELECT 
                    YEAR(OrderDate) AS Year,
                    MONTH(OrderDate) AS Month,                          
                    COUNT(OrderID) AS [Tổng hóa đơn],                  
                    CAST(SUM(TotalPrice) AS BIGINT) AS [Tổng tiền]
                FROM 
                    [Order]
                WHERE 
                    YEAR(OrderDate) = 2024
                GROUP BY 
                    YEAR(OrderDate), MONTH(OrderDate)
                ORDER BY 
                    Year, Month;

                    ";

                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dữ liệu: " + ex.Message);
                return null;
            }
        }
        //top sản phẩm bán ra
        public DataTable GetTopProduct()
        {
            try
            {
                string sql = @"
                SELECT TOP 10 
                    P.ProductName,    

                    SUM(OI.Quantity) AS TotalSold  ,
                         P.ImageUrl

                FROM 
                    OrderItem OI
                JOIN 
                    Product P ON OI.ProductID = P.ProductID
                GROUP BY 
                    P.ProductName,P.ImageUrl

                ORDER BY 
                    TotalSold DESC;";

                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dữ liệu nè: " + ex.Message);
                return null;
            }
        }
        // min product
        public DataTable GetMinProduct()
        {
            try
            {
                string sql = @"
                SELECT TOP 10
                         P.ProductName,                      
                         SUM(OI.Quantity * OI.UnitPrice) AS TotalRevenue,
                         P.ImageUrl
   
                     FROM 
                         OrderItem OI
                     JOIN 
                         Product P ON OI.ProductID = P.ProductID

                     JOIN 
                         [Order] O ON OI.OrderID = O.OrderID   

                     GROUP BY 
                         P.ProductName,P.ImageUrl
                     ORDER BY 
                         TotalRevenue ASC;               
            

                    ";

                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dữ liệu: " + ex.Message);
                return null;
            }
        }
        // ss 2 brand cot chong
        public DataTable GetBrand()
        {
            try
            {
                string sql = @"
               
	            SELECT 
                    MONTH(O.OrderDate) AS Month,            
                    B.BrandName,                            
                    SUM(OI.Quantity) AS TotalQuantitySold     
                FROM 
                    OrderItem OI
                JOIN 
                    Product P ON OI.ProductID = P.ProductID
                JOIN 
                    Brand B ON P.BrandID = B.BrandID
                JOIN 
                    [Order] O ON OI.OrderID = O.OrderID
                WHERE 
 
                    YEAR(O.OrderDate) = 2024           
                GROUP BY 
                    MONTH(O.OrderDate), B.BrandName        
                ORDER BY 
                    MONTH(O.OrderDate), B.BrandName;     

                    ";

                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dữ liệu: " + ex.Message);
                return null;
            }
        }
        // brand ngang
        public DataTable GetBrandAll()
        {
            try
            {
                string sql = @"
               
	            SELECT 
                    B.BrandName AS Hang,                            
                    SUM(OI.Quantity) AS TotalQuantitySold   
                FROM 
                    OrderItem OI
                JOIN 
                    Product P ON OI.ProductID = P.ProductID
                JOIN 
                    Brand B ON P.BrandID = B.BrandID
                JOIN 
                    [Order] O ON OI.OrderID = O.OrderID
                         
                GROUP BY 
                    B.BrandName                          
                ORDER BY 
                    TotalQuantitySold DESC;                

                    ";

                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dữ liệu: " + ex.Message);
                return null;
            }
        }
        /////
        ///
        public DataTable GetSLSK2024()
        {
            try
            {
                string sql = @" 
                SELECT
                    MONTH(ThoiGian) AS Thang,
                    COUNT(ID) AS SoLuongSuKien
                FROM
                    SuKienThamGia
                WHERE
                    YEAR(ThoiGian) = 2024
                GROUP BY
                    MONTH(ThoiGian)
                ORDER BY
                    Thang;
                    ";

                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dữ liệu: " + ex.Message);
                return null;
            }

        }
        public DataTable GetSLSK2023()
        {
            try
            {
                string sql = @" 
                SELECT
                    MONTH(ThoiGian) AS Thang,
                    COUNT(ID) AS SoLuongSuKien
                FROM
                    SuKienThamGia
                WHERE
                    YEAR(ThoiGian) = 2023
                GROUP BY
                    MONTH(ThoiGian)
                ORDER BY
                    Thang;
                    ";

                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dữ liệu: " + ex.Message);
                return null;
            }
        }
        public DataTable GetTopViecLam()
        {
            try
            {
                string sql = @"
	            SELECT TOP 10
                    csv.MaSV,
                    csv.HoTen,
	                csv.Email,
	                csv.SoDienThoai,
	                csv.TenCongTy,
	                csv.NoiLamViec,
	                csv.ThoiGianCapNhat
   
                FROM
                    CuuSinhVien csv
               
                ";

                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dữ liệu nè: " + ex.Message);
                return null;
            }
        }
        public DataTable GetTopThamGiaSuKien()
        {
            try
            {
                string sql = @"
	            SELECT TOP 10
                    csv.MaSV,
                    csv.HoTen,
                    csv.Email,
                    csv.SoDienThoai,
                    COUNT(sktg.ID) AS SoLuongSuKienDaThamGia
                FROM
                    CuuSinhVien csv
                JOIN
                    SuKienThamGia sktg ON csv.MaSV = sktg.MaSV
                GROUP BY
                    csv.MaSV, csv.HoTen, csv.Email, csv.SoDienThoai
                ORDER BY
                    SoLuongSuKienDaThamGia DESC;
               
                ";

                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dữ liệu nè: " + ex.Message);
                return null;
            }
        }
        public DataTable GetDSSuKien()
        {
            try
            {
                string sql = @"
                SELECT
                    sktg.TenSuKien,
                    COUNT(CASE WHEN sktg.TrangThaiThamGia = 1 THEN 1 ELSE NULL END) AS SoLuongSinhVienThamGia,
                    COUNT(CASE WHEN sktg.TrangThaiThamGia = 0 THEN 1 ELSE NULL END) AS SoLuongSinhVienKhongThamGia
                FROM
                    SuKienThamGia sktg
                GROUP BY
                    sktg.TenSuKien;
               
                ";

                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy dữ liệu nè: " + ex.Message);
                return null;
            }
        }





        /////////////
    }
}
