using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BrandDL
    {
        private static BrandDL Instance;
        public static BrandDL GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new BrandDL();
                }
                return Instance;
            }
        }

        private BrandDL() { }

        #region Lấy Danh Sách Nhan hang
        public DataTable GetDanhSachNhanHang()
        {
            try
            {
                string sql = "SELECT [BrandID]\r\n,[BrandName]\r\n,[ImgBrand]\r\nFROM [WatchStoreC#].[dbo].[Brand]";
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

    }
}
