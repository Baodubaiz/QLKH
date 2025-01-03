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
    public partial class frmGiangVien : Form
    {
        public frmGiangVien()
        {
            InitializeComponent();
        }

        private void DsGiangVien_Load(object sender, EventArgs e)
        {
            try
            {
                KhoaHocContextDB context = new KhoaHocContextDB();
                List<GiangVien> ListGiangVien = context.GiangViens.ToList();
                BindGrid(ListGiangVien);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindGrid(List<GiangVien> ListGiangVien)
        {
            dgvDsGiangVien.Rows.Clear();
            foreach (var item in ListGiangVien)
            { 
                int index = dgvDsGiangVien.Rows.Add();
                dgvDsGiangVien.Rows[index].Cells[0].Value = item.MaGiangVien;
                dgvDsGiangVien.Rows[index].Cells[1].Value = item.HoTen;
                dgvDsGiangVien.Rows[index].Cells[2].Value = item.SoDienThoai;
                dgvDsGiangVien.Rows[index].Cells[3].Value = item.Email;
                dgvDsGiangVien.Rows[index].Cells[4].Value = item.MaTaiKhoan;
            }
        }

        private void dgvDsGiangVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String MaGiangVien = dgvDsGiangVien.Rows[e.RowIndex].Cells[0].Value.ToString();
            String HoTen = dgvDsGiangVien.Rows[e.RowIndex].Cells[1].Value.ToString();
            String SDT = dgvDsGiangVien.Rows[e.RowIndex].Cells[2].Value.ToString();
            String Email = dgvDsGiangVien.Rows[e.RowIndex].Cells[3].Value.ToString();
            String MaTaiKhoan = dgvDsGiangVien.Rows[e.RowIndex].Cells[4].Value.ToString();

            txtMaGiangVien.Text = MaGiangVien;
            txtTenGiangVien.Text = HoTen;
            txtSDT.Text = SDT;
            txtEmail.Text = Email;
            txtMaTaiKhoan.Text = MaTaiKhoan;
        }

        private void btnThemGV_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaGiangVien.Text))
                {
                    MessageBox.Show("Mã Giảng viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtMaGiangVien.Text.Length > 10 || txtMaGiangVien.Text.Any(c => !char.IsLetterOrDigit(c)))
                {
                    MessageBox.Show("Mã Giảng viên không hợp lệ! Độ dài tối đa 10 ký tự, không chứa khoảng trắng hoặc ký tự đặc biệt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTenGiangVien.Text))
                {
                    MessageBox.Show("Họ và tên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtTenGiangVien.Text.Length > 255 || txtTenGiangVien.Text.Any(c => !char.IsLetter(c) && !char.IsWhiteSpace(c)))
                {
                    MessageBox.Show("Họ và tên chỉ được chứa chữ cái và khoảng trắng, độ dài tối đa 255 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSDT.Text))
                {
                    MessageBox.Show("Số điện thoại không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtSDT.Text.Length > 10 || txtSDT.Text.Any(c => !char.IsDigit(c)))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ! Độ dài tối đa 10 ký tự, chỉ chứa chữ số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Email không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!txtEmail.Text.EndsWith("@gmail.com") || !txtEmail.Text.Contains("@"))
                {
                    MessageBox.Show("Email không hợp lệ! Vui lòng nhập email đúng định dạng (@gmail.com).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }






                KhoaHocContextDB context = new KhoaHocContextDB();
                List<GiangVien> DsGiangVien = context.GiangViens.ToList();

                if (DsGiangVien.Any(s => s.MaGiangVien == txtMaGiangVien.Text))
                {
                    MessageBox.Show("Mã Giảng viên đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (DsGiangVien.Any(s => s.Email == txtEmail.Text))
                {
                    MessageBox.Show("Email đã được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
               

                var newGiangVien = new GiangVien
                {
                    MaGiangVien = txtMaGiangVien.Text,
                    HoTen = txtTenGiangVien.Text,
                    SoDienThoai = txtSDT.Text,
                    Email = txtEmail.Text,
                    MaTaiKhoan = txtMaTaiKhoan.Text,
                };

                context.GiangViens.Add(newGiangVien);
                context.SaveChanges();

                BindGrid(context.GiangViens.ToList());
                MessageBox.Show("Thêm giảng viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaGV_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaGiangVien.Text))
                {
                    MessageBox.Show("Mã giảng viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtMaGiangVien.Text.Length > 10 || txtMaGiangVien.Text.Any(char.IsWhiteSpace) || !txtMaGiangVien.Text.All(c => char.IsLetterOrDigit(c)))
                {
                    MessageBox.Show("Mã giảng viên không hợp lệ! Độ dài tối đa 10 ký tự, không chứa khoảng trắng hoặc ký tự đặc biệt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtTenGiangVien.Text))
                {
                    MessageBox.Show("Họ và tên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtTenGiangVien.Text.Length > 255 || !txtTenGiangVien.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                {
                    MessageBox.Show("Họ và tên không hợp lệ! Độ dài tối đa 255 ký tự và chỉ được phép chứa chữ cái và khoảng trắng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtSDT.Text))
                {
                    MessageBox.Show("Số điện thoại không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtSDT.Text.Length > 10 || !txtSDT.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ! Độ dài tối đa 10 ký tự và chỉ được phép chứa số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Email không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!txtEmail.Text.EndsWith("@gmail.com"))
                {
                    MessageBox.Show("Email phải đúng định dạng và kết thúc bằng '@gmail.com'!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                KhoaHocContextDB context = new KhoaHocContextDB();
                List<GiangVien> giangViens = context.GiangViens.ToList();
                var giangvien = giangViens.FirstOrDefault(s => s.MaGiangVien == txtMaGiangVien.Text);
                if (giangvien != null)
                {
                    if (giangViens.Any(s => s.Email == txtEmail.Text.Trim() && s.MaGiangVien != giangvien.MaGiangVien))
                    {
                        MessageBox.Show("Email đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    giangvien.MaGiangVien = txtMaGiangVien.Text.Trim();
                    giangvien.HoTen = txtTenGiangVien.Text.Trim();
                    giangvien.SoDienThoai = txtSDT.Text.Trim();
                    giangvien.Email = txtEmail.Text.Trim();

                    context.SaveChanges();

                    BindGrid(context.GiangViens.ToList());
                    MessageBox.Show("Chỉnh sửa thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy giảng viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaGV_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaGiangVien.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã giảng viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                KhoaHocContextDB context = new KhoaHocContextDB();

                var giangvien = context.GiangViens.FirstOrDefault(s => s.MaGiangVien == txtMaGiangVien.Text.Trim());
                if (giangvien != null)
                {
                    var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa giảng viên: {giangvien.HoTen}?",
                                                 "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        context.GiangViens.Remove(giangvien);
                        context.SaveChanges();

                        BindGrid(context.GiangViens.ToList());
                        MessageBox.Show("Xóa giảng viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy giảng viên với mã này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMaGiangVien_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
