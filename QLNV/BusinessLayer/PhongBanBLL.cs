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

        public static void PhongBanToCombobox(ComboBox comboBox)
        {
            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Value";
            foreach (PHONGBAN pb in DataAccessHelper.DB.PHONGBANs.ToList())
            {
                comboBox.Items.Add(new { Text = pb.TenPhong.Trim(), Value = pb.MaPhong.Trim() });
            }
        }
        public static string LayTenPBTheoMa(string maPB)
        {
            return PhongBanDAL.LayTenPBTheoMa(maPB);
        }
    }
}
