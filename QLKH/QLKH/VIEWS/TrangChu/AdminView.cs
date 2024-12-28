using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLKH.VIEWS.DanhSach;
using QLKH.VIEWS.NhanVien;

namespace QLKH.VIEWS.TrangChu
{
    public partial class AdminView : Form
    {
        public AdminView()
        {
            InitializeComponent();
        }

        private Form currentFormChild;

        private void openChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }

            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel1.Controls.Add(childForm);
            panel1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void btnKhoaHoc_Click(object sender, EventArgs e)
        {
            openChildForm(new frmKhoaHoc());
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            openChildForm(new frmNhanVien());
        }

        private void btnLopHoc_Click(object sender, EventArgs e)
        {

        }

        private void btnGiangVien_Click(object sender, EventArgs e)
        {
            openChildForm(new frmGiangVien());
        }
    }
}
