namespace HTQLKaraoke
{
    partial class frmCaiDatHeThong
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCaiDatHeThong));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.branchCodeLabel = new System.Windows.Forms.Label();
            this.btnShowBranchInfo = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.txtTenChiNhanh = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(135, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cài Đặt Chuỗi Chi Nhánh";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(252, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tên Chi Nhánh:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(40, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Mã Chi Nhánh:";
            // 
            // branchCodeLabel
            // 
            this.branchCodeLabel.AutoSize = true;
            this.branchCodeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.branchCodeLabel.Location = new System.Drawing.Point(141, 83);
            this.branchCodeLabel.Name = "branchCodeLabel";
            this.branchCodeLabel.Size = new System.Drawing.Size(105, 17);
            this.branchCodeLabel.TabIndex = 10;
            this.branchCodeLabel.Text = "chọn chi nhánh";
            this.branchCodeLabel.Visible = false;
            // 
            // btnShowBranchInfo
            // 
            this.btnShowBranchInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnShowBranchInfo.Image")));
            this.btnShowBranchInfo.Location = new System.Drawing.Point(127, 132);
            this.btnShowBranchInfo.Name = "btnShowBranchInfo";
            this.btnShowBranchInfo.Size = new System.Drawing.Size(119, 58);
            this.btnShowBranchInfo.TabIndex = 11;
            this.btnShowBranchInfo.UseVisualStyleBackColor = true;
            this.btnShowBranchInfo.Click += new System.EventHandler(this.btnShowBranchInfo_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.Image = ((System.Drawing.Image)(resources.GetObject("btnHuy.Image")));
            this.btnHuy.Location = new System.Drawing.Point(328, 132);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(119, 58);
            this.btnHuy.TabIndex = 50;
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // txtTenChiNhanh
            // 
            this.txtTenChiNhanh.Enabled = false;
            this.txtTenChiNhanh.Location = new System.Drawing.Point(373, 83);
            this.txtTenChiNhanh.Name = "txtTenChiNhanh";
            this.txtTenChiNhanh.Size = new System.Drawing.Size(160, 20);
            this.txtTenChiNhanh.TabIndex = 51;
            // 
            // frmCaiDatHeThong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(567, 231);
            this.Controls.Add(this.txtTenChiNhanh);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnShowBranchInfo);
            this.Controls.Add(this.branchCodeLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmCaiDatHeThong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cài đặt chuỗi chi nhánh";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label branchCodeLabel;
        private System.Windows.Forms.Button btnShowBranchInfo;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.TextBox txtTenChiNhanh;
    }
}