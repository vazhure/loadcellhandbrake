
namespace USBHandbrake
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblKg = new System.Windows.Forms.Label();
            this.lblPercent = new System.Windows.Forms.Label();
            this.lblRawInput = new System.Windows.Forms.Label();
            this.lblRawOutput = new System.Windows.Forms.Label();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.lblTips = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnApplyChanges = new System.Windows.Forms.Button();
            this.lblVirtualButton = new System.Windows.Forms.Label();
            this.pbVirtualButton = new System.Windows.Forms.PictureBox();
            this.btnDiscardChanges = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.lblMid = new System.Windows.Forms.Label();
            this.lblMin = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.verticalBar2 = new USBHandbrake.VerticalBar();
            this.verticalBar1 = new USBHandbrake.VerticalBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVirtualButton)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::USBHandbrake.Properties.Resources.handbrake512px;
            this.pictureBox1.Location = new System.Drawing.Point(12, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "Изображение устройства");
            // 
            // lblKg
            // 
            this.lblKg.Location = new System.Drawing.Point(530, 18);
            this.lblKg.Name = "lblKg";
            this.lblKg.Size = new System.Drawing.Size(64, 17);
            this.lblKg.TabIndex = 0;
            this.lblKg.Text = "20 кг";
            this.lblKg.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.lblKg, "Значение в кг");
            // 
            // lblPercent
            // 
            this.lblPercent.Location = new System.Drawing.Point(600, 18);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(64, 17);
            this.lblPercent.TabIndex = 3;
            this.lblPercent.Text = "100%";
            this.lblPercent.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.lblPercent, "Значение в процентах");
            // 
            // lblRawInput
            // 
            this.lblRawInput.Location = new System.Drawing.Point(530, 561);
            this.lblRawInput.Name = "lblRawInput";
            this.lblRawInput.Size = new System.Drawing.Size(64, 17);
            this.lblRawInput.TabIndex = 2;
            this.lblRawInput.Text = "65535";
            this.lblRawInput.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.lblRawInput, "Значение RAW");
            // 
            // lblRawOutput
            // 
            this.lblRawOutput.Location = new System.Drawing.Point(600, 561);
            this.lblRawOutput.Name = "lblRawOutput";
            this.lblRawOutput.Size = new System.Drawing.Size(64, 17);
            this.lblRawOutput.TabIndex = 5;
            this.lblRawOutput.Text = "65535";
            this.lblRawOutput.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.lblRawOutput, "Значение RAW");
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalibrate.Location = new System.Drawing.Point(670, 96);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(306, 49);
            this.btnCalibrate.TabIndex = 7;
            this.btnCalibrate.Text = "Калибровка";
            this.toolTip1.SetToolTip(this.btnCalibrate, "Запуск ручной калибровки");
            this.btnCalibrate.UseVisualStyleBackColor = true;
            this.btnCalibrate.Click += new System.EventHandler(this.BtnCalibrate_Click);
            // 
            // lblTips
            // 
            this.lblTips.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTips.Location = new System.Drawing.Point(670, 148);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(306, 155);
            this.lblTips.TabIndex = 8;
            this.toolTip1.SetToolTip(this.lblTips, "Инструкции по ручной калибровке");
            // 
            // btnNext
            // 
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Location = new System.Drawing.Point(876, 306);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(100, 36);
            this.btnNext.TabIndex = 10;
            this.btnNext.Text = "Далее";
            this.toolTip1.SetToolTip(this.btnNext, "Следующий шаг калибровки");
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Visible = false;
            this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(770, 306);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 36);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Отмена";
            this.toolTip1.SetToolTip(this.btnCancel, "Отменить калибровку");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefault.Location = new System.Drawing.Point(670, 41);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(306, 49);
            this.btnDefault.TabIndex = 6;
            this.btnDefault.Text = "Рекомендуемые значения";
            this.toolTip1.SetToolTip(this.btnDefault, "Установить рекомендуемые значения калибровки");
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.BtnDefault_Click);
            // 
            // btnApplyChanges
            // 
            this.btnApplyChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplyChanges.Location = new System.Drawing.Point(795, 504);
            this.btnApplyChanges.Name = "btnApplyChanges";
            this.btnApplyChanges.Size = new System.Drawing.Size(181, 49);
            this.btnApplyChanges.TabIndex = 12;
            this.btnApplyChanges.Text = "Применить изменения";
            this.toolTip1.SetToolTip(this.btnApplyChanges, "Применить новые значения калибровки");
            this.btnApplyChanges.UseVisualStyleBackColor = true;
            this.btnApplyChanges.Click += new System.EventHandler(this.BtnApplyChanges_Click);
            // 
            // lblVirtualButton
            // 
            this.lblVirtualButton.AutoSize = true;
            this.lblVirtualButton.Location = new System.Drawing.Point(12, 561);
            this.lblVirtualButton.Name = "lblVirtualButton";
            this.lblVirtualButton.Size = new System.Drawing.Size(144, 17);
            this.lblVirtualButton.TabIndex = 13;
            this.lblVirtualButton.Text = "Виртуальная кнопка";
            // 
            // pbVirtualButton
            // 
            this.pbVirtualButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.pbVirtualButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbVirtualButton.Location = new System.Drawing.Point(162, 558);
            this.pbVirtualButton.Name = "pbVirtualButton";
            this.pbVirtualButton.Size = new System.Drawing.Size(26, 26);
            this.pbVirtualButton.TabIndex = 11;
            this.pbVirtualButton.TabStop = false;
            this.toolTip1.SetToolTip(this.pbVirtualButton, "Статус нажатия виртуальной кнопки");
            // 
            // btnDiscardChanges
            // 
            this.btnDiscardChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDiscardChanges.Location = new System.Drawing.Point(670, 504);
            this.btnDiscardChanges.Name = "btnDiscardChanges";
            this.btnDiscardChanges.Size = new System.Drawing.Size(119, 49);
            this.btnDiscardChanges.TabIndex = 11;
            this.btnDiscardChanges.Text = "Отмена";
            this.toolTip1.SetToolTip(this.btnDiscardChanges, "Отменить изменения");
            this.btnDiscardChanges.UseVisualStyleBackColor = true;
            this.btnDiscardChanges.Click += new System.EventHandler(this.BtnDiscardChanges_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(670, 356);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 23);
            this.label1.TabIndex = 14;
            this.label1.Text = "Калибровочные значения";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(673, 389);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(120, 17);
            this.lblMax.TabIndex = 15;
            this.lblMax.Text = "Верхний порог: 0";
            this.toolTip1.SetToolTip(this.lblMax, "Актуальные калибровочные значения");
            // 
            // lblMid
            // 
            this.lblMid.AutoSize = true;
            this.lblMid.Location = new System.Drawing.Point(673, 417);
            this.lblMid.Name = "lblMid";
            this.lblMid.Size = new System.Drawing.Size(89, 17);
            this.lblMid.TabIndex = 15;
            this.lblMid.Text = "Середина: 0";
            this.toolTip1.SetToolTip(this.lblMid, "Актуальные калибровочные значения");
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Location = new System.Drawing.Point(673, 445);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(116, 17);
            this.lblMin.TabIndex = 15;
            this.lblMin.Text = "Нижний порог: 0";
            this.toolTip1.SetToolTip(this.lblMin, "Актуальные калибровочные значения");
            // 
            // verticalBar2
            // 
            this.verticalBar2.BackColor = System.Drawing.Color.DarkGray;
            this.verticalBar2.EnableUserEdit = false;
            this.verticalBar2.ForeColor = System.Drawing.Color.DodgerBlue;
            this.verticalBar2.Location = new System.Drawing.Point(600, 41);
            this.verticalBar2.MaxLimit = ((ushort)(65535));
            this.verticalBar2.MaxLimitColor = System.Drawing.Color.Red;
            this.verticalBar2.MinLimit = ((ushort)(0));
            this.verticalBar2.MinLimitColor = System.Drawing.Color.Tomato;
            this.verticalBar2.Name = "verticalBar2";
            this.verticalBar2.Size = new System.Drawing.Size(64, 512);
            this.verticalBar2.TabIndex = 4;
            this.verticalBar2.TabStop = false;
            this.toolTip1.SetToolTip(this.verticalBar2, "Значение оси в игре");
            this.verticalBar2.Value = ((ushort)(0));
            // 
            // verticalBar1
            // 
            this.verticalBar1.BackColor = System.Drawing.Color.DarkGray;
            this.verticalBar1.EnableUserEdit = true;
            this.verticalBar1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.verticalBar1.Location = new System.Drawing.Point(530, 41);
            this.verticalBar1.MaxLimit = ((ushort)(65535));
            this.verticalBar1.MaxLimitColor = System.Drawing.Color.Red;
            this.verticalBar1.MinLimit = ((ushort)(0));
            this.verticalBar1.MinLimitColor = System.Drawing.Color.Tomato;
            this.verticalBar1.Name = "verticalBar1";
            this.verticalBar1.Size = new System.Drawing.Size(64, 512);
            this.verticalBar1.TabIndex = 1;
            this.verticalBar1.TabStop = false;
            this.toolTip1.SetToolTip(this.verticalBar1, "Измеренное значение");
            this.verticalBar1.Value = ((ushort)(0));
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(988, 590);
            this.Controls.Add(this.lblMin);
            this.Controls.Add(this.lblMid);
            this.Controls.Add(this.lblMax);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbVirtualButton);
            this.Controls.Add(this.lblVirtualButton);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lblTips);
            this.Controls.Add(this.btnDiscardChanges);
            this.Controls.Add(this.btnApplyChanges);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnCalibrate);
            this.Controls.Add(this.lblRawOutput);
            this.Controls.Add(this.lblRawInput);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.lblKg);
            this.Controls.Add(this.verticalBar2);
            this.Controls.Add(this.verticalBar1);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Калибровка ручного тормоза";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVirtualButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private VerticalBar verticalBar1;
        private VerticalBar verticalBar2;
        private System.Windows.Forms.Label lblKg;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Label lblRawInput;
        private System.Windows.Forms.Label lblRawOutput;
        private System.Windows.Forms.Button btnCalibrate;
        private System.Windows.Forms.Label lblTips;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnApplyChanges;
        private System.Windows.Forms.Label lblVirtualButton;
        private System.Windows.Forms.PictureBox pbVirtualButton;
        private System.Windows.Forms.Button btnDiscardChanges;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label lblMid;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

