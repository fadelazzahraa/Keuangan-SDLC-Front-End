namespace Keuangan
{
    partial class FormCashFlow
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
            dataGridViewRecord = new System.Windows.Forms.DataGridView();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            textBoxValue = new System.Windows.Forms.TextBox();
            textBoxDetail = new System.Windows.Forms.TextBox();
            pictureBoxPhoto = new System.Windows.Forms.PictureBox();
            comboBoxTransaction = new System.Windows.Forms.ComboBox();
            buttonAdd = new System.Windows.Forms.Button();
            buttonEdit = new System.Windows.Forms.Button();
            buttonDelete = new System.Windows.Forms.Button();
            comboBoxPhoto = new System.Windows.Forms.ComboBox();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            textBoxSearch = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            comboBoxUser = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            comboBox4 = new System.Windows.Forms.ComboBox();
            textBoxTag = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            comboBoxCategory = new System.Windows.Forms.ComboBox();
            label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRecord).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewRecord
            // 
            dataGridViewRecord.AllowUserToAddRows = false;
            dataGridViewRecord.AllowUserToDeleteRows = false;
            dataGridViewRecord.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridViewRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewRecord.Location = new System.Drawing.Point(11, 43);
            dataGridViewRecord.Margin = new System.Windows.Forms.Padding(2);
            dataGridViewRecord.Name = "dataGridViewRecord";
            dataGridViewRecord.ReadOnly = true;
            dataGridViewRecord.RowHeadersWidth = 62;
            dataGridViewRecord.RowTemplate.Height = 28;
            dataGridViewRecord.Size = new System.Drawing.Size(762, 182);
            dataGridViewRecord.TabIndex = 19;
            dataGridViewRecord.CellClick += dataGridViewRecord_CellClick;
            // 
            // label6
            // 
            label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(46, 247);
            label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(67, 15);
            label6.TabIndex = 23;
            label6.Text = "Transaction";
            // 
            // label7
            // 
            label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(46, 276);
            label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(35, 15);
            label7.TabIndex = 24;
            label7.Text = "Value";
            // 
            // label8
            // 
            label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(46, 302);
            label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(37, 15);
            label8.TabIndex = 25;
            label8.Text = "Detail";
            // 
            // label11
            // 
            label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(477, 247);
            label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(39, 15);
            label11.TabIndex = 28;
            label11.Text = "Photo";
            // 
            // textBoxValue
            // 
            textBoxValue.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxValue.Location = new System.Drawing.Point(133, 272);
            textBoxValue.Margin = new System.Windows.Forms.Padding(2);
            textBoxValue.Name = "textBoxValue";
            textBoxValue.Size = new System.Drawing.Size(319, 23);
            textBoxValue.TabIndex = 32;
            // 
            // textBoxDetail
            // 
            textBoxDetail.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxDetail.Location = new System.Drawing.Point(133, 299);
            textBoxDetail.Margin = new System.Windows.Forms.Padding(2);
            textBoxDetail.Multiline = true;
            textBoxDetail.Name = "textBoxDetail";
            textBoxDetail.Size = new System.Drawing.Size(319, 101);
            textBoxDetail.TabIndex = 33;
            // 
            // pictureBoxPhoto
            // 
            pictureBoxPhoto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            pictureBoxPhoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            pictureBoxPhoto.Location = new System.Drawing.Point(521, 272);
            pictureBoxPhoto.Margin = new System.Windows.Forms.Padding(2);
            pictureBoxPhoto.Name = "pictureBoxPhoto";
            pictureBoxPhoto.Size = new System.Drawing.Size(213, 213);
            pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBoxPhoto.TabIndex = 36;
            pictureBoxPhoto.TabStop = false;
            // 
            // comboBoxTransaction
            // 
            comboBoxTransaction.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBoxTransaction.FormattingEnabled = true;
            comboBoxTransaction.Items.AddRange(new object[] { "Debit", "Credit" });
            comboBoxTransaction.Location = new System.Drawing.Point(133, 244);
            comboBoxTransaction.Name = "comboBoxTransaction";
            comboBoxTransaction.Size = new System.Drawing.Size(319, 23);
            comboBoxTransaction.TabIndex = 37;
            comboBoxTransaction.SelectedIndexChanged += comboBoxTransaction_SelectedIndexChanged;
            // 
            // buttonAdd
            // 
            buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            buttonAdd.Location = new System.Drawing.Point(201, 503);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new System.Drawing.Size(130, 27);
            buttonAdd.TabIndex = 39;
            buttonAdd.Text = "➕ Add";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += button1_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            buttonEdit.Location = new System.Drawing.Point(337, 503);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new System.Drawing.Size(130, 27);
            buttonEdit.TabIndex = 40;
            buttonEdit.Text = "✏ Edit";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += button2_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            buttonDelete.Location = new System.Drawing.Point(473, 503);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new System.Drawing.Size(130, 27);
            buttonDelete.TabIndex = 41;
            buttonDelete.Text = "🗑 Delete";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += button3_Click;
            // 
            // comboBoxPhoto
            // 
            comboBoxPhoto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            comboBoxPhoto.FormattingEnabled = true;
            comboBoxPhoto.Location = new System.Drawing.Point(521, 244);
            comboBoxPhoto.Name = "comboBoxPhoto";
            comboBoxPhoto.Size = new System.Drawing.Size(213, 23);
            comboBoxPhoto.TabIndex = 42;
            comboBoxPhoto.SelectedIndexChanged += comboBoxPhoto_SelectedIndexChanged;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel1, toolStripProgressBar1 });
            statusStrip1.Location = new System.Drawing.Point(0, 538);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            statusStrip1.Size = new System.Drawing.Size(784, 23);
            statusStrip1.TabIndex = 44;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(39, 18);
            toolStripStatusLabel1.Text = "Ready";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new System.Drawing.Size(117, 17);
            // 
            // textBoxSearch
            // 
            textBoxSearch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxSearch.Location = new System.Drawing.Point(77, 12);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new System.Drawing.Size(390, 23);
            textBoxSearch.TabIndex = 45;
            textBoxSearch.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(22, 17);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(42, 15);
            label1.TabIndex = 46;
            label1.Text = "Search";
            // 
            // comboBoxUser
            // 
            comboBoxUser.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            comboBoxUser.FormattingEnabled = true;
            comboBoxUser.Location = new System.Drawing.Point(562, 12);
            comboBoxUser.Name = "comboBoxUser";
            comboBoxUser.Size = new System.Drawing.Size(199, 23);
            comboBoxUser.TabIndex = 47;
            comboBoxUser.SelectedIndexChanged += comboBoxUser_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(493, 17);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(63, 15);
            label2.TabIndex = 48;
            label2.Text = "Select user";
            // 
            // label10
            // 
            label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(46, 411);
            label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(31, 15);
            label10.TabIndex = 27;
            label10.Text = "Date";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dateTimePicker1.Location = new System.Drawing.Point(133, 405);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new System.Drawing.Size(319, 23);
            dateTimePicker1.TabIndex = 49;
            // 
            // comboBox4
            // 
            comboBox4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new System.Drawing.Point(197, 582);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new System.Drawing.Size(265, 23);
            comboBox4.TabIndex = 51;
            // 
            // textBoxTag
            // 
            textBoxTag.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxTag.Location = new System.Drawing.Point(133, 462);
            textBoxTag.Margin = new System.Windows.Forms.Padding(2);
            textBoxTag.Name = "textBoxTag";
            textBoxTag.Size = new System.Drawing.Size(319, 23);
            textBoxTag.TabIndex = 52;
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(46, 436);
            label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(55, 15);
            label3.TabIndex = 53;
            label3.Text = "Category";
            // 
            // comboBoxCategory
            // 
            comboBoxCategory.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBoxCategory.FormattingEnabled = true;
            comboBoxCategory.Location = new System.Drawing.Point(133, 434);
            comboBoxCategory.Name = "comboBoxCategory";
            comboBoxCategory.Size = new System.Drawing.Size(319, 23);
            comboBoxCategory.TabIndex = 54;
            comboBoxCategory.SelectedIndexChanged += comboBoxCategory_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(46, 464);
            label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(25, 15);
            label4.TabIndex = 55;
            label4.Text = "Tag";
            // 
            // FormCashFlow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(784, 561);
            Controls.Add(label4);
            Controls.Add(comboBoxCategory);
            Controls.Add(label3);
            Controls.Add(textBoxTag);
            Controls.Add(comboBox4);
            Controls.Add(dateTimePicker1);
            Controls.Add(label2);
            Controls.Add(comboBoxUser);
            Controls.Add(label1);
            Controls.Add(textBoxSearch);
            Controls.Add(statusStrip1);
            Controls.Add(comboBoxPhoto);
            Controls.Add(buttonDelete);
            Controls.Add(buttonEdit);
            Controls.Add(buttonAdd);
            Controls.Add(comboBoxTransaction);
            Controls.Add(pictureBoxPhoto);
            Controls.Add(textBoxDetail);
            Controls.Add(textBoxValue);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(dataGridViewRecord);
            Margin = new System.Windows.Forms.Padding(2);
            MinimumSize = new System.Drawing.Size(800, 600);
            Name = "FormCashFlow";
            Text = "Cash Flow | CashFlow Tracker";
            Load += FormCashFlow_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewRecord).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewRecord;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxDetail;
        private System.Windows.Forms.PictureBox pictureBoxPhoto;
        private System.Windows.Forms.ComboBox comboBoxTransaction;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.ComboBox comboBoxPhoto;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.TextBox textBoxTag;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Label label4;
    }
}