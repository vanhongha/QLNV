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

        private decimal phuCap;

        public NhanVienBienChe(string _maNV, string _hoTen, DateTime _ngaySinh, string _sdt, string _maLoaiNV, string _maPhong) : base(_maNV, _hoTen, _ngaySinh, _sdt, _maLoaiNV, _maPhong)
        {
        }

        public decimal PhuCap
        {
            get { return phuCap; }
            set { phuCap = value; }
        }


    }
}
