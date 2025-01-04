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
using QLKH.VIEWS.NhanVien;

namespace QLKH.Report
{
    public partial class ReportKhoaHoc : Form
    {
        public ReportKhoaHoc()
        {
            InitializeComponent();
        }

        private void ReportKhoaHoc_Load(object sender, EventArgs e)
        {
            KhoaHocContextDB context = new KhoaHocContextDB();
            List<KhoaHoc> listKhoaHoc = context.KhoaHocs.ToList();
            List<RPKhoaHoc> listReport = new List<RPKhoaHoc>();
            foreach (KhoaHoc sv in listKhoaHoc)
            {
                RPKhoaHoc temp = new RPKhoaHoc();
                temp.MaKhoaHoc = sv.MaKhoaHoc;
                temp.TenKhoaHoc = sv.TenKhoaHoc;
                temp.ThoiGianHoc = sv.ThoiGianHoc;
                temp.LichHoc = sv.LichHoc;
                temp.MaGiangVien = sv.MaGiangVien;

                listReport.Add(temp);
            }
            reportViewer1.LocalReport.ReportPath = "RPKhoaHoc.rdlc";
            var source = new ReportDataSource("DataSetKhoaHoc", listReport);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.RefreshReport();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            frmKhoaHoc khoaHoc = new frmKhoaHoc();
            khoaHoc.Show();
        }
    }
}
