using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNV.Entities
{
    class NhanVienCongNhat : NhanVien
    {
        private float ngayLam;

        public NhanVienCongNhat(string _maNV, string _hoTen, DateTime _ngaySinh, string _sdt, string _maLoaiNV, string _maPhong) : base(_maNV, _hoTen, _ngaySinh, _sdt, _maLoaiNV, _maPhong)
        {
        }

        public float NgayLam
        {
            get { return ngayLam; }
            set { ngayLam = value; }
        }

        
    }
}
