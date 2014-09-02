namespace BuildWatch
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
            this.logBoxTxt = new System.Windows.Forms.TextBox();
            this.pingTimer = new System.Windows.Forms.Timer(this.components);
            this.topList = new System.Windows.Forms.ListView();
            this.colBuildName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBuildColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBuildFinish = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.soundCheckBtn = new System.Windows.Forms.Button();
            this.persistLogBoxTxt = new System.Windows.Forms.TextBox();
            this.staleLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // logBoxTxt
            // 
            this.logBoxTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.logBoxTxt.Location = new System.Drawing.Point(378, 346);
            this.logBoxTxt.Multiline = true;
            this.logBoxTxt.Name = "logBoxTxt";
            this.logBoxTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBoxTxt.Size = new System.Drawing.Size(329, 108);
            this.logBoxTxt.TabIndex = 0;
            // 
            // pingTimer
            // 
            this.pingTimer.Interval = 1000;
            this.pingTimer.Tick += new System.EventHandler(this.pingTimer_Tick);
            // 
            // topList
            // 
            this.topList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colBuildName,
            this.colBuildColor,
            this.colBuildFinish,
            this.colUser});
            this.topList.Font = new System.Drawing.Font("Arial Narrow", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.topList.Location = new System.Drawing.Point(12, 12);
            this.topList.Name = "topList";
            this.topList.Size = new System.Drawing.Size(360, 442);
            this.topList.TabIndex = 1;
            this.topList.UseCompatibleStateImageBehavior = false;
            this.topList.View = System.Windows.Forms.View.Details;
            this.topList.ClientSizeChanged += new System.EventHandler(this.topList_ClientSizeChanged);
            // 
            // colBuildName
            // 
            this.colBuildName.Text = "Build";
            this.colBuildName.Width = 180;
            // 
            // colBuildColor
            // 
            this.colBuildColor.Text = "Color";
            // 
            // colBuildFinish
            // 
            this.colBuildFinish.Text = "Finish";
            this.colBuildFinish.Width = 120;
            // 
            // colUser
            // 
            this.colUser.Text = "User";
            this.colUser.Width = 80;
            // 
            // soundCheckBtn
            // 
            this.soundCheckBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.soundCheckBtn.Location = new System.Drawing.Point(592, 12);
            this.soundCheckBtn.Name = "soundCheckBtn";
            this.soundCheckBtn.Size = new System.Drawing.Size(115, 23);
            this.soundCheckBtn.TabIndex = 2;
            this.soundCheckBtn.Text = "Sound Check";
            this.soundCheckBtn.UseVisualStyleBackColor = true;
            this.soundCheckBtn.Click += new System.EventHandler(this.soundCheckBtn_Click);
            // 
            // persistLogBoxTxt
            // 
            this.persistLogBoxTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.persistLogBoxTxt.Location = new System.Drawing.Point(378, 232);
            this.persistLogBoxTxt.Multiline = true;
            this.persistLogBoxTxt.Name = "persistLogBoxTxt";
            this.persistLogBoxTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.persistLogBoxTxt.Size = new System.Drawing.Size(329, 108);
            this.persistLogBoxTxt.TabIndex = 3;
            // 
            // staleLbl
            // 
            this.staleLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.staleLbl.BackColor = System.Drawing.Color.Indigo;
            this.staleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.staleLbl.ForeColor = System.Drawing.Color.OrangeRed;
            this.staleLbl.Location = new System.Drawing.Point(378, 38);
            this.staleLbl.Name = "staleLbl";
            this.staleLbl.Size = new System.Drawing.Size(329, 42);
            this.staleLbl.TabIndex = 4;
            this.staleLbl.Text = "STALE";
            this.staleLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.staleLbl.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 466);
            this.Controls.Add(this.staleLbl);
            this.Controls.Add(this.persistLogBoxTxt);
            this.Controls.Add(this.soundCheckBtn);
            this.Controls.Add(this.topList);
            this.Controls.Add(this.logBoxTxt);
            this.Name = "MainForm";
            this.Text = "Build Watch";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox logBoxTxt;
        private System.Windows.Forms.Timer pingTimer;
        private System.Windows.Forms.ListView topList;
        private System.Windows.Forms.ColumnHeader colBuildName;
        private System.Windows.Forms.ColumnHeader colBuildColor;
        private System.Windows.Forms.ColumnHeader colBuildFinish;
        private System.Windows.Forms.Button soundCheckBtn;
        private System.Windows.Forms.ColumnHeader colUser;
        private System.Windows.Forms.TextBox persistLogBoxTxt;
        private System.Windows.Forms.Label staleLbl;
    }
}

