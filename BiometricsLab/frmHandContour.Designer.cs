namespace BiometricsLab
{
    partial class frmHandContour
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHandContour));
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.btnThinning = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCoords = new System.Windows.Forms.Label();
            this.lblPixelCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.butttonExplore = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbMain
            // 
            this.pbMain.Location = new System.Drawing.Point(12, 12);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(801, 601);
            this.pbMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMain.TabIndex = 1;
            this.pbMain.TabStop = false;
            this.pbMain.Click += new System.EventHandler(this.pbMain_Click);
            this.pbMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMain_Paint);
            this.pbMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseMove);
            // 
            // btnThinning
            // 
            this.btnThinning.Enabled = false;
            this.btnThinning.Location = new System.Drawing.Point(828, 573);
            this.btnThinning.Name = "btnThinning";
            this.btnThinning.Size = new System.Drawing.Size(99, 40);
            this.btnThinning.TabIndex = 13;
            this.btnThinning.Text = "Thinning";
            this.btnThinning.UseVisualStyleBackColor = true;
            this.btnThinning.Visible = false;
            this.btnThinning.Click += new System.EventHandler(this.btnThinning_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCoords);
            this.groupBox1.Controls.Add(this.lblPixelCount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(819, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 444);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Additional Data";
            // 
            // lblCoords
            // 
            this.lblCoords.AutoSize = true;
            this.lblCoords.Location = new System.Drawing.Point(16, 396);
            this.lblCoords.Name = "lblCoords";
            this.lblCoords.Size = new System.Drawing.Size(0, 17);
            this.lblCoords.TabIndex = 2;
            // 
            // lblPixelCount
            // 
            this.lblPixelCount.AutoSize = true;
            this.lblPixelCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPixelCount.ForeColor = System.Drawing.Color.Blue;
            this.lblPixelCount.Location = new System.Drawing.Point(16, 46);
            this.lblPixelCount.Name = "lblPixelCount";
            this.lblPixelCount.Size = new System.Drawing.Size(0, 13);
            this.lblPixelCount.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pixel Count:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(825, 486);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(306, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Click at any point in the picture to detect the edges and valleys.";
            // 
            // butttonExplore
            // 
            this.butttonExplore.Enabled = false;
            this.butttonExplore.Location = new System.Drawing.Point(828, 513);
            this.butttonExplore.Name = "butttonExplore";
            this.butttonExplore.Size = new System.Drawing.Size(99, 40);
            this.butttonExplore.TabIndex = 16;
            this.butttonExplore.Text = "Explore";
            this.butttonExplore.UseVisualStyleBackColor = true;
            this.butttonExplore.Click += new System.EventHandler(this.buttonExplore_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // frmHandContour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 639);
            this.Controls.Add(this.butttonExplore);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnThinning);
            this.Controls.Add(this.pbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmHandContour";
            this.Text = "Hand Contour";
            this.Load += new System.EventHandler(this.frmHandContour_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmHandContour_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.Button btnThinning;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPixelCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCoords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butttonExplore;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}