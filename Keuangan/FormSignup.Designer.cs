namespace Keuangan
{
    partial class FormSignup
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
            textBox1 = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            textBox2 = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            textBox3 = new System.Windows.Forms.TextBox();
            checkBox1 = new System.Windows.Forms.CheckBox();
            pictureBox3 = new System.Windows.Forms.PictureBox();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            label5 = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            label4 = new System.Windows.Forms.Label();
            button3 = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(327, 83);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(323, 39);
            textBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(114, 86);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(121, 32);
            label1.TabIndex = 1;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(114, 139);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(111, 32);
            label2.TabIndex = 3;
            label2.Text = "Password";
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(327, 136);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(323, 39);
            textBox2.TabIndex = 2;
            textBox2.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(114, 236);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(188, 32);
            label3.TabIndex = 5;
            label3.Text = "Admin Password";
            // 
            // textBox3
            // 
            textBox3.Enabled = false;
            textBox3.Location = new System.Drawing.Point(327, 234);
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(323, 39);
            textBox3.TabIndex = 4;
            textBox3.UseSystemPasswordChar = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(114, 190);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(227, 36);
            checkBox1.TabIndex = 6;
            checkBox1.Text = "Signup as Admin";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources._lock;
            pictureBox3.Location = new System.Drawing.Point(69, 139);
            pictureBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new System.Drawing.Size(38, 36);
            pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.user;
            pictureBox2.Location = new System.Drawing.Point(69, 83);
            pictureBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(38, 39);
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(328, 356);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(145, 32);
            label5.TabIndex = 18;
            label5.Text = "Signing up...";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label5.Visible = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.enter;
            pictureBox1.Location = new System.Drawing.Point(348, 323);
            pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(96, 104);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 17;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(354, 429);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(81, 32);
            label4.TabIndex = 19;
            label4.Text = "Signin";
            label4.Click += label4_Click;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(659, 131);
            button3.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(67, 49);
            button3.TabIndex = 20;
            button3.Text = "👁‍🗨";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button1
            // 
            button1.Enabled = false;
            button1.Location = new System.Drawing.Point(659, 229);
            button1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(67, 49);
            button1.TabIndex = 21;
            button1.Text = "👁‍🗨";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FormSignup
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(774, 499);
            Controls.Add(button1);
            Controls.Add(button3);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(checkBox1);
            Controls.Add(label3);
            Controls.Add(textBox3);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MaximumSize = new System.Drawing.Size(800, 570);
            MinimumSize = new System.Drawing.Size(800, 570);
            Name = "FormSignup";
            Text = "Sign up | CashFlow Tracker";
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
    }
}