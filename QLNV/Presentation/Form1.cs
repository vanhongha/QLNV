using System;
using QLNV.DataLayer;
using System.Windows.Forms;
using QLNV.BusinessLayer;
using QLNV.Entities;

namespace QLNV
{
    public partial class Form1 : Form
    {
        string currentMaNV;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rdoNVCongNhat.Checked = true;
            currentMaNV = NhanVienBLL.AutoMaNV();
            txtMaNV.Text = currentMaNV;
            UpDateInput();
            PhongBanBLL.PhongBanToCombobox(cboPhongBan);
            PhongBanBLL.PhongBanToCombobox(cboPhongBan_Loc);
            LoaiNhanVienBLL.LoaiNVToCombobox(cboLoaiNV);
            LoadDatagridview();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            int maLoai;
            if (rdoNVBienChe.Checked)
                maLoai = 1;
            else
                maLoai = 2;

            NhanVien NV = new NhanVien(currentMaNV,
                txtTenNV.Text,
                dtNgaySinh.Value.Date,
                txtSDTNV.Text,
                maLoai,
                cboPhongBan.ValueMember);

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
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin cần thiết!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void UpDateInput()
        {
            if(rdoNVCongNhat.Checked)
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

        private void rdoNVBienChe_CheckedChanged(object sender, EventArgs e)
        {
            UpDateInput();
        }

        private void rdoNVCongNhat_CheckedChanged(object sender, EventArgs e)
        {
            UpDateInput();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ClearAllInput();
        }

        void LoadDatagridview()
        {
            dgv.DataSource = NhanVienBLL.GetList();
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
            rdoNVCongNhat.Checked = true;
        }
    }
}
