using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.Data.SqlClient;
using QLKH.MODELS;

namespace QLKH.VIEWS.NhanVien
{
    public partial class DSKhoaHoc : Form
    {
        public DSKhoaHoc()
        {
            InitializeComponent();

            dgvDanhSach.SelectionChanged += dgvDanhSach_SelectionChanged;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo context để kết nối cơ sở dữ liệu
                KhoaHocContextDB context = new KhoaHocContextDB();

                // Lấy danh sách khóa học hiện tại từ cơ sở dữ liệu
                List<KhoaHoc> listKhoaHoc = context.KhoaHocs.ToList();

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtMaKhoaHoc.Text))
                {
                    MessageBox.Show("Mã khóa học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaKhoaHoc.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtTenKhoaHoc.Text))
                {
                    MessageBox.Show("Tên khóa học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenKhoaHoc.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtThoiGianHoc.Text))
                {
                    MessageBox.Show("Thời gian học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtThoiGianHoc.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtLichHoc.Text))
                {
                    MessageBox.Show("Lịch học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLichHoc.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtGiangVien.Text))
                {
                    MessageBox.Show("Mã giảng viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGiangVien.Focus();
                    return;
                }

                // Kiểm tra trùng lặp mã khóa học
                if (listKhoaHoc.Any(s => s.MaKhoaHoc == txtMaKhoaHoc.Text))
                {
                    MessageBox.Show("Mã khóa học đã tồn tại. Vui lòng nhập mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaKhoaHoc.Focus();
                    return;
                }

                // Tạo một đối tượng KhoaHoc mới
                var newKhoaHoc = new KhoaHoc
                {
                    MaKhoaHoc = txtMaKhoaHoc.Text,
                    TenKhoaHoc = txtTenKhoaHoc.Text,
                    ThoiGianHoc = txtThoiGianHoc.Text,
                    LichHoc = txtLichHoc.Text,
                    MaGiangVien = txtGiangVien.Text,
                };

                // Thêm khóa học mới vào cơ sở dữ liệu
                context.KhoaHocs.Add(newKhoaHoc);
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
                BindGrid(context.KhoaHocs.ToList());
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
                KhoaHocContextDB context = new KhoaHocContextDB();

                // Lấy danh sách khóa học hiện tại từ cơ sở dữ liệu
                List<KhoaHoc> listKhoaHoc = context.KhoaHocs.ToList();

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtMaKhoaHoc.Text))
                {
                    MessageBox.Show("Mã khóa học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaKhoaHoc.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtTenKhoaHoc.Text))
                {
                    MessageBox.Show("Tên khóa học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenKhoaHoc.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtThoiGianHoc.Text))
                {
                    MessageBox.Show("Thời gian học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtThoiGianHoc.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtLichHoc.Text))
                {
                    MessageBox.Show("Lịch học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLichHoc.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtGiangVien.Text))
                {
                    MessageBox.Show("Mã giảng viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGiangVien.Focus();
                    return;
                }

                // Tìm khóa học theo mã khóa học
                var khoaHoc = listKhoaHoc.FirstOrDefault(s => s.MaKhoaHoc == txtMaKhoaHoc.Text);

                if (khoaHoc != null)
                {
                    // Cập nhật thông tin khóa học
                    khoaHoc.TenKhoaHoc = txtTenKhoaHoc.Text;
                    khoaHoc.ThoiGianHoc = txtThoiGianHoc.Text;
                    khoaHoc.LichHoc = txtLichHoc.Text;
                    khoaHoc.MaGiangVien = txtGiangVien.Text;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    context.SaveChanges();

                    // Tải lại dữ liệu lên DataGridView
                    BindGrid(context.KhoaHocs.ToList());
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
                KhoaHocContextDB context = new KhoaHocContextDB();

                // Lấy danh sách khóa học hiện tại từ cơ sở dữ liệu
                List<KhoaHoc> listKhoaHoc = context.KhoaHocs.ToList();

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtMaKhoaHoc.Text))
                {
                    MessageBox.Show("Mã khóa học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaKhoaHoc.Focus();
                    return;
                }

                // Tìm khóa học theo mã khóa học
                var khoaHoc = listKhoaHoc.FirstOrDefault(s => s.MaKhoaHoc == txtMaKhoaHoc.Text);

                if (khoaHoc != null)
                {
                    // Xóa khóa học khỏi cơ sở dữ liệu
                    context.KhoaHocs.Remove(khoaHoc);
                    context.SaveChanges();

                    // Tải lại dữ liệu lên DataGridView
                    BindGrid(context.KhoaHocs.ToList());
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

        private void DSKhoaHoc_Load(object sender, EventArgs e)
        {
            try
            {
                KhoaHocContextDB context = new KhoaHocContextDB();
                List<KhoaHoc> listKhoaHoc = context.KhoaHocs.ToList(); //lấy sinh viên
                KhoaHoc khoahoc = new KhoaHoc();
                BindGrid(listKhoaHoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BindGrid(List<KhoaHoc> DSKhoaHoc)
        {
            dgvDanhSach.Rows.Clear();
            foreach (var item in DSKhoaHoc)
            {
                int index = dgvDanhSach.Rows.Add();
                dgvDanhSach.Rows[index].Cells[0].Value = item.MaKhoaHoc;
                dgvDanhSach.Rows[index].Cells[1].Value = item.TenKhoaHoc;
                dgvDanhSach.Rows[index].Cells[2].Value = item.ThoiGianHoc;
                dgvDanhSach.Rows[index].Cells[3].Value = item.LichHoc;
                dgvDanhSach.Rows[index].Cells[4].Value = item.GiangVien.HoTen;
            }
        }



        private void dgvDanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy hàng được chọn từ DataGridView
                DataGridViewRow selectedRow = dgvDanhSach.Rows[e.RowIndex];

                // Hiển thị dữ liệu từ hàng được chọn lên các TextBox
                txtMaKhoaHoc.Text = selectedRow.Cells["Column1"].Value.ToString();
                txtTenKhoaHoc.Text = selectedRow.Cells["Column2"].Value.ToString();
                txtThoiGianHoc.Text = selectedRow.Cells["Column3"].Value.ToString();
                txtLichHoc.Text = selectedRow.Cells["Column4"].Value.ToString();
                txtGiangVien.Text = selectedRow.Cells["Column5"].Value.ToString();
            }
        }

        private void dgvDanhSach_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDanhSach.SelectedRows.Count > 0)
            {
                var row = dgvDanhSach.SelectedRows[0];
                txtMaKhoaHoc.Text = row.Cells["Column1"].Value.ToString();
                txtTenKhoaHoc.Text = row.Cells["Column2"].Value.ToString();
                txtThoiGianHoc.Text = row.Cells["Column3"].Value.ToString();
                txtLichHoc.Text = row.Cells["Column4"].Value.ToString();
                txtGiangVien.Text = row.Cells["Column5"].Value.ToString();
            }
        }
    }
}
