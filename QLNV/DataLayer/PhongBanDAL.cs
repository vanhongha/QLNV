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
    }
}
