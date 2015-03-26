namespace BuildWatch.Controls
{
    partial class WideBuildStatusBox
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctlGrid = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.buildNameTxt = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buildUserTxt = new System.Windows.Forms.Label();
            this.buildStatusTxt = new System.Windows.Forms.Label();
            this.buildFinishTxt = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buildUserPic = new System.Windows.Forms.PictureBox();
            this.ctlRow1 = new System.Windows.Forms.TableLayoutPanel();
            this.ctlRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.ctlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildUserPic)).BeginInit();
            this.ctlRow1.SuspendLayout();
            this.ctlRow2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctlGrid
            // 
            this.ctlGrid.ColumnCount = 2;
            this.ctlGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ctlGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 161F));
            this.ctlGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ctlGrid.Controls.Add(this.buildUserPic, 1, 0);
            this.ctlGrid.Controls.Add(this.ctlRow1, 0, 0);
            this.ctlGrid.Controls.Add(this.ctlRow2, 0, 1);
            this.ctlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlGrid.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctlGrid.Location = new System.Drawing.Point(0, 0);
            this.ctlGrid.Name = "ctlGrid";
            this.ctlGrid.RowCount = 2;
            this.ctlGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ctlGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ctlGrid.Size = new System.Drawing.Size(825, 207);
            this.ctlGrid.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(540, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Build";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // buildNameTxt
            // 
            this.buildNameTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buildNameTxt.AutoEllipsis = true;
            this.buildNameTxt.AutoSize = true;
            this.buildNameTxt.Font = new System.Drawing.Font("Arial Narrow", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buildNameTxt.Location = new System.Drawing.Point(3, 29);
            this.buildNameTxt.Name = "buildNameTxt";
            this.buildNameTxt.Size = new System.Drawing.Size(540, 68);
            this.buildNameTxt.TabIndex = 1;
            this.buildNameTxt.Text = "UI Tools Build";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(369, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(287, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "User";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // buildUserTxt
            // 
            this.buildUserTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buildUserTxt.AutoEllipsis = true;
            this.buildUserTxt.AutoSize = true;
            this.buildUserTxt.Font = new System.Drawing.Font("Arial Narrow", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buildUserTxt.Location = new System.Drawing.Point(369, 29);
            this.buildUserTxt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.buildUserTxt.Name = "buildUserTxt";
            this.buildUserTxt.Size = new System.Drawing.Size(287, 69);
            this.buildUserTxt.TabIndex = 6;
            this.buildUserTxt.Text = "John F. Kennedy";
            this.buildUserTxt.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buildStatusTxt
            // 
            this.buildStatusTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buildStatusTxt.AutoEllipsis = true;
            this.buildStatusTxt.AutoSize = true;
            this.buildStatusTxt.Font = new System.Drawing.Font("Arial Narrow", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buildStatusTxt.Location = new System.Drawing.Point(548, 29);
            this.buildStatusTxt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.buildStatusTxt.Name = "buildStatusTxt";
            this.buildStatusTxt.Size = new System.Drawing.Size(108, 68);
            this.buildStatusTxt.TabIndex = 7;
            this.buildStatusTxt.Text = "FAIL";
            this.buildStatusTxt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buildFinishTxt
            // 
            this.buildFinishTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buildFinishTxt.AutoEllipsis = true;
            this.buildFinishTxt.AutoSize = true;
            this.buildFinishTxt.Font = new System.Drawing.Font("Arial Narrow", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buildFinishTxt.Location = new System.Drawing.Point(2, 29);
            this.buildFinishTxt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.buildFinishTxt.Name = "buildFinishTxt";
            this.buildFinishTxt.Size = new System.Drawing.Size(363, 69);
            this.buildFinishTxt.TabIndex = 3;
            this.buildFinishTxt.Text = "10 Mar 2015 (-10d)";
            this.buildFinishTxt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(548, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 29);
            this.label4.TabIndex = 5;
            this.label4.Text = "Status";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(363, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "Finish";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // buildUserPic
            // 
            this.buildUserPic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buildUserPic.Image = global::BuildWatch.Controls.Properties.Resources.dpc_Silhouette;
            this.buildUserPic.Location = new System.Drawing.Point(667, 3);
            this.buildUserPic.Name = "buildUserPic";
            this.ctlGrid.SetRowSpan(this.buildUserPic, 2);
            this.buildUserPic.Size = new System.Drawing.Size(155, 201);
            this.buildUserPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.buildUserPic.TabIndex = 8;
            this.buildUserPic.TabStop = false;
            // 
            // ctlRow1
            // 
            this.ctlRow1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlRow1.ColumnCount = 2;
            this.ctlRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ctlRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ctlRow1.Controls.Add(this.buildNameTxt, 0, 1);
            this.ctlRow1.Controls.Add(this.label1, 0, 0);
            this.ctlRow1.Controls.Add(this.buildStatusTxt, 1, 1);
            this.ctlRow1.Controls.Add(this.label4, 1, 0);
            this.ctlRow1.Location = new System.Drawing.Point(3, 3);
            this.ctlRow1.Name = "ctlRow1";
            this.ctlRow1.RowCount = 2;
            this.ctlRow1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ctlRow1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ctlRow1.Size = new System.Drawing.Size(658, 97);
            this.ctlRow1.TabIndex = 9;
            // 
            // ctlRow2
            // 
            this.ctlRow2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlRow2.ColumnCount = 2;
            this.ctlRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ctlRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ctlRow2.Controls.Add(this.label3, 1, 0);
            this.ctlRow2.Controls.Add(this.label2, 0, 0);
            this.ctlRow2.Controls.Add(this.buildUserTxt, 1, 1);
            this.ctlRow2.Controls.Add(this.buildFinishTxt, 0, 1);
            this.ctlRow2.Location = new System.Drawing.Point(3, 106);
            this.ctlRow2.Name = "ctlRow2";
            this.ctlRow2.RowCount = 2;
            this.ctlRow2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ctlRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ctlRow2.Size = new System.Drawing.Size(658, 98);
            this.ctlRow2.TabIndex = 10;
            // 
            // WideBuildStatusBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctlGrid);
            this.DoubleBuffered = true;
            this.Name = "WideBuildStatusBox";
            this.Size = new System.Drawing.Size(825, 207);
            this.ctlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.buildUserPic)).EndInit();
            this.ctlRow1.ResumeLayout(false);
            this.ctlRow1.PerformLayout();
            this.ctlRow2.ResumeLayout(false);
            this.ctlRow2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel ctlGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label buildNameTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label buildUserTxt;
        private System.Windows.Forms.Label buildStatusTxt;
        private System.Windows.Forms.Label buildFinishTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox buildUserPic;
        private System.Windows.Forms.TableLayoutPanel ctlRow1;
        private System.Windows.Forms.TableLayoutPanel ctlRow2;
    }
}
