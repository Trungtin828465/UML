using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{

    public class ProductDL
    {
        private static ProductDL Instance;
        public static ProductDL GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new ProductDL();
                }
                return Instance;
            }
        }
        private ProductDL() { }

        #region Lấy Tổng Sản Phẩm Đã Bán
        //public int GetTongSanPhamDaBan()
        //{
        //    string sql = "SELECT SUM(SOLUONG) FROM CTHD";
        //    DataTable dt = new DataTable();
        //    dt = DataProvider.GetTable(sql);
        //    int sl = int.Parse(dt.Rows[0][0].ToString());
        //    return sl;
        //}
        #endregion

        #region Lấy Mã Sản Phẩm Max
        //public int GetMaSPMax()
        //{
        //    try
        //    {
        //        string sql = "SELECT MAX(MASP) FROM SANPHAM";
        //        DataTable dt = new DataTable();
        //        dt = DataProvider.GetTable(sql);
        //        return int.Parse(dt.Rows[0][0].ToString());
        //    }
        //    catch (Exception)
        //    {
        //        return 0;
        //    }
        //}
        #endregion

        #region Lấy Tên Sản Phẩm
        public string GetTenSP(int MASP)
        {
            try
            {
                string sql = "SELECT ProductName FROM Product WHERE ProductID = " + MASP;
                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt.Rows[0][0].ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion

        #region Kiểm Tra Mã Sản Phẩm
        //public bool CheckMaSP(string MASP)
        //{
        //    try
        //    {
        //        string sql = "SELECT * FROM SANPHAM WHERE MASP='" + MASP + "'";
        //        DataTable dt = new DataTable();
        //        dt = DataProvider.GetTable(sql);
        //        if (dt.Rows.Count > 0)
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show("Lỗi database: " + ex.Message);
        //        return false;
        //    }
        //}
        #endregion

        #region Lấy Danh Sách Sản Phẩm
        public DataTable GetDanhSachSanPham()
        {
            try
            {
                string sql = "SELECT ProductID as N'Mã SP',ProductName as N'Tên SP',CategoryID as N'Mã Loại SP',SupplierID as N'Mã NCC',CreatedAt as N'Ngày SX',StockQuantity as N'Số Lượng', Price as N'Đơn Giá Bán',Discount as N'Khuyến Mãi',ImageUrl as N'Link Hình Ảnh' FROM Product";
                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Lỗi database: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region Lấy Danh Sách Sản Phẩm Theo NCC
        public DataTable GetDanhSachSanPhamTheoNCC(int MANCC)
        {
            try
            {
                string sql = "SELECT ProductID as N'Mã SP',ProductName as N'Tên SP',CategoryID as N'Mã Loại SP',SupplierID as N'Mã NCC',CreatedAt as N'Ngày SX',StockQuantity as N'Số Lượng', Price as N'Đơn Giá Bán',Discount as N'Khuyến Mãi',ImageUrl as N'Link Hình Ảnh' FROM Product WHERE  SupplierID =" + MANCC;
                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Lỗi database: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region Lấy Danh Sách Sản Phẩm Theo Bộ Lọc
        public DataTable GetDanhSachSanPhamTheoBoLoc(string TENSP, string MALOAISP, string MANCC)
        {
            try
            {
                // Sử dụng StringBuilder để dễ quản lý và thêm điều kiện
                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append(@"SELECT 
                    Brand.BrandName AS BrandName,
                    Product.ProductID AS MãSP,
                    Product.ProductName AS TênSP,
                    Product.CategoryID AS MãLoaiSP,
                    Product.SupplierID AS MãNCC,
                    Product.CreatedAt AS NgaySX,
                    Product.StockQuantity AS SoLuong,
                    Product.Price AS DonGiaBan,
                    Product.Discount AS KhuyenMai,
                    Product.ImageUrl AS LinkHinhAnh
                FROM Product
                JOIN Brand ON Product.BrandID = Brand.BrandID
                WHERE 1 = 1");

                if (!string.IsNullOrWhiteSpace(TENSP))
                {
                    sqlBuilder.Append(" AND Product.ProductName LIKE @TenSP");
                }

                if (MALOAISP != "-1")
                {
                    sqlBuilder.Append(" AND Product.CategoryID = @MaLoaiSP");
                }

                if (MANCC != "-1")
                {
                    sqlBuilder.Append(" AND Product.SupplierID = @MaNCC");
                }

                string sql = sqlBuilder.ToString();

                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@TenSP", $"%{TENSP}%"), // Sử dụng wildcard '%' để tìm kiếm gần đúng
                    new SqlParameter("@MaLoaiSP", MALOAISP),
                    new SqlParameter("@MaNCC", MANCC)
                };

                // Gọi phương thức thực thi truy vấn
                return DataProvider.GetTable1(sql, parameters.ToArray());
            }
            catch (Exception ex)
            {
                // Log lỗi (nếu cần) hoặc trả về null
                Console.WriteLine($"Lỗi: {ex.Message}");
                return null;
            }
        }

        #endregion

        #region Ngừng Kinh Doanh Sản Phẩm
        public bool NgungKinhDoanhSanPham(string MASP)
        {
            try
            {
                string sql = "UPDATE SANPHAM SET NGUNGKINHDOANH=1 WHERE MASP='" + MASP + "'";
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
                return false;
            }
        }
        #endregion

        #region Thêm Sản Phẩm
        //public bool ThemSanPham(ProductDTO spDTO)
        //{
        //    try
        //    {
        //        string sql = "INSERT INTO Product (ProductName, BrandID, Price, Description, StockQuantity, CategoryID, ImageUrl, SupplierID, CreatedBy,Discount)";
        //        SqlConnection con = DataProvider.Openconnect();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandText = sql;
        //        cmd.Parameters.AddWithValue("@TENSP", spDTO.productname);
        //        cmd.Parameters.AddWithValue("@MALOAISP", spDTO);
        //        cmd.Parameters.AddWithValue("@DVT", spDTO.dvt);
        //        cmd.Parameters.AddWithValue("@MANCC", spDTO.mancc);
        //        cmd.Parameters.AddWithValue("@NGAYSX", spDTO.ngaysx);
        //        cmd.Parameters.AddWithValue("@NGAYHETHAN", spDTO.ngayhethan);
        //        cmd.Parameters.AddWithValue("@SOLUONG", 0);
        //        cmd.Parameters.AddWithValue("@GIABAN", spDTO.giaban);
        //        cmd.Parameters.AddWithValue("@GIANHAP", spDTO.gianhap);
        //        cmd.Parameters.AddWithValue("@LOINHUAN", spDTO.loinhuan);
        //        cmd.Parameters.AddWithValue("@KHUYENMAI", spDTO.khuyenmai);
        //        cmd.Parameters.AddWithValue("@HINHANH", spDTO.hinhanh);
        //        cmd.Parameters.AddWithValue("@NGUNGKINHDOANH", 0);
        //        cmd.Connection = con;
        //        int rows = cmd.ExecuteNonQuery();
        //        DataProvider.Disconnect(con);
        //        if (rows > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // MessageBox.Show("Lỗi database: " + ex.Message);
        //        return false;
        //    }
        //}
        #endregion

        #region Sửa Sản Phẩm
        //public bool SuaSanPham(ProductDTO spDTO)
        //{
        //    try
        //    {
        //        string sql = "UPDATE SANPHAM SET TENSP = @TENSP,NGAYSX = @NGAYSX,NGAYHETHAN = @NGAYHETHAN,DONGIANHAP = @DONGIANHAP,LOINHUAN = @LOINHUAN,DONGIABAN = @DONGIABAN,KHUYENMAI = @KHUYENMAI,HINHANH = @HINHANH WHERE MASP = @MASP";
        //        SqlConnection con = DataProvider.Openconnect();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandText = sql;
        //        cmd.Parameters.AddWithValue("@MASP", spDTO.masp);
        //        cmd.Parameters.AddWithValue("@TENSP", spDTO.tensp);
        //        cmd.Parameters.AddWithValue("@NGAYSX", spDTO.ngaysx);
        //        cmd.Parameters.AddWithValue("@NGAYHETHAN", spDTO.ngayhethan);
        //        cmd.Parameters.AddWithValue("@DONGIANHAP", spDTO.gianhap);
        //        cmd.Parameters.AddWithValue("@LOINHUAN", spDTO.loinhuan);
        //        cmd.Parameters.AddWithValue("@DONGIABAN", spDTO.giaban);
        //        cmd.Parameters.AddWithValue("@KHUYENMAI", spDTO.khuyenmai);
        //        cmd.Parameters.AddWithValue("@HINHANH", spDTO.hinhanh);
        //        cmd.Connection = con;
        //        int rows = cmd.ExecuteNonQuery();
        //        DataProvider.Disconnect(con);
        //        if (rows > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // MessageBox.Show("Lỗi database: " + ex.Message);
        //        return false;
        //    }
        //}
        #endregion

        #region Cập Nhật Số Lượng
        public bool CapNhatSoLuong(int MaSP, int SoLuong)
        {
            try
            {
                string sql = "UPDATE SANPHAM SET SoLuong = SoLuong+@SoLuong WHERE MASP = @MASP";
                SqlConnection con = DataProvider.Openconnect();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@MASP", MaSP);
                cmd.Parameters.AddWithValue("@SOLUONG", SoLuong);
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
                // MessageBox.Show("Lỗi database: " + ex.Message);
                return false;
            }
        }
        #endregion

        #region Cập Nhật Số Lượng Khi Bán Hàng
        public bool CapNhatSoLuongKhiBanHang(int MaSP, int SoLuong)
        {
            try
            {
                string sql = "UPDATE Product SET StockQuantity = StockQuantity - @SoLuong WHERE ProductID = @MASP";
                SqlConnection con = DataProvider.Openconnect();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@MASP", MaSP);
                cmd.Parameters.AddWithValue("@SOLUONG", SoLuong);
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
                // MessageBox.Show("Lỗi database: " + ex.Message);
                return false;
            }
        }
        #endregion

        #region Lấy Tổng Doanh Thu
        public double GetTongDoanhThu()
        {
            string sql = "SELECT SUM(THANHTIEN) FROM HOADON";
            DataTable dt = new DataTable();
            dt = DataProvider.GetTable(sql);
            double doanhthu = double.Parse(dt.Rows[0][0].ToString());
            return doanhthu;
        }
        #endregion

        #region Lấy Tổng Khách Hàng
        public int GetTongKhachHang()
        {
            string sql = "SELECT COUNT(*) FROM KHACHHANG";
            DataTable dt = new DataTable();
            dt = DataProvider.GetTable(sql);
            int kh = int.Parse(dt.Rows[0][0].ToString());
            return kh;
        }
        #endregion

        #region Lấy Top 10 Sản Phẩm
        //public List<ProductDTO> GetTop10SP(int top)
        //{
        //    string sql = "SELECT TOP " + top + " cthd.MASP,sp.TENSP,SUM(cthd.SOLUONG) FROM CTHD cthd JOIN SANPHAM sp ON cthd.MASP=sp.MASP GROUP BY cthd.MASP, sp.TENSP ORDER BY SUM(cthd.SOLUONG)";
        //    DataTable dt = new DataTable();
        //    List<ProductDTO> lstSP = new List<ProductDTO>();
        //    dt = DataProvider.GetTable(sql);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        ProductDTO spDTO = new ProductDTO();
        //        spDTO.masp = int.Parse(dt.Rows[i][0].ToString());
        //        spDTO.tensp = dt.Rows[i][1].ToString();
        //        spDTO.soluong = int.Parse(dt.Rows[i][2].ToString());

        //        lstSP.Add(spDTO);
        //    }
        //    return lstSP;
        //}
        #endregion

        #region Lấy Doanh Thu Hôm Nay
        public double GetDoanhThuHomNay()
        {
            try
            {
                string sql = "SELECT SUM(hd.THANHTIEN) FROM HOADON hd WHERE (YEAR(hd.NGAYLAP) = YEAR('" + DateTime.Now + "') AND MONTH(hd.NGAYLAP) = MONTH('" + DateTime.Now + "') AND DAY(hd.NGAYLAP) = DAY('" + DateTime.Now + "')) AND hd.DATHANHTOAN = '1'";
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


        public DataTable GetProductToPrint(int id)
        {
            string sql =
                @"SELECT pd.ProductName as ProductName,  oi.Quantity as Quantity, oi.Quantity*oi.UnitPrice as SubPrice, od.TotalPrice as TotalPrice, oi.OrderID as ID


                FROM [OrderItem] as oi
                join Product as pd on pd.ProductID = oi.ProductID
                join [Order] as od on od.OrderID = oi.OrderID
                join Customer as cus on cus.CustomerID = od.CustomerID
                where cus.CustomerID = " + id + " and od.OrderStatus = 2";
            DataTable dt = DataProvider.GetTable(sql);
            return dt;
        }
    }
}
