using QLDSV.Controller;
using QLDSV.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Windows.Forms;

namespace QLDSV.View
{
    public partial class Bangdiem : UserControl, IView 
    {
        BangdiemComtroller controller = new BangdiemComtroller();
        SinhvienController svcontroller = new SinhvienController();
        MonhocController mhcontroller = new MonhocController();

        public Bangdiem()
        {
            InitializeComponent();
            LoadDataGrid();


            this.Resize += (s, e) =>
            {
                panel1.Location = new Point((this.ClientSize.Width - panel1.Width) / 2,
                                            (this.ClientSize.Height - panel1.Height) / 2);
            };

            diemtp.TextChanged += diemtp_TextChanged;
            diemthi.TextChanged += diemthi_TextChanged;

            LoadSinhvienToComboBox();
            commasv.SelectedIndexChanged += commasv_SelectedIndexChanged;

            LoadMonhocToComboBox();
            commamh.SelectedIndexChanged += combomamh_SelectedIndexChanged;

            dataGridViewbangdiem.CellClick += dataGridViewbangdiem_CellClick;
          


        }

        public void GetDataFromText()
        {
            BangdiemModel bangDiem = new BangdiemModel
            {
                maSV = Convert.ToInt32(commasv.SelectedValue),
                maMH = commamh.SelectedValue.ToString(),
                diemThanhPhan = float.TryParse(diemtp.Text, out float dtp) ? dtp : 0,
                diemThi = float.TryParse(diemthi.Text, out float dthi) ? dthi : 0
            };

            //// Tính điểm tổng kết và xếp loại
            //bangDiem.diemTongKet = (bangDiem.diemThanhPhan * 0.4f) + (bangDiem.diemThi * 0.6f);
            //bangDiem.xepLoai = bangDiem.diemTongKet >= 8.5f ? "Giỏi" :
            //                    bangDiem.diemTongKet >= 6.5f ? "Khá" :
            //                    bangDiem.diemTongKet >= 5.0f ? "Trung Bình" : "Yếu";

            // Kiểm tra nếu bản ghi đã tồn tại
            if (controller.IsExist(bangDiem.maSV))
            {
                controller.Update(bangDiem);
                MessageBox.Show("Bảng điểm updated successfully.");
            }
            else
            {
                controller.Add(bangDiem);
                MessageBox.Show("New bảng điểm added successfully.");
            }
        }

        public void SetDataToText(object item)
        {
            if (item is BangdiemModel bangDiem)
            {
                commasv.SelectedValue = bangDiem.maSV;
                commamh.SelectedValue = bangDiem.maMH;
                texttensv.Text = bangDiem.tenSV;
                texttenmh.Text = bangDiem.tenMH;
                diemtp.Text = bangDiem.diemThanhPhan.ToString();
                diemthi.Text = bangDiem.diemThi.ToString();
                diemtk.Text = bangDiem.diemTongKet.ToString();
                xeploai.Text = bangDiem.xepLoai;
            }
            else
            {
                throw new ArgumentException("Item must be of type BangdiemModel");
            }
        }



        private void LoadComboBoxes()
        {
            // Load data for ComboBox of student IDs
            var studentList = svcontroller.LoadAll();
            commasv.DataSource = studentList;
            commasv.DisplayMember = "maSV";
            commasv.ValueMember = "maSV";

        }

        private void LoadSinhvienToComboBox()
        {
            SinhvienController svController = new SinhvienController();

            List<SinhvienModel> listSinhvien = svController.GetAllSinhvien();

            if (listSinhvien != null && listSinhvien.Count > 0)
            {
                commasv.DataSource = listSinhvien;       // ComboBox sinh viên
                commasv.DisplayMember = "maSV";         // Hiển thị tên sinh viên
                commasv.ValueMember = "maSV";            // Giá trị là mã sinh viên
            }
            else
            {
                MessageBox.Show("No data found in Sinhvien table.");
            }
        }

        private void commasv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (commasv.SelectedValue != null)
            {
                // Lấy mã sinh viên từ ComboBox
                object selectedMaSV = commasv.SelectedValue;

                // Sử dụng hàm Read của SinhvienController để lấy thông tin sinh viên
                SinhvienModel selectedStudent = svcontroller.Read(selectedMaSV) as SinhvienModel;

                // Kiểm tra và hiển thị tên sinh viên trong TextBox
                if (selectedStudent != null)
                {
                    texttensv.Text = selectedStudent.tenSV;
                }
                else
                {
                    texttensv.Text = string.Empty;
                }
            }
        }





        private void LoadMonhocToComboBox()
        {
            MonhocController mhController = new MonhocController();

            List<MonhocModel> listMonhoc = mhController.GetAllMonhoc();

            if (listMonhoc != null && listMonhoc.Count > 0)
            {
                commamh.DataSource = listMonhoc;       
                commamh.DisplayMember = "maMH";       
                commamh.ValueMember = "maMH";        
            }
            else
            {
                MessageBox.Show("No data found in Monhoc table.");
            }
        }

