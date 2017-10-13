using System;
using QLNV.DataLayer;
using System.Windows.Forms;
using QLNV.BusinessLayer;
using QLNV.Entities;
using System.Collections.Generic;

namespace QLNV
{
    public enum DGVTypeLoad
    {
        None,
        Phong,
        LoaiNV
    }
    public partial class Form1 : Form
    {
        string currentMaNV;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboPhongBan_Loc.Enabled = false;
            cboLoaiNV.Enabled = false;
            currentMaNV = NhanVienBLL.AutoMaNV();
            txtMaNV.Text = currentMaNV;
            cboThang.Enabled = false;
            txtNam.Enabled = false;
           
            PhongBanBLL.PhongBanToCombobox(cboPhongBan);
            PhongBanBLL.PhongBanToCombobox(cboPhongBan_Loc);
            LoaiNhanVienBLL.LoaiNVToCombobox(cboLoaiNV);
            LoaiNhanVienBLL.LoaiNVToCombobox(cboLoai);

            LoadDatagridview(DGVTypeLoad.None);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maLoai = GetKeyFromCombobox(cboLoai.SelectedItem.ToString());
            string maPhong = GetKeyFromCombobox(cboPhongBan.SelectedItem.ToString());
            NhanVien NV = new NhanVien(currentMaNV,
                txtTenNV.Text,
                dtNgaySinh.Value.Date,
                txtSDTNV.Text,
                maLoai,
                maPhong);
            try
            {
                if (NhanVienBLL.ThemNV(NV))
                {
                    MessageBox.Show("Đã thêm nhân viên thành công", "Thông báo", MessageBoxButtons.OK);
                    currentMaNV = NhanVienBLL.AutoMaNV();
                    txtMaNV.Text = currentMaNV;
                    ClearAllInput();
                }
                else
                    MessageBox.Show("Lỗi chưa xác định", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void UpDateInput()
        {
            if ((cboLoaiNV.Text.Trim()) != null)
            {
                if (GetKeyFromCombobox(cboLoai.SelectedItem.ToString()).Equals("Ma1"))
                {
                    lbBL_NgayLam.Text = "Ngày làm:";
                    lbPhuCap.Visible = false;
                    txtPhuCap.Visible = false;
                }
                else
                {
                    lbBL_NgayLam.Text = "Bậc lương:";
                    lbPhuCap.Visible = true;
                    txtPhuCap.Visible = true;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ClearAllInput();
        }

        void LoadDatagridview(DGVTypeLoad type, string key = null)
        {
            switch (type)
            {
                case DGVTypeLoad.None:
                    dgv.DataSource = NhanVienBLL.GetList();
                    break;
                case DGVTypeLoad.Phong:
                    dgv.DataSource = NhanVienBLL.GetListTheoKey(type, key);
                    break;
                case DGVTypeLoad.LoaiNV:
                    dgv.DataSource = NhanVienBLL.GetListTheoKey(type, "", key);
                    break;
            }
         
            dgv.Columns[0].HeaderText = "Mã NV";
            dgv.Columns[1].HeaderText = "Họ tên";
            dgv.Columns[2].HeaderText = "Ngày sinh";
            dgv.Columns[3].HeaderText = "Điện thoại";
            dgv.Columns[4].HeaderText = "Loại NV";
            dgv.Columns[5].HeaderText = "Lương";
            dgv.Columns[6].HeaderText = "Mã Phòng";
        }
        void ClearAllInput()
        {
            txtTenNV.Text = "";
            txtSDTNV.Text = "";
            cboPhongBan.Text = "";
        }

        private void ckLocLoaiNV_CheckedChanged(object sender, EventArgs e)
        {
            if (ckLocTheoPhong.Checked)
            {
                LoadDatagridview(DGVTypeLoad.LoaiNV);
                cboLoaiNV.Enabled = true;
            }
            else
            {
                cboLoaiNV.Enabled = true;
            }
        }

        private void cboPhongBan_Loc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = GetKeyFromCombobox(cboPhongBan_Loc.SelectedItem.ToString());
            LoadDatagridview(DGVTypeLoad.Phong, key);
        }
        private void cboLoaiNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = GetKeyFromCombobox(cboLoaiNV.SelectedItem.ToString());
            LoadDatagridview(DGVTypeLoad.LoaiNV, key);
        }

        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpDateInput();
        }

        private void ckLocTheoPhong_CheckedChanged(object sender, EventArgs e)
        {
            if (ckLocTheoPhong.Checked)
            {
                LoadDatagridview(DGVTypeLoad.Phong);
                cboPhongBan_Loc.Enabled = true;
            }
            else
            {
                cboPhongBan_Loc.Enabled = false;
            }
        }

        string GetKeyFromCombobox(string value)
        {
            if (value != null)
            {
                var code = value.Split(new[] { "Value = " }, StringSplitOptions.None)[1];
                code = code.Substring(0, code.Length - 2);
                return code;
            }
            return null;
        }
    }
}
