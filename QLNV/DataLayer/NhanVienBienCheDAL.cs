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
    class NhanVienBienCheDAL
    {
        public static void ThemNhanVienBC(NhanVienBienChe NV)
        {
            DataAccessHelper db = new DataAccessHelper();
            SqlCommand cmd = db.Command("ThemNhanVienBienChe");

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaNV", NV.MaNV);
            cmd.Parameters.AddWithValue("@BacLuong", NV.BacLuong);
            cmd.Parameters.AddWithValue("@PhuCap", NV.PhuCap);
            cmd.Parameters.AddWithValue("@LuongThang", NV.LuongThang);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            db.dt = new DataTable();
            da.Fill(db.dt);
        }

        public static void CapNhatNhanVienBC(NhanVienBienChe NV)
        {
            DataAccessHelper db = new DataAccessHelper();
            SqlCommand cmd = db.Command("CapNhatNhanVienBienChe");

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaNV", NV.MaNV);
            cmd.Parameters.AddWithValue("@BacLuong", NV.BacLuong);
            cmd.Parameters.AddWithValue("@PhuCap", NV.PhuCap);
            cmd.Parameters.AddWithValue("@LuongThang", NV.LuongThang);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            db.dt = new DataTable();
            da.Fill(db.dt);
        }
    }
}
