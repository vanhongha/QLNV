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

        public static bool ThemNhanVien(NHANVIEN NV)
        {
            var CheckExistUser = DataAccessHelper.DB.NHANVIENs.FirstOrDefault(s => s.MaNV == NV.MaNV);

            if (CheckExistUser != null)
            {
                return false;
            }

            DataAccessHelper.DB.NHANVIENs.InsertOnSubmit(NV);
            DataAccessHelper.DB.SubmitChanges();
            return true;
        }

        public static bool ThemNVBienChe(NHANVIENBIENCHE NV)
        {
            var CheckExistUser = DataAccessHelper.DB.NHANVIENBIENCHEs.FirstOrDefault(s => s.MaNV == NV.MaNV);

            if (CheckExistUser != null)
            {
                return false;
            }

            DataAccessHelper.DB.NHANVIENBIENCHEs.InsertOnSubmit(NV);
            DataAccessHelper.DB.SubmitChanges();
            return true;
        }

        public static bool ThemNVCongNhat(NHANVIENCONGNHAT NV)
        {
            var CheckExistUser = DataAccessHelper.DB.NHANVIENCONGNHATs.FirstOrDefault(s => s.MaNV == NV.MaNV);

            if (CheckExistUser != null)
            {
                return false;
            }

            DataAccessHelper.DB.NHANVIENCONGNHATs.InsertOnSubmit(NV);
            DataAccessHelper.DB.SubmitChanges();
            return true;
        }

        public static LUONG GetLuong(string maNV)
        {
           var luong = DataAccessHelper.DB.LUONGs.FirstOrDefault(s => s.MaNV == maNV);
            return luong;
        }

        public static bool CapNhatNhanVien(NHANVIEN nv)
        {
            NHANVIEN NV = DataAccessHelper.DB.NHANVIENs.Single(_nv => _nv.MaNV == nv.MaNV);

            if (NV == null)
                return false;

            NV.TenNV = nv.TenNV;
            NV.SoDT = nv.SoDT;
            NV.NgaySinh = nv.NgaySinh;
            NV.MaLoaiNV = nv.MaLoaiNV;
            NV.MaPhong = nv.MaPhong;

            DataAccessHelper.DB.SubmitChanges();

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

        public static dynamic GetList()
        {
            var list = from nv in DataAccessHelper.DB.NHANVIENs
                       join luong in DataAccessHelper.DB.LUONGs on nv.MaNV equals luong.MaNV
                       orderby nv.MaNV
                       select new
                       {
                           nv.MaNV,
                           nv.TenNV,
                           nv.NgaySinh,
                           nv.SoDT,
                           nv.MaLoaiNV,
                           luong.Luong1,
                           nv.MaPhong
                       };
            return list;
        }

        public static NHANVIENBIENCHE GetNhanVienBienChe(string maNV)
        {
            var NV = from nv in DataAccessHelper.DB.NHANVIENBIENCHEs
                     where nv.MaNV == maNV
                     select nv;

            return NV.FirstOrDefault();
        }

        public static NHANVIENCONGNHAT GetNhanVienCongNhat(string maNV)
        {
            var NV = from nv in DataAccessHelper.DB.NHANVIENCONGNHATs
                     where nv.MaNV == maNV
                     select nv;

            return NV.FirstOrDefault();
        }

        public static NHANVIEN GetNhanVien(string maNV)
        {
            var NV = from nv in DataAccessHelper.DB.NHANVIENs
                     where nv.MaNV == maNV
                     select nv;

            if (NV == null)
                return null;

            return NV.FirstOrDefault();
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
                    dt = db.GetDataTable("SELECT * FROM NHANVIEN nv FULL OUTER JOIN LUONG l on nv.MaNV = l.MaNV WHERE l.Thang = '" + thang + "' AND l.Nam = '" + nam + "' ORDER BY l.Luong DESC");
                    break;
                case DGVTypeLoad.Loai_Phong:
                    dt = db.GetDataTable("SELECT * FROM NHANVIEN nv FULL OUTER JOIN LUONG l on nv.MaNV = l.MaNV WHERE MaLoaiNV = '" + maLoai + "' AND MAPHONG = '" + maPhong + "'");
                    break;
                case DGVTypeLoad.Luong_Phong:
                    dt = db.GetDataTable("SELECT * FROM NHANVIEN nv FULL OUTER JOIN LUONG l on nv.MaNV = l.MaNV WHERE MAPHONG = '" + maPhong + "' AND l.Thang = '" + thang + "' AND l.Nam = '" + nam + "' ORDER BY l.Luong DESC");
                    break;
                case DGVTypeLoad.Luong_Loai:
                    dt = db.GetDataTable("SELECT * FROM NHANVIEN nv FULL OUTER JOIN LUONG l on nv.MaNV = l.MaNV WHERE MaLoaiNV = '" + maLoai + "' AND l.Thang = '" + thang + "' AND l.Nam = '" + nam + "' ORDER BY l.Luong DESC");
                    break;
                case DGVTypeLoad.All:
                    dt = db.GetDataTable("SELECT * FROM NHANVIEN nv FULL OUTER JOIN LUONG l on nv.MaNV = l.MaNV WHERE MaLoaiNV = '" + maLoai + "' AND MAPHONG = '" + maPhong + "' AND l.Thang = '" + thang + "' AND l.Nam = '" + nam + "' ORDER BY l.Luong DESC");
                    break;
            }
          
            List<NhanVien> list = new List<NhanVien>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new NhanVien(row));
            }
            return list;
        }

        public static void ThemLuong(NHANVIEN NV, int thang, int nam, decimal luong)
        {
            var CheckExistUser = DataAccessHelper.DB.LUONGs.FirstOrDefault(s => s.MaNV == NV.MaNV);

            if (CheckExistUser != null)
            {
                MessageBox.Show("Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LUONG _luong = new LUONG(NV.MaNV, thang, nam, luong);

            DataAccessHelper.DB.LUONGs.InsertOnSubmit(_luong);
            DataAccessHelper.DB.SubmitChanges();
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
