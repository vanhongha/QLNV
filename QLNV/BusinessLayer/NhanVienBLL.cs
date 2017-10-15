using QLNV.DataLayer;
using QLNV.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNV.BusinessLayer
{
    class NhanVienBLL
    {
        public static string AutoMaNV()
        {
            string id = NhanVienDAL.GetLastID().Trim();
            if (id == "")
            {
                return "MANV000001";
            }
            int nextID = int.Parse(id.Remove(0, "MANV".Length)) + 1;
            id = "000000" + nextID.ToString();
            id = id.Substring(id.Length -6, 6);
            return "MANV" + id;
        }

        public static bool ThemNV(NhanVien NV)
        {
            DataTable dt = NhanVienDAL.ThemNhanVien(NV);
            //foreach (DataRow row in dt.Rows)
            //{
            //    if (row.ItemArray[0].ToString().Trim() == "0")
            //        return true;
            //}
            //return false;
            return true;
        }

        public static bool ThemNV(NhanVienBienChe NV)
        {
            DataTable dt = NhanVienDAL.ThemNhanVien(NV);
            return true;
        }

        public static bool ThemNV(NhanVienCongNhat NV)
        {
            DataTable dt = NhanVienDAL.ThemNhanVien(NV);
            return true;
        }
        public static bool CapNhatNhanVien(NhanVien NV)
        {
            return NhanVienDAL.CapNhatNhanVien(NV);
        }

        public static void XoaNhanVien(string maNV)
        {
            NhanVienDAL.XoaNhanVien(maNV);
        }

        public static List<NhanVien> GetList()
        {
            return NhanVienDAL.GetList();
        }

        public static List<NhanVien> GetListTheoKey(DGVTypeLoad type, string maPhong = null, string loaiLoai = null, string thang = null)
        {
            return NhanVienDAL.GetListTheoKey(type, maPhong, loaiLoai, thang);
        }
        
    }
}
