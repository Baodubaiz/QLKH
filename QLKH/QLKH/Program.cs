using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLKH.VIEWS;
using QLKH.VIEWS.DanhSach;
using QLKH.VIEWS.NhanVien;
using QLKH.VIEWS.TrangChu;

namespace QLKH
{
    internal static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AdminView());
        }
    }
}
