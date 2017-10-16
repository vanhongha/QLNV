using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNV.Entities
{
    class NhanVienBienChe : NhanVien
    {
        private float bacLuong;

        public float BacLuong
        {
            get { return bacLuong; }
            set { bacLuong = value; }
        }

        public NhanVienBienChe(string _maNV, float _bacLuong, decimal _phuCap, decimal _luongThang)
        {
            phuCap = _phuCap;
            bacLuong = _bacLuong;
            MaNV = _maNV;
            luongThang = _luongThang;
        }

        public NhanVienBienChe(string _maNV, string _hoTen, DateTime _ngaySinh, string _sdt, string _maLoaiNV, string _maPhong, decimal _luong) : base(_maNV, _hoTen, _ngaySinh, _sdt, _maLoaiNV, _maPhong, _luong)
        {
        }

        private decimal phuCap;

        public decimal PhuCap
        {
            get { return phuCap; }
            set { phuCap = value; }
        }

        private decimal luongThang;

        public decimal LuongThang
        {
            get { return luongThang; }
            set { luongThang = value; }
        }

    }
}
