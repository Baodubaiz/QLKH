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
    public partial class frmHocVien : Form
    {
        public frmHocVien()
        {
            InitializeComponent();
        }
        private void BindGrid(List<HocVien> DSHocVien)
        {
            dgvStudent.Rows.Clear();
            foreach (var item in DSHocVien)
            {
                int index = dgvStudent.Rows.Add();
                dgvStudent.Rows[index].Cells[0].Value = item.MaHocVien;
                dgvStudent.Rows[index].Cells[1].Value = item.MaLop; // Hiển thị mã lớp
                dgvStudent.Rows[index].Cells[2].Value = item.HoTen;
                dgvStudent.Rows[index].Cells[3].Value = item.GioiTinh;
                dgvStudent.Rows[index].Cells[4].Value = item.NgaySinh;
                dgvStudent.Rows[index].Cells[5].Value = item.DiaChi;
                dgvStudent.Rows[index].Cells[6].Value = item.SoDienThoai;
                dgvStudent.Rows[index].Cells[7].Value = item.Email;

            }
        }

        private void frmHocVien_Load(object sender, EventArgs e)
        {
            try
            {
                KhoaHocContextDB context = new KhoaHocContextDB();
                List<HocVien> listStudent = context.HocViens.ToList(); //lấy sinh viên
                BindGrid(listStudent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // Gán dữ liệu cho ComboBox
            var genders = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Nam", "Nam"),
                new KeyValuePair<string, string>("Nữ", "Nữ"),
                new KeyValuePair<string, string>("Khác", "Khác")
            };

            cmbGioiTinh.DataSource = genders;
            cmbGioiTinh.DisplayMember = "Value"; // Hiển thị
            cmbGioiTinh.ValueMember = "Key";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các textbox có giá trị không
                if (string.IsNullOrEmpty(txtMaHocVien.Text) ||
                    string.IsNullOrEmpty(txtMaLop.Text) ||
                    string.IsNullOrEmpty(txtHoTen.Text) ||
                    string.IsNullOrEmpty(txtNgaySinh.Text) ||
                    string.IsNullOrEmpty(txtSoDienThoai.Text) ||
                    string.IsNullOrEmpty(txtEmail.Text) ||
                    string.IsNullOrEmpty(txtDiaChi.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                    return;
                }

                // Kiểm tra cmbGioiTinh có giá trị không
                if (cmbGioiTinh.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn giới tính.");
                    return;
                }

                KhoaHocContextDB context = new KhoaHocContextDB();
                List<HocVien> studentLst = context.HocViens.ToList();

                // Kiểm tra xem mã học viên đã tồn tại hay chưa
                if (studentLst.Any(s => s.MaHocVien.ToString() == txtMaHocVien.Text))
                {
                    MessageBox.Show("Mã học viên đã tồn tại. Vui lòng nhập một mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gọi constructor và truyền các tham số
                var newStudent = new HocVien
                {
                    MaHocVien = txtMaHocVien.Text,
                    MaLop = txtMaLop.Text,
                    HoTen = txtHoTen.Text,
                    NgaySinh = txtNgaySinh.Text,
                    GioiTinh = cmbGioiTinh.SelectedValue.ToString(),
                    SoDienThoai = txtSoDienThoai.Text,
                    Email = txtEmail.Text,
                    DiaChi = txtDiaChi.Text,
                };

                var classExists = context.LopHocs.Any(l => l.MaLop == txtMaLop.Text);
                if (!classExists)
                {
                    MessageBox.Show("Mã lớp không hợp lệ. Vui lòng kiểm tra lại.");
                    return;
                }

                // Thêm học viên mới vào danh sách
                context.HocViens.Add(newStudent);
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

                // Tải lại dữ liệu
                BindGrid(context.HocViens.ToList());
                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy hàng được chọn từ DataGridView
                DataGridViewRow selectedRow = dgvStudent.Rows[e.RowIndex];

                // Hiển thị dữ liệu từ hàng được chọn lên các TextBox
                txtMaHocVien.Text = selectedRow.Cells["HocVien"].Value.ToString();
                txtMaLop.Text = selectedRow.Cells["MaLop"].Value.ToString();
                txtHoTen.Text = selectedRow.Cells["HoTen"].Value.ToString();
                cmbGioiTinh.Text = selectedRow.Cells["GioiTinh"].Value.ToString();
                txtNgaySinh.Text = selectedRow.Cells["NgaySinh"].Value.ToString();
                txtDiaChi.Text = selectedRow.Cells["DiaChi"].Value.ToString();
                txtSoDienThoai.Text = selectedRow.Cells["DienThoai"].Value.ToString();
                txtEmail.Text = selectedRow.Cells["Email"].Value.ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu nhập
                if (string.IsNullOrEmpty(txtMaHocVien.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã học viên cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (KhoaHocContextDB context = new KhoaHocContextDB())
                {
                    // Tìm học viên dựa trên mã học viên
                    var student = context.HocViens.SingleOrDefault(s => s.MaHocVien == txtMaHocVien.Text);

                    if (student == null)
                    {
                        MessageBox.Show("Không tìm thấy học viên với mã này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kiểm tra các trường thông tin khác
                    if (string.IsNullOrEmpty(txtMaLop.Text) ||
                        string.IsNullOrEmpty(txtHoTen.Text) ||
                        string.IsNullOrEmpty(txtNgaySinh.Text) ||
                        string.IsNullOrEmpty(txtSoDienThoai.Text) ||
                        string.IsNullOrEmpty(txtEmail.Text) ||
                        string.IsNullOrEmpty(txtDiaChi.Text) ||
                        cmbGioiTinh.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Cập nhật thông tin học viên
                    student.MaLop = txtMaLop.Text;
                    student.HoTen = txtHoTen.Text;
                    student.GioiTinh = cmbGioiTinh.SelectedValue.ToString();
                    student.NgaySinh = txtNgaySinh.Text;
                    student.DiaChi = txtDiaChi.Text;
                    student.SoDienThoai = txtSoDienThoai.Text;
                    student.Email = txtEmail.Text;

                    // Kiểm tra mã lớp có tồn tại
                    var classExists = context.LopHocs.Any(l => l.MaLop == txtMaLop.Text);
                    if (!classExists)
                    {
                        MessageBox.Show("Mã lớp không hợp lệ. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Lưu thay đổi vào cơ sở dữ liệu
                    context.SaveChanges();

                    // Tải lại dữ liệu lên DataGridView
                    BindGrid(context.HocViens.ToList());

                    MessageBox.Show("Cập nhật thông tin học viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                // Kiểm tra xem mã học viên có được nhập không
                if (string.IsNullOrEmpty(txtMaHocVien.Text))
                {
                    MessageBox.Show("Vui lòng chọn hoặc nhập mã học viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hiển thị hộp thoại xác nhận
                var confirmResult = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa học viên này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    using (KhoaHocContextDB context = new KhoaHocContextDB())
                    {
                        // Tìm học viên theo mã học viên
                        var student = context.HocViens.SingleOrDefault(s => s.MaHocVien == txtMaHocVien.Text);

                        if (student == null)
                        {
                            MessageBox.Show("Không tìm thấy học viên với mã này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Xóa học viên khỏi database
                        context.HocViens.Remove(student);
                        context.SaveChanges();

                        // Làm mới dữ liệu trên DataGridView
                        BindGrid(context.HocViens.ToList());

                        MessageBox.Show("Xóa học viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
