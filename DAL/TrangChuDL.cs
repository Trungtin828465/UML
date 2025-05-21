using DTO;
using System.Data;
using System.Text.RegularExpressions;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer
{

    public class TrangChuDL
    {
        private static TrangChuDL Instance;
        public static TrangChuDL GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new TrangChuDL();
                }
                return Instance;
            }
        }
        private TrangChuDL() { }

        #region Lấy Tổng Sản Phẩm Đã Bán
        public int GetTongSanPhamDaBan()
        {
            try
            {
                string sql = "SELECT SUM(OrderItem.Quantity) FROM OrderItem JOIN [Order] ON [Order].OrderId = OrderItem.OrderId WHERE [Order].OrderStatus = 1;";
                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                int sl = 0;
                sl = int.Parse(dt.Rows[0][0].ToString());
                return sl;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion

        #region Lấy Tổng Doanh Thu
        public double GetTongDoanhThu()
        {
            try
            {
                string sql = "SELECT SUM(OrderItem.Quantity * OrderItem.UnitPrice) FROM OrderItem JOIN [Order] ON [Order].OrderId = OrderItem.OrderId WHERE [Order].OrderStatus = 1;";
                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                double doanhthu = double.Parse(dt.Rows[0][0].ToString());
                return doanhthu;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion

        #region Lấy Tổng Khách Hàng
        public int GetTongKhachHang()
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM Customer";
                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                int kh = int.Parse(dt.Rows[0][0].ToString());
                return kh;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion

        #region Lấy Top 10 SP Theo Số Lượng Hôm Nay
        public List<ProductDTO> GetTop10SPTheoSoLuongHomNay()
        {
            try
            {
                string sql = "SELECT TOP 10 OrderItem.ProductID, Product.ProductName, SUM(OrderItem.Quantity) AS TotalQuantity FROM OrderItem JOIN Product ON OrderItem.ProductID = Product.ProductID JOIN[Order] AS pr ON OrderItem.OrderID = pr.OrderID WHERE CAST(pr.OrderDate AS DATE) = CAST(GETDATE() AS DATE) GROUP BY OrderItem.ProductID, Product.ProductName ORDER BY TotalQuantity DESC;";
                DataTable dt = new DataTable();
                List<ProductDTO> lstSP = new List<ProductDTO>();
                dt = DataProvider.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductDTO pdDTO = new ProductDTO();
                    pdDTO.productid = int.Parse(dt.Rows[i][0].ToString());
                    pdDTO.productname = dt.Rows[i][1].ToString();
                    pdDTO.quantity = int.Parse(dt.Rows[i][2].ToString());

                    lstSP.Add(pdDTO);
                }
                return lstSP;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        #endregion

        #region Lấy Top 10 SP Theo Số Lượng Hôm Qua
        public List<ProductDTO> GetTop10SPTheoSoLuongHomQua()
        {
            try
            {
                string sql = "SELECT TOP 10 OrderItem.ProductID, Product.ProductName, SUM(OrderItem.Quantity) AS TotalQuantity FROM OrderItem JOIN Product ON OrderItem.ProductID = Product.ProductID JOIN[Order] AS pr ON OrderItem.OrderID = pr.OrderID WHERE CAST(pr.OrderDate AS DATE) = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE) GROUP BY OrderItem.ProductID, Product.ProductName ORDER BY TotalQuantity DESC;";
                DataTable dt = new DataTable();
                List<ProductDTO> lstSP = new List<ProductDTO>();
                dt = DataProvider.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductDTO pdDTO = new ProductDTO();
                    pdDTO.productid = int.Parse(dt.Rows[i][0].ToString());
                    pdDTO.productname = dt.Rows[i][1].ToString();
                    pdDTO.quantity = int.Parse(dt.Rows[i][2].ToString());

                    lstSP.Add(pdDTO);
                }
                return lstSP;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Lấy Top 10 SP Theo Số Lượng 7 Ngày Qua
        public List<ProductDTO> GetTop10SPTheoSoLuong7NgayQua()
        {
            try
            {
                string sql = "SELECT TOP 10 OrderItem.ProductID, Product.ProductName, SUM(OrderItem.Quantity) AS TotalQuantity FROM OrderItem JOIN Product ON OrderItem.ProductID = Product.ProductID JOIN[Order] AS pr ON OrderItem.OrderID = pr.OrderID WHERE CAST(pr.OrderDate AS DATE) >= CAST(DATEADD(DAY, -7, GETDATE()) AS DATE)  GROUP BY OrderItem.ProductID, Product.ProductName ORDER BY TotalQuantity DESC;";
                DataTable dt = new DataTable();
                List<ProductDTO> lstSP = new List<ProductDTO>();
                dt = DataProvider.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductDTO pdDTO = new ProductDTO();
                    pdDTO.productid = int.Parse(dt.Rows[i][0].ToString());
                    pdDTO.productname = dt.Rows[i][1].ToString();
                    pdDTO.quantity = int.Parse(dt.Rows[i][2].ToString());

                    lstSP.Add(pdDTO);
                }
                return lstSP;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Lấy Top 10 SP Theo Số Lượng Tháng Này
        public List<ProductDTO> GetTop10SPTheoSoLuongThangNay()
        {
            try
            {
                string sql = "SELECT TOP 10 OrderItem.ProductID, Product.ProductName, SUM(OrderItem.Quantity) AS TotalQuantity FROM OrderItem JOIN Product ON OrderItem.ProductID = Product.ProductID JOIN[Order] AS pr ON OrderItem.OrderID = pr.OrderID WHERE YEAR(pr.OrderDate) = YEAR(GETDATE()) AND MONTH(pr.OrderDate) = MONTH(GETDATE()) GROUP BY OrderItem.ProductID, Product.ProductName ORDER BY TotalQuantity DESC;";
                DataTable dt = new DataTable();
                List<ProductDTO> lstSP = new List<ProductDTO>();
                dt = DataProvider.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductDTO pdDTO = new ProductDTO();
                    pdDTO.productid = int.Parse(dt.Rows[i][0].ToString());
                    pdDTO.productname = dt.Rows[i][1].ToString();
                    pdDTO.quantity = int.Parse(dt.Rows[i][2].ToString());

                    lstSP.Add(pdDTO);
                }
                return lstSP;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Lấy Top 10 SP Theo Số Lượng Tháng Trước
        public List<ProductDTO> GetTop10SPTheoSoLuongThangTruoc()
        {
            try
            {
                string sql = "SELECT TOP 10 OrderItem.ProductID, Product.ProductName, SUM(OrderItem.Quantity) AS TotalQuantity FROM OrderItem JOIN Product ON OrderItem.ProductID = Product.ProductID JOIN[Order] AS pr ON OrderItem.OrderID = pr.OrderID WHERE YEAR(pr.OrderDate) = YEAR(DATEADD(MONTH, -1, GETDATE())) AND MONTH(pr.OrderDate) = MONTH(DATEADD(MONTH, -1, GETDATE())) GROUP BY OrderItem.ProductID, Product.ProductName ORDER BY TotalQuantity DESC;";
                DataTable dt = new DataTable();
                List<ProductDTO> lstSP = new List<ProductDTO>();
                dt = DataProvider.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductDTO pdDTO = new ProductDTO();
                    pdDTO.productid = int.Parse(dt.Rows[i][0].ToString());
                    pdDTO.productname = dt.Rows[i][1].ToString();
                    pdDTO.quantity = int.Parse(dt.Rows[i][2].ToString());

                    lstSP.Add(pdDTO);
                }
                return lstSP;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Lấy Top 10 SP Theo Doanh Thu Hôm Nay
        public List<ProductDTO> GetTop10SPTheoDoanhThuHomNay()
        {
            try
            {
                DataProvider.Openconnect();
                string sql = "SELECT TOP 10 OrderItem.ProductID, pd.ProductName, SUM(OrderItem.Quantity * pd.Price - ((OrderItem.Quantity * pd.Price * pd.Discount) / 100)) AS TotalSales FROM OrderItem JOIN Product pd ON OrderItem.ProductID = pd.ProductID JOIN [Order] ON OrderItem.OrderID = [Order].OrderID WHERE CAST([Order].OrderDate AS DATE) = CAST(GETDATE() AS DATE) GROUP BY OrderItem.ProductID, pd.ProductName ORDER BY TotalSales DESC;";
                DataTable dt = new DataTable();
                List<ProductDTO> lstSP = new List<ProductDTO>();
                dt = DataProvider.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductDTO pdDTO = new ProductDTO();
                    pdDTO.productid = int.Parse(dt.Rows[i][0].ToString());
                    pdDTO.productname = dt.Rows[i][1].ToString();
                    pdDTO.tongdoanhthu = double.Parse(dt.Rows[i][2].ToString());

                    lstSP.Add(pdDTO);
                }
                return lstSP;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Lấy Top 10 SP Theo Doanh Thu Hôm Qua
        public List<ProductDTO> GetTop10SPTheoDoanhThuHomQua()
        {
            try
            {
                DataProvider.Openconnect();
                string sql = "SELECT TOP 10 OrderItem.ProductID, pd.ProductName, SUM(OrderItem.Quantity * pd.Price - ((OrderItem.Quantity * pd.Price * pd.Discount) / 100)) AS TotalSales FROM OrderItem JOIN Product pd ON OrderItem.ProductID = pd.ProductID JOIN [Order] ON OrderItem.OrderID = [Order].OrderID WHERE CAST([Order].OrderDate AS DATE) = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE) GROUP BY OrderItem.ProductID, pd.ProductName ORDER BY TotalSales DESC;";
                DataTable dt = new DataTable();
                List<ProductDTO> lstSP = new List<ProductDTO>();
                dt = DataProvider.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductDTO pdDTO = new ProductDTO();
                    pdDTO.productid = int.Parse(dt.Rows[i][0].ToString());
                    pdDTO.productname = dt.Rows[i][1].ToString();
                    pdDTO.tongdoanhthu = double.Parse(dt.Rows[i][2].ToString());

                    lstSP.Add(pdDTO);
                }
                return lstSP;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Lấy Top 10 SP Theo Doanh Thu 7 Ngày Qua
        public List<ProductDTO> GetTop10SPTheoDoanhThu7NgayQua()
        {
            try
            {
                DataProvider.Openconnect();
                string sql = "SELECT TOP 10 OrderItem.ProductID, pd.ProductName, SUM(OrderItem.Quantity * pd.Price - ((OrderItem.Quantity * pd.Price * pd.Discount) / 100)) AS TotalSales FROM OrderItem JOIN Product pd ON OrderItem.ProductID = pd.ProductID JOIN [Order] ON OrderItem.OrderID = [Order].OrderID WHERE CAST([Order].OrderDate AS DATE) >= CAST(DATEADD(DAY, -7, GETDATE()) AS DATE) GROUP BY OrderItem.ProductID, pd.ProductName ORDER BY TotalSales DESC;";
                DataTable dt = new DataTable();
                List<ProductDTO> lstSP = new List<ProductDTO>();
                dt = DataProvider.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductDTO pdDTO = new ProductDTO();
                    pdDTO.productid = int.Parse(dt.Rows[i][0].ToString());
                    pdDTO.productname = dt.Rows[i][1].ToString();
                    pdDTO.tongdoanhthu = double.Parse(dt.Rows[i][2].ToString());

                    lstSP.Add(pdDTO);
                }
                return lstSP;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Lấy Top 10 SP Theo Doanh Thu Tháng Này
        public List<ProductDTO> GetTop10SPTheoDoanhThuThangNay()
        {
            try
            {
                DataProvider.Openconnect();
                string sql = "SELECT TOP 10 OrderItem.ProductID, pd.ProductName, SUM(OrderItem.Quantity * pd.Price - ((OrderItem.Quantity * pd.Price * pd.Discount) / 100)) AS TotalSales FROM OrderItem JOIN Product pd ON OrderItem.ProductID = pd.ProductID JOIN [Order] ON OrderItem.OrderID = [Order].OrderID WHERE YEAR([Order].OrderDate) = YEAR(GETDATE()) AND MONTH([Order].OrderDate) = MONTH(GETDATE()) GROUP BY OrderItem.ProductID, pd.ProductName ORDER BY TotalSales DESC;";
                DataTable dt = new DataTable();
                List<ProductDTO> lstSP = new List<ProductDTO>();
                dt = DataProvider.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductDTO pdDTO = new ProductDTO();
                    pdDTO.productid = int.Parse(dt.Rows[i][0].ToString());
                    pdDTO.productname = dt.Rows[i][1].ToString();
                    pdDTO.tongdoanhthu = double.Parse(dt.Rows[i][2].ToString());

                    lstSP.Add(pdDTO);
                }
                return lstSP;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Lấy Top SP Theo Doanh Thu Tháng Trước
        public List<ProductDTO> GetTop10SPTheoDoanhThuThangTruoc()
        {
            try
            {
                DataProvider.Openconnect();
                string sql = "SELECT TOP 10 OrderItem.ProductID, pd.ProductName, SUM(OrderItem.Quantity * pd.Price - ((OrderItem.Quantity * pd.Price * pd.Discount) / 100)) AS TotalSales FROM OrderItem JOIN Product pd ON OrderItem.ProductID = pd.ProductID JOIN [Order] ON OrderItem.OrderID = [Order].OrderID WHERE YEAR([Order].OrderDate) = YEAR(DATEADD(MONTH, -1, GETDATE())) AND MONTH([Order].OrderDate) = MONTH(DATEADD(MONTH, -1, GETDATE())) GROUP BY OrderItem.ProductID, pd.ProductName ORDER BY TotalSales DESC;";
                DataTable dt = new DataTable();
                List<ProductDTO> lstSP = new List<ProductDTO>();
                dt = DataProvider.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProductDTO pdDTO = new ProductDTO();
                    pdDTO.productid = int.Parse(dt.Rows[i][0].ToString());
                    pdDTO.productname = dt.Rows[i][1].ToString();
                    pdDTO.tongdoanhthu = double.Parse(dt.Rows[i][2].ToString());

                    lstSP.Add(pdDTO);
                }
                return lstSP;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Lấy Doanh Thu Hôm Nay
        public double GetDoanhThuHomNay()
        {
            try
            {
                string sql = "SELECT SUM([Order].TotalPrice) AS TotalSalesToday FROM [Order] WHERE [Order].OrderStatus = 1 and CAST([Order].OrderDate AS DATE) = CAST(GETDATE() AS DATE);";
                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                double doanhthu = double.Parse(dt.Rows[0][0].ToString());
                return doanhthu;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }
        #endregion

        #region Lấy Doanh Thu Hôm Qua
        public DoanhThuDTO GetDoanhThuHomQua()
        {
            try
            {
                string sql = "SELECT CONVERT(VARCHAR(10), [Order].OrderDate, 101) AS NgayLap, SUM([Order].TotalPrice) AS TotalAmount FROM [Order] WHERE CAST([Order].OrderDate AS DATE) = CAST(DATEADD(DAY, -1, GETDATE()) AS DATE) AND[Order].OrderStatus = '1' GROUP BY CONVERT(VARCHAR(10), [Order].OrderDate, 101);";
                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                DoanhThuDTO dtDTO = new DoanhThuDTO();
                dtDTO.ngay = Convert.ToDateTime(dt.Rows[0][0]);
                dtDTO.doanhthu = double.Parse(dt.Rows[0][1].ToString());
                return dtDTO;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        #endregion

        #region Lấy Doanh Thu Theo Số Ngày
        public List<DoanhThuDTO> GetDoanhThu(int songay)
        {
            try
            {
                string sql = "SELECT TOP " + songay + " [Order].OrderDate, SUM([Order].TotalPrice) FROM [Order] GROUP BY [Order].OrderDate ORDER BY [Order].OrderDate DESC";
                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                List<DoanhThuDTO> lstdtDTO = new List<DoanhThuDTO>();
                dt = DataProvider.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DoanhThuDTO dtDTO = new DoanhThuDTO();
                    dtDTO.ngay = Convert.ToDateTime(dt.Rows[i][0].ToString());
                    dtDTO.doanhthu = double.Parse(dt.Rows[i][1].ToString());

                    lstdtDTO.Add(dtDTO);
                }
                return lstdtDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Lấy Doanh Thu 7 Ngày Qua
        public List<DoanhThuDTO> GetDoanhThu7NgayQua()
        {
            try
            {
                string sql = "SELECT TOP 7 CONVERT(VARCHAR(10), [Order].OrderDate, 101) AS OrderDate, SUM([Order].TotalPrice) AS TotalSales FROM [Order] WHERE CAST([Order].OrderDate AS DATE) >= CAST(DATEADD(DAY, -7, GETDATE()) AS DATE) GROUP BY CONVERT(VARCHAR(10), [Order].OrderDate, 101) ORDER BY OrderDate DESC;";
                DataTable dt = new DataTable();
                List<DoanhThuDTO> lstdtDTO = new List<DoanhThuDTO>();
                dt = DataProvider.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DoanhThuDTO dtDTO = new DoanhThuDTO();
                    dtDTO.ngay = Convert.ToDateTime(dt.Rows[i][0].ToString());
                    dtDTO.doanhthu = double.Parse(dt.Rows[i][1].ToString());

                    lstdtDTO.Add(dtDTO);
                }
                return lstdtDTO;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        #endregion

        #region Lấy Doanh Thu Tháng Này
        public List<DoanhThuDTO> GetDoanhThuThangNay()
        {
            try
            {
                string sql = "SELECT CONVERT(VARCHAR(10), [Order].OrderDate, 101) AS OrderDate, SUM([Order].TotalPrice) AS TotalSales FROM [Order] WHERE YEAR([Order].OrderDate) = YEAR(GETDATE()) AND MONTH([Order].OrderDate) = MONTH(GETDATE()) GROUP BY CONVERT(VARCHAR(10), [Order].OrderDate, 101) ORDER BY CONVERT(VARCHAR(10),[Order].OrderDate,101) DESC;";
                DataTable dt = new DataTable();
                List<DoanhThuDTO> lstdtDTO = new List<DoanhThuDTO>();
                dt = DataProvider.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DoanhThuDTO dtDTO = new DoanhThuDTO();
                    dtDTO.ngay = Convert.ToDateTime(dt.Rows[i][0].ToString());
                    dtDTO.doanhthu = double.Parse(dt.Rows[i][1].ToString());

                    lstdtDTO.Add(dtDTO);
                }
                return lstdtDTO;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        #endregion

        #region Lấy Doanh Thu Tháng Trước
        public List<DoanhThuDTO> GetDoanhThuThangTruoc()
        {
            try
            {
                string sql = "SELECT CONVERT(VARCHAR(10), [Order].OrderDate, 101) AS OrderDate, SUM([Order].TotalPrice) AS TotalSales FROM [Order] WHERE YEAR([Order].OrderDate) = YEAR(DATEADD(MONTH, -1, GETDATE())) AND MONTH([Order].OrderDate) = MONTH(DATEADD(MONTH, -1, GETDATE())) GROUP BY CONVERT(VARCHAR(10), [Order].OrderDate, 101) ORDER BY CONVERT(VARCHAR(10), [Order].OrderDate, 101) DESC;";
                DataTable dt = new DataTable();
                List<DoanhThuDTO> lstdtDTO = new List<DoanhThuDTO>();
                dt = DataProvider.GetTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DoanhThuDTO dtDTO = new DoanhThuDTO();
                    dtDTO.ngay = Convert.ToDateTime(dt.Rows[i][0].ToString());
                    dtDTO.doanhthu = double.Parse(dt.Rows[i][1].ToString());

                    lstdtDTO.Add(dtDTO);
                }
                return lstdtDTO;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        #endregion

        public List<DoanhThuDTO> GetDoanhThuByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                string sql = @"
            SELECT CONVERT(VARCHAR(10), [Order].OrderDate, 101) AS OrderDate, 
                   SUM([Order].TotalPrice) AS TotalSales 
            FROM [Order] 
            WHERE CAST([Order].OrderDate AS DATE) BETWEEN @StartDate AND @EndDate 
            AND [Order].OrderStatus = 1 
            GROUP BY CONVERT(VARCHAR(10), [Order].OrderDate, 101) 
            ORDER BY OrderDate DESC;";
                var parameters = new[]
                {
            new SqlParameter("@StartDate", startDate),
            new SqlParameter("@EndDate", endDate)
        };

                DataTable dt = DataProvider.GetTable1(sql, parameters);
                List<DoanhThuDTO> lstdtDTO = new List<DoanhThuDTO>();

                foreach (DataRow row in dt.Rows)
                {
                    lstdtDTO.Add(new DoanhThuDTO
                    {
                        ngay = Convert.ToDateTime(row["OrderDate"]),
                        doanhthu = double.Parse(row["TotalSales"].ToString())
                    });
                }
                return lstdtDTO;
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                Console.WriteLine(ex.Message);
                return new List<DoanhThuDTO>();
            }
        }

    }
}
