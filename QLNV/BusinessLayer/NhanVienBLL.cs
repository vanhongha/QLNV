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
                return "NV00000001";
            }
            int nextID = int.Parse(id.Remove(0, "NV".Length)) + 1;
            id = "0000000" + nextID.ToString();
            id = id.Substring(id.Length -8, 8);
            return "NV" + id;
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
