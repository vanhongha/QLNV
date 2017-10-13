using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private int maLoaiNV;

        public int MaLoaiNV
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

        public NhanVien(string _maNV, string _hoTen, DateTime _ngaySinh, string _sdt, int _maLoaiNV,string _maPhong)
        {
            maNV = _maNV;
            hoTen = _hoTen;
            ngaySinh = _ngaySinh;
            sdt = _sdt;
            MaLoaiNV = _maLoaiNV;
            maPhong = _maPhong;
        }

        public NhanVien(DataRow row)
        {
            this.maNV = row["MaNV"].ToString();
            this.hoTen = row["TenNV"].ToString();
            this.sdt = row["SoDT"].ToString();
            this.ngaySinh = (DateTime)row["NgaySinh"];
            maLoaiNV = (int)row["MaLoaiNV"];
            this.maPhong = row["MaPhong"].ToString();
        }
    }
}
