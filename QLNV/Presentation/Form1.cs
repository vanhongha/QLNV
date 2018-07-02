using System;
using QLNV.DataLayer;
using System.Windows.Forms;
using QLNV.BusinessLayer;
using QLNV.Entities;
using System.Collections.Generic;
using System.Linq;

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
        string maPhong;
        string maLoaiNV;
        int thang;
        int nam;
        bool locLuong;
        
        DataClassesDataContext db = new DataClassesDataContext();
        const string nvCongNhat = "MALNV00002";

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            locLuong = false;
            maPhong = "";
            maLoaiNV = "";
            thang = 0;
            nam = 0;
            cboPhongBan_Loc.Enabled = false;
            cboLoaiNV_Loc.Enabled = false;
            currentMaNV = NhanVienBLL.AutoMaNV();
            txtMaNV.Text = currentMaNV;
            cboThang.Enabled = false;
            txtNam.Enabled = false;

            AddThangCombobox();

            PhongBanBLL.PhongBanToCombobox(cboPhongBan);
            PhongBanBLL.PhongBanToCombobox(cboPhongBan_Loc);
            LoaiNhanVienBLL.LoaiNVToCombobox(cboLoaiNV_Loc);
            LoaiNhanVienBLL.LoaiNVToCombobox(cboLoaiNV);

            LoadDatagridview();
            ClearAllInput();
            initButtonStyle();
        }

        void initButtonStyle()
        {
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.FlatAppearance.BorderColor = System.Drawing.Color.MediumSeaGreen;
            btnThem.FlatAppearance.BorderSize = 1;

            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.FlatAppearance.BorderColor = System.Drawing.Color.MediumSeaGreen;
            btnXoa.FlatAppearance.BorderSize = 1;

            btnSua.FlatStyle = FlatStyle.Flat;
            btnSua.FlatAppearance.BorderColor = System.Drawing.Color.MediumSeaGreen;
            btnSua.FlatAppearance.BorderSize = 1;

            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.FlatAppearance.BorderColor = System.Drawing.Color.MediumSeaGreen;
            btnLamMoi.FlatAppearance.BorderSize = 1;
        }

        void AddThangCombobox()
        {
            for (int i = 1; i <= 12; i++)
            {
                cboThang.Items.Add(i.ToString());
            }
        }

        

        // LINQ TO SQL
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenNV.Text.Trim() == "")
            {
                MessageBox.Show("Tên nhân viên không được bỏ trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maLoai, maPhong;
            decimal luong = 0;
            try
            {
                maLoai = GetKeyFromCombobox(cboLoaiNV.SelectedItem.ToString());
            }
            catch
            {
                MessageBox.Show("Chưa chọn loại nhân viên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                maPhong = GetKeyFromCombobox(cboPhongBan.SelectedItem.ToString());
            }
            catch
            {
                MessageBox.Show("Chưa chọn phòng ban cho nhân viên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NHANVIEN NV = new NHANVIEN(txtMaNV.Text.Trim(),
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
                    btnThem.Enabled = false;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;

                    if (maLoai == nvCongNhat)
                    {
                        NHANVIENCONGNHAT nv = new NHANVIENCONGNHAT(NV.MaNV,
                              float.Parse(txtBacLuong_SoNgayLam.Text),
                              decimal.Parse(txtPhuCap_LuongNgay.Text));
                        NhanVienBLL.ThemNVCongNhat(nv);
                        luong = decimal.Parse(txtPhuCap_LuongNgay.Text) * decimal.Parse(txtBacLuong_SoNgayLam.Text);                     
                    }
                    else
                    {
                        NHANVIENBIENCHE nv = new NHANVIENBIENCHE(NV.MaNV,
                            float.Parse(txtBacLuong_SoNgayLam.Text),
                            decimal.Parse(txtPhuCap_LuongNgay.Text),
                            decimal.Parse(txtLuongThang.Text));
                        NhanVienBLL.ThemNVBienChe(nv);
                        luong = decimal.Parse(txtPhuCap_LuongNgay.Text) + decimal.Parse(txtLuongThang.Text) * decimal.Parse(txtBacLuong_SoNgayLam.Text);
                    }

                    NhanVienBLL.ThemLuong(NV, DateTime.Now.Month, DateTime.Now.Year, luong);
                    LoadDatagridview();
                }
                else
                    MessageBox.Show("Nhân viên đã tồn tại!", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void CapNhatNhanVienBienChe(string maNV)
        {
            NHANVIENBIENCHE NV = DataAccessHelper.DB.NHANVIENBIENCHEs.Single(nv => nv.MaNV == maNV);
            NV.LuongThang = decimal.Parse(txtLuongThang.Text);
            NV.PhuCap = decimal.Parse(txtPhuCap_LuongNgay.Text);
            NV.BacLuong = float.Parse(txtBacLuong_SoNgayLam.Text);

            DataAccessHelper.DB.SubmitChanges();
        }

        void CapNhatNhanVienCongNhat(string maNV)
        {
            NHANVIENCONGNHAT NV = DataAccessHelper.DB.NHANVIENCONGNHATs.Single(nv => nv.MaNV == maNV);
            NV.LuongNgay = decimal.Parse(txtPhuCap_LuongNgay.Text);
            NV.SoNgayLam = float.Parse(txtBacLuong_SoNgayLam.Text);

            DataAccessHelper.DB.SubmitChanges();
        }

        void UpdateVisibleProperty(bool value)
        {
            lbPC_LN.Visible = value;
            lbBL_SNL.Visible = value;
            txtBacLuong_SoNgayLam.Visible = value;
            txtPhuCap_LuongNgay.Visible = value;
            lbLuongThang.Visible = value;
            txtLuongThang.Visible = value;
        }

        void UpDateInput()
        {
            if ((cboLoaiNV_Loc.Text.Trim()) != null)
            {
                if (GetKeyFromCombobox(cboLoaiNV.SelectedItem.ToString()).Equals("MALNV00001"))
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
            LoadDatagridview();
            MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK);
            LamMoi();
        }

        void LoadDatagridview()
        {
            dgv.DataSource = NhanVienBLL.GetList(locLuong, maPhong, maLoaiNV, thang, nam);          

            //dgv.Columns[0].HeaderText = "Mã NV";
            //dgv.Columns[1].HeaderText = "Họ tên";
            //dgv.Columns[2].HeaderText = "Ngày sinh";
            //dgv.Columns[3].HeaderText = "Điện thoại";
            //dgv.Columns[4].HeaderText = "Loại NV";
            //dgv.Columns[5].HeaderText = "Lương";
            //dgv.Columns[6].HeaderText = "Mã Phòng";
        }

        void ClearAllInput()
        {
            txtTenNV.Text = "";
            txtSDTNV.Text = "";
            cboPhongBan.Text = "";
            cboLoaiNV.Text = "";

            txtBacLuong_SoNgayLam.Text = "0";
            txtLuongThang.Text = "0";
            txtPhuCap_LuongNgay.Text = "0";
        }

#region FILTER
        private void ckLocLoaiNV_CheckedChanged(object sender, EventArgs e)
        {
            maLoaiNV = string.Empty;
            if (ckLocLoaiNV.Checked)
            {
                cboLoaiNV_Loc.Enabled = true;
            }
            else
            {
                cboLoaiNV_Loc.Enabled = false;
            }
            LoadDatagridview();
        }

        private void ckLocTheoPhong_CheckedChanged(object sender, EventArgs e)
        {
            maPhong = string.Empty;
            if (ckLocTheoPhong.Checked)
            {
                cboPhongBan_Loc.Enabled = true;
            }
            else
            {
                cboPhongBan_Loc.Enabled = false;
            }
            LoadDatagridview();
        }

        private void ckLocLuong_CheckedChanged(object sender, EventArgs e)
        {
            thang = 0;
            nam = 0;
            locLuong = !locLuong;
            if (ckLocLuong.Checked)
            {       
                cboThang.Enabled = true;
                txtNam.Enabled = true;
            }
            else
            {
                cboThang.Enabled = false;
                txtNam.Enabled = false;
            }
            LoadDatagridview();
        }

        private void cboPhongBan_Loc_SelectedIndexChanged(object sender, EventArgs e)
        {
            maPhong = GetKeyFromCombobox(cboPhongBan_Loc.SelectedItem.ToString());
            LoadDatagridview();
        }

        private void cboLoaiNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            maLoaiNV = GetKeyFromCombobox(cboLoaiNV_Loc.SelectedItem.ToString());
            LoadDatagridview();
        }


        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            thang = Convert.ToInt32(cboThang.SelectedItem.ToString());
            LoadDatagridview();
        }

        private void txtNam_TextChanged(object sender, EventArgs e)
        {
            nam = Convert.ToInt32(txtNam.Text);
            LoadDatagridview();
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
            cboLoaiNV.Enabled = true;
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex > dgv.RowCount) { return; }
            txtMaNV.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();

            NHANVIEN nv = NhanVienDAL.GetNhanVien(txtMaNV.Text);

            txtTenNV.Text = nv.TenNV;
            txtSDTNV.Text = nv.SoDT;
            dtNgaySinh.Value = Convert.ToDateTime(nv.NgaySinh);
            cboLoaiNV.Text = LoaiNhanVienBLL.LayTenLoaiTheoMa(nv.MaLoaiNV).Trim();
            cboPhongBan.Text = PhongBanBLL.LayTenPBTheoMa(nv.MaPhong).Trim();

            if (string.Equals(nv.MaLoaiNV, nvCongNhat))
            {
                NHANVIENCONGNHAT NV = NhanVienDAL.GetNhanVienCongNhat(nv.MaNV);
                if (NV != null)
                {
                    txtBacLuong_SoNgayLam.Text = NV.SoNgayLam.ToString();
                    txtPhuCap_LuongNgay.Text = NV.LuongNgay.ToString();
                }
            }
            else
            {
                NHANVIENBIENCHE NV = NhanVienDAL.GetNhanVienBienChe(nv.MaNV);
                if (NV != null)
                {
                    txtLuongThang.Text = NV.LuongThang.ToString();
                    txtBacLuong_SoNgayLam.Text = NV.BacLuong.ToString();
                    txtPhuCap_LuongNgay.Text = NV.PhuCap.ToString();
                }
            }

            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnThem.Enabled = false;
            cboLoaiNV.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maLoai = GetKeyFromCombobox(cboLoaiNV.SelectedItem.ToString());
            string maPhong = GetKeyFromCombobox(cboPhongBan.SelectedItem.ToString());
            NHANVIEN NV = new NHANVIEN(txtMaNV.Text.Trim(),
                txtTenNV.Text,
                dtNgaySinh.Value.Date,
                txtSDTNV.Text,
                maLoai,
                maPhong);
            try
            {
                if (NhanVienBLL.CapNhatNhanVien(NV))
                {
                    LoadDatagridview();

                    if (maLoai == nvCongNhat)
                        CapNhatNhanVienCongNhat(NV.MaNV);
                    else
                        CapNhatNhanVienBienChe(NV.MaNV);

                    MessageBox.Show("Đã cập nhật nhân viên thành công", "Thông báo", MessageBoxButtons.OK);

                    LoadDatagridview();
                }
                else
                    MessageBox.Show("Lỗi chưa xác định", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtBacLuong_SoNgayLam_TextChanged(object sender, EventArgs e)
        {
            if(txtBacLuong_SoNgayLam.Text.Trim() == "") { return; }

            try
            {
                double.Parse(txtBacLuong_SoNgayLam.Text.Trim());
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập số", "Lỗi nhập", MessageBoxButtons.OK);
                txtBacLuong_SoNgayLam.Text = "0";
            }
        }

        private void txtPhuCap_LuongNgay_TextChanged(object sender, EventArgs e)
        {
            if (txtPhuCap_LuongNgay.Text.Trim() == "") { return; }

            try
            {
                double.Parse(txtPhuCap_LuongNgay.Text.Trim());
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập số", "Lỗi nhập", MessageBoxButtons.OK);
                txtPhuCap_LuongNgay.Text = "0";
            }
        }

        private void txtLuongThang_TextChanged(object sender, EventArgs e)
        {
            if (txtLuongThang.Text.Trim() == "") { return; }

            try
            {
                double.Parse(txtLuongThang.Text.Trim());
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập số", "Lỗi nhập", MessageBoxButtons.OK);
                txtLuongThang.Text = "0";
            }
        }
    }
}
