﻿namespace FirstStepMulti
{
    partial class Form2
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonSnap1 = new System.Windows.Forms.Button();
            this.buttonStop1 = new System.Windows.Forms.Button();
            this.buttonPlay1 = new System.Windows.Forms.Button();
            this.buttonSettings1 = new System.Windows.Forms.Button();
            this.DispWnd1 = new System.Windows.Forms.PictureBox();
            this.buttonSnap2 = new System.Windows.Forms.Button();
            this.buttonStop2 = new System.Windows.Forms.Button();
            this.buttonPlay2 = new System.Windows.Forms.Button();
            this.buttonSettings2 = new System.Windows.Forms.Button();
            this.DispWnd2 = new System.Windows.Forms.PictureBox();
            this.buttonSnap4 = new System.Windows.Forms.Button();
            this.buttonStop4 = new System.Windows.Forms.Button();
            this.buttonPlay4 = new System.Windows.Forms.Button();
            this.buttonSettings4 = new System.Windows.Forms.Button();
            this.DispWnd4 = new System.Windows.Forms.PictureBox();
            this.buttonSnap3 = new System.Windows.Forms.Button();
            this.buttonStop3 = new System.Windows.Forms.Button();
            this.buttonPlay3 = new System.Windows.Forms.Button();
            this.buttonSettings3 = new System.Windows.Forms.Button();
            this.DispWnd3 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonSnapAll = new System.Windows.Forms.Button();
            this.buttonBeginRecord = new System.Windows.Forms.Button();
            this.captureImageBox = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.DispWnd1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DispWnd2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DispWnd4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DispWnd3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.captureImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSnap1
            // 
            this.buttonSnap1.Location = new System.Drawing.Point(288, 73);
            this.buttonSnap1.Name = "buttonSnap1";
            this.buttonSnap1.Size = new System.Drawing.Size(75, 23);
            this.buttonSnap1.TabIndex = 9;
            this.buttonSnap1.Text = "抓图";
            this.buttonSnap1.UseVisualStyleBackColor = true;
            this.buttonSnap1.Click += new System.EventHandler(this.buttonSnap1_Click);
            // 
            // buttonStop1
            // 
            this.buttonStop1.Location = new System.Drawing.Point(195, 73);
            this.buttonStop1.Name = "buttonStop1";
            this.buttonStop1.Size = new System.Drawing.Size(75, 23);
            this.buttonStop1.TabIndex = 8;
            this.buttonStop1.Text = "停止";
            this.buttonStop1.UseVisualStyleBackColor = true;
            this.buttonStop1.Click += new System.EventHandler(this.buttonStop1_Click);
            // 
            // buttonPlay1
            // 
            this.buttonPlay1.Location = new System.Drawing.Point(102, 73);
            this.buttonPlay1.Name = "buttonPlay1";
            this.buttonPlay1.Size = new System.Drawing.Size(75, 23);
            this.buttonPlay1.TabIndex = 7;
            this.buttonPlay1.Text = "播放";
            this.buttonPlay1.UseVisualStyleBackColor = true;
            this.buttonPlay1.Click += new System.EventHandler(this.buttonPlay1_Click);
            // 
            // buttonSettings1
            // 
            this.buttonSettings1.Location = new System.Drawing.Point(9, 73);
            this.buttonSettings1.Name = "buttonSettings1";
            this.buttonSettings1.Size = new System.Drawing.Size(75, 23);
            this.buttonSettings1.TabIndex = 6;
            this.buttonSettings1.Text = "相机设置";
            this.buttonSettings1.UseVisualStyleBackColor = true;
            this.buttonSettings1.Click += new System.EventHandler(this.buttonSettings1_Click);
            // 
            // DispWnd1
            // 
            this.DispWnd1.Location = new System.Drawing.Point(9, 102);
            this.DispWnd1.Name = "DispWnd1";
            this.DispWnd1.Size = new System.Drawing.Size(355, 314);
            this.DispWnd1.TabIndex = 5;
            this.DispWnd1.TabStop = false;
            // 
            // buttonSnap2
            // 
            this.buttonSnap2.Location = new System.Drawing.Point(687, 73);
            this.buttonSnap2.Name = "buttonSnap2";
            this.buttonSnap2.Size = new System.Drawing.Size(75, 23);
            this.buttonSnap2.TabIndex = 14;
            this.buttonSnap2.Text = "抓图";
            this.buttonSnap2.UseVisualStyleBackColor = true;
            this.buttonSnap2.Click += new System.EventHandler(this.buttonSnap2_Click);
            // 
            // buttonStop2
            // 
            this.buttonStop2.Location = new System.Drawing.Point(594, 73);
            this.buttonStop2.Name = "buttonStop2";
            this.buttonStop2.Size = new System.Drawing.Size(75, 23);
            this.buttonStop2.TabIndex = 13;
            this.buttonStop2.Text = "停止";
            this.buttonStop2.UseVisualStyleBackColor = true;
            this.buttonStop2.Click += new System.EventHandler(this.buttonStop2_Click);
            // 
            // buttonPlay2
            // 
            this.buttonPlay2.Location = new System.Drawing.Point(501, 73);
            this.buttonPlay2.Name = "buttonPlay2";
            this.buttonPlay2.Size = new System.Drawing.Size(75, 23);
            this.buttonPlay2.TabIndex = 12;
            this.buttonPlay2.Text = "播放";
            this.buttonPlay2.UseVisualStyleBackColor = true;
            this.buttonPlay2.Click += new System.EventHandler(this.buttonPlay2_Click);
            // 
            // buttonSettings2
            // 
            this.buttonSettings2.Location = new System.Drawing.Point(408, 73);
            this.buttonSettings2.Name = "buttonSettings2";
            this.buttonSettings2.Size = new System.Drawing.Size(75, 23);
            this.buttonSettings2.TabIndex = 11;
            this.buttonSettings2.Text = "相机设置";
            this.buttonSettings2.UseVisualStyleBackColor = true;
            this.buttonSettings2.Click += new System.EventHandler(this.buttonSettings2_Click);
            // 
            // DispWnd2
            // 
            this.DispWnd2.Location = new System.Drawing.Point(408, 102);
            this.DispWnd2.Name = "DispWnd2";
            this.DispWnd2.Size = new System.Drawing.Size(355, 314);
            this.DispWnd2.TabIndex = 10;
            this.DispWnd2.TabStop = false;
            // 
            // buttonSnap4
            // 
            this.buttonSnap4.Location = new System.Drawing.Point(687, 444);
            this.buttonSnap4.Name = "buttonSnap4";
            this.buttonSnap4.Size = new System.Drawing.Size(75, 23);
            this.buttonSnap4.TabIndex = 24;
            this.buttonSnap4.Text = "抓图";
            this.buttonSnap4.UseVisualStyleBackColor = true;
            this.buttonSnap4.Click += new System.EventHandler(this.buttonSnap4_Click);
            // 
            // buttonStop4
            // 
            this.buttonStop4.Location = new System.Drawing.Point(594, 444);
            this.buttonStop4.Name = "buttonStop4";
            this.buttonStop4.Size = new System.Drawing.Size(75, 23);
            this.buttonStop4.TabIndex = 23;
            this.buttonStop4.Text = "停止";
            this.buttonStop4.UseVisualStyleBackColor = true;
            this.buttonStop4.Click += new System.EventHandler(this.buttonStop4_Click);
            // 
            // buttonPlay4
            // 
            this.buttonPlay4.Location = new System.Drawing.Point(501, 444);
            this.buttonPlay4.Name = "buttonPlay4";
            this.buttonPlay4.Size = new System.Drawing.Size(75, 23);
            this.buttonPlay4.TabIndex = 22;
            this.buttonPlay4.Text = "播放";
            this.buttonPlay4.UseVisualStyleBackColor = true;
            this.buttonPlay4.Click += new System.EventHandler(this.buttonPlay4_Click);
            // 
            // buttonSettings4
            // 
            this.buttonSettings4.Location = new System.Drawing.Point(408, 444);
            this.buttonSettings4.Name = "buttonSettings4";
            this.buttonSettings4.Size = new System.Drawing.Size(75, 23);
            this.buttonSettings4.TabIndex = 21;
            this.buttonSettings4.Text = "相机设置";
            this.buttonSettings4.UseVisualStyleBackColor = true;
            this.buttonSettings4.Click += new System.EventHandler(this.buttonSettings4_Click);
            // 
            // DispWnd4
            // 
            this.DispWnd4.Location = new System.Drawing.Point(408, 473);
            this.DispWnd4.Name = "DispWnd4";
            this.DispWnd4.Size = new System.Drawing.Size(355, 314);
            this.DispWnd4.TabIndex = 20;
            this.DispWnd4.TabStop = false;
            // 
            // buttonSnap3
            // 
            this.buttonSnap3.Location = new System.Drawing.Point(288, 444);
            this.buttonSnap3.Name = "buttonSnap3";
            this.buttonSnap3.Size = new System.Drawing.Size(75, 23);
            this.buttonSnap3.TabIndex = 19;
            this.buttonSnap3.Text = "抓图";
            this.buttonSnap3.UseVisualStyleBackColor = true;
            this.buttonSnap3.Click += new System.EventHandler(this.buttonSnap3_Click);
            // 
            // buttonStop3
            // 
            this.buttonStop3.Location = new System.Drawing.Point(195, 444);
            this.buttonStop3.Name = "buttonStop3";
            this.buttonStop3.Size = new System.Drawing.Size(75, 23);
            this.buttonStop3.TabIndex = 18;
            this.buttonStop3.Text = "停止";
            this.buttonStop3.UseVisualStyleBackColor = true;
            this.buttonStop3.Click += new System.EventHandler(this.buttonStop3_Click);
            // 
            // buttonPlay3
            // 
            this.buttonPlay3.Location = new System.Drawing.Point(102, 444);
            this.buttonPlay3.Name = "buttonPlay3";
            this.buttonPlay3.Size = new System.Drawing.Size(75, 23);
            this.buttonPlay3.TabIndex = 17;
            this.buttonPlay3.Text = "播放";
            this.buttonPlay3.UseVisualStyleBackColor = true;
            this.buttonPlay3.Click += new System.EventHandler(this.buttonPlay3_Click);
            // 
            // buttonSettings3
            // 
            this.buttonSettings3.Location = new System.Drawing.Point(9, 444);
            this.buttonSettings3.Name = "buttonSettings3";
            this.buttonSettings3.Size = new System.Drawing.Size(75, 23);
            this.buttonSettings3.TabIndex = 16;
            this.buttonSettings3.Text = "相机设置";
            this.buttonSettings3.UseVisualStyleBackColor = true;
            this.buttonSettings3.Click += new System.EventHandler(this.buttonSettings3_Click);
            // 
            // DispWnd3
            // 
            this.DispWnd3.Location = new System.Drawing.Point(9, 473);
            this.DispWnd3.Name = "DispWnd3";
            this.DispWnd3.Size = new System.Drawing.Size(355, 314);
            this.DispWnd3.TabIndex = 15;
            this.DispWnd3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 419);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(406, 419);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 792);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 27;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(406, 790);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "label4";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonSnapAll
            // 
            this.buttonSnapAll.Location = new System.Drawing.Point(195, 23);
            this.buttonSnapAll.Name = "buttonSnapAll";
            this.buttonSnapAll.Size = new System.Drawing.Size(75, 23);
            this.buttonSnapAll.TabIndex = 29;
            this.buttonSnapAll.Text = "一键抓图";
            this.buttonSnapAll.UseVisualStyleBackColor = true;
            this.buttonSnapAll.Click += new System.EventHandler(this.buttonSnapAll_Click);
            // 
            // buttonBeginRecord
            // 
            this.buttonBeginRecord.Location = new System.Drawing.Point(289, 23);
            this.buttonBeginRecord.Name = "buttonBeginRecord";
            this.buttonBeginRecord.Size = new System.Drawing.Size(94, 23);
            this.buttonBeginRecord.TabIndex = 30;
            this.buttonBeginRecord.Text = "开始一键录制";
            this.buttonBeginRecord.UseVisualStyleBackColor = true;
            this.buttonBeginRecord.Click += new System.EventHandler(this.buttonBeginRecord_Click);
            // 
            // captureImageBox
            // 
            this.captureImageBox.Location = new System.Drawing.Point(573, 9);
            this.captureImageBox.Name = "captureImageBox";
            this.captureImageBox.Size = new System.Drawing.Size(75, 58);
            this.captureImageBox.TabIndex = 31;
            this.captureImageBox.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 810);
            this.Controls.Add(this.captureImageBox);
            this.Controls.Add(this.buttonBeginRecord);
            this.Controls.Add(this.buttonSnapAll);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSnap4);
            this.Controls.Add(this.buttonStop4);
            this.Controls.Add(this.buttonPlay4);
            this.Controls.Add(this.buttonSettings4);
            this.Controls.Add(this.DispWnd4);
            this.Controls.Add(this.buttonSnap3);
            this.Controls.Add(this.buttonStop3);
            this.Controls.Add(this.buttonPlay3);
            this.Controls.Add(this.buttonSettings3);
            this.Controls.Add(this.DispWnd3);
            this.Controls.Add(this.buttonSnap2);
            this.Controls.Add(this.buttonStop2);
            this.Controls.Add(this.buttonPlay2);
            this.Controls.Add(this.buttonSettings2);
            this.Controls.Add(this.DispWnd2);
            this.Controls.Add(this.buttonSnap1);
            this.Controls.Add(this.buttonStop1);
            this.Controls.Add(this.buttonPlay1);
            this.Controls.Add(this.buttonSettings1);
            this.Controls.Add(this.DispWnd1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.DispWnd1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DispWnd2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DispWnd4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DispWnd3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.captureImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSnap1;
        private System.Windows.Forms.Button buttonStop1;
        private System.Windows.Forms.Button buttonPlay1;
        private System.Windows.Forms.Button buttonSettings1;
        private System.Windows.Forms.PictureBox DispWnd1;
        private System.Windows.Forms.Button buttonSnap2;
        private System.Windows.Forms.Button buttonStop2;
        private System.Windows.Forms.Button buttonPlay2;
        private System.Windows.Forms.Button buttonSettings2;
        private System.Windows.Forms.PictureBox DispWnd2;
        private System.Windows.Forms.Button buttonSnap4;
        private System.Windows.Forms.Button buttonStop4;
        private System.Windows.Forms.Button buttonPlay4;
        private System.Windows.Forms.Button buttonSettings4;
        private System.Windows.Forms.PictureBox DispWnd4;
        private System.Windows.Forms.Button buttonSnap3;
        private System.Windows.Forms.Button buttonStop3;
        private System.Windows.Forms.Button buttonPlay3;
        private System.Windows.Forms.Button buttonSettings3;
        private System.Windows.Forms.PictureBox DispWnd3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonSnapAll;
        private System.Windows.Forms.Button buttonBeginRecord;
        private Emgu.CV.UI.ImageBox captureImageBox;
    }
}

