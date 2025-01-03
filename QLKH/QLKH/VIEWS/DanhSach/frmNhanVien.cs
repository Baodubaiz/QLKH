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
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
            dgvDanhSach.SelectionChanged += dgvDanhSach_SelectionChanged;
        }

        private void DSNhanVien_Load(object sender, EventArgs e)
        {
            try
            {
                KhoaHocContextDB context = new KhoaHocContextDB();
                List<MODEL.NhanVien> listNhanVien = context.NhanViens.ToList();
                MODEL.NhanVien nhanVien = new MODEL.NhanVien();
                BindGrid(listNhanVien);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindGrid(List<MODEL.NhanVien> DSNhanVien)
        {
            dgvDanhSach.Rows.Clear();
            foreach (var item in DSNhanVien)
            {
                int index = dgvDanhSach.Rows.Add();
                dgvDanhSach.Rows[index].Cells[0].Value = item.MaNhanVien;
                dgvDanhSach.Rows[index].Cells[1].Value = item.HoTen;
                dgvDanhSach.Rows[index].Cells[2].Value = item.SoDienThoai;
                dgvDanhSach.Rows[index].Cells[3].Value = item.Email;
                dgvDanhSach.Rows[index].Cells[4].Value = item.MaTaiKhoan;
            }
        }

        private void dgvDanhSach_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDanhSach.SelectedRows.Count > 0)
            {
                var row = dgvDanhSach.SelectedRows[0];
                txtMaNhanVien.Text = row.Cells["Column1"].Value.ToString();
                txtHoTen.Text = row.Cells["Column2"].Value.ToString();
                txtSDT.Text = row.Cells["Column3"].Value.ToString();
                txtEmail.Text = row.Cells["Column4"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo context để kết nối cơ sở dữ liệu
                KhoaHocContextDB context = new KhoaHocContextDB();

                // Lấy danh sách khóa học hiện tại từ cơ sở dữ liệu
                List<MODEL.NhanVien> listNhanVien = context.NhanViens.ToList();

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text))
                {
                    MessageBox.Show("Mã nhân viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNhanVien.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show("Tên nhân viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHoTen.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtSDT.Text))
                {
                    MessageBox.Show("Số điện thoại không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSDT.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Email không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtMaTaiKhoan.Text))
                {
                    MessageBox.Show("Mã tài khoản không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaTaiKhoan.Focus();
                    return;
                }

                var taiKhoan = context.TaiKhoans.FirstOrDefault(s => s.MaTaiKhoan == txtMaTaiKhoan.Text);
                if (taiKhoan == null)
                {
                    MessageBox.Show("Mã tài khoản không tồn tại. Vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaTaiKhoan.Focus();
                    return;
                }

                if (listNhanVien.Any(s => s.MaNhanVien == txtMaNhanVien.Text))
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại. Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNhanVien.Focus();
                    return;
                }

                var newNhanVien = new MODEL.NhanVien
                {
                    MaNhanVien = txtMaNhanVien.Text,
                    HoTen = txtHoTen.Text,
                    SoDienThoai = txtSDT.Text,
                    Email = txtEmail.Text,
                    MaTaiKhoan = txtMaTaiKhoan.Text,
                };

                context.NhanViens.Add(newNhanVien);
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
                BindGrid(context.NhanViens.ToList());
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo context để kết nối cơ sở dữ liệu
                KhoaHocContextDB context = new KhoaHocContextDB();

                // Lấy danh sách khóa học hiện tại từ cơ sở dữ liệu
                List<MODEL.NhanVien> listNhanVien = context.NhanViens.ToList();

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text))
                {
                    MessageBox.Show("Mã nhân viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNhanVien.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show("Tên nhân viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHoTen.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtSDT.Text))
                {
                    MessageBox.Show("Trùng số điện thoại gòi nhé hihi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSDT.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Email không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtMaTaiKhoan.Text))
                {
                    MessageBox.Show("Mã tài khoản không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaTaiKhoan.Focus();
                    return;
                }


                var taiKhoan = context.TaiKhoans.FirstOrDefault(s => s.MaTaiKhoan == txtMaTaiKhoan.Text);
                if (taiKhoan == null)
                {
                    MessageBox.Show("Mã tài khoản không tồn tại. Vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaTaiKhoan.Focus();
                    return;
                }

                var nhanVien = listNhanVien.FirstOrDefault(s => s.MaNhanVien == txtMaNhanVien.Text);

                if (nhanVien != null)
                {
                    // Cập nhật thông tin khóa học
                    nhanVien.HoTen = txtHoTen.Text;
                    nhanVien.SoDienThoai = txtSDT.Text;
                    nhanVien.Email = txtEmail.Text;
                    nhanVien.MaTaiKhoan = txtMaTaiKhoan.Text;



                    // Lưu thay đổi vào cơ sở dữ liệu
                    context.SaveChanges();

                    // Tải lại dữ liệu lên DataGridView
                    BindGrid(context.NhanViens.ToList());
                    MessageBox.Show("Nhân viên đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên với mã đã nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo context để kết nối cơ sở dữ liệu
                KhoaHocContextDB context = new KhoaHocContextDB();

                // Lấy danh sách khóa học hiện tại từ cơ sở dữ liệu
                List<MODEL.NhanVien> listNhanVien = context.NhanViens.ToList();

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text))
                {
                    MessageBox.Show("Mã nhân viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNhanVien.Focus();
                    return;
                }

                // Tìm khóa học theo mã khóa học
                var nhanVien = listNhanVien.FirstOrDefault(s => s.MaNhanVien == txtMaNhanVien.Text);

                if (nhanVien != null)
                {
                    context.NhanViens.Remove(nhanVien);
                    context.SaveChanges();

                    BindGrid(context.NhanViens.ToList());
                    MessageBox.Show("nhân viên đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên với mã đã nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvDanhSach.Rows[e.RowIndex];

                txtMaNhanVien.Text = selectedRow.Cells["Column1"].Value.ToString();
                txtHoTen.Text = selectedRow.Cells["Column2"].Value.ToString();
                txtSDT.Text = selectedRow.Cells["Column3"].Value.ToString();
                txtEmail.Text = selectedRow.Cells["Column4"].Value.ToString();
                txtMaTaiKhoan.Text = selectedRow.Cells["Column5"].Value.ToString();
            }
        }

        private void txtMaNhanVien_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
