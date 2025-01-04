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
    public partial class ReportGiangVien : Form
    {
        public ReportGiangVien()
        {
            InitializeComponent();
        }

        private void ReportGiangVien_Load(object sender, EventArgs e)
        {
            KhoaHocContextDB context = new KhoaHocContextDB();
            List<GiangVien> listGiangVien = context.GiangViens.ToList();
            List<RPGiangVien> listReport = new List<RPGiangVien>();
            foreach (GiangVien sv in listGiangVien)
            {
                RPGiangVien temp = new RPGiangVien();
                temp.MaGiangVien = sv.MaGiangVien;
                temp.HoTen = sv.HoTen;
                temp.SoDienThoai = sv.SoDienThoai;
                temp.Email = sv.Email;

                listReport.Add(temp);
            }
            reportViewer1.LocalReport.ReportPath = "RPGiangVien.rdlc";
            var source = new ReportDataSource("DataSetGiangVien", listReport);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
