using QLNV.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNV.DataLayer
{
    class NhanVienDAL
    {
        public static DataTable ThemNhanVien(NhanVien NV)
        {
            DataAccessHelper db = new DataAccessHelper();
            SqlCommand cmd = db.Command("ThemNhanVien");

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaNV", NV.MaNV);
            cmd.Parameters.AddWithValue("@TenNV", NV.HoTen);
            cmd.Parameters.AddWithValue("@SoDT", NV.SDT);
            cmd.Parameters.AddWithValue("@NgaySinh", NV.NgaySinh);
            cmd.Parameters.AddWithValue("@MaPhong", NV.MaPhong);
            cmd.Parameters.AddWithValue("@MaLoaiNV", NV.LoaiNhanVien.MaLoai);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            db.dt = new DataTable();
            da.Fill(db.dt);
            return db.dt;
        }

        public static string GetLastID()
        {
            DataAccessHelper db = new DataAccessHelper();
            DataTable dt = db.GetDataTable("Select top 1 MaNV from NHANVIEN order by MaNV desc");
            foreach (DataRow row in dt.Rows)
            {
                return row.ItemArray[0].ToString();
            }
            return "";
        }
    }
}
