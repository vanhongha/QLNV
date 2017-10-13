using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNV.Entities
{
    class LoaiNhanVien
    {
        private string maLoai;

        public string MaLoai
        {
            get { return maLoai; }
            set { maLoai = value; }
        }

        private string tenLoai;

        public string TenLoai
        {
            get { return tenLoai; }
            set { tenLoai = value; }
        }

        public LoaiNhanVien(DataRow row)
        {
            this.maLoai = row["MaLoaiNV"].ToString();
            this.tenLoai = row["TenLoaiNV"].ToString();
        }

        public LoaiNhanVien(string _maLoai, string _tenLoai)
        {
            maLoai = _maLoai;
            tenLoai = _tenLoai;
        }

        public LoaiNhanVien() { }
    }
}
