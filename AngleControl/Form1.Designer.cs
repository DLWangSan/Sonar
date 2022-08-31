
namespace AngleControl
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.anglePicture1 = new AngleControl.AnglePicture();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // anglePicture1
            // 
            this.anglePicture1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("anglePicture1.BackgroundImage")));
            this.anglePicture1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.anglePicture1.Location = new System.Drawing.Point(477, 89);
            this.anglePicture1.Name = "anglePicture1";
            this.anglePicture1.Size = new System.Drawing.Size(127, 120);
            this.anglePicture1.TabIndex = 0;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(477, 250);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            45,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            45,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 494);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.anglePicture1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AnglePicture anglePicture1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}

