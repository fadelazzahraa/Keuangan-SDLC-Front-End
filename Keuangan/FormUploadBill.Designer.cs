namespace Keuangan
{
    partial class FormUploadBill
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
        /// 

        private void InitializeComponent()
        {
            pictureBox = new System.Windows.Forms.PictureBox();
            selectImageButton = new System.Windows.Forms.Button();
            uploadButton = new System.Windows.Forms.Button();
            textBox1 = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            pictureBox.Location = new System.Drawing.Point(17, 18);
            pictureBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new System.Drawing.Size(352, 349);
            pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // selectImageButton
            // 
            selectImageButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            selectImageButton.Location = new System.Drawing.Point(13, 373);
            selectImageButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            selectImageButton.Name = "selectImageButton";
            selectImageButton.Size = new System.Drawing.Size(357, 28);
            selectImageButton.TabIndex = 1;
            selectImageButton.Text = "Select Image";
            selectImageButton.UseVisualStyleBackColor = true;
            selectImageButton.Click += SelectImageButton_Click;
            // 
            // uploadButton
            // 
            uploadButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            uploadButton.Enabled = false;
            uploadButton.Location = new System.Drawing.Point(13, 490);
            uploadButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            uploadButton.Name = "uploadButton";
            uploadButton.Size = new System.Drawing.Size(357, 28);
            uploadButton.TabIndex = 0;
            uploadButton.Text = "Upload Bill";
            uploadButton.UseVisualStyleBackColor = true;
            uploadButton.Click += UploadButton_Click;
            // 
            // textBox1
            // 
            textBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox1.Enabled = false;
            textBox1.Location = new System.Drawing.Point(61, 406);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(309, 78);
            textBox1.TabIndex = 2;
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(14, 410);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(37, 15);
            label1.TabIndex = 3;
            label1.Text = "Detail";
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            label2.AutoSize = true;
            label2.Enabled = false;
            label2.Location = new System.Drawing.Point(155, 440);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(71, 15);
            label2.TabIndex = 4;
            label2.Text = "Uploading...";
            label2.Visible = false;
            // 
            // FormUploadBill
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(383, 532);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(uploadButton);
            Controls.Add(selectImageButton);
            Controls.Add(pictureBox);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            MaximizeBox = false;
            MinimumSize = new System.Drawing.Size(399, 571);
            Name = "FormUploadBill";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Upload Bill | CashFlow Tracker";
            Load += FormUploadBill_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button selectImageButton;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}