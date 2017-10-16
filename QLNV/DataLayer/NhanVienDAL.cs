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

        public static bool CapNhatNhanVien(NhanVien NV)
        {
            try
            {
                DataAccessHelper db = new DataAccessHelper();
                SqlCommand cmd = db.Command("CapNhatNhanVien");

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
            }
            catch { return false; }
            return true;
        }

        public static void XoaNhanVien(string maNV)
        {
            try
            {
                DataAccessHelper db = new DataAccessHelper();
                SqlCommand cmd = db.Command("XoaNhanVien");

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaNV", maNV);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                db.dt = new DataTable();
                da.Fill(db.dt);
            }
            catch
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK);
            }
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
            DataTable dt = db.GetDataTable("SELECT * FROM NHANVIEN nv FULL OUTER JOIN LUONG l on nv.MaNV = l.MaNV");
            List<NhanVien> list = new List<NhanVien>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new NhanVien(row));
            }
            return list;
        }

        public static List<NhanVien> GetListTheoKey(DGVTypeLoad type, string maPhong = null, string maLoai = null, string thang = null, string nam = null)
        {
            DataAccessHelper db = new DataAccessHelper();
            DataTable dt = null;
            switch (type)
            {
                case DGVTypeLoad.Phong:
                     dt = db.GetDataTable("SELECT * FROM NHANVIEN nv FULL OUTER JOIN LUONG l on nv.MaNV = l.MaNV WHERE MAPHONG = '" + maPhong + "'");
                    break;
                case DGVTypeLoad.LoaiNV:
                    dt = db.GetDataTable("SELECT * FROM NHANVIEN nv FULL OUTER JOIN LUONG l on nv.MaNV = l.MaNV WHERE MaLoaiNV = '" + maLoai + "'");
                    break;
                case DGVTypeLoad.Luong:
                    dt = db.GetDataTable("SELECT * FROM NHANVIEN nv FULL OUTER JOIN LUONG l on nv.MaNV = l.MaNV ORDER BY l.Luong DESC");
                    break;
                case DGVTypeLoad.Loai_Phong:
                    dt = db.GetDataTable("SELECT * FROM NHANVIEN nv FULL OUTER JOIN LUONG l on nv.MaNV = l.MaNV WHERE MaLoaiNV = '" + maLoai + "' AND MAPHONG = '" + maPhong + "'");
                    break;
                case DGVTypeLoad.Luong_Phong:
                    dt = db.GetDataTable("SELECT * FROM NHANVIEN nv FULL OUTER JOIN LUONG l on nv.MaNV = l.MaNV WHERE MAPHONG = '" + maPhong + "' ORDER BY l.Luong DESC");
                    break;
                case DGVTypeLoad.Luong_Loai:
                    dt = db.GetDataTable("SELECT * FROM NHANVIEN nv FULL OUTER JOIN LUONG l on nv.MaNV = l.MaNV WHERE MaLoaiNV = '" + maLoai + "' ORDER BY l.Luong DESC");
                    break;
                case DGVTypeLoad.All:
                    dt = db.GetDataTable("SELECT * FROM NHANVIEN nv FULL OUTER JOIN LUONG l on nv.MaNV = l.MaNV WHERE MaLoaiNV = '" + maLoai + "' AND MAPHONG = '" + maPhong + "' ORDER BY l.Luong DESC");
                    break;
            }
          
            List<NhanVien> list = new List<NhanVien>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new NhanVien(row));
            }
            return list;
        }

        public static void ThemLuong(string maNV, int thang, int nam, decimal luong)
        {
            try
            {
                DataAccessHelper db = new DataAccessHelper();
                SqlCommand cmd = db.Command("ThemLuong");

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                cmd.Parameters.AddWithValue("@Thang", thang);
                cmd.Parameters.AddWithValue("@Nam", nam);
                cmd.Parameters.AddWithValue("@Luong", luong);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                db.dt = new DataTable();
                da.Fill(db.dt);
            }
            catch
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK);
            }
        }

        public static void CapNhatLuong(string maNV, int thang, int nam, decimal luong)
        {
            try
            {
                DataAccessHelper db = new DataAccessHelper();
                SqlCommand cmd = db.Command("CapNhatLuong");

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                cmd.Parameters.AddWithValue("@Thang", thang);
                cmd.Parameters.AddWithValue("@Nam", nam);
                cmd.Parameters.AddWithValue("@Luong", luong);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                db.dt = new DataTable();
                da.Fill(db.dt);
            }
            catch
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK);
            }
        }
    }
}
