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
                //DataAccessHelper db = new DataAccessHelper();
                //SqlCommand cmd = db.Command("XoaNhanVien");

                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@MaNV", maNV);

                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //db.dt = new DataTable();
                //da.Fill(db.dt);
                try
                {
                    NHANVIENBIENCHE NVBC = DataAccessHelper.DB.NHANVIENBIENCHEs.Single(_nv => _nv.MaNV == maNV);
                    DataAccessHelper.DB.NHANVIENBIENCHEs.DeleteOnSubmit(NVBC);
                }
                catch { }

                try
                {
                    NHANVIENCONGNHAT NVCN = DataAccessHelper.DB.NHANVIENCONGNHATs.Single(_nv => _nv.MaNV == maNV);
                    DataAccessHelper.DB.NHANVIENCONGNHATs.DeleteOnSubmit(NVCN);
                }
                catch { }

                try
                {
                    LUONG luong = DataAccessHelper.DB.LUONGs.Single(_luong => _luong.MaNV == maNV);
                    DataAccessHelper.DB.LUONGs.DeleteOnSubmit(luong);
                }
                catch { }

                NHANVIEN NV = DataAccessHelper.DB.NHANVIENs.Single(_nv => _nv.MaNV == maNV);
                DataAccessHelper.DB.NHANVIENs.DeleteOnSubmit(NV);

                DataAccessHelper.DB.SubmitChanges();
               
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

        public static dynamic GetList(bool locLuong, string maphong, string maLoai, int thang, int nam)
        {
            var queryLocLuong = from nv in DataAccessHelper.DB.NHANVIENs
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
                            nv.MaPhong,
                            luong.Thang,
                            luong.Nam
                        };

            var query = from nv in DataAccessHelper.DB.NHANVIENs
                                orderby nv.MaNV
                                select new
                                {
                                    nv.MaNV,
                                    nv.TenNV,
                                    nv.NgaySinh,
                                    nv.SoDT,
                                    nv.MaLoaiNV,
                                    nv.MaPhong,
                                };


            if (thang != 0)
                queryLocLuong = queryLocLuong.Where(nv => nv.Thang == thang);

            if (nam != 0) 
                queryLocLuong = queryLocLuong.Where(nv => nv.Nam == nam);

            if (string.Compare(maphong, string.Empty) != 0)
                query = query.Where(nv => nv.MaPhong == maphong);

            if(string.Compare(maLoai, string.Empty) != 0)
                query = query.Where(nv => nv.MaLoaiNV == maLoai);

            if(locLuong)
            {
                if (string.Compare(maphong, string.Empty) != 0)
                    queryLocLuong = queryLocLuong.Where(nv => nv.MaPhong == maphong);

                if (string.Compare(maLoai, string.Empty) != 0)
                    queryLocLuong = queryLocLuong.Where(nv => nv.MaLoaiNV == maLoai);

                return queryLocLuong;
            }
            else
                return query;            
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
    }
}
