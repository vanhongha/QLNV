using System.Text;
using System.Threading.Tasks;
using QLNV.DataLayer;
using QLNV.Entities;

namespace QLNV.BusinessLayer
{
    class NhanVienBienCheBLL
    {
        public static void ThemNhanVienBC(NhanVienBienChe NV)
        {
            NhanVienBienCheDAL.ThemNhanVienBC(NV);
        }
        public static void CapNhatNhanVienBC(NhanVienBienChe NV)
        {
            NhanVienBienCheDAL.CapNhatNhanVienBC(NV);
        }
    }
}
