using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{

    public class SupplierDL
    {
        public static int SelectedSupplierID { get; set; }


        private static SupplierDL Instance;
        public static SupplierDL GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new SupplierDL();
                }
                return Instance;
            }
        }


        #region Lấy Danh sach NCC
        public DataTable GetDanhSachNhaCungCap()
        {
            try
            {
                string sql = @"
              
                      SELECT [SupplierID]
                            ,[ContactName]
                            ,[ImgSupplier]
                            ,[Phone]
                            ,[Email]
                            ,[Address]
                            ,[CreatedAt]
                        FROM [WatchStoreC#].[dbo].[Supplier] WHERE [NGUNGHOPTAC]=1 ";

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

        #region Thêm NCC Full
        public bool ThemNCCFull(SupplierDTO nccDTO)
        {
            try
            {
                string contactName = nccDTO.ContactName;
                string phone = nccDTO.Phone;
                string email = nccDTO.Email;
                string address = nccDTO.Address;
                int ngungHopTac = 1;

                string sql = "INSERT INTO [dbo].[Supplier] (" +
                             "[ContactName], [Phone], [Email], [Address], [CreatedAt], [NGUNGHOPTAC]) " +
                             "VALUES (" +
                              "N'" + contactName + "', " +
                              "'" + phone + "', " +
                              "N'" + email + "', " +
                             "N'" + address + "', " +
                             "GETDATE(), " +
                             ngungHopTac + ")";


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

        #region Cập Nhật NCC
        public bool CapNhatNCC(SupplierDTO nccDTO)
        {
            try
            {


                string sql = "UPDATE Supplier " +
                    "SET ContactName = N'" + nccDTO.ContactName + "', " +
                    "Address = N'" + nccDTO.Address + "', " +
                    "Phone = N'" + nccDTO.Phone + "', " +
                    "Email = N'" + nccDTO.Email + "', " +
                    "CreatedAt = GETDATE(), " +
                    "NGUNGHOPTAC = 1 " +
                    "WHERE SupplierID = N'" + nccDTO.SupplierID + "'";




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

        #region Ngừng Hợp Tác NCC
        public bool NgungHopTacNCC(string MANCC)
        {
            try
            {
                string sql = "UPDATE Supplier SET NGUNGHOPTAC=0 WHERE SupplierID='" + MANCC + "'";
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


        #region Lấy Danh Sách NCC Cua tao
        public DataTable GetDanhSachNCC()
        {
            try
            {
                DataTable dt = new DataTable();
                string sql = "SELECT SupplierId ,[ContactName] ,Address AS N'Địa Chỉ NCC',Phone AS N'SĐT',Email AS 'Email' FROM Supplier where [NGUNGHOPTAC] = 1";
                dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi database: " + ex.Message);
                return null;
            }
        }
        #endregion
    }
}
