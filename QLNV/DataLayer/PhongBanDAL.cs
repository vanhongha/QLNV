using System;
using System.Collections.Generic;
using QLNV.Entities;
using System.Data;

namespace QLNV.DataLayer
{
    class PhongBanDAL
    {
        public static List<PhongBan> GetList()
        {
            DataAccessHelper db = new DataAccessHelper();
            DataTable dt = db.GetDataTable("SELECT * FROM PHONGBAN");
            List<PhongBan> list = new List<PhongBan>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new PhongBan(row));
            }
            return list;
        }
        public static string LayTenPBTheoMa(string maPB)
        {
            DataAccessHelper db = new DataAccessHelper();
            DataTable dt = db.GetDataTable("Select TenPhong from PHONGBAN where MaPhong = '" + maPB + "'");
            foreach (DataRow row in dt.Rows)
            {
                return row.ItemArray[0].ToString();
            }
            return "";
        }
    }
}
