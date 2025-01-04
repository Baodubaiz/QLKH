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
    public partial class ReportHocVien : Form
    {
        public ReportHocVien()
        {
            InitializeComponent();
        }

        private void ReportHocVien_Load(object sender, EventArgs e)
        {
            KhoaHocContextDB context = new KhoaHocContextDB();
            List<HocVien> listHocVien = context.HocViens.ToList();
            List<RPHocVien> listReport = new List<RPHocVien>();
            foreach (HocVien sv in listHocVien)
            {
                RPHocVien temp = new RPHocVien();
                temp.MaHocVien = sv.MaHocVien;
                temp.MaLop=sv.MaLop;
                temp.HoTen = sv.HoTen;
                temp.GioiTinh = sv.GioiTinh;
                temp.NgaySinh = sv.NgaySinh;
                temp.DiaChi = sv.DiaChi;
                temp.SoDienThoai = sv.SoDienThoai;
                temp.Email = sv.Email;

                listReport.Add(temp);
            }
            reportViewer1.LocalReport.ReportPath = "RPHocVien.rdlc";
            var source = new ReportDataSource("DataSetHocVien", listReport);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.RefreshReport();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            frmHocVien hocVien = new frmHocVien();
            hocVien.Show();
        }
    }
}
