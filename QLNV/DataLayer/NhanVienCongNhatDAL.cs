using QLNV.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNV.DataLayer
{
    class NhanVienCongNhatDAL
    {
        public static void ThemNhanVienCN(NhanVienCongNhat NV)
        {
            DataAccessHelper db = new DataAccessHelper();
            SqlCommand cmd = db.Command("ThemNhanVienCongNhat");

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaNV", NV.MaNV);
            cmd.Parameters.AddWithValue("@LuongNgay", NV.SoNgayLam);
            cmd.Parameters.AddWithValue("@SoNgayLam", NV.LuongNgay);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            db.dt = new DataTable();
            da.Fill(db.dt);
        }

        public static void CapNhatNhanVienCN(NhanVienCongNhat NV)
        {
            DataAccessHelper db = new DataAccessHelper();
            SqlCommand cmd = db.Command("CapNhatNhanVienCongNhat");

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaNV", NV.MaNV);
            cmd.Parameters.AddWithValue("@LuongNgay", NV.SoNgayLam);
            cmd.Parameters.AddWithValue("@SoNgayLam", NV.LuongNgay);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            db.dt = new DataTable();
            da.Fill(db.dt);
        }
    }
}
