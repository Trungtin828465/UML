using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class CuuSinhVienBL
    {
        private static CuuSinhVienBL Instance;
        public static CuuSinhVienBL GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new CuuSinhVienBL();
                }
                return Instance;
            }
        }

       
        //Lay danh sách SP
        public DataTable GetDanhCuuSinhVien()
        {
            return CuuSinhVienDL.GetInstance.GetDanhCuuSinhVien();
        }
        public DataTable TimKiemCuuSinhVien(string searchText)
        {
            return CuuSinhVienDL.GetInstance.TimKiemCuuSinhVien(searchText);
        }
        public DataTable TimKiemCuuSinhVienn(string searchText)
        {
            return CuuSinhVienDL.GetInstance.TimKiemCuuSinhVienn(searchText);
        }


        public bool AddCuuSinhVien(string maSV, string hoTen, DateTime ngaySinh, string gioiTinh, string khoaHoc,
                                  string nganhHoc, float gpa, string email, string soDienThoai, string noiLamViec, string tenCongTy)
        {
            return CuuSinhVienDL.GetInstance.AddCuuSinhVien(maSV, hoTen, ngaySinh, gioiTinh, khoaHoc, nganhHoc, gpa, email, soDienThoai, noiLamViec, tenCongTy);
        }

        public bool UpdateCuuSinhVien(string maSV, string hoTen, DateTime ngaySinh, string gioiTinh, string khoaHoc,
                                     string nganhHoc, float gpa, string email, string soDienThoai, string noiLamViec, string tenCongTy)
        {
            return CuuSinhVienDL.GetInstance.UpdateCuuSinhVien(maSV, hoTen, ngaySinh, gioiTinh, khoaHoc, nganhHoc, gpa, email, soDienThoai, noiLamViec, tenCongTy);
        }
        //// co hoi việc làm
         public DataTable GetDSCoHoiVieclam()
        {
            return CuuSinhVienDL.GetInstance.GetDSCoHoiVieclam();
        }
        public bool AddViecLam(string maSV, 
            string viTriTuyenDung, string tenCongTy, string noiLamViec, DateTime thoiGianDang,
            string moTaCongViec, DateTime ngayHetHan, int trangThaiTuyenDung, string ghiChu)
        {
           
            return CuuSinhVienDL.GetInstance.AddViecLam(maSV,
                viTriTuyenDung, tenCongTy, noiLamViec, thoiGianDang, moTaCongViec,
                ngayHetHan, trangThaiTuyenDung, ghiChu);
        }
        public bool UpdateViecLam(int id, string maSV, 
            string viTriTuyenDung, string tenCongTy, string noiLamViec, DateTime thoiGianDang,
            string moTaCongViec, DateTime ngayHetHan, int trangThaiTuyenDung, string ghiChu)
        {
           

            return CuuSinhVienDL.GetInstance.UpdateViecLam(id, maSV, 
                viTriTuyenDung, tenCongTy, noiLamViec, thoiGianDang, moTaCongViec,
                ngayHetHan, trangThaiTuyenDung, ghiChu);
        }
    }
}
