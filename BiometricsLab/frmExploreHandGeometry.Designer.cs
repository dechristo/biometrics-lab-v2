namespace BiometricsLab
{
    partial class frmExploreHandGeometry
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
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.buttonCalcFingerTipsDistance = new System.Windows.Forms.Button();
            this.buttonCalcValleysDist = new System.Windows.Forms.Button();
            this.buttonFingerWidthsCalc = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtbAttributes = new System.Windows.Forms.RichTextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelImageName = new System.Windows.Forms.Label();
            this.buttonCalcPalmWidth = new System.Windows.Forms.Button();
            this.buttonCalcDistanceBetweenValleys = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbMain
            // 
            this.pbMain.Location = new System.Drawing.Point(3, 12);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(801, 601);
            this.pbMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMain.TabIndex = 2;
            this.pbMain.TabStop = false;
            // 
            // buttonCalcFingerTipsDistance
            // 
            this.buttonCalcFingerTipsDistance.Enabled = false;
            this.buttonCalcFingerTipsDistance.Location = new System.Drawing.Point(810, 58);
            this.buttonCalcFingerTipsDistance.Name = "buttonCalcFingerTipsDistance";
            this.buttonCalcFingerTipsDistance.Size = new System.Drawing.Size(128, 40);
            this.buttonCalcFingerTipsDistance.TabIndex = 3;
            this.buttonCalcFingerTipsDistance.Text = "Centroid to Fingertips";
            this.buttonCalcFingerTipsDistance.UseVisualStyleBackColor = true;
            this.buttonCalcFingerTipsDistance.Click += new System.EventHandler(this.buttonCalcFingerTipsDistance_Click);
            // 
            // buttonCalcValleysDist
            // 
            this.buttonCalcValleysDist.Enabled = false;
            this.buttonCalcValleysDist.Location = new System.Drawing.Point(810, 104);
            this.buttonCalcValleysDist.Name = "buttonCalcValleysDist";
            this.buttonCalcValleysDist.Size = new System.Drawing.Size(128, 40);
            this.buttonCalcValleysDist.TabIndex = 4;
            this.buttonCalcValleysDist.Text = "Centroid to Valleys";
            this.buttonCalcValleysDist.UseVisualStyleBackColor = true;
            this.buttonCalcValleysDist.Click += new System.EventHandler(this.buttonCalcValleysDist_Click);
            // 
            // buttonFingerWidthsCalc
            // 
            this.buttonFingerWidthsCalc.Location = new System.Drawing.Point(810, 12);
            this.buttonFingerWidthsCalc.Name = "buttonFingerWidthsCalc";
            this.buttonFingerWidthsCalc.Size = new System.Drawing.Size(128, 40);
            this.buttonFingerWidthsCalc.TabIndex = 5;
            this.buttonFingerWidthsCalc.Text = "Finger Widths";
            this.buttonFingerWidthsCalc.UseVisualStyleBackColor = true;
            this.buttonFingerWidthsCalc.Click += new System.EventHandler(this.buttonFingerWidthsCalc_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtbAttributes);
            this.groupBox1.Location = new System.Drawing.Point(810, 150);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 463);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Attributes";
            // 
            // rtbAttributes
            // 
            this.rtbAttributes.Location = new System.Drawing.Point(6, 19);
            this.rtbAttributes.Name = "rtbAttributes";
            this.rtbAttributes.Size = new System.Drawing.Size(282, 438);
            this.rtbAttributes.TabIndex = 0;
            this.rtbAttributes.Text = "";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(810, 619);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(288, 45);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelImageName
            // 
            this.labelImageName.AutoSize = true;
            this.labelImageName.Font = new System.Drawing.Font("Microsoft YaHei UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImageName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.labelImageName.Location = new System.Drawing.Point(262, 616);
            this.labelImageName.Name = "labelImageName";
            this.labelImageName.Size = new System.Drawing.Size(0, 48);
            this.labelImageName.TabIndex = 8;
            // 
            // buttonCalcPalmWidth
            // 
            this.buttonCalcPalmWidth.Location = new System.Drawing.Point(954, 12);
            this.buttonCalcPalmWidth.Name = "buttonCalcPalmWidth";
            this.buttonCalcPalmWidth.Size = new System.Drawing.Size(128, 40);
            this.buttonCalcPalmWidth.TabIndex = 9;
            this.buttonCalcPalmWidth.Text = "Palm Width";
            this.buttonCalcPalmWidth.UseVisualStyleBackColor = true;
            // 
            // buttonCalcDistanceBetweenValleys
            // 
            this.buttonCalcDistanceBetweenValleys.Location = new System.Drawing.Point(954, 58);
            this.buttonCalcDistanceBetweenValleys.Name = "buttonCalcDistanceBetweenValleys";
            this.buttonCalcDistanceBetweenValleys.Size = new System.Drawing.Size(128, 40);
            this.buttonCalcDistanceBetweenValleys.TabIndex = 10;
            this.buttonCalcDistanceBetweenValleys.Text = "Between Valleys";
            this.buttonCalcDistanceBetweenValleys.UseVisualStyleBackColor = true;
            this.buttonCalcDistanceBetweenValleys.Click += new System.EventHandler(this.buttonCalcDistanceBetweenValleys_Click);
            // 
            // frmExploreHandGeometry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1110, 670);
            this.Controls.Add(this.buttonCalcDistanceBetweenValleys);
            this.Controls.Add(this.buttonCalcPalmWidth);
            this.Controls.Add(this.labelImageName);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonFingerWidthsCalc);
            this.Controls.Add(this.buttonCalcValleysDist);
            this.Controls.Add(this.buttonCalcFingerTipsDistance);
            this.Controls.Add(this.pbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmExploreHandGeometry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hand Geometry Attributes";
            this.Load += new System.EventHandler(this.frmExploreHandGeometry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.Button buttonCalcFingerTipsDistance;
        private System.Windows.Forms.Button buttonCalcValleysDist;
        private System.Windows.Forms.Button buttonFingerWidthsCalc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtbAttributes;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelImageName;
        private System.Windows.Forms.Button buttonCalcPalmWidth;
        private System.Windows.Forms.Button buttonCalcDistanceBetweenValleys;
    }
}