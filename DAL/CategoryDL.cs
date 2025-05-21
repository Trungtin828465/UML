using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoryDL
    {
        private static CategoryDL Instance;
        public static CategoryDL GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new CategoryDL();
                }
                return Instance;
            }
        }
        private CategoryDL() { }

        #region Lấy Danh Sách Loại Sản Phẩm
        public DataTable GetDanhSachLoaiSanPham()
        {
            try
            {
                string sql = "SELECT [CategoryID],[CategoryName]  FROM [WatchStoreC#].[dbo].[Category] WHERE [NGUNGHOPTAC]=1 ";
                DataTable dt = new DataTable();
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
              //  MessageBox.Show("Lỗi database: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region Thêm Loại Sản Phẩm
        public bool ThemLoaiSanPham(CategoryDTO lspDTO)
        {
            try
            {
                string sql = "INSERT INTO Category VALUES(N'" + lspDTO.CategoryName + "',1)";
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
        #region Ngừng Kinh Doanh Sản Phẩm
        public bool NgungKinhDoanh(string MALOAISP)
        {
            try
            {
                string sql = "UPDATE Category SET [NGUNGHOPTAC]=0 WHERE CategoryID='" + MALOAISP + "'";
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
              //  MessageBox.Show("Lỗi database: " + ex.Message);
                return false;
            }
        }
        #endregion

        #region Cập Nhật Loại Sản Phẩm
        public bool CapNhatLoaiSanPham(CategoryDTO lspDTO)
        {
            try
            {
                string sql = "UPDATE Category SET CategoryName=N'" + lspDTO.CategoryName + "' WHERE CategoryID='" + lspDTO.CategoryID + "'";
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
             //   MessageBox.Show("Lỗi database: " + ex.Message);
                return false;
            }
        }
        #endregion


    }
}