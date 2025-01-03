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
using QLKH.VIEWS.DanhSach;


namespace QLKH
{
    public partial class frmLopHoc : Form
    {
        public frmLopHoc()
        {
            InitializeComponent();
        }

        private void BindGrid(List<LopHoc> DSLopHoc)
        {
            dgvLopHoc.Rows.Clear();
            foreach (var item in DSLopHoc)
            {
                int index = dgvLopHoc.Rows.Add();
                dgvLopHoc.Rows[index].Cells[0].Value = item.MaLop;
                dgvLopHoc.Rows[index].Cells[1].Value = item.MaKhoaHoc; 
                dgvLopHoc.Rows[index].Cells[2].Value = item.MaGiangVien;
                dgvLopHoc.Rows[index].Cells[3].Value = item.TenLop;
                dgvLopHoc.Rows[index].Cells[4].Value = item.NgayBatDau;
                dgvLopHoc.Rows[index].Cells[5].Value = item.NgayKetThuc;
                dgvLopHoc.Rows[index].Cells[6].Value = item.SoLuongHocVien;

            }
        }
        private void Classes_Load(object sender, EventArgs e)
        {
            try
            {
                KhoaHocContextDB context = new KhoaHocContextDB();
                List<LopHoc> listLopHoc = context.LopHocs.ToList(); //lấy sinh viên
                BindGrid(listLopHoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLop.Text))
            {
                MessageBox.Show("Vui lòng chọn lớp học để xem chi tiết học viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmChiTiet frmChiTiet = new frmChiTiet(txtMaLop.Text);
            frmChiTiet.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các textbox có giá trị không
                if (string.IsNullOrEmpty(txtMaLop.Text) ||
                    string.IsNullOrEmpty(txtMaKhoa.Text) ||
                    string.IsNullOrEmpty(txtMaGiangVien.Text) ||
                    string.IsNullOrEmpty(txtTenLopHoc.Text) ||
                    string.IsNullOrEmpty(txtNgayBatDau.Text) ||
                    string.IsNullOrEmpty(txtNgayKetThuc.Text) ||
                    string.IsNullOrEmpty(txtSoLuong.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                    return;
                }

                // Kết nối đến cơ sở dữ liệu
                using (KhoaHocContextDB context = new KhoaHocContextDB())
                {
                    // Kiểm tra sự tồn tại của mã khóa học
                    bool isMaKhoaHocValid = context.KhoaHocs.Any(kh => kh.MaKhoaHoc == txtMaKhoa.Text);
                    if (!isMaKhoaHocValid)
                    {
                        MessageBox.Show("Mã Khóa Học không hợp lệ. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kiểm tra sự tồn tại của mã giảng viên
                    bool isMaGiangVienValid = context.GiangViens.Any(gv => gv.MaGiangVien == txtMaGiangVien.Text);
                    if (!isMaGiangVienValid)
                    {
                        MessageBox.Show("Mã Giảng Viên không hợp lệ. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kiểm tra mã lớp đã tồn tại hay chưa
                    if (context.LopHocs.Any(s => s.MaLop == txtMaLop.Text))
                    {
                        MessageBox.Show("Mã lớp học đã tồn tại. Vui lòng nhập một mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Gọi constructor và truyền các tham số
                    var newLopHoc = new LopHoc
                    {
                        MaLop = txtMaLop.Text,
                        MaKhoaHoc = txtMaKhoa.Text,
                        MaGiangVien = txtMaGiangVien.Text,
                        TenLop = txtTenLopHoc.Text,
                        NgayBatDau = txtNgayBatDau.Text,
                        NgayKetThuc = txtNgayKetThuc.Text,
                        SoLuongHocVien = int.Parse(txtSoLuong.Text),
                    };

                    // Thêm lớp học mới vào cơ sở dữ liệu
                    context.LopHocs.Add(newLopHoc);

                    try
                    {
                        context.SaveChanges();
                        // Tải lại dữ liệu
                        BindGrid(context.LopHocs.ToList());
                        MessageBox.Show("Thêm lớp học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvLopHoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy hàng được chọn từ DataGridView
                DataGridViewRow selectedRow = dgvLopHoc.Rows[e.RowIndex];

                // Hiển thị dữ liệu từ hàng được chọn lên các TextBox
                txtMaLop.Text = selectedRow.Cells["Column1"].Value.ToString();
                txtMaKhoa.Text = selectedRow.Cells["Column6"].Value.ToString();
                txtMaGiangVien.Text = selectedRow.Cells["Column7"].Value.ToString();
                txtTenLopHoc.Text = selectedRow.Cells["Column2"].Value.ToString();
                txtNgayBatDau.Text = selectedRow.Cells["Column3"].Value.ToString();
                txtNgayKetThuc.Text = selectedRow.Cells["Column4"].Value.ToString();
                txtSoLuong.Text = selectedRow.Cells["Column5"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các textbox có giá trị không
                if (string.IsNullOrEmpty(txtMaLop.Text) ||
                    string.IsNullOrEmpty(txtMaKhoa.Text) ||
                    string.IsNullOrEmpty(txtMaGiangVien.Text) ||
                    string.IsNullOrEmpty(txtTenLopHoc.Text) ||
                    string.IsNullOrEmpty(txtNgayBatDau.Text) ||
                    string.IsNullOrEmpty(txtNgayKetThuc.Text) ||
                    string.IsNullOrEmpty(txtSoLuong.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                    return;
                }

                // Kết nối đến cơ sở dữ liệu
                using (KhoaHocContextDB context = new KhoaHocContextDB())
                {
                    // Tìm lớp học theo MaLop
                    var lopHoc = context.LopHocs.SingleOrDefault(l => l.MaLop == txtMaLop.Text);
                    if (lopHoc == null)
                    {
                        MessageBox.Show("Không tìm thấy lớp học với mã này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kiểm tra sự tồn tại của mã khóa học
                    bool isMaKhoaHocValid = context.KhoaHocs.Any(kh => kh.MaKhoaHoc == txtMaKhoa.Text);
                    if (!isMaKhoaHocValid)
                    {
                        MessageBox.Show("Mã Khóa Học không hợp lệ. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kiểm tra sự tồn tại của mã giảng viên
                    bool isMaGiangVienValid = context.GiangViens.Any(gv => gv.MaGiangVien == txtMaGiangVien.Text);
                    if (!isMaGiangVienValid)
                    {
                        MessageBox.Show("Mã Giảng Viên không hợp lệ. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Cập nhật thông tin lớp học
                    lopHoc.MaKhoaHoc = txtMaKhoa.Text;
                    lopHoc.MaGiangVien = txtMaGiangVien.Text;
                    lopHoc.TenLop = txtTenLopHoc.Text;
                    lopHoc.NgayBatDau = txtNgayBatDau.Text;
                    lopHoc.NgayKetThuc = txtNgayKetThuc.Text;
                    lopHoc.SoLuongHocVien = int.Parse(txtSoLuong.Text);

                    // Lưu thay đổi
                    context.SaveChanges();

                    // Tải lại dữ liệu
                    BindGrid(context.LopHocs.ToList());
                    MessageBox.Show("Cập nhật lớp học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem người dùng đã chọn lớp học nào trong DataGridView chưa
                if (string.IsNullOrEmpty(txtMaLop.Text))
                {
                    MessageBox.Show("Vui lòng chọn lớp học cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hiển thị hộp thoại xác nhận xóa
                DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa lớp học này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes) return;

                // Kết nối đến cơ sở dữ liệu
                using (KhoaHocContextDB context = new KhoaHocContextDB())
                {
                    // Tìm lớp học theo MaLop
                    var lopHoc = context.LopHocs.SingleOrDefault(l => l.MaLop == txtMaLop.Text);
                    if (lopHoc == null)
                    {
                        MessageBox.Show("Không tìm thấy lớp học với mã này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Xóa lớp học khỏi cơ sở dữ liệu
                    context.LopHocs.Remove(lopHoc);

                    // Lưu thay đổi
                    context.SaveChanges();

                    // Tải lại dữ liệu
                    BindGrid(context.LopHocs.ToList());
                    MessageBox.Show("Xóa lớp học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
