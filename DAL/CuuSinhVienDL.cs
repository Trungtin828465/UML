using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public  class CuuSinhVienDL
    {
        private static CuuSinhVienDL Instance;

        public static CuuSinhVienDL GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new CuuSinhVienDL();
                }
                return Instance;
            }
        }

        #region Lấy Danh Sách Cựu Sinh Viên
        public DataTable GetDanhCuuSinhVien()
        {
            try
            {
                string sql = @"
                SELECT TOP (1000) [MaSV]
                                  ,[HoTen]
                                  ,[NgaySinh]
                                  ,[GioiTinh]
                                  ,[KhoaHoc]
                                  ,[NganhHoc]
                                  ,[GPA]
                                  ,[Email]
                                  ,[SoDienThoai]
                                  ,[NoiLamViec]
                                  ,[KhoaHoc]
                                  ,[TenCongTy]
                                    ,[NganhHoc]
                  FROM [WatchStoreC#].[dbo].[CuuSinhVien] ";

                DataTable dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product list: " + ex.Message);
            }
        }
        #endregion

        #region Tìm Kiếm SV
        public DataTable TimKiemCuuSinhVien(string searchText)
        {
            try
            {
                string sql = "SELECT * FROM CuuSinhVien WHERE ( HoTen LIKE '%" + searchText + "%')";

                DataTable dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching employees: " + ex.Message);
            }
        }
        #endregion
        public DataTable TimKiemCuuSinhVienn(string searchText)
        {
            try
            {
                string sql = "SELECT " +
                             "chvl.ID, " +
                             "csv.MaSV, " +
                             "csv.HoTen, " +
                             "csv.Email, " +
                             "csv.SoDienThoai, " +
                             "chvl.ViTriTuyenDung, " +
                             "chvl.TenCongTy, " +
                             "chvl.NoiLamViec, " +
                             "chvl.ThoiGianDang, " +
                             "chvl.MoTaCongViec, " +
                             "chvl.NgayHetHan, " +
                             "chvl.TrangThaiTuyenDung, " +
                             "chvl.GhiChu " +
                             "FROM CoHoiViecLam AS chvl " +
                             "INNER JOIN CuuSinhVien AS csv ON chvl.MaSV = csv.MaSV " +
                             "WHERE csv.HoTen LIKE N'%" + searchText + "%'";

                DataTable dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching employees: " + ex.Message);
            }
        }



        #region Thêm Cựu Sinh Viên
        public bool AddCuuSinhVien(string maSV, string hoTen, DateTime ngaySinh, string gioiTinh, string khoaHoc,
                                  string nganhHoc, float gpa, string email, string soDienThoai, string noiLamViec, string tenCongTy)
        {
            try
            {
                string sql = @"
                INSERT INTO [WatchStoreC#].[dbo].[CuuSinhVien] 
                (MaSV, HoTen, NgaySinh, GioiTinh, KhoaHoc, NganhHoc, GPA, Email, SoDienThoai, NoiLamViec, TenCongTy)
                VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @KhoaHoc, @NganhHoc, @GPA, @Email, @SoDienThoai, @NoiLamViec, @TenCongTy)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaSV", maSV),
                    new SqlParameter("@HoTen", hoTen),
                    new SqlParameter("@NgaySinh", ngaySinh),
                    new SqlParameter("@GioiTinh", gioiTinh),
                    new SqlParameter("@KhoaHoc", khoaHoc),
                    new SqlParameter("@NganhHoc", nganhHoc),
                    new SqlParameter("@GPA", gpa),
                    new SqlParameter("@Email", email),
                    new SqlParameter("@SoDienThoai", soDienThoai),
                    new SqlParameter("@NoiLamViec", noiLamViec ?? (object)DBNull.Value), // Xử lý null
                    new SqlParameter("@TenCongTy", tenCongTy ?? (object)DBNull.Value)   // Xử lý null
                };

                int rowsAffected = DataProvider.JustExcuteWithParameter(sql, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding cựu sinh viên: " + ex.Message);
            }
        }
        #endregion

        #region Sửa Thông Tin Cựu Sinh Viên
        public bool UpdateCuuSinhVien(string maSV, string hoTen, DateTime ngaySinh, string gioiTinh, string khoaHoc,
                                     string nganhHoc, float gpa, string email, string soDienThoai, string noiLamViec, string tenCongTy)
        {
            try
            {
                string sql = @"
                UPDATE [WatchStoreC#].[dbo].[CuuSinhVien]
                SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, KhoaHoc = @KhoaHoc, 
                    NganhHoc = @NganhHoc, GPA = @GPA, Email = @Email, SoDienThoai = @SoDienThoai, 
                    NoiLamViec = @NoiLamViec, TenCongTy = @TenCongTy
                WHERE MaSV = @MaSV";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaSV", maSV),
                    new SqlParameter("@HoTen", hoTen),
                    new SqlParameter("@NgaySinh", ngaySinh),
                    new SqlParameter("@GioiTinh", gioiTinh),
                    new SqlParameter("@KhoaHoc", khoaHoc),
                    new SqlParameter("@NganhHoc", nganhHoc),
                    new SqlParameter("@GPA", gpa),
                    new SqlParameter("@Email", email),
                    new SqlParameter("@SoDienThoai", soDienThoai),
                    new SqlParameter("@NoiLamViec", noiLamViec ?? (object)DBNull.Value), // Xử lý null
                    new SqlParameter("@TenCongTy", tenCongTy ?? (object)DBNull.Value)   // Xử lý null
                };

                int rowsAffected = DataProvider.JustExcuteWithParameter(sql, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating cựu sinh viên: " + ex.Message);
            }
        }
        #endregion
        //.. cơ hội việc làm

        public DataTable GetDSCoHoiVieclam()
        {
            try
            {
                string sql = @"
               SELECT TOP (1000) chvl.[ID]
                      ,chvl.[MaSV]
	                  ,csv.HoTen
	                  ,csv.Email
	                  ,csv.SoDienThoai
                      ,chvl.[ViTriTuyenDung]
                      ,chvl.[TenCongTy]
                      ,chvl.[NoiLamViec]
                      ,chvl.[ThoiGianDang]
                      ,chvl.[MoTaCongViec]
                      ,chvl.[NgayHetHan]
                      ,chvl.[TrangThaiTuyenDung]
                      ,chvl.[GhiChu]
                  FROM [WatchStoreC#].[dbo].[CoHoiViecLam] as chvl , CuuSinhVien as csv
                  Where chvl.MaSV = csv.MaSV ";

                DataTable dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product list: " + ex.Message);
            }
        }
        //
        public bool UpdateViecLam(int id, string maSV,
            string viTriTuyenDung, string tenCongTy, string noiLamViec, DateTime thoiGianDang,
            string moTaCongViec, DateTime ngayHetHan, int trangThaiTuyenDung, string ghiChu)
        {
            try
            {
                string sql = @"
               UPDATE CoHoiViecLam
                    SET MaSV = @MaSV,
                        ViTriTuyenDung = @ViTriTuyenDung, TenCongTy = @TenCongTy, 
                        NoiLamViec = @NoiLamViec, ThoiGianDang = @ThoiGianDang, 
                        MoTaCongViec = @MoTaCongViec, NgayHetHan = @NgayHetHan, 
                        TrangThaiTuyenDung = @TrangThaiTuyenDung, GhiChu = @GhiChu
                    WHERE ID = @ID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", id),
                    new SqlParameter("@MaSV", maSV),
                 
                    new SqlParameter("@ViTriTuyenDung", viTriTuyenDung),
                    new SqlParameter("@TenCongTy", tenCongTy),
                    new SqlParameter("@NoiLamViec", noiLamViec),
                    new SqlParameter("@ThoiGianDang", thoiGianDang),
                    new SqlParameter("@MoTaCongViec", moTaCongViec),
                    new SqlParameter("@NgayHetHan", ngayHetHan),
                    new SqlParameter("@TrangThaiTuyenDung", trangThaiTuyenDung),
                    new SqlParameter("@GhiChu", ghiChu ?? (object)DBNull.Value)
                };

                int rowsAffected = DataProvider.JustExcuteWithParameter(sql, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating cựu sinh viên: " + ex.Message);
            }
        }
        //
        public bool AddViecLam(string maSV, 
            string viTriTuyenDung, string tenCongTy, string noiLamViec, DateTime thoiGianDang,
            string moTaCongViec, DateTime ngayHetHan, int trangThaiTuyenDung, string ghiChu)
        {
            try
            {
                string sql = @"
               INSERT INTO CoHoiVieclam (MaSV, ViTriTuyenDung, 
                        TenCongTy, NoiLamViec, ThoiGianDang, MoTaCongViec, NgayHetHan, 
                        TrangThaiTuyenDung, GhiChu)
                    VALUES (@MaSV, @ViTriTuyenDung, 
                        @TenCongTy, @NoiLamViec, @ThoiGianDang, @MoTaCongViec, @NgayHetHan, 
                        @TrangThaiTuyenDung, @GhiChu)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaSV", maSV),
                   
                    new SqlParameter("@ViTriTuyenDung", viTriTuyenDung),
                    new SqlParameter("@TenCongTy", tenCongTy),
                    new SqlParameter("@NoiLamViec", noiLamViec),
                    new SqlParameter("@ThoiGianDang", thoiGianDang),
                    new SqlParameter("@MoTaCongViec", moTaCongViec),
                    new SqlParameter("@NgayHetHan", ngayHetHan),
                    new SqlParameter("@TrangThaiTuyenDung", trangThaiTuyenDung),
                    new SqlParameter("@GhiChu", ghiChu ?? (object)DBNull.Value)

                };

                int rowsAffected = DataProvider.JustExcuteWithParameter(sql, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding cựu sinh viên: " + ex.Message);
            }
        }
    }
}
