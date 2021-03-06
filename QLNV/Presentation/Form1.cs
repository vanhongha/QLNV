﻿using System;
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
        LoaiNV,
        Luong,
        Loai_Phong,
        Luong_Loai,
        Luong_Phong,
        All
    }
    public partial class Form1 : Form
    {
        string currentMaNV;
        string locPhongKey;
        string locLoaiKey;
        string locThang;
        string locNam;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            locPhongKey = "";
            locLoaiKey = "";
            locThang = "";
            locNam = "";
            cboPhongBan_Loc.Enabled = false;
            cboLoaiNV.Enabled = false;
            currentMaNV = NhanVienBLL.AutoMaNV();
            txtMaNV.Text = currentMaNV;
            cboThang.Enabled = false;
            txtNam.Enabled = false;

            AddThangCombobox();

            PhongBanBLL.PhongBanToCombobox(cboPhongBan);
            PhongBanBLL.PhongBanToCombobox(cboPhongBan_Loc);
            LoaiNhanVienBLL.LoaiNVToCombobox(cboLoaiNV);
            LoaiNhanVienBLL.LoaiNVToCombobox(cboLoai);

            LoadDatagridview(DGVTypeLoad.None);
            ClearAllInput();
        }

        void AddThangCombobox()
        {
            for (int i=1;i<=12;i++)
            {
                cboThang.Items.Add(i.ToString());
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maLoai, maPhong;
            decimal luong = 0;
            try
            {
                maLoai = GetKeyFromCombobox(cboLoai.SelectedItem.ToString());
            }
            catch { maLoai = ""; }
            try
            {
                maPhong = GetKeyFromCombobox(cboPhongBan.SelectedItem.ToString());
            }
            catch { maPhong = ""; }

            NhanVien NV = new NhanVien(txtMaNV.Text.Trim(),
                txtTenNV.Text,
                dtNgaySinh.Value.Date,
                txtSDTNV.Text,
                maLoai,
                maPhong,
                luong);
            try
            {
                if (NhanVienBLL.ThemNV(NV))
                {
                    LoadDatagridview(DGVTypeLoad.None);
                    MessageBox.Show("Đã thêm nhân viên thành công", "Thông báo", MessageBoxButtons.OK);
                    btnThem.Enabled = false;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;                  

                    if (maLoai == "MALNV00001")
                    {
                        themNhanVienBienChe(NV.MaNV);
                        luong = decimal.Parse(txtPC_LN.Text) + decimal.Parse(txtLuongThang.Text) * decimal.Parse(txtBL_SNL.Text);
                    }
                    else
                    {
                        themNhanVienCongNhat(NV.MaNV);
                        luong = decimal.Parse(txtPC_LN.Text) * decimal.Parse(txtBL_SNL.Text);
                    }

                    themLuong(NV.MaNV, luong);
                    LoadDatagridview(DGVTypeLoad.None);
                }
                else
                    MessageBox.Show("Lỗi chưa xác định", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void themNhanVienBienChe(string maNV)
        {
            NhanVienBienChe NVBC = new NhanVienBienChe(
                maNV,
                float.Parse(txtBL_SNL.Text),
                decimal.Parse(txtPC_LN.Text),
                decimal.Parse(txtLuongThang.Text));
            NhanVienBienCheBLL.ThemNhanVienBC(NVBC);
        }

        void themNhanVienCongNhat(string maNV)
        {
            NhanVienCongNhat NVCN = new NhanVienCongNhat(
                maNV,
                float.Parse(txtBL_SNL.Text),
                decimal.Parse(txtPC_LN.Text));

            NhanVienCongNhatBLL.ThemNhanVienCN(NVCN);
        }

        void capNhatNhanVienBienChe(string maNV)
        {
            NhanVienBienChe NVBC = new NhanVienBienChe(
                maNV,
                float.Parse(txtBL_SNL.Text),
                decimal.Parse(txtPC_LN.Text),
                decimal.Parse(txtLuongThang.Text));
            NhanVienBienCheBLL.CapNhatNhanVienBC(NVBC);
        }

        void capNhatNhanVienCongNhat(string maNV)
        {
            NhanVienCongNhat NVCN = new NhanVienCongNhat(
                maNV,
                float.Parse(txtBL_SNL.Text),
                decimal.Parse(txtPC_LN.Text));

            NhanVienCongNhatBLL.CapNhatNhanVienCN(NVCN);
        }

        void themLuong(string maNV, decimal luong)
        {
            NhanVienBLL.ThemLuong(maNV, DateTime.Now.Month, DateTime.Now.Year, luong);
        }

        void UpdateVisibleProperty(bool value)
        {
            lbPC_LN.Visible = value;
            lbBL_SNL.Visible = value;
            txtBL_SNL.Visible = value;
            txtPC_LN.Visible = value;
            lbLuongThang.Visible = value;
            txtLuongThang.Visible = value;
        }

        void UpDateInput()
        {
            if ((cboLoaiNV.Text.Trim()) != null)
            {
                if (GetKeyFromCombobox(cboLoai.SelectedItem.ToString()).Equals("MALNV00001"))
                {
                    lbBL_SNL.Text = "Bậc lương:";
                    lbPC_LN.Text = "Phụ cấp:";

                    lbLuongThang.Visible = true;
                    txtLuongThang.Visible = true;
                }
                else
                {
                    lbBL_SNL.Text = "Số ngày làm việc:";
                    lbPC_LN.Text = "Lương ngày:";
                    txtLuongThang.Visible = false;
                    lbLuongThang.Visible = false;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            NhanVienBLL.XoaNhanVien(txtMaNV.Text.Trim());
            LoadDatagridview(DGVTypeLoad.None);
            MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK);
            LamMoi();
        }

        void LoadDatagridview(DGVTypeLoad type, string maPhong = null, string maLoaiNV = null, string thang = null, string nam = null)
        {
            switch (type)
            {
                case DGVTypeLoad.None:
                    dgv.DataSource = NhanVienBLL.GetList();
                    break;
                case DGVTypeLoad.Phong:
                    dgv.DataSource = NhanVienBLL.GetListTheoKey(type, maPhong);
                    break;
                case DGVTypeLoad.LoaiNV:
                    dgv.DataSource = NhanVienBLL.GetListTheoKey(type, null, maLoaiNV);
                    break;
                case DGVTypeLoad.Luong:
                    dgv.DataSource = NhanVienBLL.GetListTheoKey(DGVTypeLoad.Luong, null, null, thang, nam);
                    break;
                case DGVTypeLoad.Loai_Phong:
                    dgv.DataSource = NhanVienBLL.GetListTheoKey(type, maPhong, maLoaiNV);
                    break;
                case DGVTypeLoad.Luong_Phong:
                    dgv.DataSource = NhanVienBLL.GetListTheoKey(type, maPhong, null, thang, nam);
                    break;
                case DGVTypeLoad.Luong_Loai:
                    dgv.DataSource = NhanVienBLL.GetListTheoKey(type, null, maLoaiNV, thang, nam);
                    break;
                case DGVTypeLoad.All:
                    dgv.DataSource = NhanVienBLL.GetListTheoKey(type, maPhong, maLoaiNV, thang, nam);
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
            cboLoai.Text = "";

            txtBL_SNL.Text = "0";
            txtLuongThang.Text = "0";
            txtPC_LN.Text = "0";
        }

#region FILTER
        private void ckLocLoaiNV_CheckedChanged(object sender, EventArgs e)
        {
            if (ckLocLoaiNV.Checked)
            {
                if (!ckLocTheoPhong.Checked && !ckLocLuong.Checked)
                    LoadDatagridview(DGVTypeLoad.LoaiNV, locPhongKey, locLoaiKey, locThang, locNam);
                else if (ckLocTheoPhong.Checked && !ckLocLuong.Checked)
                    LoadDatagridview(DGVTypeLoad.Loai_Phong, locPhongKey, locLoaiKey, locThang, locNam);
                else if (!ckLocTheoPhong.Checked && ckLocLuong.Checked)
                    LoadDatagridview(DGVTypeLoad.Luong_Loai, locPhongKey, locLoaiKey, locThang, locNam);
                else
                    LoadDatagridview(DGVTypeLoad.All, locPhongKey, locLoaiKey, locThang, locNam);
                cboLoaiNV.Enabled = true;
            }
            else
            {
                if (!ckLocTheoPhong.Checked && !ckLocLuong.Checked)
                    LoadDatagridview(DGVTypeLoad.None, locPhongKey, locLoaiKey, locThang, locNam);
                else if (ckLocTheoPhong.Checked && !ckLocLuong.Checked)
                    LoadDatagridview(DGVTypeLoad.Phong, locPhongKey, locLoaiKey, locThang, locNam);
                else if (!ckLocTheoPhong.Checked && ckLocLuong.Checked)
                    LoadDatagridview(DGVTypeLoad.Luong, locPhongKey, locLoaiKey, locThang, locNam);
                else LoadDatagridview(DGVTypeLoad.Luong_Phong, locPhongKey, locLoaiKey, locThang, locNam);
                cboLoaiNV.Enabled = false;
            }
        }

        private void ckLocTheoPhong_CheckedChanged(object sender, EventArgs e)
        {
            if (ckLocTheoPhong.Checked)
            {
                if (!ckLocLoaiNV.Checked && !ckLocLuong.Checked)
                    LoadDatagridview(DGVTypeLoad.Phong, locPhongKey, locLoaiKey, locThang, locNam);
                else if (ckLocLoaiNV.Checked && !ckLocLuong.Checked)
                    LoadDatagridview(DGVTypeLoad.Loai_Phong, locPhongKey, locLoaiKey, locThang, locNam);
                else if (!ckLocLoaiNV.Checked && ckLocLuong.Checked)
                    LoadDatagridview(DGVTypeLoad.Luong_Phong, locPhongKey, locLoaiKey, locThang, locNam);
                else
                    LoadDatagridview(DGVTypeLoad.All, locPhongKey, locLoaiKey, locThang, locNam);
                cboPhongBan_Loc.Enabled = true;
            }
            else
            {
                if (!ckLocLoaiNV.Checked && !ckLocLuong.Checked)
                    LoadDatagridview(DGVTypeLoad.None, locPhongKey, locLoaiKey, locThang, locNam);
                else if (ckLocLoaiNV.Checked && !ckLocLuong.Checked)
                    LoadDatagridview(DGVTypeLoad.LoaiNV, locPhongKey, locLoaiKey, locThang, locNam);
                else if (!ckLocLoaiNV.Checked && ckLocLuong.Checked)
                    LoadDatagridview(DGVTypeLoad.Luong, locPhongKey, locLoaiKey, locThang, locNam);
                else
                    LoadDatagridview(DGVTypeLoad.Luong_Loai, locPhongKey, locLoaiKey, locThang, locNam);
                cboPhongBan_Loc.Enabled = false;
                cboPhongBan_Loc.Text = "";
            }
        }

        private void ckLocLuong_CheckedChanged(object sender, EventArgs e)
        {
            if (ckLocLuong.Checked)
            {
                if (!ckLocLoaiNV.Checked && !ckLocTheoPhong.Checked)
                    LoadDatagridview(DGVTypeLoad.Luong, locPhongKey, locLoaiKey, locThang, locNam);
                else if (ckLocLoaiNV.Checked && !ckLocTheoPhong.Checked)
                    LoadDatagridview(DGVTypeLoad.Luong_Loai, locPhongKey, locLoaiKey, locThang, locNam);
                else if (!ckLocLoaiNV.Checked && ckLocTheoPhong.Checked)
                    LoadDatagridview(DGVTypeLoad.Luong_Phong, locPhongKey, locLoaiKey, locThang, locNam);
                else
                    LoadDatagridview(DGVTypeLoad.All, locPhongKey, locLoaiKey, locThang, locNam);

                cboThang.Enabled = true;
                txtNam.Enabled = true;
            }
            else
            {
                if (!ckLocLoaiNV.Checked && !ckLocTheoPhong.Checked)
                    LoadDatagridview(DGVTypeLoad.None, locPhongKey, locLoaiKey, locThang, locNam);
                else if (ckLocLoaiNV.Checked && !ckLocTheoPhong.Checked)
                    LoadDatagridview(DGVTypeLoad.LoaiNV, locPhongKey, locLoaiKey, locThang, locNam);
                else if (!ckLocLoaiNV.Checked && ckLocTheoPhong.Checked)
                    LoadDatagridview(DGVTypeLoad.Phong, locPhongKey, locLoaiKey, locThang, locNam);
                else
                    LoadDatagridview(DGVTypeLoad.Loai_Phong, locPhongKey, locLoaiKey, locThang, locNam);

                cboThang.Enabled = false;
                txtNam.Enabled = false;
            }
        }

        private void cboPhongBan_Loc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string phongKey = GetKeyFromCombobox(cboPhongBan_Loc.SelectedItem.ToString());
            locPhongKey = phongKey;
            if (!ckLocLoaiNV.Checked)
                LoadDatagridview(DGVTypeLoad.Phong, phongKey);
            else
            {
                string loaiKey;
                if (cboLoaiNV.SelectedItem == null)
                    loaiKey = "";
                else
                    loaiKey = GetKeyFromCombobox(cboLoaiNV.SelectedItem.ToString());
                LoadDatagridview(DGVTypeLoad.Loai_Phong, phongKey, loaiKey);
            }
        }

        private void cboLoaiNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loaiKey = GetKeyFromCombobox(cboLoaiNV.SelectedItem.ToString());
            locLoaiKey = loaiKey;

            if (!ckLocTheoPhong.Checked && !ckLocLuong.Checked)
                LoadDatagridview(DGVTypeLoad.LoaiNV, locPhongKey, locLoaiKey, locThang, locNam);
            else if (ckLocTheoPhong.Checked && !ckLocLuong.Checked)
                LoadDatagridview(DGVTypeLoad.Loai_Phong, locPhongKey, locLoaiKey, locThang, locNam);
            else if (!ckLocTheoPhong.Checked && ckLocLuong.Checked)
                LoadDatagridview(DGVTypeLoad.Luong_Loai, locPhongKey, locLoaiKey, locThang, locNam);
            else
                LoadDatagridview(DGVTypeLoad.All, locPhongKey, locLoaiKey, locThang, locNam);
        }


        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            locThang = cboThang.SelectedItem.ToString();
            if(!ckLocLoaiNV.Checked && !ckLocTheoPhong.Checked)
                LoadDatagridview(DGVTypeLoad.Luong, locPhongKey, locLoaiKey, locThang, locNam);
            else if (ckLocLoaiNV.Checked && !ckLocTheoPhong.Checked)
                LoadDatagridview(DGVTypeLoad.Luong_Loai, locPhongKey, locLoaiKey, locThang, locNam);
            else if (!ckLocLoaiNV.Checked && ckLocTheoPhong.Checked)
                LoadDatagridview(DGVTypeLoad.Luong_Phong, locPhongKey, locLoaiKey, locThang, locNam);
            else
                LoadDatagridview(DGVTypeLoad.All, locPhongKey, locLoaiKey, locThang, locNam);
        }

        private void txtNam_TextChanged(object sender, EventArgs e)
        {
            locNam = txtNam.Text;
            if (!ckLocLoaiNV.Checked && !ckLocTheoPhong.Checked)
                LoadDatagridview(DGVTypeLoad.Luong, locPhongKey, locLoaiKey, locThang, locNam);
            else if (ckLocLoaiNV.Checked && !ckLocTheoPhong.Checked)
                LoadDatagridview(DGVTypeLoad.Luong_Loai, locPhongKey, locLoaiKey, locThang, locNam);
            else if (!ckLocLoaiNV.Checked && ckLocTheoPhong.Checked)
                LoadDatagridview(DGVTypeLoad.Luong_Phong, locPhongKey, locLoaiKey, locThang, locNam);
            else
                LoadDatagridview(DGVTypeLoad.All, locPhongKey, locLoaiKey, locThang, locNam);
        }
        #endregion

        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!lbBL_SNL.Visible)
                UpdateVisibleProperty(true);
            UpDateInput();
        }


        string GetKeyFromCombobox(string value)
        {
            if (value != null)
            {
                var code = value.Split(new[] { "Value = " }, StringSplitOptions.None)[1];
                code = code.Substring(0, code.Length - 2);
                return code;
            }
            return "";
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        void LamMoi()
        {
            ClearAllInput();
            txtMaNV.Text = NhanVienBLL.AutoMaNV();
            UpdateVisibleProperty(false);

            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            cboLoai.Enabled = true;
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNV.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
            txtTenNV.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
            txtSDTNV.Text = dgv.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();
            dtNgaySinh.Value = Convert.ToDateTime(dgv.Rows[e.RowIndex].Cells[2].Value.ToString().Trim());
            cboLoai.Text = LoaiNhanVienBLL.LayTenLoaiTheoMa(dgv.Rows[e.RowIndex].Cells[4].Value.ToString().Trim()).Trim();
            cboPhongBan.Text = PhongBanBLL.LayTenPBTheoMa(dgv.Rows[e.RowIndex].Cells[6].Value.ToString().Trim()).Trim();

            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnThem.Enabled = false;
            cboLoai.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maLoai = GetKeyFromCombobox(cboLoai.SelectedItem.ToString());
            string maPhong = GetKeyFromCombobox(cboPhongBan.SelectedItem.ToString());
            NhanVien NV = new NhanVien(txtMaNV.Text.Trim(),
                txtTenNV.Text,
                dtNgaySinh.Value.Date,
                txtSDTNV.Text,
                maLoai,
                maPhong,
                0);
            try
            {
                if (NhanVienBLL.CapNhatNhanVien(NV))
                {
                    LoadDatagridview(DGVTypeLoad.None);
                    MessageBox.Show("Đã cập nhật nhân viên thành công", "Thông báo", MessageBoxButtons.OK);
                    if (maLoai == "MALNV00001")
                        capNhatNhanVienBienChe(NV.MaNV);
                    else
                        capNhatNhanVienCongNhat(NV.MaNV);
                }
                else
                    MessageBox.Show("Lỗi chưa xác định", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }  
    }
}
