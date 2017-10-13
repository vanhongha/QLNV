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

        public NhanVienCongNhat(string _maNV, string _hoTen, DateTime _ngaySinh, string _sdt, LoaiNhanVien _loaiNhanVien, string _maPhong) : base(_maNV, _hoTen, _ngaySinh, _sdt, _loaiNhanVien, _maPhong)
        {
            
        }

        public float NgayLam
        {
            get { return ngayLam; }
            set { ngayLam = value; }
        }

        
    }
}
