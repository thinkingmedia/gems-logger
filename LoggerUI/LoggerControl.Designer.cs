namespace LoggerUI
{
    partial class LoggerControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param _name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbLog = new System.Windows.Forms.ListBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnMark = new System.Windows.Forms.Button();
            this.checkShowDetails = new System.Windows.Forms.CheckBox();
            this.checkShowFinest = new System.Windows.Forms.CheckBox();
            this.chkShowDebug = new System.Windows.Forms.CheckBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbLog
            // 
            this.lbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLog.FormattingEnabled = true;
            this.lbLog.HorizontalScrollbar = true;
            this.lbLog.ItemHeight = 14;
            this.lbLog.Location = new System.Drawing.Point(0, 29);
            this.lbLog.Name = "lbLog";
            this.lbLog.ScrollAlwaysVisible = true;
            this.lbLog.Size = new System.Drawing.Size(568, 551);
            this.lbLog.TabIndex = 8;
            this.lbLog.DoubleClick += new System.EventHandler(this.onDoubleClick);
            this.lbLog.MouseDown += new System.Windows.Forms.MouseEventHandler(this.onMouseDown);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnMark);
            this.panel4.Controls.Add(this.checkShowDetails);
            this.panel4.Controls.Add(this.checkShowFinest);
            this.panel4.Controls.Add(this.chkShowDebug);
            this.panel4.Controls.Add(this.btnClearLog);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(568, 29);
            this.panel4.TabIndex = 9;
            // 
            // btnMark
            // 
            this.btnMark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMark.Location = new System.Drawing.Point(422, 3);
            this.btnMark.Name = "btnMark";
            this.btnMark.Size = new System.Drawing.Size(59, 23);
            this.btnMark.TabIndex = 4;
            this.btnMark.Text = "Mark";
            this.btnMark.UseVisualStyleBackColor = true;
            this.btnMark.Click += new System.EventHandler(this.onBtnMark);
            // 
            // checkShowDetails
            // 
            this.checkShowDetails.AutoSize = true;
            this.checkShowDetails.Checked = true;
            this.checkShowDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkShowDetails.Location = new System.Drawing.Point(196, 6);
            this.checkShowDetails.Name = "checkShowDetails";
            this.checkShowDetails.Size = new System.Drawing.Size(88, 17);
            this.checkShowDetails.TabIndex = 3;
            this.checkShowDetails.Text = "Show Details";
            this.checkShowDetails.UseVisualStyleBackColor = true;
            this.checkShowDetails.CheckedChanged += new System.EventHandler(this.onShowDetails);
            // 
            // checkShowFinest
            // 
            this.checkShowFinest.AutoSize = true;
            this.checkShowFinest.Location = new System.Drawing.Point(106, 6);
            this.checkShowFinest.Name = "checkShowFinest";
            this.checkShowFinest.Size = new System.Drawing.Size(84, 17);
            this.checkShowFinest.TabIndex = 2;
            this.checkShowFinest.Text = "Show Finest";
            this.checkShowFinest.UseVisualStyleBackColor = true;
            this.checkShowFinest.CheckedChanged += new System.EventHandler(this.onShowFinest);
            // 
            // chkShowDebug
            // 
            this.chkShowDebug.AutoSize = true;
            this.chkShowDebug.Location = new System.Drawing.Point(12, 7);
            this.chkShowDebug.Name = "chkShowDebug";
            this.chkShowDebug.Size = new System.Drawing.Size(88, 17);
            this.chkShowDebug.TabIndex = 1;
            this.chkShowDebug.Text = "Show Debug";
            this.chkShowDebug.UseVisualStyleBackColor = true;
            this.chkShowDebug.CheckedChanged += new System.EventHandler(this.onShowDebug);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLog.Location = new System.Drawing.Point(487, 3);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearLog.TabIndex = 0;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.onBtnClear);
            // 
            // LoggerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.panel4);
            this.Name = "LoggerControl";
            this.Size = new System.Drawing.Size(568, 580);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.CheckBox chkShowDebug;
        private System.Windows.Forms.CheckBox checkShowFinest;
        private System.Windows.Forms.CheckBox checkShowDetails;
        private System.Windows.Forms.Button btnMark;
    }
}
