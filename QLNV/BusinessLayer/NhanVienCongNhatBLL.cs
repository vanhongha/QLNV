using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLNV.DataLayer;
using QLNV.Entities;

namespace QLNV.BusinessLayer
{
    class NhanVienCongNhatBLL
    {
        public static void ThemNhanVienCN(NhanVienCongNhat NV)
        {
            NhanVienCongNhatDAL.ThemNhanVienCN(NV);
        }
        public static void CapNhatNhanVienCN(NhanVienCongNhat NV)
        {
            NhanVienCongNhatDAL.CapNhatNhanVienCN(NV);
        }
    }
}
