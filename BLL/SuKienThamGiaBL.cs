using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SuKienThamGiaBL
    {
        private static SuKienThamGiaBL Instance;
        public static SuKienThamGiaBL GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new SuKienThamGiaBL();
                }
                return Instance;
            }
        }


        //Lay danh sách SP
        public DataTable GetDanhSuKien()
        {
            return SuKienThamGiaDL.GetInstance.GetDanhSuKien();
        }
        //  Lay danh sách ten SK
        public DataTable GetTenSuKien()
        {
            return SuKienThamGiaDL.GetInstance.GetTenSuKien();
        }
        public DataTable GetMSSV()
        {
            return SuKienThamGiaDL.GetInstance.GetMSSV();
        }
        public DataTable GetMSSVV()
        {
            return SuKienThamGiaDL.GetInstance.GetMSSVV();
        }
        public DataTable TimKiemSuKien(string searchText)
        {
            return SuKienThamGiaDL.GetInstance.TimKiemSuKien(searchText);
        }
        #region Thêm SV vào SK
        public bool AddSVSuKien(string maSV, string tenSuKien, string moTa, DateTime thoiGian, string diaDiem, int? trangThaiThamGia, string ghiChu)
        {
            try
            {
                return SuKienThamGiaDL.GetInstance.AddSVSuKien(maSV, tenSuKien, moTa, thoiGian, diaDiem, trangThaiThamGia, ghiChu);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding cựu sinh viên sự kiện từ BL: " + ex.Message);
            }
        }
        #endregion

        #region Cập nhật SV trong SK
        public bool UpdateSVSuKien(int id, string maSV, string tenSuKien, string moTa, DateTime thoiGian, string diaDiem, int? trangThaiThamGia, string ghiChu)
        {
            try
            {
                return SuKienThamGiaDL.GetInstance.UpdateSVSuKien(id, maSV, tenSuKien, moTa, thoiGian, diaDiem, trangThaiThamGia, ghiChu);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating cựu sinh viên sự kiện từ BL: " + ex.Message);
            }
        }
        #endregion
        // them sk
        public DataTable GetSuKien()
        {
            return SuKienThamGiaDL.GetInstance.GetSuKien();
        }
        public bool AddSuKien(string tenSuKien, string moTa, DateTime thoiGian, string diaDiem, int? trangThai)
        {
            return SuKienThamGiaDL.GetInstance.AddSuKien(tenSuKien, moTa, thoiGian, diaDiem, trangThai) > 0;
        }

        public bool UpdateSuKien(int id, string tenSuKien, string moTa, DateTime thoiGian, string diaDiem)
        {
            return SuKienThamGiaDL.GetInstance.UpdateSuKien(id, tenSuKien, moTa, thoiGian, diaDiem) > 0;
        }
        // lấy email
        //public DataTable GetEmail()
        //{
        //    return SuKienThamGiaDL.GetInstance.GetEmail();
        //}
        public List<string> GetEmailList()
        {
            return SuKienThamGiaDL.GetInstance.GetEmailList();
        }
    }
}