        private void combomamh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (commamh.SelectedValue != null)
            {
       
                object selectedMaMH = commamh.SelectedValue;

             
                MonhocModel selectedCourse = mhcontroller.Read(selectedMaMH) as MonhocModel;

                if (selectedCourse != null)
                {
                    texttenmh.Text = selectedCourse.tenMH;
                }
                else
                {
                    texttenmh.Text = string.Empty;
                }
            }
        }


        private void TinhDiemTongKetVaXepLoai()
        {
            try
            {

                float diemThanhPhan = float.Parse(diemtp.Text);
                float diemThi = string.IsNullOrEmpty(diemthi.Text) ? 0: Convert.ToSingle(diemthi.Text) ;


                float diemTongKet = (diemThanhPhan * 0.4f) + (diemThi * 0.6f);
                diemtk.Text = diemTongKet.ToString("0.00");  

                string xepLoai;
                if (diemTongKet >= 8.0 )
                {
                    xepLoai = "Giỏi";
                }
                else if (diemTongKet >= 7.0)
                {
                    xepLoai = "Khá";
                }
                else if (diemTongKet >= 5.5)
                {
                    xepLoai = "Trung Bình";
                }
                else if (diemTongKet >= 4.0)
                {
                    xepLoai = "Yếu";
                }
                else
                {
                    xepLoai = "Kém";
                }

                xeploai.Text = xepLoai;  // Hiển thị xếp loại trong TextBox
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng điểm.");
            }
        }


        private void LoadDataGrid()
        {
            if (controller.Load())
            {
                List<BangdiemModel> list = controller.Items
                    .OfType<BangdiemModel>()
                    .ToList();

                if (list.Count > 0)
                {
                    dataGridViewbangdiem.DataSource = list;
                    dataGridViewbangdiem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    MessageBox.Show("No data found.");
                }
            }
            else
            {
                MessageBox.Show("Failed to load data.");
            }
        }

        private void dataGridViewbangdiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewbangdiem.Rows[e.RowIndex];

                commasv.Text = row.Cells["maSV"].Value.ToString();
                commamh.Text = row.Cells["maMH"].Value.ToString();
                texttensv.Text = row.Cells["tenSV"].Value.ToString();
                texttenmh.Text = row.Cells["tenMH"].Value.ToString();
                diemtp.Text = row.Cells["diemThanhPhan"].Value.ToString();
                diemthi.Text = row.Cells["diemThi"].Value.ToString();
                diemtk.Text = row.Cells["diemTongKet"].Value.ToString();
                xeploai.Text = row.Cells["xepLoai"].Value.ToString();
            }
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void diemtp_TextChanged(object sender, EventArgs e)
        {
            TinhDiemTongKetVaXepLoai();

        }

        private void diemthi_TextChanged(object sender, EventArgs e)
        {
            TinhDiemTongKetVaXepLoai();
        }
        private BangdiemModel GetDataFromView()
        {
            try
            {
                BangdiemModel bangdiem = new BangdiemModel
                {
                    maSV = int.Parse(commasv.SelectedValue.ToString()), // Lấy giá trị từ ComboBox
                    maMH = commamh.SelectedValue.ToString(),
                    tenSV = texttensv.Text.Trim(), // Lấy giá trị từ TextBox
                    tenMH = texttenmh.Text.Trim(),
                    diemThanhPhan = float.Parse(diemtp.Text.Trim()),
                    diemThi = float.Parse(diemthi.Text.Trim()),
                    diemTongKet = float.Parse(diemtk.Text.Trim()),
                    xepLoai = xeploai.Text.Trim()
                };

                return bangdiem;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ form: " + ex.Message);
                return null;
            }
        }


        private bool KiemTraDuLieuDauVao()
        {
            if (string.IsNullOrEmpty(commasv.Text) || string.IsNullOrEmpty(commamh.Text))
            {
                MessageBox.Show("Vui lòng chọn đầy đủ mã sinh viên và mã môn học.");
                return false;
            }

            if (!float.TryParse(diemtp.Text, out _) || !float.TryParse(diemthi.Text, out _))
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng điểm.");
                return false;
            }

            return true;
        }


        private void save_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieuDauVao())
            {
                return;
            }

            try
            {
                BangdiemModel bangdiem = GetDataFromView();

                // Kiểm tra mã sinh viên có tồn tại trong bảng SinhVien không
                if (!controller.IsStudentValid(bangdiem.maSV))
                {
                    MessageBox.Show("Mã sinh viên không tồn tại. Vui lòng kiểm tra lại.");
                    return;
                }

                // Thêm mới điểm cho sinh viên
                if (controller.IsSubjectExist(bangdiem.maSV, bangdiem.maMH))
                {
                    MessageBox.Show("Môn học này đã tồn tại cho sinh viên này. Không thể thêm mới.");
                }
                else
                {
                    controller.Add(bangdiem);
                    MessageBox.Show("Thông tin đã được thêm mới thành công.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }

            LoadDataGrid();
        }



        private void close_Click(object sender, EventArgs e)
        {
            Trangchu mainForm = (Trangchu)this.ParentForm;
            mainForm.Show();
            this.Dispose();
        }

        private void srten_Click(object sender, EventArgs e)
        {
            string tenSV = txttimtensv.Text.Trim();
            var results = controller.SearchByTenSV(tenSV);

            if (results.Any())
            {
                // Hiển thị kết quả trong DataGridView hoặc MessageBox
                dataGridViewbangdiem.DataSource = results; 
            }
            else
            {
                MessageBox.Show("Không tìm thấy sinh viên nào có tên phù hợp.");
            }
        }

        private void srmh_Click(object sender, EventArgs e)
        {
            string tenMH = txttimmasv.Text.Trim();
            var results = controller.SearchByTenMH(tenMH);

            if (results.Any())
            {
                // Hiển thị kết quả trong DataGridView hoặc MessageBox
                dataGridViewbangdiem.DataSource = results; 
            }
            else
            {
                MessageBox.Show("Không tìm thấy môn học nào có tên phù hợp.");
            }
        }


    }
}
