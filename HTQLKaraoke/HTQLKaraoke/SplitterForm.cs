using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTQLKaraoke
{
    public partial class SplitterForm : Form
    {
        private Panel leftPanel;  // Panel cố định
        private Panel dynamicPanel;  // Panel kéo
        private Panel splitter;  // Thanh kéo
        private bool isDragging = false;  // Trạng thái kéo
        private int startMouseX;  // Vị trí chuột bắt đầu kéo
        private Label arrowLabel; // Mũi tên chỉ dẫn
        private Timer blinkTimer;

        public SplitterForm()
        {
            this.Text = "Mở cửa công ty đi";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(865, 653);
            BackgroundImage = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/cuaphai.png");

            // Panel bên trái (cố định)
            leftPanel = new Panel
            {
                BackgroundImage = Image.FromFile(@"/HTQLKaraoke/HTQLKaraoke/Image/cuatrai.png"),
                Dock = DockStyle.Left,
                Width = 431
            };

            dynamicPanel = new Panel
            {
                BackColor = Color.White,
                Dock = DockStyle.Left,
                Width = 0
            };

            splitter = new Panel
            {
                BackColor = Color.Gray,
                Width = 4,
                Dock = DockStyle.Left,
                Cursor = Cursors.SizeWE
            };

            arrowLabel = new Label
            {
                Text = "➔", // Ký tự Unicode mũi tên
                Font = new Font("Arial", 70, FontStyle.Bold),
                ForeColor = Color.Red,
                BackColor = Color.Transparent,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Left
            };


            // Hiệu ứng nhấp nháy
            blinkTimer = new Timer { Interval = 500 }; // 500ms
            blinkTimer.Tick += (s, e) => arrowLabel.Visible = !arrowLabel.Visible; // Hiển thị/ẩn
            blinkTimer.Start();

            splitter.MouseDown += Splitter_MouseDown;
            splitter.MouseMove += Splitter_MouseMove;
            splitter.MouseUp += Splitter_MouseUp;

            // Thêm các thành phần vào Form
            this.Controls.Add(dynamicPanel);
            this.Controls.Add(splitter);
            this.Controls.Add(leftPanel);
            this.Controls.Add(arrowLabel); // Thêm cuối cùng để đảm bảo nó ở trên các control khác
            arrowLabel.BringToFront(); // Đưa mũi tên lên trên

        }

        private void Splitter_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                startMouseX = e.X;
            }
        }
        private int speedLimit = 3; // Giới hạn số pixel thay đổi mỗi lần kéo

        private void Splitter_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = e.X - startMouseX;

                // Giới hạn thay đổi chiều rộng
                if (Math.Abs(deltaX) > speedLimit)
                {
                    deltaX = speedLimit * Math.Sign(deltaX); // Điều chỉnh theo hướng kéo
                }

                int newWidth = dynamicPanel.Width + deltaX;

                if (newWidth >= 0 && newWidth <= this.ClientSize.Width - leftPanel.Width)
                {
                    dynamicPanel.Width = newWidth;
                }
            }
        }


        private void Splitter_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;

                if (dynamicPanel.Width >= 200)
                {
                    var mainForm = new frmMain();
                    mainForm.FormClosed += MainForm_FormClosed; 
                    mainForm.Show();
                    this.Hide(); 
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); 
        }

    }
}
