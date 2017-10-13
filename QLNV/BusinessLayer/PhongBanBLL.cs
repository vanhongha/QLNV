using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLNV.DataLayer;
using QLNV.Entities;
using System.Windows.Forms;

namespace QLNV.BusinessLayer
{
    class PhongBanBLL
    {
        public static List<PhongBan> GetList()
        {
            return PhongBanDAL.GetList();
        }

        public static void PhongBanToCombobox(ComboBox combobox)
        {
            foreach(PhongBan pb in GetList())
            {
                combobox.Items.Add(pb.TenPhong.Trim());
                combobox.ValueMember = pb.MaPhong.Trim();
                combobox.DisplayMember = pb.TenPhong.Trim();
            }
        }
    }
}
