using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLKH.MODEL;

namespace QLKH.VIEWS.DanhSach
{
    public partial class frmTaiKhoan : Form
    {
        public frmTaiKhoan()
        {
            InitializeComponent();
        }

        private void frmTaiKhoan_Load(object sender, EventArgs e)
        {
            try
            {
                KhoaHocContextDB context = new KhoaHocContextDB();
                List<TaiKhoan> taiKhoans = context.TaiKhoans.ToList();
                FillRoleCombobox();
                BindGrid(taiKhoans);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillRoleCombobox()
        {
            try
            {
                List<string> roles = new List<string> { "admin", "giangvien", "nhanvien" };

                cmbRole.DataSource = roles;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu tài khoản: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BindGrid(List<MODEL.TaiKhoan> taiKhoans)
        {
            dgvDsTaiKhoan.Rows.Clear();
            foreach (var item in taiKhoans)
            {
                int index = dgvDsTaiKhoan.Rows.Add();
                dgvDsTaiKhoan.Rows[index].Cells[0].Value = item.MaTaiKhoan;
                dgvDsTaiKhoan.Rows[index].Cells[1].Value = item.Username;
                dgvDsTaiKhoan.Rows[index].Cells[2].Value = item.Password;
                dgvDsTaiKhoan.Rows[index].Cells[3].Value = item.Role;
            }
        }


        private void btnThemTaiKhoan_Click(object sender, EventArgs e)
        {
            try
            {
                KhoaHocContextDB context = new KhoaHocContextDB();

                List<MODEL.TaiKhoan> taiKhoans = context.TaiKhoans.ToList();

                if (string.IsNullOrWhiteSpace(txtMaTaiKhoan.Text))
                {
                    MessageBox.Show("Mã tài khoản không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaTaiKhoan.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Username không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Mật khẩu không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }
                

                if (taiKhoans.Any(s => s.MaTaiKhoan == txtMaTaiKhoan.Text))
                {
                    MessageBox.Show("Mã tài khoản đã tồn tại. Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaTaiKhoan.Focus();
                    return;
                }

                var newTaiKhoan = new MODEL.TaiKhoan
                {
                    MaTaiKhoan = txtMaTaiKhoan.Text,
                    Username = txtUsername.Text,
                    Password = txtPassword.Text,
                    Role = cmbRole.SelectedValue.ToString(),
                };

                context.TaiKhoans.Add(newTaiKhoan);
                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var validationError in ex.EntityValidationErrors)
                    {
                        foreach (var error in validationError.ValidationErrors)
                        {
                            MessageBox.Show($"Thuộc tính: {error.PropertyName} - Lỗi: {error.ErrorMessage}");
                        }
                    }
                }

                BindGrid(context.TaiKhoans.ToList());
                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaTaiKhoan_Click(object sender, EventArgs e)
        {
            try
            {
                KhoaHocContextDB context = new KhoaHocContextDB();

                List<MODEL.TaiKhoan> taiKhoans = context.TaiKhoans.ToList();

                if (string.IsNullOrWhiteSpace(txtMaTaiKhoan.Text))
                {
                    MessageBox.Show("Mã tài khoản không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaTaiKhoan.Focus();
                    return;
                }

                var TaiKhoan = taiKhoans.FirstOrDefault(s => s.MaTaiKhoan == txtMaTaiKhoan.Text);

                if (TaiKhoan != null)
                {
                    DialogResult result = MessageBox.Show("Bạn có muốn xóa tài khoản này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        context.TaiKhoans.Remove(TaiKhoan);
                        context.SaveChanges();

                        BindGrid(context.TaiKhoans.ToList());
                        MessageBox.Show("Tài khoản đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tài khoản với mã đã nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaTaiKhoan_Click(object sender, EventArgs e)
        {
            try
            {
                KhoaHocContextDB context = new KhoaHocContextDB();

                List<MODEL.TaiKhoan> taiKhoans = context.TaiKhoans.ToList();

                if (string.IsNullOrWhiteSpace(txtMaTaiKhoan.Text))
                {
                    MessageBox.Show("Mã tài khoản không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaTaiKhoan.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Username không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }
                
                


                var taiKhoan = taiKhoans.FirstOrDefault(s => s.MaTaiKhoan == txtMaTaiKhoan.Text);

                if (taiKhoan != null)
                {
                    taiKhoan.Username = txtUsername.Text;
                    taiKhoan.Password = txtPassword.Text;
                    taiKhoan.Role = cmbRole.SelectedValue.ToString();

                    context.SaveChanges();

                    BindGrid(context.TaiKhoans.ToList());
                    MessageBox.Show("Tài khoản đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tài khoản với mã đã nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDsTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvDsTaiKhoan.Rows[e.RowIndex];

                txtMaTaiKhoan.Text = selectedRow.Cells["Column1"].Value.ToString();
                txtUsername.Text = selectedRow.Cells["Column2"].Value.ToString();
                txtPassword.Text = selectedRow.Cells["Column3"].Value.ToString();
                cmbRole.Text = selectedRow.Cells["Column4"].Value.ToString();
            }
        }
    }
}
