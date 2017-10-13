using System;
using System.Collections.Generic;
using QLNV.DataLayer;
using QLNV.Entities;
using System.Windows.Forms;

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
            foreach (LoaiNhanVien loaiNV in GetList())
            {
                comboBox.Items.Add(new { Text = loaiNV.TenLoai.Trim(), Value = loaiNV.MaLoai.ToString().Trim() });
            }
        }
    }
}
