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
            cmd.Parameters.AddWithValue("@MaLoaiNV", NV.MaLoaiNV);

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

        public static List<NhanVien> GetList()
        {
            DataAccessHelper db = new DataAccessHelper();
            DataTable dt = db.GetDataTable("SELECT * FROM NHANVIEN");
            List<NhanVien> list = new List<NhanVien>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new NhanVien(row));
            }
            return list;
        }

        public static List<NhanVien> GetListTheoKey(DGVTypeLoad type, string maPhong = null, string maLoai = null, string thang = null)
        {
            DataAccessHelper db = new DataAccessHelper();
            DataTable dt = null;
            switch (type)
            {
                case DGVTypeLoad.Phong:
                     dt = db.GetDataTable("SELECT * FROM NHANVIEN WHERE MAPHONG = '" + maPhong + "'");
                    break;
                case DGVTypeLoad.LoaiNV:
                    dt = db.GetDataTable("SELECT * FROM NHANVIEN WHERE MaLoaiNV = '" + maLoai + "'");
                    break;
            }
          
            List<NhanVien> list = new List<NhanVien>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new NhanVien(row));
            }
            return list;
        }
    }
}
