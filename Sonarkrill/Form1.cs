using GeoAPI.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SonarKrill
{
    public partial class Form1 : Form
    {
        //实例化串口对象   
        SerialPort serialNetPort = new SerialPort(GetConfigValue("Net").Split(',')[0]); // 实例化网具串口
        SerialPort serialGpsPort = new SerialPort(GetConfigValue("GPS").Split(',')[0]); // 实例化GPS串口
        SerialPort serialSpeedPort = new SerialPort(GetConfigValue("Speed").Split(',')[0]); // 实例化测速串口

        private double pLonFrom = -1000;
        private double pLatFrom = -1000;
        private double pLonTo = -1000;
        private double pLatTo = -1000;

        public int count = 0;

        private bool configFinished = false; // 用来表示数据库配置是否完成

        private static string GetConfigValue(string key)
        {
            string strValue;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            strValue = config.AppSettings.Settings[key].Value.ToString();
            return strValue;
        }
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; // 允许新创建的线程操作UI
            RefreshVesselInfo();
            InitChat();
            InitConfigs();


        }

        private void InitConfigs()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings["SqliteDB"] == null)
            {

                string path = string.Empty;
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "设置数据库文件";
                fdlg.InitialDirectory = @"c:\";   //@ 取消转义
                fdlg.Filter = "data base file（*.db）|*.db|All files(*.*)|*.* ";
                /*
                 * FilterIndex 属性用于选择了何种文件类型,缺省设置为0,系统取Filter属性设置第一项
                 * ,相当于FilterIndex 属性设置为1.如果你编了3个文件类型，当FilterIndex ＝2时是指第2个.
                 */
                fdlg.FilterIndex = 1;
                /*
                 *如果值为false，那么下一次选择文件的初始目录是上一次你选择的那个目录，
                 *不固定；如果值为true，每次打开这个对话框初始目录不随你的选择而改变，是固定的  
                 */
                fdlg.RestoreDirectory = false;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    path = System.IO.Path.GetFullPath(fdlg.FileName);
                    //MessageBox.Show(path);
                    SetConfigValue("SqliteDB", path);
                    configFinished = true;
                }
            }
            else
            {
                configFinished = true;
            }
        }
        private bool SetConfigValue(string key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings[key] != null)
                    config.AppSettings.Settings[key].Value = value;
                else
                    config.AppSettings.Settings.Add(key, value);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void InitChat()
        {
            chartMain.Series.Clear();

            // create a line chart series
            Series newSeries = new Series("网深");
            newSeries.ChartType = SeriesChartType.Point;
            newSeries.BorderWidth = 8;
            newSeries.Color = Color.Red;

            newSeries.XValueType = ChartValueType.DateTime;
            chartMain.Series.Add(newSeries);

            Series seriesSonar = new Series("最佳深度");
            seriesSonar.ChartType = SeriesChartType.Point;
            seriesSonar.BorderWidth = 2;
            seriesSonar.Color = Color.Green;
            seriesSonar.XValueType = ChartValueType.DateTime;
            chartMain.Series.Add(seriesSonar);

            Series lineSeries = new Series("规划路径");
            lineSeries.ChartType = SeriesChartType.Line;
            lineSeries.BorderWidth = 1;
            lineSeries.Color = Color.Red;
            lineSeries.XValueType = ChartValueType.DateTime;
            chartMain.Series.Add(lineSeries);

            //chartMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(223)))), ((int)(((byte)(240)))));
            //chartMain.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            //chartMain.BackSecondaryColor = System.Drawing.Color.White;
            chartMain.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            chartMain.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartMain.BorderlineWidth = 2;
            chartMain.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.None;
            chartMain.ChartAreas[0].Area3DStyle.Inclination = 15;
            chartMain.ChartAreas[0].Area3DStyle.IsClustered = true;
            chartMain.ChartAreas[0].Area3DStyle.IsRightAngleAxes = false;
            chartMain.ChartAreas[0].Area3DStyle.Perspective = 10;
            chartMain.ChartAreas[0].Area3DStyle.Rotation = 10;
            chartMain.ChartAreas[0].Area3DStyle.WallWidth = 0;
            chartMain.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);

            //chartMain.ChartAreas[0].AxisX.LineColor = System.Drawing.Color.White;
            //chartMain.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.White;
            //chartMain.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.White;
            //chartMain.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.White;


            chartMain.ChartAreas[0].AxisX.LabelStyle.Format = "mm:ss"; //X轴显示的时间格式，HH为大写时是24小时制，hh小写时是12小时制
            double minValue = DateTime.Now.AddSeconds(-5).ToOADate();
            double maxValue = DateTime.Now.AddSeconds(45).ToOADate();
            //double minValue = (Convert.ToDateTime("2021-04-20 09:38:00")).ToOADate();
            //double maxValue = (Convert.ToDateTime("2021-04-20 09:40:00")).ToOADate();
            chartMain.ChartAreas[0].AxisX.Minimum = minValue;
            chartMain.ChartAreas[0].AxisX.Maximum = maxValue;
            chartMain.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;//如果是时间类型的数据，间隔方式可以是秒、分、时
            chartMain.ChartAreas[0].AxisX.Interval = DateTime.Parse("00:00:05").Second;//间隔为2秒
            chartMain.ChartAreas[0].AxisX.MajorGrid.Interval = DateTime.Parse("00:00:05").Second;//间隔为2秒
            chartMain.ChartAreas[0].AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;

            chartMain.ChartAreas[0].AxisX.LabelStyle.Interval = DateTime.Parse("00:00:05").Second;//间隔为2秒
            chartMain.ChartAreas[0].AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;

            //chartMain.Series[0].MarkerImage = "DividentMarker.bmp";

            chartMain.Series[0].XValueType = ChartValueType.DateTime;
            chartMain.ChartAreas[0].AxisY.Maximum = 0;
        }

        private void RefreshVesselInfo()
        {
            string str = GetConfigValue("vessel").ToString();
            string[] parts = str.Split(new char[] { ',' });
            lblBoatName.Text = parts[0];
            lblBoatLength.Text = parts[1];
            lblBoatWidth.Text = parts[2];
            lblBoatPower.Text = parts[3];
            lblBoatWeight.Text = parts[4];
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // 检查网具串口，并绑定触发的事件
            Control.CheckForIllegalCrossThreadCalls = false;
            serialNetPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(netDataReceived);
            if (!serialNetPort.IsOpen)
            {
                try
                {
                    serialNetPort.Open(); //打开串口
                }
                catch (Exception ex)
                {
                    MessageBox.Show("网具串口打开失败！具体原因：" + ex.Message, "提示信息");
                }
            }
            // 检查GPS串口，并绑定触发的事件
            serialGpsPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(gpsDataRecived);
            if (!serialGpsPort.IsOpen)
            {
                try
                {
                    serialGpsPort.Open(); //打开串口
                }
                catch (Exception ex)
                {
                    MessageBox.Show("GPS串口打开失败！具体原因：" + ex.Message, "提示信息");
                }
            }
            serialSpeedPort.DataReceived += new SerialDataReceivedEventHandler(speedDataRecivedHandle);
            if (!serialSpeedPort.IsOpen)
            {
                try
                {
                    serialSpeedPort.Open(); //打开串口
                }
                catch (Exception ex)
                {
                    MessageBox.Show("串行端口打开失败！具体原因：" + ex.Message, "提示信息");
                }
            }


        }
        private void speedDataRecivedHandle(object sender, SerialDataReceivedEventArgs e)
        {
            System.Threading.Thread.Sleep(20);
            //MessageBox.Show("Data:" + serialSpeedPort.ReadLine(), "Error");
            try
            {

                string lastdata = serialSpeedPort.ReadLine();
                double speed_now = parseSpeedFromCom(lastdata);
                // 数据无效
                if (speed_now == -1)
                {
                    return;
                }
                else
                {
                    lblSSpeed.Text = speed_now.ToString();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("获取数据无效！" + er.Message, "Error");
                return;
            }
        }
        private double parseSpeedFromCom(string lastdata)
        {
            String GPRMC = MidStrEx(lastdata, "$VDVBW,", "\r");
            String[] paramss = GPRMC.Split(',');
            double result;
            try
            {
                result = Convert.ToDouble(paramss[0]);
                return result;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private void gpsDataRecived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string lastdata = serialGpsPort.ReadLine();
                GPSData myGps = parseData(lastdata);
                // 数据无效
                if (myGps.getDtatime() == "ERROR")
                {
                    return;
                }
                else
                {
                    RefreshUI(myGps);
                }
            }
            catch
            {
                return;
            }
            
        }

        private void RefreshUI(GPSData myGps)
        {
            pLonFrom = pLonTo;
            pLatFrom = pLatTo;
            pLonTo = Convert.ToDouble(myGps.getLon());
            pLatTo = Convert.ToDouble(myGps.getLat());

            lblNTime.Text = myGps.getTime();
            lblBTime.Text = myGps.getTime();
            lblSTime.Text = myGps.getTime();

            int sppedInt = Convert.ToInt32(myGps.getSpeed());
            int spped_shi = sppedInt / 10;
            int spped_ge = sppedInt % 10;

            int positionInt = Convert.ToInt32(myGps.getDirect());
            int position_bai = positionInt / 100;
            int position_shi = (positionInt % 100) / 10;
            int position_ge = (positionInt % 100) % 10;

            lblNumSpeed_shi.Text = spped_shi.ToString();
            lblNumSpeed_ge.Text = spped_ge.ToString();

            lblNumPosition_bai.Text = position_bai.ToString();
            lblNumPosition_shi.Text = position_shi.ToString();
            lblNumPosition_ge.Text = position_ge.ToString();

            string lon_du = Math.Floor(myGps.getLon()).ToString();
            string lon_fen = (Math.Round((
                Convert.ToDouble(Math.Round(myGps.getLon(), 4) - Math.Floor(myGps.getLon())) * 60), 4)).ToString();
            string lon_str = lon_du + "° " + lon_fen + "′";

            string lat_du = Math.Floor(myGps.getLat()).ToString();
            string lat_fen = (Math.Round((
                Convert.ToDouble(Math.Round(myGps.getLat(), 4) - Math.Floor(myGps.getLat())) * 60), 4)).ToString();
            string lat_str = lat_du + "° " + lat_fen + "′";


            lblLon.Text = lon_str;
            lblLat.Text = lat_str;

            //lblLon.Text = myGps.getLon().ToString();
            //lblLat.Text = myGps.getLat().ToString();

        }
        private GPSData parseData(string lastdata)
        {
            String GPRMC = MidStrEx(lastdata, "$GPRMC,", "*");
            String[] paramss = GPRMC.Split(',');
            String status;
            try
            {
                status = paramss[1];
            }
            catch
            {
                status = "";
                GPSData myGpsData = new GPSData("ERROR", 0.00, 0.00, 0.00F, 0.00F);
                return myGpsData;

            }

            if (status == "A") // 定位状态有效
            {
                String time = paramss[0].Substring(0, 2) + ":" + paramss[0].Substring(2, 2) + ":" + paramss[0].Substring(4, 2);
                double lat = Convert.ToDouble(paramss[2].Substring(0, 2)) + Convert.ToDouble(paramss[2].Substring(2, 7)) / 60;
                double lon = Convert.ToDouble(paramss[4].Substring(0, 3)) + Convert.ToDouble(paramss[4].Substring(3, 7)) / 60;
                float speed = Convert.ToSingle(paramss[6]); //地面速率
                float direct = Convert.ToSingle(paramss[7]); // 地面航向
                String date = "20" + paramss[8].Substring(4, 2) + "-" + paramss[8].Substring(2, 2) + "-" + paramss[8].Substring(0, 2);
                String date_time = date + " " + time;
                GPSData myGpsData = new GPSData(date_time, lat, lon, speed, direct);
                return myGpsData;
            }
            else
            {
                String date_time = "ERROR";
                Double lat = 0.00;
                Double lon = 0.00;
                float speed = 0.00F;
                float direct = 0.00F;
                GPSData myGpsData = new GPSData(date_time, lat, lon, speed, direct);

                return myGpsData;
            }

        }

        private string MidStrEx(string sourse, string startstr, string endstr)
        {
            string result = string.Empty;
            int startindex, endindex;
            try
            {
                startindex = sourse.IndexOf(startstr);
                if (startindex == -1)
                    return result;
                string tmpstr = sourse.Substring(startindex + startstr.Length);
                endindex = tmpstr.IndexOf(endstr);
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("MidStrEx Err:" + ex.Message);
            }
            return result;
        }

        private void netDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            System.Threading.Thread.Sleep(20); // 这个延时是为了保证每次接收到的缓冲区的数据是完整的，避免出现高频错包

            if (!serialNetPort.IsOpen)
            {
                try
                {
                    serialNetPort.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "error");
                }
            }
            try
            {
                StringBuilder strbuilder = new StringBuilder();
                byte[] buf = new byte[10];//声明一个临时数组存储当前来的串口数据  (byte型 数据)
                int n = serialNetPort.Read(buf, 0, 10);//读取缓冲数据  
                Array.Resize(ref buf, n);
                foreach (byte b in buf)
                {
                    strbuilder.Append(b.ToString("X2") + " ");//一个字节一个字节的处理
                }
                //WriteLog("接收： " + strbuilder.ToString());
                if (n == 5) // 缓冲区接收到5个字节的数据，一般是0x23开头的，下位机传回的数据。
                {
                    string[] datas = strbuilder.ToString().Split(' ');
                    string data_header = datas[0];
                    byte data_header_code = buf[0];

                    string angle = datas[1];
                    byte angle_code = buf[1];

                    string depth = datas[2];
                    byte depth_code = buf[2];

                    string status = datas[3];
                    byte status_code = buf[3];

                    string check = datas[4];
                    byte check_code = buf[4];

                    int v_status;   // 电压状态 0表示正常 1表示异常 下同
                    int lihe_status;    // 离合状态
                    int code_status;    // 编码状态
                    int future_status;  // 预留状态
                    int v_value;    //电压值

                    int reCheck = angle_code ^ depth_code ^ status_code;
                    byte reCheckCode = BitConverter.GetBytes(reCheck)[0];
                    if (Convert.ToInt32(data_header_code) == 0x23 && reCheckCode == check_code)
                    {
                        string angleValue;
                        string depthValue;
                        // 校验码检测正确 开始解码并更新UI
                        //WriteLog("校验码检测正确");
                        angleValue = (Convert.ToInt32(angle_code) - 35).ToString();
                        //switch (angle_code)
                        //{
                        //    case 0x01:
                        //        angleValue = "-45";
                        //        break;
                        //    case 0x02:
                        //        angleValue = "-40";
                        //        break;
                        //    case 0x03:
                        //        angleValue = "-35";
                        //        break;
                        //    case 0x04:
                        //        angleValue = "-30";
                        //        break;
                        //    case 0x05:
                        //        angleValue = "-25";
                        //        break;
                        //    case 0x06:
                        //        angleValue = "-20";
                        //        break;
                        //    case 0x07:
                        //        angleValue = "-15";
                        //        break;
                        //    case 0x08:
                        //        angleValue = "-10";
                        //        break;
                        //    case 0x09:
                        //        angleValue = "-5";
                        //        break;
                        //    case 0x0A:
                        //        angleValue = "0";
                        //        break;
                        //    case 0x0B:
                        //        angleValue = "5";
                        //        break;
                        //    case 0x0C:
                        //        angleValue = "10";
                        //        break;
                        //    case 0x0D:
                        //        angleValue = "15";
                        //        break;
                        //    case 0x0E:
                        //        angleValue = "20";
                        //        break;
                        //    case 0x0F:
                        //        angleValue = "25";
                        //        break;
                        //    case 0x10:
                        //        angleValue = "30";
                        //        break;
                        //    case 0x11:
                        //        angleValue = "35";
                        //        break;
                        //    case 0x12:
                        //        angleValue = "40";
                        //        break;
                        //    case 0x13:
                        //        angleValue = "45";
                        //        break;
                        //    default:
                        //        angleValue = "signal error";
                        //        //WriteLog("传回的角度有误！");
                        //        return;
                        //}

                        depthValue = depth_code.ToString();
                        string statusStr = System.Convert.ToString(Convert.ToInt32(status_code), 2);
                        // 补齐高位的0 方便解析状态
                        if (statusStr.Length < 8)
                        {
                            int t = 8 - statusStr.Length;
                            while (t > 0)
                            {
                                statusStr = "0" + statusStr;
                                t--;
                            }
                        }
                        v_status = Convert.ToInt32(statusStr[7].ToString());
                        lihe_status = Convert.ToInt32(statusStr[6].ToString());
                        code_status = Convert.ToInt32(statusStr[5].ToString());
                        future_status = Convert.ToInt32(statusStr[4].ToString());

                        v_value = Convert.ToInt32(statusStr[3].ToString())
                            + Convert.ToInt32(statusStr[2].ToString()) * 2
                            + Convert.ToInt32(statusStr[1].ToString()) * 4
                            + Convert.ToInt32(statusStr[0].ToString()) * 8;
                        string v_value_str = v_value.ToString() + " V";

                        //WriteLog(angleValue + "°    " + depthValue + "m    "
                        //    + v_status.ToString() + "  " + lihe_status.ToString() + "  "
                        //    + code_status.ToString() + "  " + future_status.ToString() + "  "
                        //    + v_value.ToString());

                        // 更新UI
                        lblNAngle.Text = angleValue;
                        lblNDepth.Text = depthValue;
                        lblNU.Text = v_value.ToString();

                        int angleInt = Convert.ToInt32(angleValue);
                        int angleInt_shi;
                        int angleInt_ge;
                        if (angleInt < 0)
                        {
                            lblNumAngle_bai.Text = "-";
                            lblNumNAngle_bai.Text = "-";
                            angleInt_shi = angleInt / -10;
                            angleInt_ge = (angleInt % 10) * -1;
                        }
                        else
                        {
                            lblNumAngle_bai.Text = "+";
                            lblNumNAngle_bai.Text = "+";
                            angleInt_shi = angleInt / 10;
                            angleInt_ge = angleInt % 10;
                        }
                        
                        lblNumAngle_shi.Text = angleInt_shi.ToString();
                        lblNumAngle_ge.Text = angleInt_ge.ToString();

                        lblNumNAngle_shi.Text = angleInt_shi.ToString();
                        lblNumNAngle_ge.Text = angleInt_ge.ToString();

                        int depthInt = Convert.ToInt32(depthValue);
                        int depth_bai = depthInt / 100;
                        int depth_shi = (depthInt % 100) / 10;
                        int depth_ge = (depthInt % 100) % 10;

                        lblNumDepth_bai.Text = depth_bai.ToString();
                        lblNumDepth_shi.Text = depth_shi.ToString();
                        lblNumDepth_ge.Text = depth_ge.ToString();

                        lblNumNDepth_bai.Text = depth_bai.ToString();
                        lblNumNDepth_shi.Text = depth_shi.ToString();
                        lblNumNDepth_ge.Text = depth_ge.ToString();

                        lblNumNU_shi.Text = (v_value / 10).ToString(); 
                        lblNumNU_ge.Text = (v_value % 10).ToString();

                        anglePicture1.turn(angleInt);

                        try
                        {
                            Bitmap a = new Bitmap(pictureBox1.Image);//得到图片框中的图片
                            pictureBox1.Image = Rotate(a, -45);
                            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                            //pictureBox1.Location = panel1.Location;
                            pictureBox1.Refresh();//最后刷新图片框
                        }
                        catch { }

                        // 调整状态显示
                        if (v_status == 0)
                        {
                            pictureBox1.Image = Image.FromFile(Application.StartupPath + "./images/green.png");
                        }
                        else
                        {
                            pictureBox1.Image = Image.FromFile(Application.StartupPath + "./images/red.png");
                        }

                        if (lihe_status == 0)
                        {
                            pictureBox2.Image = Image.FromFile(Application.StartupPath + "./images/green.png");
                        }
                        else
                        {
                            pictureBox2.Image = Image.FromFile(Application.StartupPath + "./images/red.png");
                        }

                        if (code_status == 0)
                        {
                            pictureBox3.Image = Image.FromFile(Application.StartupPath + "./images/green.png");
                        }
                        else
                        {
                            pictureBox3.Image = Image.FromFile(Application.StartupPath + "./images/red.png");
                        }

                        if (future_status == 0)
                        {
                            pictureBox4.Image = Image.FromFile(Application.StartupPath + "./images/green.png");
                        }
                        else
                        {
                            pictureBox4.Image = Image.FromFile(Application.StartupPath + "./images/red.png");
                        }
                        
                        int u_10 = v_value / 10;
                        int u_1 = v_value % 10;

                        lblNumU_shi.Text = u_10.ToString();
                        lblNumU_ge.Text = u_1.ToString();

                    }
                    else
                    {
                       
                        MessageBox.Show("校验码检测错误");
                    }
                }
                //MessageBox.Show(n.ToString() + ": " + strbuilder.ToString());
                else if (n == 4)
                {
                    string[] datas = strbuilder.ToString().Split(' ');

                    string data_header = datas[0];
                    byte data_header_code = buf[0];

                    string angle = datas[1];
                    byte angle_code = buf[1];

                    string depth = datas[2];
                    byte depth_code = buf[2];

                    string check = datas[3];
                    byte check_code = buf[3];

                    int reCheck = angle_code ^ depth_code;

                    if (Convert.ToInt32(check_code) != reCheck)
                    {
                        //WriteLog("数据校验失败");
                        MessageBox.Show("数据校验失败！");
                        return;
                    }

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                //textBoxReceive.Text = "";//清空

            }

            //System.Threading.Thread.Sleep(waitingMilliSecond);
            try
            {
                serialNetPort.DiscardInBuffer();
            }
            catch
            {

            }
            // 开始允许发送
            //sendAbledFlag = true;
        
    }

        private Bitmap Rotate(Bitmap a, int v)
        {
            throw new NotImplementedException();
        }

        //void tm_Elapsed(object sender, EventArgs e)
        //{
        //    if (isStart)
        //    {
        //        angle += 5;
        //        if (angle >= 359) angle = 0;
        //        RotateImage(pic1, Properties.Resources._1, angle);
        //    }
        //}



        private void chartMain_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("人工控制界面");
            serialGpsPort.Close();
            serialNetPort.Close();
            serialSpeedPort.Close();

            FormMain anotherForm;
            anotherForm = new FormMain();
            this.Hide();
            //this.Close();
            anotherForm.ShowDialog();
            Application.ExitThread();
            //Application.Run(new FormMain());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!configFinished)
            {
                return;
            }
            Random ran = new Random();
            DateTime dtNetTime = DateTime.Now;
            //float fNetDeep = Convert.ToSingle(ran.Next(-100 * 10, -50) / 10.0);
            float fNetDeep = getfNetDeep();
            float fLineDeep = getLineDeep();
            //lblNTime.Text = dtNetTime.ToString("HH;mm:ss");
            //float lift_angle = Convert.ToSingle(ran.Next(0 * 10, 90 * 10) / 10.0);
            ////lblNAngle.Text = lift_angle.ToString();
            ////lblNDeep.Text= fNetDeep.ToString();

            //float distance_hor = Convert.ToSingle(ran.Next(100 * 10, 200 * 10) / 10.0);
            //lblNDistance.Text = distance_hor.ToString();

            string net_time = lblNTime.Text;

            chartMain.Series[0].Points.AddXY(dtNetTime, fNetDeep);
            try
            {
                chartMain.Series[0].MarkerImage = "test30.png";
            }
            catch (Exception eR)
            {
                MessageBox.Show(eR.ToString(), "ERROR");
            }

            double minValue = dtNetTime.AddSeconds(-1).ToOADate();
            double maxValue = dtNetTime.AddSeconds(45).ToOADate();
            chartMain.ChartAreas[0].AxisX.Minimum = minValue;
            chartMain.ChartAreas[0].AxisX.Maximum = maxValue;

            float fSonarDeep;
            float fLineDepp;
            //fSonarDeep = Convert.ToSingle(ran.Next(-150 * 10, -50) / 10.0);
            //fSonarDeep = getfSonarDeep();
            fSonarDeep = getSonarDeepReal();
            //fLineDeep = getLineDeepReal(fSonarDeep);
            fLineDeep = getLineDeepReal();
            float fSeconds = Convert.ToSingle(210 / 5.0);

            DateTime dtSonar = DateTime.Now.AddSeconds(fSeconds);
            float fakeDeep = getfFakeNetDeep();

            chartMain.Series[1].Points.AddXY(dtSonar, fSonarDeep);
            //chartMain.Series[1].Points.AddXY(dtSonar, fakeDeep);
            // Series2是轨迹图层，
            chartMain.Series[2].Points.AddXY(dtSonar, fLineDeep);
            //chartMain.Series[2].Points.AddXY(dtSonar, fLineDeep);

        }

        private float getLineDeepReal()
        {
            /*Fake version  需要参数 float fSonarDeep */
            //count += 1;
            //Random ra = new Random();
            //float[] err = { (float)-0.3, (float)-3.2, (float)-2.8, (float)2.8,
            //    (float)2.5, (float)-3.2, (float)-4.1, (float)-2.5, (float)-2.3,
            //    (float)-1.8, (float)2.5,
            //    (float)2.2, (float)-1.4, (float)2.3, (float)2.45, (float)-5.4};
            //return fSonarDeep + err[count % 16] / 10 + ra.Next(-1, 1);


            /*Real version*/
            string path = GetConfigValue("SqliteDB");
            float t = -1;
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + path + ";Version=3;");
            if (m_dbConnection.State != System.Data.ConnectionState.Open)
            {
                m_dbConnection.Open();
                //String sql_message = "SELECT depth_cacl FROM main.estimate_depths ORDER BY time DESC LIMIT 1";
                String sql_message = "SELECT depth FROM main.paths ORDER BY time DESC LIMIT 1";
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = m_dbConnection;
                cmd.CommandText = sql_message;
                try
                {
                    SQLiteDataReader test = cmd.ExecuteReader();

                    while (test.Read())
                    {
                        //t = Convert.ToSingle(test["depth_cacl"].ToString());
                        t = Convert.ToSingle(test["depth"].ToString());
                    }
                    return t * -1;
                }
                catch
                {
                    return -5;
                }

            }
            return (float)-1;

        }

        private float getLineDeep()
        {
            count += 1;
            //需要改成从数据库查找合适的点（从小师姐的表中获得）
            float[] lines = { (float)-40.3, (float)-40.5, (float)-41.7, (float)-43.2, 
                (float)-44.5, 
                (float)-44.5, (float)-45.7, (float)-45.5, 
                (float)-46.2, (float)-47.9, (float)-49.7, 
                (float)-50.1, (float)-52.7, (float)-53.7,
                (float)-55.2,(float)-56.2,
                (float)-57.3, (float)-58.5, (float)-60.2, (float)-61.5, 
                (float)-61.3, (float)-62.5,
                (float)-60.3, (float)-58.2,(float)-58.3,-58,
                (float)-56.2,(float)-56.3,(float)-55.1,(float)-55.2,
                (float)-54.0,(float)-55.2,(float)-55.2,(float)-54.2,
                (float)-54.2,(float)-53.8,
                (float)-53.8,(float)-52.0,(float)-52.5,(float)-52.0,
                (float)-51.2,(float)-51.8,
                (float)-51.4,(float)-51.0,(float)-50.2,
                (float)-49.2,(float)-48.3,(float)-47.1,(float)-46.2,
                (float)-45.0,(float)-44.2,(float)-45.2,(float)-44.2,
                (float)-44.2,(float)-43.8,
                (float)-43.8,(float)-42.0,(float)-42.5,(float)-42.0,
                (float)-41.2,(float)-41.8,
                (float)-41.4,(float)-41.0,(float)-40.2,};
            return lines[count % 63];
            //Random ran = new Random();
            //return ran.Next(-100, -50);
        }

        
        private float getfSonarDeep()
        {
            if (lblSDepth.Text.Equals("waiting"))
            {
                return Convert.ToSingle(50) * -1;
            }
            else
            {
                return Convert.ToSingle(lblSDepth.Text.ToString()) * -1;
            }
        }
        

        private float getSonarDeepReal()
        {
            string path = GetConfigValue("SqliteDB");
            float t = -1;
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + path + ";Version=3;");
            if (m_dbConnection.State != System.Data.ConnectionState.Open)
            {
                m_dbConnection.Open();
                String sql_message = "SELECT depth_cacl FROM main.estimate_depths ORDER BY time DESC LIMIT 1";
                //String sql_message = "SELECT depth FROM main.paths ORDER BY time DESC LIMIT 1";
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = m_dbConnection;
                cmd.CommandText = sql_message;
                try
                {
                    SQLiteDataReader test = cmd.ExecuteReader();

                    while (test.Read())
                    {
                        t = Convert.ToSingle(test["depth_cacl"].ToString());
                        //t = Convert.ToSingle(test["depth"].ToString());
                    }
                    return t*-1;
                }
                catch
                {
                    return -2;
                }

            }
            return (float)-1;

        }

        private float getfNetDeep()
        {
            if (lblNDepth.Text.Equals("waiting"))
            {
                return Convert.ToSingle(-10);
            }
            else
            {
                return Convert.ToSingle(lblNDepth.Text.ToString()) * -1;
            }
        }

        private float getfFakeNetDeep()
        {
            float[] lines = { (float)-40.3, (float)-40.5, (float)-41.7, (float)-43.2,
                (float)-44.5,
                (float)-44.5, (float)-45.7, (float)-45.5,
                (float)-46.2, (float)-47.9, (float)-49.7,
                (float)-50.1, (float)-52.7, (float)-53.7,
                (float)-55.2,(float)-56.2,
                (float)-57.3, (float)-58.5, (float)-60.2, (float)-61.5,
                (float)-61.3, (float)-62.5,
                (float)-60.3, (float)-58.2,(float)-58.3,-58,
                (float)-56.2,(float)-56.3,(float)-55.1,(float)-55.2,
                (float)-54.0,(float)-55.2,(float)-55.2,(float)-54.2,
                (float)-54.2,(float)-53.8,
                (float)-53.8,(float)-52.0,(float)-52.5,(float)-52.0,
                (float)-51.2,(float)-51.8,
                (float)-51.4,(float)-51.0,(float)-50.2,
                (float)-49.2,(float)-48.3,(float)-47.1,(float)-46.2,
                (float)-45.0,(float)-44.2,(float)-45.2,(float)-44.2,
                (float)-44.2,(float)-43.8,
                (float)-43.8,(float)-42.0,(float)-42.5,(float)-42.0,
                (float)-41.2,(float)-41.8,
                (float)-41.4,(float)-41.0,(float)-40.2,};
            
            Random ra = new Random();
            float[] err = { (float)-0.3, (float)-3.2, (float)-2.8, (float)2.8,
                (float)2.5, (float)-3.2, (float)-4.1, (float)-2.5, (float)-2.3,
                (float)-1.8, (float)2.5,
                (float)2.2, (float)-1.4, (float)2.3, (float)2.45, (float)-5.4};
            return lines[count % 63] + err[count%16] + ra.Next(-5,5);

        }

        private void lblBoatName_Click(object sender, EventArgs e)
        {

        }

        private void lblBoatWeight_Click(object sender, EventArgs e)
        {

        }
    }
}
