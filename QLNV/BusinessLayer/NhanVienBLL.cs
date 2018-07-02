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

        public static bool ThemNV(NHANVIEN NV)
        {
            return NhanVienDAL.ThemNhanVien(NV);
        }

        public static bool ThemNVBienChe(NHANVIENBIENCHE NV)
        {
            return NhanVienDAL.ThemNVBienChe(NV);
        }

        public static bool ThemNVCongNhat(NHANVIENCONGNHAT NV)
        {
            return NhanVienDAL.ThemNVCongNhat(NV);
        }


        public static bool CapNhatNhanVien(NHANVIEN NV)
        {
            return NhanVienDAL.CapNhatNhanVien(NV);
        }

        public static void XoaNhanVien(string maNV)
        {
            NhanVienDAL.XoaNhanVien(maNV);
        }

        public static dynamic GetList()
        {
            return NhanVienDAL.GetList();
        }

        public static dynamic GetList(bool locLuong, string maPhong, string maLoai, int thang, int nam)
        {
            return NhanVienDAL.GetList(locLuong, maPhong, maLoai, thang, nam);
        }

        public static void ThemLuong(NHANVIEN NV, int thang, int nam, decimal luong)
        {
            NhanVienDAL.ThemLuong(NV, thang, nam, luong);
        }

    }
}
