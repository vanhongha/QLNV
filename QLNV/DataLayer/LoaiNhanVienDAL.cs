using QLNV.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace QLNV.DataLayer
{
    class LoaiNhanVienDAL
    {
        public static string LayTenLoaiTheoMa(string maLoai)
        {
            LOAINHANVIEN loaiNhanVien = DataAccessHelper.DB.LOAINHANVIENs.Single(loaiNV => loaiNV.MaLoaiNV == maLoai);
            if(loaiNhanVien == null) return "";
            return loaiNhanVien.TenLoaiNV;
        }
    }
}
