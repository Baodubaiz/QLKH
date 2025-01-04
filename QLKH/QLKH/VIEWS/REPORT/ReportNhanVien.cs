using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using QLKH.MODEL;
using QLKH.VIEWS.DanhSach;

namespace QLKH.Report
{
    public partial class ReportNhanVien : Form
    {
        public ReportNhanVien()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            frmNhanVien nhanVien = new frmNhanVien();
            nhanVien.Show();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            KhoaHocContextDB context = new KhoaHocContextDB();
            List<NhanVien> listNhanVien = context.NhanViens.ToList();
            List<RPNhanVien> listReport = new List<RPNhanVien>();
            foreach (NhanVien sv in listNhanVien)
            {
                RPNhanVien temp = new RPNhanVien();
                temp.MaNhanVien = sv.MaNhanVien;
                temp.HoTen = sv.HoTen;
                temp.SoDienThoai = sv.SoDienThoai;
                temp.Email = sv.Email;

                listReport.Add(temp);
            }
            reportViewer1.LocalReport.ReportPath = "RPNhanVien.rdlc";
            var source = new ReportDataSource("DataSetNhanVien", listReport);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.RefreshReport();
        }
    }
}
