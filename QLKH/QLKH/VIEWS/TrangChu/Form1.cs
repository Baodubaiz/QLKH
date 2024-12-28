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
    public partial class NhanVien_View : Form
    {
        public NhanVien_View()
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

        private void button1_Click(object sender, EventArgs e)
        {
            openChildForm(new DSKhoaHoc());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new DSNhanVien());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new DSKhoaHoc());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openChildForm(new DSKhoaHoc());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openChildForm(new DSKhoaHoc());
        }
    }
}
