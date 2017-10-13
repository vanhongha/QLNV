using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNV.Entities
{
    class PhongBan
    {
        private string maPhong;

        public string MaPhong
        {
            get { return maPhong; }
            set { maPhong = value; }
        }

        private string tenPhong;

        public string TenPhong
        {
            get { return tenPhong; }
            set { tenPhong = value; }
        }
        public PhongBan(DataRow row)
        {
            this.maPhong = row["MaPhong"].ToString();
            this.tenPhong = row["TenPhong"].ToString();
        }
    }
}
