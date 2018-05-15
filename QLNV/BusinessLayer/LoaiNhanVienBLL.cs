using System;
using System.Collections.Generic;
using QLNV.DataLayer;
using QLNV.Entities;
using System.Windows.Forms;
using System.Linq;

namespace QLNV.BusinessLayer
{
    class LoaiNhanVienBLL
    {
        public static List<LoaiNhanVien> GetList()
        {
            return LoaiNhanVienDAL.GetList();
        }

        public static void LoaiNVToCombobox(ComboBox comboBox)
        {
            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Value";
            using (DataClassesDataContext db = new DataClassesDataContext())
                foreach (LOAINHANVIEN loaiNV in db.LOAINHANVIENs.ToList())
                {
                    comboBox.Items.Add(new { Text = loaiNV.TenLoaiNV.Trim(), Value = loaiNV.MaLoaiNV.ToString().Trim() });
                }
        }

        public static string LayTenLoaiTheoMa(string maLoai)
        {
           return LoaiNhanVienDAL.LayTenLoaiTheoMa(maLoai);
        }
    }
}
