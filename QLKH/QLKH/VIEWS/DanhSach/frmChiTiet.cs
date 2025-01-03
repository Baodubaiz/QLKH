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
    public partial class frmChiTiet : Form
    {
        private string maLop;
        public frmChiTiet(string maLop)
        {
            InitializeComponent();
            this.maLop = maLop;
            LoadHocVien();
        }

        private void LoadHocVien()
        {
            using (KhoaHocContextDB context = new KhoaHocContextDB())
            {
                var hocViens = context.HocViens.Where(hv => hv.MaLop == maLop).ToList();
                BindGrid(hocViens);
            }
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
        private void frmChiTiet_Load(object sender, EventArgs e)
        {
            try
            {
                KhoaHocContextDB context = new KhoaHocContextDB();
                List<HocVien> listStudent = context.HocViens.ToList(); //lấy sinh viên
                
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
    }
}
