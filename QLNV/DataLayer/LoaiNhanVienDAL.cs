using QLNV.Entities;
using System.Collections.Generic;
using System.Data;


namespace QLNV.DataLayer
{
    class LoaiNhanVienDAL
    {
        public static List<LoaiNhanVien> GetList()
        {
            DataAccessHelper db = new DataAccessHelper();
            DataTable dt = db.GetDataTable("SELECT * FROM LOAINHANVIEN");
            List<LoaiNhanVien> list = new List<LoaiNhanVien>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new LoaiNhanVien(row));
            }
            return list;
        }

        public static string LayTenLoaiTheoMa(string maLoai)
        {
            DataAccessHelper db = new DataAccessHelper();
            DataTable dt = db.GetDataTable("Select TenLoaiNV from LOAINHANVIEN where MaLoaiNV = '"+ maLoai+ "'");
            foreach (DataRow row in dt.Rows)
            {
                return row.ItemArray[0].ToString();
            }
            return "";
        }
    }
}
