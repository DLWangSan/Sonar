
namespace SonarKrill
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.PocDataGridView = new System.Windows.Forms.DataGridView();
            this.SetPoc = new System.Windows.Forms.Button();
            this.AltButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.DelButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.anglePicture1 = new AngleControl.AnglePicture();
            ((System.ComponentModel.ISupportInitialize)(this.PocDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // PocDataGridView
            // 
            this.PocDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PocDataGridView.Location = new System.Drawing.Point(45, 126);
            this.PocDataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.PocDataGridView.Name = "PocDataGridView";
            this.PocDataGridView.RowHeadersWidth = 51;
            this.PocDataGridView.RowTemplate.Height = 23;
            this.PocDataGridView.Size = new System.Drawing.Size(968, 388);
            this.PocDataGridView.TabIndex = 0;
            // 
            // SetPoc
            // 
            this.SetPoc.Location = new System.Drawing.Point(99, 49);
            this.SetPoc.Margin = new System.Windows.Forms.Padding(4);
            this.SetPoc.Name = "SetPoc";
            this.SetPoc.Size = new System.Drawing.Size(100, 29);
            this.SetPoc.TabIndex = 1;
            this.SetPoc.Text = "查询";
            this.SetPoc.UseVisualStyleBackColor = true;
            this.SetPoc.Click += new System.EventHandler(this.SetPoc_Load);
            // 
            // AltButton
            // 
            this.AltButton.Location = new System.Drawing.Point(244, 49);
            this.AltButton.Margin = new System.Windows.Forms.Padding(4);
            this.AltButton.Name = "AltButton";
            this.AltButton.Size = new System.Drawing.Size(100, 29);
            this.AltButton.TabIndex = 1;
            this.AltButton.Text = "修改";
            this.AltButton.UseVisualStyleBackColor = true;
            this.AltButton.Click += new System.EventHandler(this.AltButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(395, 49);
            this.AddButton.Margin = new System.Windows.Forms.Padding(4);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(100, 29);
            this.AddButton.TabIndex = 1;
            this.AddButton.Text = "增加";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DelButton
            // 
            this.DelButton.Location = new System.Drawing.Point(564, 49);
            this.DelButton.Margin = new System.Windows.Forms.Padding(4);
            this.DelButton.Name = "DelButton";
            this.DelButton.Size = new System.Drawing.Size(100, 29);
            this.DelButton.TabIndex = 1;
            this.DelButton.Text = "删除";
            this.DelButton.UseVisualStyleBackColor = true;
            this.DelButton.Click += new System.EventHandler(this.DelButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(721, 49);
            this.RefreshButton.Margin = new System.Windows.Forms.Padding(4);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(100, 29);
            this.RefreshButton.TabIndex = 1;
            this.RefreshButton.Text = "刷新";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // anglePicture1
            // 
            this.anglePicture1.BackColor = System.Drawing.Color.Transparent;
            this.anglePicture1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("anglePicture1.BackgroundImage")));
            this.anglePicture1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.anglePicture1.Location = new System.Drawing.Point(708, 241);
            this.anglePicture1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.anglePicture1.Name = "anglePicture1";
            this.anglePicture1.Size = new System.Drawing.Size(169, 150);
            this.anglePicture1.TabIndex = 2;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 562);
            this.Controls.Add(this.anglePicture1);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.DelButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.AltButton);
            this.Controls.Add(this.SetPoc);
            this.Controls.Add(this.PocDataGridView);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form4";
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PocDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView PocDataGridView;
        private System.Windows.Forms.Button SetPoc;
        private System.Windows.Forms.Button AltButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button DelButton;
        private System.Windows.Forms.Button RefreshButton;
        private AngleControl.AnglePicture anglePicture1;
    }
}