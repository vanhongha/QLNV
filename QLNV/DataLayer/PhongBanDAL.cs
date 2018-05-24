using System;
using System.Collections.Generic;
using QLNV.Entities;
using System.Data;
using System.Linq;

namespace QLNV.DataLayer
{
    class PhongBanDAL
    {
        public static string LayTenPBTheoMa(string maPB)
        {
            PHONGBAN phongBan = DataAccessHelper.DB.PHONGBANs.Single(pb => pb.MaPhong == maPB);
            if (phongBan == null) return "";
            return phongBan.TenPhong;
        }
    }
}
