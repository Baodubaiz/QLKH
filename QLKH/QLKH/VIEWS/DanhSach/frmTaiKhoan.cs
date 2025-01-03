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
                List<MODEL.TaiKhoan> taiKhoans = context.TaiKhoans.ToList();
                MODEL.TaiKhoan taiKhoan = new MODEL.TaiKhoan();
                BindGrid(taiKhoans);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void dgvDanhSach_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDsTaiKhoan.SelectedRows.Count > 0)
            {
                var row = dgvDsTaiKhoan.SelectedRows[0];
                txtMaTaiKhoan.Text = row.Cells["Column1"].Value.ToString();
                txtUsername.Text = row.Cells["Column2"].Value.ToString();
                txtPassword.Text = row.Cells["Column3"].Value.ToString();
                txtRole.Text = row.Cells["Column4"].Value.ToString();
            }
        }

        private void btnThemTaiKhoan_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo context để kết nối cơ sở dữ liệu
                KhoaHocContextDB context = new KhoaHocContextDB();

                // Lấy danh sách khóa học hiện tại từ cơ sở dữ liệu
                List<MODEL.TaiKhoan> taiKhoans = context.TaiKhoans.ToList();

                // Kiểm tra dữ liệu đầu vào
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
                if (string.IsNullOrWhiteSpace(txtRole.Text))
                {
                    MessageBox.Show("Vai trò không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRole.Focus();
                    return;
                }

                // Kiểm tra trùng lặp mã tài khoản
                if (taiKhoans.Any(s => s.MaTaiKhoan == txtMaTaiKhoan.Text))
                {
                    MessageBox.Show("Mã tài khoản đã tồn tại. Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaTaiKhoan.Focus();
                    return;
                }

                // Tạo một đối tượng TaiKhoan mới
                var newTaiKhoan = new MODEL.TaiKhoan
                {
                    MaTaiKhoan = txtMaTaiKhoan.Text,
                    Username = txtUsername.Text,
                    Password = txtPassword.Text,
                    Role = txtRole.Text,
                };

                // Thêm tài khoản mới vào cơ sở dữ liệu
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

                // Tải lại dữ liệu lên DataGridView
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
                // Tạo context để kết nối cơ sở dữ liệu
                KhoaHocContextDB context = new KhoaHocContextDB();

                // Lấy danh sách khóa học hiện tại từ cơ sở dữ liệu
                List<MODEL.TaiKhoan> taiKhoans = context.TaiKhoans.ToList();

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtMaTaiKhoan.Text))
                {
                    MessageBox.Show("Mã tài khoản không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaTaiKhoan.Focus();
                    return;
                }

                // Tìm khóa học theo mã khóa học
                var TaiKhoan = taiKhoans.FirstOrDefault(s => s.MaTaiKhoan == txtMaTaiKhoan.Text);

                if (TaiKhoan != null)
                {
                    // Xóa khóa học khỏi cơ sở dữ liệu
                    context.TaiKhoans.Remove(TaiKhoan);
                    context.SaveChanges();

                    // Tải lại dữ liệu lên DataGridView
                    BindGrid(context.TaiKhoans.ToList());
                    MessageBox.Show("Tài khoản đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                // Tạo context để kết nối cơ sở dữ liệu
                KhoaHocContextDB context = new KhoaHocContextDB();

                // Lấy danh sách khóa học hiện tại từ cơ sở dữ liệu
                List<MODEL.TaiKhoan> taiKhoans = context.TaiKhoans.ToList();

                // Kiểm tra dữ liệu đầu vào
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
                
                if (string.IsNullOrWhiteSpace(txtRole.Text))
                {
                    MessageBox.Show("Vai trò không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRole.Focus();
                    return;
                }


                // Tìm khóa học theo mã khóa học
                var taiKhoan = taiKhoans.FirstOrDefault(s => s.MaTaiKhoan == txtMaTaiKhoan.Text);

                if (taiKhoan != null)
                {
                    // Cập nhật thông tin khóa học
                    taiKhoan.Username = txtUsername.Text;
                    taiKhoan.Password = txtPassword.Text;
                    taiKhoan.Role = txtRole.Text;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    context.SaveChanges();

                    // Tải lại dữ liệu lên DataGridView
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
                // Lấy hàng được chọn từ DataGridView
                DataGridViewRow selectedRow = dgvDsTaiKhoan.Rows[e.RowIndex];

                // Hiển thị dữ liệu từ hàng được chọn lên các TextBox
                txtMaTaiKhoan.Text = selectedRow.Cells["Column1"].Value.ToString();
                txtUsername.Text = selectedRow.Cells["Column2"].Value.ToString();
                txtPassword.Text = selectedRow.Cells["Column3"].Value.ToString();
                txtRole.Text = selectedRow.Cells["Column4"].Value.ToString();
            }
        }
    }
}
