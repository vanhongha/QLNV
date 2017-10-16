using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNV.Entities
{
    class NhanVienCongNhat : NhanVien
    {
        private float soNgayLam;


        public float SoNgayLam
        {
            get { return soNgayLam; }
            set { soNgayLam = value; }
        }

        private decimal luongNgay;


        public decimal LuongNgay
        {
            get { return luongNgay; }
            set { luongNgay = value; }
        }

        public NhanVienCongNhat(string _maNV, string _hoTen, DateTime _ngaySinh, string _sdt, string _maLoaiNV, string _maPhong, decimal _luong) : base(_maNV, _hoTen, _ngaySinh, _sdt, _maLoaiNV, _maPhong, _luong)
        {
            MaNV = _maNV;
            HoTen = _hoTen;
            NgaySinh = _ngaySinh;
            SDT = _sdt;
            MaLoaiNV = _maLoaiNV;
            MaPhong = _maPhong;
        }

        public NhanVienCongNhat(string _maNV, float _soNgayLam, decimal _luongNgay)
        {
            MaNV = _maNV;
            luongNgay = _luongNgay;
            soNgayLam = _soNgayLam;
        }

        public NhanVienCongNhat() { }
    }
}
