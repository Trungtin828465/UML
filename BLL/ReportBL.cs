using DAL;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReportBL
    {
        private static ReportBL Instance;
        public static ReportBL GetInstance

        {
            get
            {
                if (Instance == null)
                {
                    Instance = new ReportBL();
                }
                return Instance;
            }
        }
        // doanh thu
        public DataTable GetHoaDon2023()
        {
            return ReportDL.GetInstance.GetHoaDon2023();
        }
        public DataTable GetHoaDon2024()
        {
            return ReportDL.GetInstance.GetHoaDon2024();
        }
        // tb hóa đơn
        public DataTable GetDoanhThu2023()
        {
            return ReportDL.GetInstance.GetDoanhThu2023();
        }
        public DataTable GetDoanhThu2024()
        {
            return ReportDL.GetInstance.GetDoanhThu2024();
        }
        // top product

        public DataTable GetTopProduct()
        {
            return ReportDL.GetInstance.GetTopProduct();
        }
        // min product
        public DataTable GetMinProduct()
        {
            return ReportDL.GetInstance.GetMinProduct();
        }
        public DataTable GetBrand()
        {
            return ReportDL.GetInstance.GetBrand();
        }
        public DataTable GetBrandAll()
        {
            return ReportDL.GetInstance.GetBrandAll();
        }
        /////////////////////////////
        ///
        public DataTable GetSLSK2023()
        {
            return ReportDL.GetInstance.GetSLSK2023();
        }
        public DataTable GetSLSK2024()
        {
            return ReportDL.GetInstance.GetSLSK2024();
        }
        public DataTable GetTopViecLam()
        {
            return ReportDL.GetInstance.GetTopViecLam();
        }
        public DataTable GetTopThamGiaSuKien()
        {
            return ReportDL.GetInstance.GetTopThamGiaSuKien();
        }
        public DataTable GetDSSuKien()
        {
            return ReportDL.GetInstance.GetDSSuKien();
        }

    }
}
