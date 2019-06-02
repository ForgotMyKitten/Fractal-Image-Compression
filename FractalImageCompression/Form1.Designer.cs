namespace FractalImageCompression
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.comboBoxDomainBlockSize = new System.Windows.Forms.ComboBox();
            this.comboBoxRankBlockSize = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCompress = new System.Windows.Forms.Button();
            this.buttonDecompress = new System.Windows.Forms.Button();
            this.progressBarStatus = new System.Windows.Forms.ProgressBar();
            this.flowLayoutPanelStages = new System.Windows.Forms.FlowLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBoxRestored = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxOriginal = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxFastCompression = new System.Windows.Forms.CheckBox();
            this.checkBoxRandomImage = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRestored)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxPath
            // 
            this.textBoxPath.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBoxPath.Location = new System.Drawing.Point(15, 29);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            this.textBoxPath.Size = new System.Drawing.Size(462, 22);
            this.textBoxPath.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "Выберете картинку или сжатый файл:";
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(483, 25);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(76, 31);
            this.buttonOpen.TabIndex = 16;
            this.buttonOpen.Text = "Обзор...";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // comboBoxDomainBlockSize
            // 
            this.comboBoxDomainBlockSize.DisplayMember = "2";
            this.comboBoxDomainBlockSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDomainBlockSize.FormattingEnabled = true;
            this.comboBoxDomainBlockSize.Items.AddRange(new object[] {
            "4",
            "8",
            "16",
            "32"});
            this.comboBoxDomainBlockSize.Location = new System.Drawing.Point(192, 87);
            this.comboBoxDomainBlockSize.Name = "comboBoxDomainBlockSize";
            this.comboBoxDomainBlockSize.Size = new System.Drawing.Size(121, 24);
            this.comboBoxDomainBlockSize.TabIndex = 25;
            // 
            // comboBoxRankBlockSize
            // 
            this.comboBoxRankBlockSize.DisplayMember = "2";
            this.comboBoxRankBlockSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRankBlockSize.FormattingEnabled = true;
            this.comboBoxRankBlockSize.Items.AddRange(new object[] {
            "2",
            "4",
            "8",
            "16"});
            this.comboBoxRankBlockSize.Location = new System.Drawing.Point(192, 57);
            this.comboBoxRankBlockSize.Name = "comboBoxRankBlockSize";
            this.comboBoxRankBlockSize.Size = new System.Drawing.Size(121, 24);
            this.comboBoxRankBlockSize.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 17);
            this.label3.TabIndex = 23;
            this.label3.Text = "Размер доменного блока:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "Размер рангового блока:";
            // 
            // buttonCompress
            // 
            this.buttonCompress.Enabled = false;
            this.buttonCompress.Location = new System.Drawing.Point(319, 57);
            this.buttonCompress.Name = "buttonCompress";
            this.buttonCompress.Size = new System.Drawing.Size(117, 81);
            this.buttonCompress.TabIndex = 27;
            this.buttonCompress.Text = "Сжать";
            this.buttonCompress.UseVisualStyleBackColor = true;
            this.buttonCompress.Click += new System.EventHandler(this.buttonCompress_Click);
            // 
            // buttonDecompress
            // 
            this.buttonDecompress.Enabled = false;
            this.buttonDecompress.Location = new System.Drawing.Point(442, 57);
            this.buttonDecompress.Name = "buttonDecompress";
            this.buttonDecompress.Size = new System.Drawing.Size(117, 81);
            this.buttonDecompress.TabIndex = 28;
            this.buttonDecompress.Text = "Восстановить";
            this.buttonDecompress.UseVisualStyleBackColor = true;
            this.buttonDecompress.Click += new System.EventHandler(this.buttonDecompress_Click);
            // 
            // progressBarStatus
            // 
            this.progressBarStatus.Location = new System.Drawing.Point(15, 164);
            this.progressBarStatus.Name = "progressBarStatus";
            this.progressBarStatus.Size = new System.Drawing.Size(462, 23);
            this.progressBarStatus.TabIndex = 29;
            // 
            // flowLayoutPanelStages
            // 
            this.flowLayoutPanelStages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelStages.AutoScroll = true;
            this.flowLayoutPanelStages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelStages.Location = new System.Drawing.Point(15, 488);
            this.flowLayoutPanelStages.Name = "flowLayoutPanelStages";
            this.flowLayoutPanelStages.Size = new System.Drawing.Size(543, 336);
            this.flowLayoutPanelStages.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 468);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 17);
            this.label6.TabIndex = 33;
            this.label6.Text = "Этапы декодирования:";
            // 
            // pictureBoxRestored
            // 
            this.pictureBoxRestored.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxRestored.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBoxRestored.Location = new System.Drawing.Point(275, 3);
            this.pictureBoxRestored.Name = "pictureBoxRestored";
            this.pictureBoxRestored.Size = new System.Drawing.Size(267, 244);
            this.pictureBoxRestored.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxRestored.TabIndex = 40;
            this.pictureBoxRestored.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(199, 17);
            this.label7.TabIndex = 41;
            this.label7.Text = "Оригинальное изображение:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(275, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(221, 17);
            this.label8.TabIndex = 42;
            this.label8.Text = "Восстановленное изображение:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxOriginal, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxRestored, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 215);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(545, 250);
            this.tableLayoutPanel1.TabIndex = 44;
            // 
            // pictureBoxOriginal
            // 
            this.pictureBoxOriginal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxOriginal.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBoxOriginal.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxOriginal.Name = "pictureBoxOriginal";
            this.pictureBoxOriginal.Size = new System.Drawing.Size(266, 244);
            this.pictureBoxOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOriginal.TabIndex = 41;
            this.pictureBoxOriginal.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(15, 198);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(544, 20);
            this.tableLayoutPanel2.TabIndex = 45;
            // 
            // checkBoxFastCompression
            // 
            this.checkBoxFastCompression.AutoSize = true;
            this.checkBoxFastCompression.Location = new System.Drawing.Point(15, 117);
            this.checkBoxFastCompression.Name = "checkBoxFastCompression";
            this.checkBoxFastCompression.Size = new System.Drawing.Size(233, 21);
            this.checkBoxFastCompression.TabIndex = 46;
            this.checkBoxFastCompression.Text = "Использовать быстрое сжатие";
            this.checkBoxFastCompression.UseVisualStyleBackColor = true;
            this.checkBoxFastCompression.CheckedChanged += new System.EventHandler(this.checkBoxFastCompression_CheckedChanged);
            // 
            // checkBoxRandomImage
            // 
            this.checkBoxRandomImage.AutoSize = true;
            this.checkBoxRandomImage.Checked = true;
            this.checkBoxRandomImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRandomImage.Location = new System.Drawing.Point(15, 142);
            this.checkBoxRandomImage.Name = "checkBoxRandomImage";
            this.checkBoxRandomImage.Size = new System.Drawing.Size(423, 21);
            this.checkBoxRandomImage.TabIndex = 47;
            this.checkBoxRandomImage.Text = "Использовать произвольную картинку для восстановления";
            this.checkBoxRandomImage.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Location = new System.Drawing.Point(483, 161);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(76, 31);
            this.buttonCancel.TabIndex = 48;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 833);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.checkBoxRandomImage);
            this.Controls.Add(this.checkBoxFastCompression);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.flowLayoutPanelStages);
            this.Controls.Add(this.progressBarStatus);
            this.Controls.Add(this.buttonDecompress);
            this.Controls.Add(this.buttonCompress);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.comboBoxDomainBlockSize);
            this.Controls.Add(this.comboBoxRankBlockSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "Form1";
            this.Text = "Fractal image compression";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRestored)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox comboBoxDomainBlockSize;
        private System.Windows.Forms.ComboBox comboBoxRankBlockSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCompress;
        private System.Windows.Forms.Button buttonDecompress;
        private System.Windows.Forms.ProgressBar progressBarStatus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelStages;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBoxRestored;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBoxOriginal;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox checkBoxFastCompression;
        private System.Windows.Forms.CheckBox checkBoxRandomImage;
        private System.Windows.Forms.Button buttonCancel;
    }
}

