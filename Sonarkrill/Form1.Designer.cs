
namespace SonarKrill
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblBoatName = new System.Windows.Forms.Label();
            this.lblBoatLength = new System.Windows.Forms.Label();
            this.lblBoatWidth = new System.Windows.Forms.Label();
            this.lblBoatWeight = new System.Windows.Forms.Label();
            this.lblBoatPower = new System.Windows.Forms.Label();
            this.lblBTime = new System.Windows.Forms.Label();
            this.lblBDis = new System.Windows.Forms.Label();
            this.lblBAngle = new System.Windows.Forms.Label();
            this.lblBDepth = new System.Windows.Forms.Label();
            this.lblBSpeed = new System.Windows.Forms.Label();
            this.lblNU = new System.Windows.Forms.Label();
            this.lblNAngle = new System.Windows.Forms.Label();
            this.lblNDepth = new System.Windows.Forms.Label();
            this.lblNDis = new System.Windows.Forms.Label();
            this.lblNTime = new System.Windows.Forms.Label();
            this.lblSSpeed = new System.Windows.Forms.Label();
            this.lblSSeaDepth = new System.Windows.Forms.Label();
            this.lblSDepth = new System.Windows.Forms.Label();
            this.lblSTime = new System.Windows.Forms.Label();
            this.lblNumSpeed_shi = new System.Windows.Forms.Label();
            this.lblNumSpeed_ge = new System.Windows.Forms.Label();
            this.lblNumPosition_shi = new System.Windows.Forms.Label();
            this.lblNumPosition_bai = new System.Windows.Forms.Label();
            this.lblNumPosition_ge = new System.Windows.Forms.Label();
            this.chartMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblNumU_shi = new System.Windows.Forms.Label();
            this.lblNumU_ge = new System.Windows.Forms.Label();
            this.lblNumAngle_ge = new System.Windows.Forms.Label();
            this.lblNumAngle_shi = new System.Windows.Forms.Label();
            this.lblNumAngle_bai = new System.Windows.Forms.Label();
            this.lblNumDepth_bai = new System.Windows.Forms.Label();
            this.lblNumDepth_ge = new System.Windows.Forms.Label();
            this.lblNumDepth_shi = new System.Windows.Forms.Label();
            this.lblNumDIS_bai = new System.Windows.Forms.Label();
            this.lblNumDIS_ge = new System.Windows.Forms.Label();
            this.lblNumDIS_shi = new System.Windows.Forms.Label();
            this.lblNumNAngle_bai = new System.Windows.Forms.Label();
            this.lblNumNAngle_ge = new System.Windows.Forms.Label();
            this.lblNumNAngle_shi = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblNumNU_shi = new System.Windows.Forms.Label();
            this.lblNumNU_ge = new System.Windows.Forms.Label();
            this.lblNumNDepth_bai = new System.Windows.Forms.Label();
            this.lblNumNDepth_ge = new System.Windows.Forms.Label();
            this.lblNumNDepth_shi = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblLon = new System.Windows.Forms.Label();
            this.lblLat = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.anglePicture1 = new AngleControl.AnglePicture();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBoatName
            // 
            this.lblBoatName.AutoSize = true;
            this.lblBoatName.BackColor = System.Drawing.Color.Transparent;
            this.lblBoatName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBoatName.ForeColor = System.Drawing.Color.White;
            this.lblBoatName.Location = new System.Drawing.Point(574, 152);
            this.lblBoatName.Name = "lblBoatName";
            this.lblBoatName.Size = new System.Drawing.Size(75, 20);
            this.lblBoatName.TabIndex = 0;
            this.lblBoatName.Text = "label1";
            this.lblBoatName.Click += new System.EventHandler(this.lblBoatName_Click);
            // 
            // lblBoatLength
            // 
            this.lblBoatLength.AutoSize = true;
            this.lblBoatLength.BackColor = System.Drawing.Color.Transparent;
            this.lblBoatLength.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBoatLength.ForeColor = System.Drawing.Color.White;
            this.lblBoatLength.Location = new System.Drawing.Point(574, 188);
            this.lblBoatLength.Name = "lblBoatLength";
            this.lblBoatLength.Size = new System.Drawing.Size(75, 20);
            this.lblBoatLength.TabIndex = 1;
            this.lblBoatLength.Text = "label1";
            // 
            // lblBoatWidth
            // 
            this.lblBoatWidth.AutoSize = true;
            this.lblBoatWidth.BackColor = System.Drawing.Color.Transparent;
            this.lblBoatWidth.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBoatWidth.ForeColor = System.Drawing.Color.White;
            this.lblBoatWidth.Location = new System.Drawing.Point(574, 223);
            this.lblBoatWidth.Name = "lblBoatWidth";
            this.lblBoatWidth.Size = new System.Drawing.Size(75, 20);
            this.lblBoatWidth.TabIndex = 2;
            this.lblBoatWidth.Text = "label1";
            // 
            // lblBoatWeight
            // 
            this.lblBoatWeight.AutoSize = true;
            this.lblBoatWeight.BackColor = System.Drawing.Color.Transparent;
            this.lblBoatWeight.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBoatWeight.ForeColor = System.Drawing.Color.White;
            this.lblBoatWeight.Location = new System.Drawing.Point(574, 288);
            this.lblBoatWeight.Name = "lblBoatWeight";
            this.lblBoatWeight.Size = new System.Drawing.Size(75, 20);
            this.lblBoatWeight.TabIndex = 4;
            this.lblBoatWeight.Text = "label1";
            this.lblBoatWeight.Click += new System.EventHandler(this.lblBoatWeight_Click);
            // 
            // lblBoatPower
            // 
            this.lblBoatPower.AutoSize = true;
            this.lblBoatPower.BackColor = System.Drawing.Color.Transparent;
            this.lblBoatPower.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBoatPower.ForeColor = System.Drawing.Color.White;
            this.lblBoatPower.Location = new System.Drawing.Point(574, 253);
            this.lblBoatPower.Name = "lblBoatPower";
            this.lblBoatPower.Size = new System.Drawing.Size(75, 20);
            this.lblBoatPower.TabIndex = 3;
            this.lblBoatPower.Text = "label4";
            // 
            // lblBTime
            // 
            this.lblBTime.AutoSize = true;
            this.lblBTime.BackColor = System.Drawing.Color.Transparent;
            this.lblBTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBTime.ForeColor = System.Drawing.Color.White;
            this.lblBTime.Location = new System.Drawing.Point(249, 480);
            this.lblBTime.Name = "lblBTime";
            this.lblBTime.Size = new System.Drawing.Size(86, 20);
            this.lblBTime.TabIndex = 5;
            this.lblBTime.Text = "waiting";
            // 
            // lblBDis
            // 
            this.lblBDis.AutoSize = true;
            this.lblBDis.BackColor = System.Drawing.Color.Transparent;
            this.lblBDis.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBDis.ForeColor = System.Drawing.Color.White;
            this.lblBDis.Location = new System.Drawing.Point(249, 508);
            this.lblBDis.Name = "lblBDis";
            this.lblBDis.Size = new System.Drawing.Size(86, 20);
            this.lblBDis.TabIndex = 6;
            this.lblBDis.Text = "waiting";
            // 
            // lblBAngle
            // 
            this.lblBAngle.AutoSize = true;
            this.lblBAngle.BackColor = System.Drawing.Color.Transparent;
            this.lblBAngle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBAngle.ForeColor = System.Drawing.Color.White;
            this.lblBAngle.Location = new System.Drawing.Point(249, 561);
            this.lblBAngle.Name = "lblBAngle";
            this.lblBAngle.Size = new System.Drawing.Size(86, 20);
            this.lblBAngle.TabIndex = 8;
            this.lblBAngle.Text = "waiting";
            // 
            // lblBDepth
            // 
            this.lblBDepth.AutoSize = true;
            this.lblBDepth.BackColor = System.Drawing.Color.Transparent;
            this.lblBDepth.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBDepth.ForeColor = System.Drawing.Color.White;
            this.lblBDepth.Location = new System.Drawing.Point(249, 535);
            this.lblBDepth.Name = "lblBDepth";
            this.lblBDepth.Size = new System.Drawing.Size(86, 20);
            this.lblBDepth.TabIndex = 7;
            this.lblBDepth.Text = "waiting";
            // 
            // lblBSpeed
            // 
            this.lblBSpeed.AutoSize = true;
            this.lblBSpeed.BackColor = System.Drawing.Color.Transparent;
            this.lblBSpeed.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBSpeed.ForeColor = System.Drawing.Color.White;
            this.lblBSpeed.Location = new System.Drawing.Point(249, 586);
            this.lblBSpeed.Name = "lblBSpeed";
            this.lblBSpeed.Size = new System.Drawing.Size(86, 20);
            this.lblBSpeed.TabIndex = 9;
            this.lblBSpeed.Text = "waiting";
            // 
            // lblNU
            // 
            this.lblNU.AutoSize = true;
            this.lblNU.BackColor = System.Drawing.Color.Transparent;
            this.lblNU.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNU.ForeColor = System.Drawing.Color.White;
            this.lblNU.Location = new System.Drawing.Point(815, 586);
            this.lblNU.Name = "lblNU";
            this.lblNU.Size = new System.Drawing.Size(86, 20);
            this.lblNU.TabIndex = 14;
            this.lblNU.Text = "waiting";
            // 
            // lblNAngle
            // 
            this.lblNAngle.AutoSize = true;
            this.lblNAngle.BackColor = System.Drawing.Color.Transparent;
            this.lblNAngle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNAngle.ForeColor = System.Drawing.Color.White;
            this.lblNAngle.Location = new System.Drawing.Point(815, 561);
            this.lblNAngle.Name = "lblNAngle";
            this.lblNAngle.Size = new System.Drawing.Size(86, 20);
            this.lblNAngle.TabIndex = 13;
            this.lblNAngle.Text = "waiting";
            // 
            // lblNDepth
            // 
            this.lblNDepth.AutoSize = true;
            this.lblNDepth.BackColor = System.Drawing.Color.Transparent;
            this.lblNDepth.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNDepth.ForeColor = System.Drawing.Color.White;
            this.lblNDepth.Location = new System.Drawing.Point(815, 535);
            this.lblNDepth.Name = "lblNDepth";
            this.lblNDepth.Size = new System.Drawing.Size(86, 20);
            this.lblNDepth.TabIndex = 12;
            this.lblNDepth.Text = "waiting";
            // 
            // lblNDis
            // 
            this.lblNDis.AutoSize = true;
            this.lblNDis.BackColor = System.Drawing.Color.Transparent;
            this.lblNDis.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNDis.ForeColor = System.Drawing.Color.White;
            this.lblNDis.Location = new System.Drawing.Point(815, 508);
            this.lblNDis.Name = "lblNDis";
            this.lblNDis.Size = new System.Drawing.Size(86, 20);
            this.lblNDis.TabIndex = 11;
            this.lblNDis.Text = "waiting";
            // 
            // lblNTime
            // 
            this.lblNTime.AutoSize = true;
            this.lblNTime.BackColor = System.Drawing.Color.Transparent;
            this.lblNTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNTime.ForeColor = System.Drawing.Color.White;
            this.lblNTime.Location = new System.Drawing.Point(815, 480);
            this.lblNTime.Name = "lblNTime";
            this.lblNTime.Size = new System.Drawing.Size(86, 20);
            this.lblNTime.TabIndex = 10;
            this.lblNTime.Text = "waiting";
            // 
            // lblSSpeed
            // 
            this.lblSSpeed.AutoSize = true;
            this.lblSSpeed.BackColor = System.Drawing.Color.Transparent;
            this.lblSSpeed.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSSpeed.ForeColor = System.Drawing.Color.White;
            this.lblSSpeed.Location = new System.Drawing.Point(563, 561);
            this.lblSSpeed.Name = "lblSSpeed";
            this.lblSSpeed.Size = new System.Drawing.Size(86, 20);
            this.lblSSpeed.TabIndex = 18;
            this.lblSSpeed.Text = "waiting";
            // 
            // lblSSeaDepth
            // 
            this.lblSSeaDepth.AutoSize = true;
            this.lblSSeaDepth.BackColor = System.Drawing.Color.Transparent;
            this.lblSSeaDepth.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSSeaDepth.ForeColor = System.Drawing.Color.White;
            this.lblSSeaDepth.Location = new System.Drawing.Point(563, 535);
            this.lblSSeaDepth.Name = "lblSSeaDepth";
            this.lblSSeaDepth.Size = new System.Drawing.Size(86, 20);
            this.lblSSeaDepth.TabIndex = 17;
            this.lblSSeaDepth.Text = "waiting";
            // 
            // lblSDepth
            // 
            this.lblSDepth.AutoSize = true;
            this.lblSDepth.BackColor = System.Drawing.Color.Transparent;
            this.lblSDepth.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSDepth.ForeColor = System.Drawing.Color.White;
            this.lblSDepth.Location = new System.Drawing.Point(563, 508);
            this.lblSDepth.Name = "lblSDepth";
            this.lblSDepth.Size = new System.Drawing.Size(86, 20);
            this.lblSDepth.TabIndex = 16;
            this.lblSDepth.Text = "waiting";
            // 
            // lblSTime
            // 
            this.lblSTime.AutoSize = true;
            this.lblSTime.BackColor = System.Drawing.Color.Transparent;
            this.lblSTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSTime.ForeColor = System.Drawing.Color.White;
            this.lblSTime.Location = new System.Drawing.Point(563, 480);
            this.lblSTime.Name = "lblSTime";
            this.lblSTime.Size = new System.Drawing.Size(86, 20);
            this.lblSTime.TabIndex = 15;
            this.lblSTime.Text = "waiting";
            // 
            // lblNumSpeed_shi
            // 
            this.lblNumSpeed_shi.AutoSize = true;
            this.lblNumSpeed_shi.BackColor = System.Drawing.Color.Transparent;
            this.lblNumSpeed_shi.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumSpeed_shi.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumSpeed_shi.Location = new System.Drawing.Point(1075, 76);
            this.lblNumSpeed_shi.Name = "lblNumSpeed_shi";
            this.lblNumSpeed_shi.Size = new System.Drawing.Size(55, 60);
            this.lblNumSpeed_shi.TabIndex = 19;
            this.lblNumSpeed_shi.Text = "0";
            // 
            // lblNumSpeed_ge
            // 
            this.lblNumSpeed_ge.AutoSize = true;
            this.lblNumSpeed_ge.BackColor = System.Drawing.Color.Transparent;
            this.lblNumSpeed_ge.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumSpeed_ge.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumSpeed_ge.Location = new System.Drawing.Point(1135, 76);
            this.lblNumSpeed_ge.Name = "lblNumSpeed_ge";
            this.lblNumSpeed_ge.Size = new System.Drawing.Size(55, 60);
            this.lblNumSpeed_ge.TabIndex = 20;
            this.lblNumSpeed_ge.Text = "0";
            // 
            // lblNumPosition_shi
            // 
            this.lblNumPosition_shi.AutoSize = true;
            this.lblNumPosition_shi.BackColor = System.Drawing.Color.Transparent;
            this.lblNumPosition_shi.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumPosition_shi.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumPosition_shi.Location = new System.Drawing.Point(1333, 76);
            this.lblNumPosition_shi.Name = "lblNumPosition_shi";
            this.lblNumPosition_shi.Size = new System.Drawing.Size(55, 60);
            this.lblNumPosition_shi.TabIndex = 22;
            this.lblNumPosition_shi.Text = "0";
            // 
            // lblNumPosition_bai
            // 
            this.lblNumPosition_bai.AutoSize = true;
            this.lblNumPosition_bai.BackColor = System.Drawing.Color.Transparent;
            this.lblNumPosition_bai.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumPosition_bai.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumPosition_bai.Location = new System.Drawing.Point(1273, 76);
            this.lblNumPosition_bai.Name = "lblNumPosition_bai";
            this.lblNumPosition_bai.Size = new System.Drawing.Size(55, 60);
            this.lblNumPosition_bai.TabIndex = 21;
            this.lblNumPosition_bai.Text = "0";
            // 
            // lblNumPosition_ge
            // 
            this.lblNumPosition_ge.AutoSize = true;
            this.lblNumPosition_ge.BackColor = System.Drawing.Color.Transparent;
            this.lblNumPosition_ge.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumPosition_ge.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumPosition_ge.Location = new System.Drawing.Point(1394, 76);
            this.lblNumPosition_ge.Name = "lblNumPosition_ge";
            this.lblNumPosition_ge.Size = new System.Drawing.Size(55, 60);
            this.lblNumPosition_ge.TabIndex = 23;
            this.lblNumPosition_ge.Text = "0";
            // 
            // chartMain
            // 
            this.chartMain.BackColor = System.Drawing.Color.Transparent;
            this.chartMain.BackImageTransparentColor = System.Drawing.Color.Transparent;
            this.chartMain.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.TileFlipX;
            this.chartMain.BackSecondaryColor = System.Drawing.Color.Transparent;
            this.chartMain.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.Name = "ChartArea1";
            this.chartMain.ChartAreas.Add(chartArea2);
            this.chartMain.Location = new System.Drawing.Point(47, 667);
            this.chartMain.Margin = new System.Windows.Forms.Padding(4);
            this.chartMain.Name = "chartMain";
            series2.ChartArea = "ChartArea1";
            series2.Name = "Series1";
            this.chartMain.Series.Add(series2);
            this.chartMain.Size = new System.Drawing.Size(949, 332);
            this.chartMain.TabIndex = 24;
            this.chartMain.Text = "chart1";
            this.chartMain.Click += new System.EventHandler(this.chartMain_Click);
            // 
            // lblNumU_shi
            // 
            this.lblNumU_shi.AutoSize = true;
            this.lblNumU_shi.BackColor = System.Drawing.Color.Transparent;
            this.lblNumU_shi.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumU_shi.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumU_shi.Location = new System.Drawing.Point(1110, 533);
            this.lblNumU_shi.Name = "lblNumU_shi";
            this.lblNumU_shi.Size = new System.Drawing.Size(37, 40);
            this.lblNumU_shi.TabIndex = 25;
            this.lblNumU_shi.Text = "0";
            // 
            // lblNumU_ge
            // 
            this.lblNumU_ge.AutoSize = true;
            this.lblNumU_ge.BackColor = System.Drawing.Color.Transparent;
            this.lblNumU_ge.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumU_ge.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumU_ge.Location = new System.Drawing.Point(1152, 533);
            this.lblNumU_ge.Name = "lblNumU_ge";
            this.lblNumU_ge.Size = new System.Drawing.Size(37, 40);
            this.lblNumU_ge.TabIndex = 26;
            this.lblNumU_ge.Text = "0";
            // 
            // lblNumAngle_ge
            // 
            this.lblNumAngle_ge.AutoSize = true;
            this.lblNumAngle_ge.BackColor = System.Drawing.Color.Transparent;
            this.lblNumAngle_ge.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumAngle_ge.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumAngle_ge.Location = new System.Drawing.Point(1186, 797);
            this.lblNumAngle_ge.Name = "lblNumAngle_ge";
            this.lblNumAngle_ge.Size = new System.Drawing.Size(37, 40);
            this.lblNumAngle_ge.TabIndex = 28;
            this.lblNumAngle_ge.Text = "0";
            // 
            // lblNumAngle_shi
            // 
            this.lblNumAngle_shi.AutoSize = true;
            this.lblNumAngle_shi.BackColor = System.Drawing.Color.Transparent;
            this.lblNumAngle_shi.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumAngle_shi.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumAngle_shi.Location = new System.Drawing.Point(1137, 796);
            this.lblNumAngle_shi.Name = "lblNumAngle_shi";
            this.lblNumAngle_shi.Size = new System.Drawing.Size(37, 40);
            this.lblNumAngle_shi.TabIndex = 27;
            this.lblNumAngle_shi.Text = "0";
            // 
            // lblNumAngle_bai
            // 
            this.lblNumAngle_bai.AutoSize = true;
            this.lblNumAngle_bai.BackColor = System.Drawing.Color.Transparent;
            this.lblNumAngle_bai.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumAngle_bai.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumAngle_bai.Location = new System.Drawing.Point(1094, 797);
            this.lblNumAngle_bai.Name = "lblNumAngle_bai";
            this.lblNumAngle_bai.Size = new System.Drawing.Size(37, 40);
            this.lblNumAngle_bai.TabIndex = 29;
            this.lblNumAngle_bai.Text = "0";
            // 
            // lblNumDepth_bai
            // 
            this.lblNumDepth_bai.AutoSize = true;
            this.lblNumDepth_bai.BackColor = System.Drawing.Color.Transparent;
            this.lblNumDepth_bai.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumDepth_bai.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumDepth_bai.Location = new System.Drawing.Point(1282, 797);
            this.lblNumDepth_bai.Name = "lblNumDepth_bai";
            this.lblNumDepth_bai.Size = new System.Drawing.Size(37, 40);
            this.lblNumDepth_bai.TabIndex = 32;
            this.lblNumDepth_bai.Text = "0";
            // 
            // lblNumDepth_ge
            // 
            this.lblNumDepth_ge.AutoSize = true;
            this.lblNumDepth_ge.BackColor = System.Drawing.Color.Transparent;
            this.lblNumDepth_ge.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumDepth_ge.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumDepth_ge.Location = new System.Drawing.Point(1374, 797);
            this.lblNumDepth_ge.Name = "lblNumDepth_ge";
            this.lblNumDepth_ge.Size = new System.Drawing.Size(37, 40);
            this.lblNumDepth_ge.TabIndex = 31;
            this.lblNumDepth_ge.Text = "0";
            // 
            // lblNumDepth_shi
            // 
            this.lblNumDepth_shi.AutoSize = true;
            this.lblNumDepth_shi.BackColor = System.Drawing.Color.Transparent;
            this.lblNumDepth_shi.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumDepth_shi.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumDepth_shi.Location = new System.Drawing.Point(1328, 797);
            this.lblNumDepth_shi.Name = "lblNumDepth_shi";
            this.lblNumDepth_shi.Size = new System.Drawing.Size(37, 40);
            this.lblNumDepth_shi.TabIndex = 30;
            this.lblNumDepth_shi.Text = "0";
            // 
            // lblNumDIS_bai
            // 
            this.lblNumDIS_bai.AutoSize = true;
            this.lblNumDIS_bai.BackColor = System.Drawing.Color.Transparent;
            this.lblNumDIS_bai.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumDIS_bai.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumDIS_bai.Location = new System.Drawing.Point(1536, 797);
            this.lblNumDIS_bai.Name = "lblNumDIS_bai";
            this.lblNumDIS_bai.Size = new System.Drawing.Size(37, 40);
            this.lblNumDIS_bai.TabIndex = 35;
            this.lblNumDIS_bai.Text = "0";
            // 
            // lblNumDIS_ge
            // 
            this.lblNumDIS_ge.AutoSize = true;
            this.lblNumDIS_ge.BackColor = System.Drawing.Color.Transparent;
            this.lblNumDIS_ge.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumDIS_ge.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumDIS_ge.Location = new System.Drawing.Point(1628, 797);
            this.lblNumDIS_ge.Name = "lblNumDIS_ge";
            this.lblNumDIS_ge.Size = new System.Drawing.Size(37, 40);
            this.lblNumDIS_ge.TabIndex = 34;
            this.lblNumDIS_ge.Text = "0";
            // 
            // lblNumDIS_shi
            // 
            this.lblNumDIS_shi.AutoSize = true;
            this.lblNumDIS_shi.BackColor = System.Drawing.Color.Transparent;
            this.lblNumDIS_shi.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumDIS_shi.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumDIS_shi.Location = new System.Drawing.Point(1582, 797);
            this.lblNumDIS_shi.Name = "lblNumDIS_shi";
            this.lblNumDIS_shi.Size = new System.Drawing.Size(37, 40);
            this.lblNumDIS_shi.TabIndex = 33;
            this.lblNumDIS_shi.Text = "0";
            // 
            // lblNumNAngle_bai
            // 
            this.lblNumNAngle_bai.AutoSize = true;
            this.lblNumNAngle_bai.BackColor = System.Drawing.Color.Transparent;
            this.lblNumNAngle_bai.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumNAngle_bai.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumNAngle_bai.Location = new System.Drawing.Point(1536, 903);
            this.lblNumNAngle_bai.Name = "lblNumNAngle_bai";
            this.lblNumNAngle_bai.Size = new System.Drawing.Size(37, 40);
            this.lblNumNAngle_bai.TabIndex = 38;
            this.lblNumNAngle_bai.Text = "0";
            // 
            // lblNumNAngle_ge
            // 
            this.lblNumNAngle_ge.AutoSize = true;
            this.lblNumNAngle_ge.BackColor = System.Drawing.Color.Transparent;
            this.lblNumNAngle_ge.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumNAngle_ge.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumNAngle_ge.Location = new System.Drawing.Point(1628, 903);
            this.lblNumNAngle_ge.Name = "lblNumNAngle_ge";
            this.lblNumNAngle_ge.Size = new System.Drawing.Size(37, 40);
            this.lblNumNAngle_ge.TabIndex = 37;
            this.lblNumNAngle_ge.Text = "0";
            // 
            // lblNumNAngle_shi
            // 
            this.lblNumNAngle_shi.AutoSize = true;
            this.lblNumNAngle_shi.BackColor = System.Drawing.Color.Transparent;
            this.lblNumNAngle_shi.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumNAngle_shi.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumNAngle_shi.Location = new System.Drawing.Point(1582, 903);
            this.lblNumNAngle_shi.Name = "lblNumNAngle_shi";
            this.lblNumNAngle_shi.Size = new System.Drawing.Size(37, 40);
            this.lblNumNAngle_shi.TabIndex = 36;
            this.lblNumNAngle_shi.Text = "0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1292, 522);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(78, 67);
            this.pictureBox1.TabIndex = 39;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1384, 522);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(78, 67);
            this.pictureBox2.TabIndex = 40;
            this.pictureBox2.TabStop = false;
            // 
            // lblNumNU_shi
            // 
            this.lblNumNU_shi.AutoSize = true;
            this.lblNumNU_shi.BackColor = System.Drawing.Color.Transparent;
            this.lblNumNU_shi.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumNU_shi.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumNU_shi.Location = new System.Drawing.Point(1763, 903);
            this.lblNumNU_shi.Name = "lblNumNU_shi";
            this.lblNumNU_shi.Size = new System.Drawing.Size(37, 40);
            this.lblNumNU_shi.TabIndex = 46;
            this.lblNumNU_shi.Text = "0";
            // 
            // lblNumNU_ge
            // 
            this.lblNumNU_ge.AutoSize = true;
            this.lblNumNU_ge.BackColor = System.Drawing.Color.Transparent;
            this.lblNumNU_ge.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumNU_ge.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumNU_ge.Location = new System.Drawing.Point(1809, 903);
            this.lblNumNU_ge.Name = "lblNumNU_ge";
            this.lblNumNU_ge.Size = new System.Drawing.Size(37, 40);
            this.lblNumNU_ge.TabIndex = 44;
            this.lblNumNU_ge.Text = "0";
            // 
            // lblNumNDepth_bai
            // 
            this.lblNumNDepth_bai.AutoSize = true;
            this.lblNumNDepth_bai.BackColor = System.Drawing.Color.Transparent;
            this.lblNumNDepth_bai.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumNDepth_bai.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumNDepth_bai.Location = new System.Drawing.Point(1763, 797);
            this.lblNumNDepth_bai.Name = "lblNumNDepth_bai";
            this.lblNumNDepth_bai.Size = new System.Drawing.Size(37, 40);
            this.lblNumNDepth_bai.TabIndex = 43;
            this.lblNumNDepth_bai.Text = "0";
            // 
            // lblNumNDepth_ge
            // 
            this.lblNumNDepth_ge.AutoSize = true;
            this.lblNumNDepth_ge.BackColor = System.Drawing.Color.Transparent;
            this.lblNumNDepth_ge.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumNDepth_ge.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumNDepth_ge.Location = new System.Drawing.Point(1855, 797);
            this.lblNumNDepth_ge.Name = "lblNumNDepth_ge";
            this.lblNumNDepth_ge.Size = new System.Drawing.Size(37, 40);
            this.lblNumNDepth_ge.TabIndex = 42;
            this.lblNumNDepth_ge.Text = "0";
            // 
            // lblNumNDepth_shi
            // 
            this.lblNumNDepth_shi.AutoSize = true;
            this.lblNumNDepth_shi.BackColor = System.Drawing.Color.Transparent;
            this.lblNumNDepth_shi.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNumNDepth_shi.ForeColor = System.Drawing.Color.LightGreen;
            this.lblNumNDepth_shi.Location = new System.Drawing.Point(1809, 797);
            this.lblNumNDepth_shi.Name = "lblNumNDepth_shi";
            this.lblNumNDepth_shi.Size = new System.Drawing.Size(37, 40);
            this.lblNumNDepth_shi.TabIndex = 41;
            this.lblNumNDepth_shi.Text = "0";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(1573, 522);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(78, 67);
            this.pictureBox4.TabIndex = 48;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(1478, 522);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(78, 67);
            this.pictureBox3.TabIndex = 47;
            this.pictureBox3.TabStop = false;
            // 
            // lblLon
            // 
            this.lblLon.AutoSize = true;
            this.lblLon.BackColor = System.Drawing.Color.Transparent;
            this.lblLon.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLon.ForeColor = System.Drawing.Color.LightGreen;
            this.lblLon.Location = new System.Drawing.Point(1184, 198);
            this.lblLon.Name = "lblLon";
            this.lblLon.Size = new System.Drawing.Size(250, 30);
            this.lblLon.TabIndex = 49;
            this.lblLon.Text = "000°000′000″";
            // 
            // lblLat
            // 
            this.lblLat.AutoSize = true;
            this.lblLat.BackColor = System.Drawing.Color.Transparent;
            this.lblLat.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLat.ForeColor = System.Drawing.Color.LightGreen;
            this.lblLat.Location = new System.Drawing.Point(1184, 257);
            this.lblLat.Name = "lblLat";
            this.lblLat.Size = new System.Drawing.Size(250, 30);
            this.lblLat.TabIndex = 50;
            this.lblLat.Text = "000°000′000″";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1215, 643);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 24);
            this.label1.TabIndex = 51;
            this.label1.Text = "人工控制";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // anglePicture1
            // 
            this.anglePicture1.BackColor = System.Drawing.Color.Transparent;
            this.anglePicture1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("anglePicture1.BackgroundImage")));
            this.anglePicture1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.anglePicture1.Location = new System.Drawing.Point(1757, 443);
            this.anglePicture1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.anglePicture1.Name = "anglePicture1";
            this.anglePicture1.Size = new System.Drawing.Size(169, 150);
            this.anglePicture1.TabIndex = 54;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.anglePicture1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLat);
            this.Controls.Add(this.lblLon);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.lblNumNU_shi);
            this.Controls.Add(this.lblNumNU_ge);
            this.Controls.Add(this.lblNumNDepth_bai);
            this.Controls.Add(this.lblNumNDepth_ge);
            this.Controls.Add(this.lblNumNDepth_shi);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblNumNAngle_bai);
            this.Controls.Add(this.lblNumNAngle_ge);
            this.Controls.Add(this.lblNumNAngle_shi);
            this.Controls.Add(this.lblNumDIS_bai);
            this.Controls.Add(this.lblNumDIS_ge);
            this.Controls.Add(this.lblNumDIS_shi);
            this.Controls.Add(this.lblNumDepth_bai);
            this.Controls.Add(this.lblNumDepth_ge);
            this.Controls.Add(this.lblNumDepth_shi);
            this.Controls.Add(this.lblNumAngle_bai);
            this.Controls.Add(this.lblNumAngle_ge);
            this.Controls.Add(this.lblNumAngle_shi);
            this.Controls.Add(this.lblNumU_ge);
            this.Controls.Add(this.lblNumU_shi);
            this.Controls.Add(this.chartMain);
            this.Controls.Add(this.lblNumPosition_ge);
            this.Controls.Add(this.lblNumPosition_shi);
            this.Controls.Add(this.lblNumPosition_bai);
            this.Controls.Add(this.lblNumSpeed_ge);
            this.Controls.Add(this.lblNumSpeed_shi);
            this.Controls.Add(this.lblSSpeed);
            this.Controls.Add(this.lblSSeaDepth);
            this.Controls.Add(this.lblSDepth);
            this.Controls.Add(this.lblSTime);
            this.Controls.Add(this.lblNU);
            this.Controls.Add(this.lblNAngle);
            this.Controls.Add(this.lblNDepth);
            this.Controls.Add(this.lblNDis);
            this.Controls.Add(this.lblNTime);
            this.Controls.Add(this.lblBSpeed);
            this.Controls.Add(this.lblBAngle);
            this.Controls.Add(this.lblBDepth);
            this.Controls.Add(this.lblBDis);
            this.Controls.Add(this.lblBTime);
            this.Controls.Add(this.lblBoatWeight);
            this.Controls.Add(this.lblBoatPower);
            this.Controls.Add(this.lblBoatWidth);
            this.Controls.Add(this.lblBoatLength);
            this.Controls.Add(this.lblBoatName);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBoatName;
        private System.Windows.Forms.Label lblBoatLength;
        private System.Windows.Forms.Label lblBoatWidth;
        private System.Windows.Forms.Label lblBoatWeight;
        private System.Windows.Forms.Label lblBoatPower;
        private System.Windows.Forms.Label lblBTime;
        private System.Windows.Forms.Label lblBDis;
        private System.Windows.Forms.Label lblBAngle;
        private System.Windows.Forms.Label lblBDepth;
        private System.Windows.Forms.Label lblBSpeed;
        private System.Windows.Forms.Label lblNU;
        private System.Windows.Forms.Label lblNAngle;
        private System.Windows.Forms.Label lblNDepth;
        private System.Windows.Forms.Label lblNDis;
        private System.Windows.Forms.Label lblNTime;
        private System.Windows.Forms.Label lblSSpeed;
        private System.Windows.Forms.Label lblSSeaDepth;
        private System.Windows.Forms.Label lblSDepth;
        private System.Windows.Forms.Label lblSTime;
        private System.Windows.Forms.Label lblNumSpeed_shi;
        private System.Windows.Forms.Label lblNumSpeed_ge;
        private System.Windows.Forms.Label lblNumPosition_shi;
        private System.Windows.Forms.Label lblNumPosition_bai;
        private System.Windows.Forms.Label lblNumPosition_ge;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMain;
        private System.Windows.Forms.Label lblNumU_shi;
        private System.Windows.Forms.Label lblNumU_ge;
        private System.Windows.Forms.Label lblNumAngle_ge;
        private System.Windows.Forms.Label lblNumAngle_shi;
        private System.Windows.Forms.Label lblNumAngle_bai;
        private System.Windows.Forms.Label lblNumDepth_bai;
        private System.Windows.Forms.Label lblNumDepth_ge;
        private System.Windows.Forms.Label lblNumDepth_shi;
        private System.Windows.Forms.Label lblNumDIS_bai;
        private System.Windows.Forms.Label lblNumDIS_ge;
        private System.Windows.Forms.Label lblNumDIS_shi;
        private System.Windows.Forms.Label lblNumNAngle_bai;
        private System.Windows.Forms.Label lblNumNAngle_ge;
        private System.Windows.Forms.Label lblNumNAngle_shi;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblNumNU_shi;
        private System.Windows.Forms.Label lblNumNU_ge;
        private System.Windows.Forms.Label lblNumNDepth_bai;
        private System.Windows.Forms.Label lblNumNDepth_ge;
        private System.Windows.Forms.Label lblNumNDepth_shi;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblLon;
        private System.Windows.Forms.Label lblLat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private AngleControl.AnglePicture anglePicture1;
    }
}