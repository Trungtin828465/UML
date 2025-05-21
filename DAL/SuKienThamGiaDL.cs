using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class SuKienThamGiaDL
    {
        private static SuKienThamGiaDL Instance;

        public static SuKienThamGiaDL GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new SuKienThamGiaDL();
                }
                return Instance;
            }
        }

        #region Lấy Danh Sách Cựu Sinh Viên
        public DataTable GetDanhSuKien()
        {
            try
            {
                string sql = @"
               SELECT
                         sktg.ID ,
                      sktg.MaSV AS SuKienThamGia_MaSV,
                      csv.HoTen AS CuuSinhVien_HoTen,
                      csv.Email AS CuuSinhVien_Email,
                      csv.SoDienThoai AS CuuSinhVien_SoDienThoai,
                      sktg.TenSuKien AS SuKienThamGia_TenSuKien,
                      sktg.MoTa AS SuKienThamGia_MoTa,
                      sktg.ThoiGian AS SuKienThamGia_ThoiGian,
                      sktg.DiaDiem AS SuKienThamGia_DiaDiem,
                      sktg.TrangThaiThamGia AS SuKienThamGia_TrangThaiThamGia,
                      sktg.GhiChu AS SuKienThamGia_GhiChu
                  FROM
                      SuKienThamGia sktg  
                  LEFT JOIN
                      CuuSinhVien csv ON sktg.MaSV = csv.MaSV 
                  WHERE csv.MaSV IN (SELECT MaSV FROM CuuSinhVien WHERE checkTao != 1)
                  ORDER BY sktg.MaSV;";

                DataTable dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product list: " + ex.Message);
            }
        }
        #endregion
        #region Lấy Tên sự kiên
        public DataTable GetTenSuKien()
        {
            try
            {
                string sql = @"

                    SELECT DISTINCT TenSuKien
                    FROM SuKienThamGia;

                    ";

                DataTable dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product list: " + ex.Message);
            }
        }

        public DataTable GetMSSV()
        {
            try
            {
                string sql = @"

                    SELECT [MaSV]
                          ,[HoTen]
                          ,[SoDienThoai],
                           [Email]
                    FROM CuuSinhVien;

                    ";

                DataTable dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product list: " + ex.Message);
            }
        }
        public DataTable GetMSSVV()
        {
            try
            {
                string sql = @"

                    SELECT [MaSV] as MSSVV
                          ,[HoTen]
                          ,[SoDienThoai],
                           [Email]
                    FROM CuuSinhVien;

                    ";

                DataTable dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product list: " + ex.Message);
            }
        }

        #endregion
        //
        #region Tìm Kiếm tên Sự kiện
        public DataTable TimKiemSuKien(string searchText)
        {
            try
            { 
                string sql = "SELECT * FROM SuKienThamGia WHERE ( TenSuKien LIKE '%" + searchText + "%')";

                DataTable dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching employees: " + ex.Message);
            }
        }
        #endregion




        #region Thêm SV vào SK
        public bool AddSVSuKien(string maSV, string tenSuKien, string moTa, DateTime thoiGian, string diaDiem, int? trangThaiThamGia, string ghiChu)
        {
            try
            {
                string sql = @"
                    INSERT INTO [WatchStoreC#].[dbo].[SuKienThamGia] 
                    (MaSV, TenSuKien, MoTa, ThoiGian, DiaDiem, TrangThaiThamGia, GhiChu)
                    VALUES (@MaSV, @TenSuKien, @MoTa, @ThoiGian, @DiaDiem, @TrangThaiThamGia, @GhiChu)";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaSV", (object)maSV ?? DBNull.Value),
                    new SqlParameter("@TenSuKien", (object)tenSuKien ?? DBNull.Value),
                    new SqlParameter("@MoTa", (object)moTa ?? DBNull.Value),
                    new SqlParameter("@ThoiGian", thoiGian),
                    new SqlParameter("@DiaDiem", (object)diaDiem ?? DBNull.Value),
                    new SqlParameter("@TrangThaiThamGia", (object)trangThaiThamGia ?? DBNull.Value),
                    new SqlParameter("@GhiChu", (object)ghiChu ?? DBNull.Value),
                };

                int rowsAffected = DataProvider.JustExcuteWithParameter(sql, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding cựu sinh viên sự kiện: " + ex.Message);
            }
        }
        #endregion

        #region Cập nhật SV trong SK
        public bool UpdateSVSuKien(int id, string maSV, string tenSuKien, string moTa, DateTime thoiGian, string diaDiem, int? trangThaiThamGia, string ghiChu)
        {
            try
            {
                string sql = @"
                    UPDATE [WatchStoreC#].[dbo].[SuKienThamGia]
                    SET MaSV = @MaSV,
                        TenSuKien = @TenSuKien,
                        MoTa = @MoTa,
                        ThoiGian = @ThoiGian,
                        DiaDiem = @DiaDiem,
                        TrangThaiThamGia = @TrangThaiThamGia,
                        GhiChu = @GhiChu
                    WHERE ID = @ID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", id),
                    new SqlParameter("@MaSV", (object)maSV ?? DBNull.Value),
                    new SqlParameter("@TenSuKien", (object)tenSuKien ?? DBNull.Value),
                    new SqlParameter("@MoTa", (object)moTa ?? DBNull.Value),
                    new SqlParameter("@ThoiGian", thoiGian),
                    new SqlParameter("@DiaDiem", (object)diaDiem ?? DBNull.Value),
                    new SqlParameter("@TrangThaiThamGia", (object)trangThaiThamGia ?? DBNull.Value),
                    new SqlParameter("@GhiChu", (object)ghiChu ?? DBNull.Value),
                };

                int rowsAffected = DataProvider.JustExcuteWithParameter(sql, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating cựu sinh viên sự kiện: " + ex.Message);
            }
        }
        #endregion
        // them sk
        public DataTable GetSuKien()
        {
            try
            {
                string sql = @"

                   
		         SELECT DISTINCT TenSuKien,MoTa, ThoiGian,DiaDiem
                 FROM SuKienThamGia
                 WHERE CheckTao =1;


                    ";

                DataTable dt = DataProvider.GetTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product list: " + ex.Message);
            }
        }
      
        public int AddSuKien(string tenSuKien, string moTa, DateTime thoiGian, string diaDiem, int? trangThai)
        {
            try
            {
                string sql = @"
                    INSERT INTO [WatchStoreC#].[dbo].[SuKienThamGia] 
                    (TenSuKien, MoTa, ThoiGian, DiaDiem, CheckTao)
                    VALUES (@TenSuKien, @MoTa, @ThoiGian, @DiaDiem, @CheckTao);
                    SELECT SCOPE_IDENTITY();"; // Trả về ID của bản ghi vừa thêm

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenSuKien", (object)tenSuKien ?? DBNull.Value),
                    new SqlParameter("@MoTa", (object)moTa ?? DBNull.Value),
                    new SqlParameter("@ThoiGian", thoiGian),
                    new SqlParameter("@DiaDiem", (object)diaDiem ?? DBNull.Value),
                    new SqlParameter("@CheckTao", 1) // Đặt CheckTao = 1 cho sự kiện không có MSSV
                };

                object result = DataProvider.JustExcuteWithParameter(sql, parameters);
                if (result != null && int.TryParse(result.ToString(), out int newId))
                {
                    return newId; // Trả về ID của bản ghi vừa thêm
                }
                return -1; // Trả về -1 nếu thêm thất bại
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding sự kiện: " + ex.Message);
            }
        }

        // Phương thức cập nhật sự kiện
        public int UpdateSuKien(int id, string tenSuKien, string moTa, DateTime thoiGian, string diaDiem)
        {
            try
            {
                string sql = @"
                    UPDATE [WatchStoreC#].[dbo].[SuKienThamGia]
                    SET 
                        TenSuKien = @TenSuKien,
                        MoTa = @MoTa,
                        ThoiGian = @ThoiGian,
                        DiaDiem = @DiaDiem
                    WHERE ID = @ID";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", id),
                    new SqlParameter("@TenSuKien", (object)tenSuKien ?? DBNull.Value),
                    new SqlParameter("@MoTa", (object)moTa ?? DBNull.Value),
                    new SqlParameter("@ThoiGian", thoiGian),
                    new SqlParameter("@DiaDiem", (object)diaDiem ?? DBNull.Value)
                };

                int rowsAffected = DataProvider.JustExcuteWithParameter(sql, parameters);
                return rowsAffected; // Trả về số dòng bị ảnh hưởng
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating sự kiện: " + ex.Message);
            }
        }
        // lấy email
        public List<string> GetEmailList()
        {
            try
            {
                string sql = "SELECT DISTINCT Email FROM CuuSinhVien WHERE Email IS NOT NULL";
                DataTable dt = DataProvider.GetTable(sql);
                return dt.AsEnumerable().Select(row => row["Email"]?.ToString()).Where(email => !string.IsNullOrEmpty(email)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving email list: " + ex.Message);
            }
        }

    }
}