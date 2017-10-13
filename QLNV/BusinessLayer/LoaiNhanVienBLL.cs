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

        public static void LoaiNVToCombobox(ComboBox combobox)
        {
            foreach (LoaiNhanVien pb in GetList())
            {
                combobox.Items.Add(pb.TenLoai.Trim());
                combobox.ValueMember = pb.MaLoai.ToString().Trim();
                combobox.DisplayMember = pb.TenLoai.Trim();
            }
        }
    }
}
