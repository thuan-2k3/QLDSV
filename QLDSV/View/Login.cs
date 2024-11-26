using QLDSV.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV.View
{
    public partial class Login : UserControl
    {

        public event EventHandler LoginSuccess;
        public Login()
        {
            InitializeComponent();

            this.Resize += (s, e) =>
            {
                panel1.Location = new Point((this.ClientSize.Width - panel1.Width) / 2,
                                            (this.ClientSize.Height - panel1.Height) / 2);
            };
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dangnhap_Click(object sender, EventArgs e)
        {
            string tenTK = name.Text.Trim();
            string matKhau = pass.Text.Trim();

            if (string.IsNullOrEmpty(tenTK) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản và mật khẩu.");
                return;
            }

            LoginController loginController = new LoginController();

            // Kiểm tra đăng nhập
            bool isLoginSuccessful = loginController.CheckLogin(tenTK, matKhau);

            if (isLoginSuccessful)
            {
                MessageBox.Show("Đăng nhập thành công!");

                // Đóng form đăng nhập
                Form parentForm = this.FindForm();
                if (parentForm != null && parentForm is Trangchu trangChu)
                {
                    panel1.Controls.Clear();
                    // Hiện các mục menu sau khi đăng nhập
                    trangChu.ShowMenuItems();

                    // Đóng UserControl Login và hiển thị Trangchu
                    parentForm.Controls.Remove(this);
                }
            }
            else
            {
                MessageBox.Show("Tên tài khoản hoặc mật khẩu không chính xác.");
            }
        }








    }
}
