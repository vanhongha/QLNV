using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNV.Entities
{
    class NhanVien
    {
        private string maNV;

        public string MaNV
        {
            get { return maNV; }
            set { maNV = value; }
        }

        private string hoTen;

        public string HoTen
        {
            get { return hoTen; }
            set { hoTen = value; }
        }

        private DateTime ngaySinh;

        public DateTime NgaySinh
        {
            get { return ngaySinh; }
            set { ngaySinh = value; }
        }

        private string sdt;

        public string SDT
        {
            get { return sdt; }
            set { sdt = value; }
        }

        private string maLoaiNV;

        public string MaLoaiNV
        {
            get { return maLoaiNV; }
            set { maLoaiNV = value; }
        }

        private decimal luong;

        public decimal Luong
        {
            get { return luong; }
            set { luong = value; }
        }

        private string maPhong;

        public string MaPhong
        {
            get { return maPhong; }
            set { maPhong = value; }
        }

        public NhanVien(string _maNV, string _hoTen, DateTime _ngaySinh, string _sdt, string _maLoaiNV,string _maPhong, decimal _luong)
        {
            maNV = _maNV;
            hoTen = _hoTen;
            ngaySinh = _ngaySinh;
            sdt = _sdt;
            MaLoaiNV = _maLoaiNV;
            maPhong = _maPhong;
            luong = _luong;
        }

        public NhanVien(DataRow row)
        {
            maNV = row["MaNV"].ToString();
            hoTen = row["TenNV"].ToString();
            sdt = row["SoDT"].ToString();
            ngaySinh = (DateTime)row["NgaySinh"];
            maLoaiNV = row["MaLoaiNV"].ToString();
            maPhong = row["MaPhong"].ToString();
            if (row["Luong"].ToString() == "")
                luong = 0;
            else
                luong = decimal.Parse(row["Luong"].ToString());
        }

        public NhanVien()
        {
        }
    }
}
