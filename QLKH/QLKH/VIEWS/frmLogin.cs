using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using QLKH.VIEWS.TEACHERS.views.TEACHERS;
using QLKH.VIEWS.TrangChu;

namespace QLKH
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ form
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Chuỗi kết nối đến SQL Server (thay đổi theo cấu hình của bạn)
            string connectionString = "Data Source=DESKTOP-RAH6IHC\\MSSQLSERVER1;Initial Catalog=QLKH;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Truy vấn kiểm tra tài khoản
                    string query = @"
                        SELECT MaTaiKhoan, Role
                        FROM TaiKhoan
                        WHERE Username = @Username AND Password = @Password";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            // Lấy thông tin Role
                            string role = reader["Role"].ToString();
                            string maTaiKhoan = reader["MaTaiKhoan"].ToString();

                            // Chuyển hướng dựa trên Role
                            MessageBox.Show($"Đăng nhập thành công! Role: {role}");

                            if (role == "giangvien")
                            {
                                // Chuyển đến giao diện Giảng Viên
                                GiangVienView gvForm = new GiangVienView();
                                gvForm.Show();
                                this.Hide();
                            }
                            else if (role == "nhanvien")
                            {
                                // Chuyển đến giao diện Nhân Viên
                                NhanVienView nvForm = new NhanVienView();
                                nvForm.Show();
                                this.Hide();
                            }
                            else if (role == "admin")
                            {
                                // Chuyển đến giao diện Nhân Viên
                                AdminView nvForm = new AdminView();
                                nvForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Role không hợp lệ!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
    
}
