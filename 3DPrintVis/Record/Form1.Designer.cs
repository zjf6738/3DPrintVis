﻿namespace Record
{
    partial class Form1
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
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.DispWnd = new System.Windows.Forms.PictureBox();
            this.StateLabel = new System.Windows.Forms.Label();
            this.buttonBeginRecord = new System.Windows.Forms.Button();
            this.textBoxRecordPath = new System.Windows.Forms.TextBox();
            this.buttonSelRecordPath = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DispWnd)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(248, 12);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 7;
            this.buttonStop.Text = "停止";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(146, 12);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(75, 23);
            this.buttonPlay.TabIndex = 6;
            this.buttonPlay.Text = "播放";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Location = new System.Drawing.Point(42, 12);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(75, 23);
            this.buttonSettings.TabIndex = 5;
            this.buttonSettings.Text = "相机设置";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // DispWnd
            // 
            this.DispWnd.Location = new System.Drawing.Point(12, 93);
            this.DispWnd.Name = "DispWnd";
            this.DispWnd.Size = new System.Drawing.Size(706, 488);
            this.DispWnd.TabIndex = 9;
            this.DispWnd.TabStop = false;
            // 
            // StateLabel
            // 
            this.StateLabel.AutoSize = true;
            this.StateLabel.Location = new System.Drawing.Point(12, 593);
            this.StateLabel.Name = "StateLabel";
            this.StateLabel.Size = new System.Drawing.Size(41, 12);
            this.StateLabel.TabIndex = 10;
            this.StateLabel.Text = "状态栏";
            // 
            // buttonBeginRecord
            // 
            this.buttonBeginRecord.Location = new System.Drawing.Point(42, 54);
            this.buttonBeginRecord.Name = "buttonBeginRecord";
            this.buttonBeginRecord.Size = new System.Drawing.Size(75, 23);
            this.buttonBeginRecord.TabIndex = 11;
            this.buttonBeginRecord.Text = "开始录像";
            this.buttonBeginRecord.UseVisualStyleBackColor = true;
            this.buttonBeginRecord.Click += new System.EventHandler(this.buttonBeginRecord_Click);
            // 
            // textBoxRecordPath
            // 
            this.textBoxRecordPath.Location = new System.Drawing.Point(123, 56);
            this.textBoxRecordPath.Name = "textBoxRecordPath";
            this.textBoxRecordPath.Size = new System.Drawing.Size(443, 21);
            this.textBoxRecordPath.TabIndex = 12;
            // 
            // buttonSelRecordPath
            // 
            this.buttonSelRecordPath.Location = new System.Drawing.Point(572, 56);
            this.buttonSelRecordPath.Name = "buttonSelRecordPath";
            this.buttonSelRecordPath.Size = new System.Drawing.Size(75, 23);
            this.buttonSelRecordPath.TabIndex = 13;
            this.buttonSelRecordPath.Text = "选择";
            this.buttonSelRecordPath.UseVisualStyleBackColor = true;
            this.buttonSelRecordPath.Click += new System.EventHandler(this.buttonSelRecordPath_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 614);
            this.Controls.Add(this.buttonSelRecordPath);
            this.Controls.Add(this.textBoxRecordPath);
            this.Controls.Add(this.buttonBeginRecord);
            this.Controls.Add(this.StateLabel);
            this.Controls.Add(this.DispWnd);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonSettings);
            this.Name = "Form1";
            this.Text = "录像";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.DispWnd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.PictureBox DispWnd;
        private System.Windows.Forms.Label StateLabel;
        private System.Windows.Forms.Button buttonBeginRecord;
        private System.Windows.Forms.TextBox textBoxRecordPath;
        private System.Windows.Forms.Button buttonSelRecordPath;
        private System.Windows.Forms.Timer timer1;
    }
}

