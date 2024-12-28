using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLKH.MODELS;

namespace QLKH.VIEWS.DanhSach
{
    public partial class DSNhanVien : Form
    {
        public DSNhanVien()
        {
            InitializeComponent();
            dgvDanhSach.SelectionChanged += dgvDanhSach_SelectionChanged;
        }

        private void DSNhanVien_Load(object sender, EventArgs e)
        {
            try
            {
                NhanVienContextDB context = new NhanVienContextDB();
                List<MODELS.NhanVien> listNhanVien = context.NhanViens.ToList();
                MODELS.NhanVien nhanVien = new MODELS.NhanVien();
                BindGrid(listNhanVien);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindGrid(List<MODELS.NhanVien> DSNhanVien)
        {
            dgvDanhSach.Rows.Clear();
            foreach (var item in DSNhanVien)
            {
                int index = dgvDanhSach.Rows.Add();
                dgvDanhSach.Rows[index].Cells[0].Value = item.MaNhanVien;
                dgvDanhSach.Rows[index].Cells[1].Value = item.HoTen;
                dgvDanhSach.Rows[index].Cells[2].Value = item.SoDienThoai;
                dgvDanhSach.Rows[index].Cells[3].Value = item.Email;
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
                NhanVienContextDB context = new NhanVienContextDB();

                // Lấy danh sách khóa học hiện tại từ cơ sở dữ liệu
                List<MODELS.NhanVien> listNhanVien = context.NhanViens.ToList();

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text))
                {
                    MessageBox.Show("Mã khóa học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNhanVien.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show("Tên khóa học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHoTen.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtSDT.Text))
                {
                    MessageBox.Show("Thời gian học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSDT.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Lịch học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }

                // Kiểm tra trùng lặp mã khóa học
                if (listNhanVien.Any(s => s.MaNhanVien == txtMaNhanVien.Text))
                {
                    MessageBox.Show("Mã khóa học đã tồn tại. Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNhanVien.Focus();
                    return;
                }

                // Tạo một đối tượng KhoaHoc mới
                var newNhanVien = new MODELS.NhanVien
                {
                    MaNhanVien = txtMaNhanVien.Text,
                    HoTen = txtHoTen.Text,
                    SoDienThoai = txtSDT.Text,
                    Email = txtEmail.Text,
                };

                // Thêm khóa học mới vào cơ sở dữ liệu
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
                MessageBox.Show("Thêm khóa học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                NhanVienContextDB context = new NhanVienContextDB();

                // Lấy danh sách khóa học hiện tại từ cơ sở dữ liệu
                List<MODELS.NhanVien> listNhanVien = context.NhanViens.ToList();

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text))
                {
                    MessageBox.Show("Mã khóa học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNhanVien.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show("Tên khóa học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("Lịch học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }


                // Tìm khóa học theo mã khóa học
                var nhanVien = listNhanVien.FirstOrDefault(s => s.MaNhanVien == txtMaNhanVien.Text);

                if (nhanVien != null)
                {
                    // Cập nhật thông tin khóa học
                    nhanVien.HoTen = txtHoTen.Text;
                    nhanVien.SoDienThoai = txtSDT.Text;
                    nhanVien.Email = txtEmail.Text;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    context.SaveChanges();

                    // Tải lại dữ liệu lên DataGridView
                    BindGrid(context.NhanViens.ToList());
                    MessageBox.Show("Khóa học đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khóa học với mã đã nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                NhanVienContextDB context = new NhanVienContextDB();

                // Lấy danh sách khóa học hiện tại từ cơ sở dữ liệu
                List<MODELS.NhanVien> listNhanVien = context.NhanViens.ToList();

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text))
                {
                    MessageBox.Show("Mã khóa học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNhanVien.Focus();
                    return;
                }

                // Tìm khóa học theo mã khóa học
                var nhanVien = listNhanVien.FirstOrDefault(s => s.MaNhanVien == txtMaNhanVien.Text);

                if (nhanVien != null)
                {
                    // Xóa khóa học khỏi cơ sở dữ liệu
                    context.NhanViens.Remove(nhanVien);
                    context.SaveChanges();

                    // Tải lại dữ liệu lên DataGridView
                    BindGrid(context.NhanViens.ToList());
                    MessageBox.Show("Khóa học đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khóa học với mã đã nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // Lấy hàng được chọn từ DataGridView
                DataGridViewRow selectedRow = dgvDanhSach.Rows[e.RowIndex];

                // Hiển thị dữ liệu từ hàng được chọn lên các TextBox
                txtMaNhanVien.Text = selectedRow.Cells["Column1"].Value.ToString();
                txtHoTen.Text = selectedRow.Cells["Column2"].Value.ToString();
                txtSDT.Text = selectedRow.Cells["Column3"].Value.ToString();
                txtEmail.Text = selectedRow.Cells["Column4"].Value.ToString();
            }
        }
    }
}
