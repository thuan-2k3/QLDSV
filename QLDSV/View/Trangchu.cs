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
using System.Xml.Linq;

namespace QLDSV.View
{
    public partial class Trangchu : Form
    {
        public Trangchu()
        {
            InitializeComponent();
            HideMenuItems();

            // Tạo instance Login và đăng ký sự kiện
            Login login = new Login();
            login.LoginSuccess += Login_LoginSuccess;

            // Thêm UserControl Login vào panel
            panelTC.Controls.Clear();
            login.Dock = DockStyle.Fill;
            panelTC.Controls.Add(login);

        }


        private void Login_LoginSuccess(object sender, EventArgs e)
        {
            panelTC.Controls.Clear();
            Trangchu trangChu = new Trangchu();
            trangChu.Dock = DockStyle.Fill;
            panelTC.Controls.Add(trangChu);
            trangChu.Show();
        }

        private void thôngTinKhoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Khoa khoa = new Khoa();
            panelTC.Controls.Clear();
            khoa.Dock = DockStyle.Fill;
            panelTC.Controls.Add(khoa);
            khoa.Show();
        }

        private void panelTC_Paint(object sender, PaintEventArgs e)
        {

        }

        private void thôngTinLớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lop lop = new Lop();
            panelTC.Controls.Clear();
            lop.Dock = DockStyle.Fill;
            panelTC.Controls.Add(lop);
            lop.Show();
        }

        private void mônToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Monhoc monhoc = new Monhoc();
            panelTC.Controls.Clear();
            monhoc.Dock = DockStyle.Fill;
            panelTC.Controls.Add(monhoc);
            monhoc.Show();

        }

        private void thôngTinSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sinhvien sinhvien = new Sinhvien();
            panelTC.Controls.Clear();
            sinhvien.Dock = DockStyle.Fill;
            panelTC.Controls.Add(sinhvien);
            sinhvien.Show();
        }

        private void bảngĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bangdiem bangdiem = new Bangdiem();
            panelTC.Controls.Clear();
            bangdiem.Dock = DockStyle.Fill;
            panelTC.Controls.Add(bangdiem);
            bangdiem.Show();
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            panelTC.Controls.Clear();
            login.Dock = DockStyle.Fill;
            panelTC.Controls.Add(login);
            login.Show();

        }



        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Ẩn lại các menu khi đăng xuất
                hidemenu();

                Login login = new Login();  // Giả sử bạn có thể tái sử dụng UserControl login
                panelTC.Controls.Clear();
                login.Dock = DockStyle.Fill;
                panelTC.Controls.Add(login);
                login.Show();
                show();
            }
        }

        public void hidemenu()
        {
            đăngXuấtToolStripMenuItem.Visible = false;
            thôngTinKhoaToolStripMenuItem.Visible = false;
            thôngTinLớpToolStripMenuItem.Visible = false;
            thôngTinSinhViênToolStripMenuItem.Visible = false;
            mônToolStripMenuItem.Visible = false;
            bảngĐiểmToolStripMenuItem.Visible = false;
            // đăngXuấtToolStripMenuItem.Visible = false;
            đăngNhậpToolStripMenuItem.Visible = false;

        }

        public void HideMenuItems()
        {
            thôngTinKhoaToolStripMenuItem.Visible = false;
            thôngTinLớpToolStripMenuItem.Visible = false;
            thôngTinSinhViênToolStripMenuItem.Visible = false;
            mônToolStripMenuItem.Visible = false;
            bảngĐiểmToolStripMenuItem.Visible = false;
            đăngXuấtToolStripMenuItem.Visible = false;
           // đăngNhậpToolStripMenuItem.Visible = false;

        }

        public void ShowMenuItems()
        {
            thôngTinKhoaToolStripMenuItem.Visible = true;
            thôngTinLớpToolStripMenuItem.Visible = true;
            thôngTinSinhViênToolStripMenuItem.Visible = true;
            mônToolStripMenuItem.Visible = true;
            bảngĐiểmToolStripMenuItem.Visible = true;
            đăngXuấtToolStripMenuItem.Visible = true;
            đăngNhậpToolStripMenuItem.Visible = false;
        }

        public void show()
        {
            đăngNhậpToolStripMenuItem.Visible = true;
        }

        private void text_Click(object sender, EventArgs e)
        {

        }
    }
}
