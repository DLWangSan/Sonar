using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SharpMap;
using SharpMap.CoordinateSystems;

using SharpMap.Forms;
using SharpMap.Forms.Tools;
using SharpMap.Layers;
using System.Drawing.Drawing2D;
using SharpMap.Data;
using SharpMap.Rendering.Decoration;
using System.Windows.Forms.DataVisualization.Charting;
using System.Configuration;
using GeoAPI.Geometries;
using SharpMap.Styles;
using System.IO.Ports;
using System.Diagnostics;
using System.Data.SQLite;
using System.Timers;
using System.Threading;
using System.IO;

namespace SonarKrill
{
    public partial class FormMain : Form
    {
        //实例化串口对象   
        SerialPort serialNetPort = new SerialPort(GetConfigValue("Net").Split(',')[0]); // 实例化网具串口
        SerialPort serialGpsPort = new SerialPort(GetConfigValue("GPS").Split(',')[0]); // 实例化GPS串口
        SerialPort serialSpeedPort = new SerialPort(GetConfigValue("Speed").Split(',')[0]); // 实例化测速串口

        private Random random = new Random();
        private int pointIndex = 0;
        private SharpMap.Data.Providers.GeometryProvider geoProvider;//
        private double pLonFrom = -1000;
        private double pLatFrom = -1000;
        private double pLonTo = -1000;
        private double pLatTo = -1000;

        // 用于调整每次收到数据后 与 允许发送数据之间的等待间隔 单位是ms
        private int waitingMilliSecond = 500; // 半秒钟
        // 数据传送时间 用于计算重发等待时间
        private int sendingTime = 0; // 默认三秒 可以在界面中修改

        // 重发等待间隔 ms 表示等待收到下位机反馈的时间
        //private int reSendWaiting = 15000;

        private bool receivedDepthFlag = false; // 用来表示上位机收到来自下位机（网具）的距离数据。
        private bool configFinished = false; // 用来表示数据库配置是否完成

        // 一些全局的信号量 保证半双工正常通信
        private string sendedContent= ""; // 向下位机发送的内容，用来比对下位机发回的数据。
        private bool sendAbledFlag = false; // false表示当前通道被占用，暂时不发送；true表示可以直接发送
        private bool rightFlag = false; // false表示收到的与发出的不一致，需要重新发送
        private bool sendingDataFlag = false; // true表示正有数据在发送过程中或等待确认过程中
        private bool sendingFlagNow = false; // true表示数据正在发送（不包括等待确认的状态）

        DateTime minValue;
        DateTime maxValue;

        public FormMain()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; // 允许新创建的线程操作UI

            RefreshVesselInfo();
            InitConfigs();
            InitMap();
            InitChat();
            //CheckCOMStatus();   // 检测串口状态
            SetPara();

        }

        private void InitConfigs()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings["SqliteDB"] == null)
            {
                //string path = string.Empty;
                //System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
                //fbd.Description = "选择数据库文件";
                //if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                //    path = fbd.SelectedPath;
                //}
                //SetConfigValue("SqliteDB", path);
                //configFinished = true;
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

        private void comDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //string portName = serialPort.PortName;
            MessageBox.Show("111111", "txt");
        }

        private void CheckCOMStatus()
        {
            ConfigItem gpsConfig = getComConfig("GPS");
            ConfigItem netConfig = getComConfig("Net");
            ConfigItem speedConfig = getComConfig("Speed");

            labelGPSStatus.Text = "检测中...";
            if (checkStatus(gpsConfig))
            //if (true )
            {
                labelGPSStatus.Text = "正常";
                labelGPSStatus.ForeColor = Color.Green;
            }
            else
            {
                labelGPSStatus.Text = "异常！";
                labelGPSStatus.ForeColor = Color.Red;
            }

            labelNETStatus.Text = "检测中...";
            if (checkStatus(netConfig))
            {
                labelNETStatus.Text = "正常";
                labelNETStatus.ForeColor = Color.Green;
            }
            else
            {
                labelNETStatus.Text = "异常！";
                labelNETStatus.ForeColor = Color.Red;
            }

            labelSpeedStatus.Text = "检测中...";
            if (checkStatus(speedConfig))
            {
                labelSpeedStatus.Text = "正常";
                labelSpeedStatus.ForeColor = Color.Green;
            }
            else
            {
                labelSpeedStatus.Text = "异常！";
                labelSpeedStatus.ForeColor = Color.Red;
            }

        }

        private bool checkStatus(ConfigItem itemConfig)
        {
            try
            {
                string tempName = itemConfig.getComName();
                SerialPort tempSerialPort;
                if (tempName.Equals(GetConfigValue("NET").Split(',')[0]))
                {
                    serialNetPort = new SerialPort(GetConfigValue("Net").Split(',')[0]);
                    tempSerialPort = serialNetPort;
                }
                else if (tempName.Equals(GetConfigValue("GPS").Split(',')[0]))
                {
                    serialGpsPort = new SerialPort(GetConfigValue("GPS").Split(',')[0]);
                    tempSerialPort = serialGpsPort;
                }
                else if (tempName.Equals(GetConfigValue("SPEED").Split(',')[0]))
                {
                    serialSpeedPort = new SerialPort(GetConfigValue("SPEED").Split(',')[0]);
                    tempSerialPort = serialSpeedPort;
                }
                else
                {
                    // 待修改
                    tempSerialPort = new SerialPort();
                }
                //SerialPort serialPort = new SerialPort();
                //serialPort.Close();
                //serialPort.PortName = itemConfig.getComName();
                //serialPort.BaudRate = itemConfig.getBaudRate();
                //serialPort.DataBits = itemConfig.getDataBits();
                //serialPort.Parity = itemConfig.getParity();
                //serialPort.StopBits = itemConfig.getStopBits();

                //tempSerialPort.ReadTimeout = 1500; // 1.5秒读不到数据就认为串口配置有问题、
                //serialPort.Close();

                //if (tempSerialPort.IsOpen)
                //{
                //    tempSerialPort.Close();
                //}

                try 
                {
                    tempSerialPort.Open();

                }
                catch (Exception ex)
                {
                    // 串口存在  但是正在使用  认为状态是正常的
                    if (ex.Message.Contains("被拒绝"))
                        return true;
                    //tempSerialPort.Close();
                    return false;
                }
                //tempSerialPort.Close();
                return true;
            }
            catch
            {
                
                return false;
            }
            
        }

        private ConfigItem getComConfig(string key)
        {
            
            try
            {
                string s = ConfigurationManager.AppSettings[key];
                string[] elems = s.Split(',');
                string comName = elems[0];
                int baudRate = int.Parse(elems[1]);
                int dataBits = int.Parse(elems[2]);
                string parity_str = elems[3];
                Parity parity;
                switch (parity_str)
                {
                    case "None":
                        parity = Parity.None;
                        break;
                    case "Odd":
                        parity = Parity.Odd;
                        break;
                    case "Even":
                        parity = Parity.Even;
                        break;
                    default:
                        MessageBox.Show("Error：校验位参数配置不正确!", "Error");
                        return null;
                }

                string stopBits_str = elems[4];
                StopBits stopBits;
                switch (stopBits_str)
                {
                    case "1":
                        stopBits = StopBits.One;
                        break;
                    case "1.5":
                        stopBits = StopBits.OnePointFive;
                        break;
                    case "2":
                        stopBits = StopBits.Two;
                        break;
                    default:
                        MessageBox.Show("Error：停止位参数配置不正确!", "Error");
                        return null;
                }

                ConfigItem c = new ConfigItem(comName, baudRate, dataBits, parity, stopBits);
                return c;
            }
            catch(Exception ex)
            {
                MessageBox.Show("串口配置读取失败！请检查串口配置文件是否正常！" + ex.Message, "ERROR");
                return null;
            }

        }

        private void timerFunc(object sender, ElapsedEventArgs e)
        {
            InitGPSData();
            InitSpeedData();
        }

        private void InitSpeedData()
        {
            try
            {
                serialSpeedPort.DataReceived += new SerialDataReceivedEventHandler(speedDataRecivedHandle);
            }
            catch (NotImplementedException e)
            {
                MessageBox.Show("Error: SPEED获取数据无效！" + e.Message, "Error");
            }


            try
            {
                serialSpeedPort.Open();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
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
                    lblVSpeed.Text = speed_now.ToString();
                }
            }
            catch(Exception er)
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
            try {
                result = Convert.ToDouble(paramss[0]);
                return result;
            } catch (Exception) {
                return -1;
            }
        }


        // 初始化数据
        private void InitGPSData()
        {
            //SerialPort mySeriaPort = new SerialPort("COM4");

            //mySeriaPort.BaudRate = 9600;
            //mySeriaPort.Parity = Parity.None;
            //mySeriaPort.StopBits = StopBits.One;
            //mySeriaPort.DataBits = 8;
            //mySeriaPort.ReceivedBytesThreshold = 1;
            //mySeriaPort.Handshake = Handshake.None;
            try
            {
                serialGpsPort.DataReceived += new SerialDataReceivedEventHandler(gpsDataRecivedHandle);
            }
            catch(NotImplementedException e)
            {
                MessageBox.Show("Error: GPS信号弱，获取数据无效！" + e.Message, "Error");
            }


            try
            {
                serialGpsPort.Open();
            }
            catch(Exception error)
            {
                Console.WriteLine(error.ToString());
            }


            //InitGPSData();

            //Console.WriteLine("Press any key to continue ...");
            //Console.WriteLine();
            //Console.Read();
            //mySeriaPort.Close();
        }

        private void gpsDataRecivedHandle(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string lastdata = serialGpsPort.ReadLine();
                GPSData myGps = parseData(lastdata);
                // 数据无效
                if (myGps.getDtatime() == "ERROR")
                {
                    return;
                    //serialGpsPort.Close();
                    //lblNTime.Text = "GPS信号弱";
                    //lblVTime.Text = "GPS信号弱";
                    //lblNVTime.Text = "GPS信号弱";
                    //lblGPSLon.Text = "GPS信号弱";
                    //lblGPSLat.Text = "GPS信号弱";
                    //lblGPSSpeed.Text = "GPS信号弱";
                    //lblGPSCourse.Text = "GPS信号弱";
                    //MessageBox.Show("Error: GPS信号弱，获取数据无效！10S后刷新状态", "Error");
                    //InitGPSData();
                    //throw new NotImplementedException("GPS信号弱");
                }
                else
                {
                    RefreshUI(myGps);
                    Write2Sqlite(myGps);
                    //serialGpsPort.Close();
                }
            }
            catch
            {
                return;
            }
            
            //serialGpsPort.Close();
            
        }

        // 将GPS数据写入Sqlite数据库
        private void Write2Sqlite(GPSData myGps)
        {
            string date_time = myGps.getDtatime();
            double lon = myGps.getLon();
            double lat = myGps.getLat();
            float speed = myGps.getSpeed();
            float direct = myGps.getDirect();

            string path = getSqlConfig();
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + path +";Version=3;");
            if (m_dbConnection.State != System.Data.ConnectionState.Open)
            {
                m_dbConnection.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = m_dbConnection;
                cmd.CommandText = String.Format("INSERT INTO main.boat_info VALUES(\"{0}\", {1}, {2}, {3}, {4})", date_time, lon, lat, speed, direct);
                cmd.ExecuteNonQuery();
            }
            return;
        }

        // 用串口获得的GPS数据刷新界面
        private void RefreshUI(GPSData myGps)
        {
            pLonFrom = pLonTo;
            pLatFrom = pLatTo;
            pLonTo = Convert.ToDouble(myGps.getLon());
            pLatTo = Convert.ToDouble(myGps.getLat());

            lblNTime.Text = myGps.getTime();
            lblVTime.Text = myGps.getTime();
            lblNVTime.Text = myGps.getDtatime();
            lblGPSLon.Text = myGps.getLon().ToString();
            lblGPSLat.Text = myGps.getLat().ToString();
            lblGPSSpeed.Text = myGps.getSpeed().ToString();
            lblGPSCourse.Text = myGps.getDirect().ToString();


            if (pLonFrom >= -180 && pLatFrom >= -90)
            {
                Coordinate[] coords = new Coordinate[] { new Coordinate(pLonFrom, pLatFrom), new Coordinate(pLonTo, pLatTo) };
                var line = mapMain.Map.Factory.CreateLineString(coords);
                geoProvider.Geometries.Add(line);
                //this.mapMain.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
                mapMain.Refresh();
            }

            //throw new NotImplementedException();
        }



        private GPSData parseData(string lastdata)
        {
            String GPRMC = MidStrEx(lastdata, "$GPRMC,", "\r");
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

        //人工设置或 计算机自动计算设置参数
        private void SetPara()
        {
            lblNVTime.Text = DateTime.Now.ToString();

            lblNVDistance.Text = "200";
            lblNVAngle.Text = "5";
            lblNVDeep.Text = "150";
            lblNVSpeed.Text = "6";
        }
        private void InitChat()
        {
            chartMain.Series.Clear();

            // create a line chart series
            Series newSeries = new Series("网深");
            newSeries.ChartType = SeriesChartType.Point;
            newSeries.BorderWidth =8;
            newSeries.Color = Color.Red;

            newSeries.XValueType = ChartValueType.DateTime;
            chartMain.Series.Add(newSeries);

            Series seriesSonar = new Series("最佳深度");
            seriesSonar.ChartType = SeriesChartType.Point;
            seriesSonar.BorderWidth =2;
            seriesSonar.Color = Color.Green;
            seriesSonar.XValueType = ChartValueType.DateTime;
            chartMain.Series.Add(seriesSonar);

            chartMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(223)))), ((int)(((byte)(240)))));
            chartMain.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartMain.BackSecondaryColor = System.Drawing.Color.White;
            chartMain.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            chartMain.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartMain.BorderlineWidth = 2;
            chartMain.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
            chartMain.ChartAreas[0].Area3DStyle.Inclination = 15;
            chartMain.ChartAreas[0].Area3DStyle.IsClustered = true;
            chartMain.ChartAreas[0].Area3DStyle.IsRightAngleAxes = false;
            chartMain.ChartAreas[0].Area3DStyle.Perspective = 10;
            chartMain.ChartAreas[0].Area3DStyle.Rotation = 10;
            chartMain.ChartAreas[0].Area3DStyle.WallWidth = 0;
            chartMain.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);

            chartMain.ChartAreas[0].AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
;
            chartMain.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));


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
        private void InitMap()
        {
            mapMain.Map = ShapefileSample.InitializeMap(0);
            mapMain.Refresh();
            mapMain.ActiveTool = MapBox.Tools.Pan;

            SharpMap.Layers.VectorLayer vl = new VectorLayer("My Geometries");

            VectorStyle style = new VectorStyle();
            var cls = new SharpMap.Rendering.Symbolizer.CachedLineSymbolizer();
            //styling the road
            cls.LineSymbolizeHandlers.Add(new SharpMap.Rendering.Symbolizer.PlainLineSymbolizeHandler { Line = new System.Drawing.Pen(Color.Gold, 3) });
            //drawing the arrow
            var wls = new SharpMap.Rendering.Symbolizer.WarpedLineSymbolizeHander
            {
                Pattern = getArrowedLine(),
                Line = new System.Drawing.Pen(System.Drawing.Color.Gray, 2),
                Interval = 70
            };

            cls.LineSymbolizeHandlers.Add(wls);
            cls.ImmediateMode = true;
            style.LineSymbolizer = cls;

            vl.Style.LineSymbolizer = cls;
            geoProvider = new SharpMap.Data.Providers.GeometryProvider(new List<IGeometry>());
            vl.DataSource = geoProvider;
            mapMain.Map.Layers.Add(vl);
        }
        private GraphicsPath getArrowedLine()
        {
            //creating the Arrow graphics like this ->
            var gp = new GraphicsPath();
            gp.AddLine(5, 2, 7, 0); //     \
            gp.AddLine(0, 0, 7, 0); // -----
            gp.AddLine(5, -2, 7, 0);//     /
                                    //if you dont close the figure you will have continuesly arrows like this ->->->->->
                                    //but if you close it, depend on interval you will have ->   ->   ->
            gp.CloseFigure();
            return gp;
        }
        private void timerRealTime_Tick(object sender, System.EventArgs e)
        {
            if (!configFinished)
            {
                return;
            }
            // Define some variables
            int numberOfPointsInChart = 200;
            int numberOfPointsAfterRemoval = 150;

            // Simulate adding new data points
            int numberOfPointsAddedMin = 5;
            int numberOfPointsAddedMax = 10;
            for (int pointNumber = 0; pointNumber < random.Next(numberOfPointsAddedMin, numberOfPointsAddedMax); pointNumber++)
            {
                chartMain.Series[0].Points.AddXY(pointIndex + 1, random.Next(-5, 0));
                ++pointIndex;
                chartMain.Series[0].ChartType = SeriesChartType.Point;
            }

            // Adjust Y & X axis scale
            chartMain.ResetAutoValues();

            // Keep a constant number of points by removing them from the left
            while (chartMain.Series[0].Points.Count > numberOfPointsInChart)
            {
                // Remove data points on the left side
                while (chartMain.Series[0].Points.Count > numberOfPointsAfterRemoval)
                {
                    chartMain.Series[0].Points.RemoveAt(0);
                }
                chartMain.Series[0].ChartType = SeriesChartType.Point;
                // Adjust X axis scale
                chartMain.ChartAreas[0].AxisX.Minimum = pointIndex - numberOfPointsAfterRemoval;
                chartMain.ChartAreas[0].AxisX.Maximum = chartMain.ChartAreas[0].AxisX.Minimum + numberOfPointsInChart;
            }

            // Invalidate chart
            chartMain.Invalidate();
        }

        private void btnVesselInfo_Click(object sender, EventArgs e)
        {
            FormEditVessel vessel = new FormEditVessel();
            vessel.ShowDialog(this);

            RefreshVesselInfo();


        }
        public void RefreshVesselInfo()
        {
            string str = GetConfigValue("vessel").ToString();
            string[] parts = str.Split(new char[] { ',' });
            lblVName.Text = parts[0];
            lblVLength.Text = parts[1];
            lblVWidth.Text = parts[2];
            lblVPower.Text = parts[3];
            lblVTons.Text = parts[4];
  
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

        public static string GetConfigValue(string key)
        {
            string strValue;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            strValue = config.AppSettings.Settings[key].Value.ToString();
            return strValue;

        }

        private void button1_Click(object sender, EventArgs e)
        {
           // Form test = new Form1();
            //test.Controls.Add(new RealTimeSample());
            
             (new Form2()).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new FormChart()).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (new Form4()).Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            (new FormMap()).Show(); 
        }

        private void timerVesselGPS_Tick(object sender, EventArgs e)
        {
            if (!configFinished)
            {
                return;
            }
            Random ran = new Random();

            pLonFrom = pLonTo;
            pLatFrom = pLatTo;
            pLonTo = Convert.ToDouble(ran.Next(12000, 18000) / 100.0);
            pLatTo = Convert.ToDouble(ran.Next(-20 * 100, 30 * 100) / 100.0);


            //lblGPSLon.Text = (ran.Next(12000, 18000) / 100.0).ToString();
            //lblGPSLat.Text = (ran.Next(-20 * 100, 30 * 100) / 100.0).ToString();
            //lblGPSSpeed.Text = (ran.Next(0, 150) / 10.0).ToString();
            //lblGPSCourse.Text = (ran.Next(0, 360)).ToString();

            if (pLonFrom >= -180 && pLatFrom>=-90)
            { 
                Coordinate[] coords = new Coordinate[] { new Coordinate(pLonFrom, pLatFrom), new Coordinate(pLonTo, pLatTo) };
                var line = mapMain.Map.Factory.CreateLineString(coords);
                geoProvider.Geometries.Add(line);
                //this.mapMain.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
                mapMain.Refresh();
             }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormSetCom com = new FormSetCom();
            com.ShowDialog(this);
        }

        // 升降板返回 其深度、角度，升降板到渔船的距离，是COM 发的数据
        private void timerNet_Tick(object sender, EventArgs e)
        {
            if (!configFinished)
            {
                return;
            }


            Random ran = new Random();
            DateTime dtNetTime = DateTime.Now;
            float fNetDeep =Convert .ToSingle( ran.Next(-100 * 10,-50) / 10.0);
            //lblNTime.Text = dtNetTime.ToString("HH;mm:ss");
            float lift_angle = Convert.ToSingle(ran.Next(0 * 10, 90 * 10) / 10.0);
            //lblNAngle.Text = lift_angle.ToString();
            //lblNDeep.Text= fNetDeep.ToString();

            float distance_hor = Convert.ToSingle(ran.Next(100 * 10, 200 * 10) / 10.0);
            //lblNDistance.Text = distance_hor.ToString();

            String date_now = DateTime.Now.ToString("yyyy-MM-dd") + " ";
            string net_time = date_now + lblNTime.Text;

            net2SQLite(net_time, distance_hor, fNetDeep, lift_angle);

            chartMain.Series[0].Points.AddXY(dtNetTime, fNetDeep);
            try
            {
                chartMain.Series[0].MarkerImage = "test30.png";
            }catch (Exception eR)
            {
                MessageBox.Show(eR.ToString(), "ERROR");
            }
            
            double minValue = dtNetTime.AddSeconds(-5).ToOADate();
            double maxValue = dtNetTime.AddSeconds(45).ToOADate();
            chartMain.ChartAreas[0].AxisX.Minimum = minValue;
            chartMain.ChartAreas[0].AxisX.Maximum = maxValue;


            // 借用这个定时器完成“网船参数设定”的值
            lblNVDistance.Text = numericUpDown3.Value.ToString();
            lblNVAngle.Text = numericUpDown1.Value.ToString();
            lblNVDeep.Text = numericUpDown2.Value.ToString();
            lblNVSpeed.Text = numericUpDown4.Value.ToString();

        }

        private void net2SQLite(string net_time, float distance_hor, float fNetDeep, float lift_angle)
        {
            
            string path = getSqlConfig();
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + path + ";Version=3;");
            if (m_dbConnection.State != System.Data.ConnectionState.Open)
            {
                m_dbConnection.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = m_dbConnection;
                cmd.CommandText = String.Format("INSERT INTO main.net_info(time,distance_hori,net_depth,lift_angle) VALUES(\"{0}\", {1}, {2}, {3})", net_time, distance_hor, fNetDeep, lift_angle);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("数据库插入失败!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                m_dbConnection.Close();
            }
            else
            {
                MessageBox.Show("数据库打开失败", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private string getSqlConfig()
        {
            return GetConfigValue("SqliteDB");

            //return @"C:/projectFiles/CSharp/Sonarkrill/sonar.db";

            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //if (config.AppSettings.Settings["SqliteDB"] != null)
            //{
            //    return GetConfigValue("SqliteDB");
            //}
            //else
            //{
            //    string path = string.Empty;
            //    System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            //    if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        path = fbd.SelectedPath;
            //    }

            //    if (SetConfigValue("SqliteDB", path))
            //    {
            //        return path;
            //    }
            //    else
            //    {
            //        MessageBox.Show("数据库配置失败！", "ERROR");
            //        return null;
            //    }
            //}
        }

        /// <summary>
        /// 声呐返回 其深度、海底深度数据，渔船测速仪返回渔船相对 水面的速度，是两个COM 发的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerSonarSpeed_Tick(object sender, EventArgs e)
        {
            if (!configFinished)
            {
                return;
            }
            float fSonarDeep;
            Random ran = new Random();
            fSonarDeep = Convert .ToSingle ( ran.Next(-150 * 10, -50) / 10.0);
            float fSeconds = Convert .ToSingle ( 210 / 5.0);
            DateTime dtSonar = DateTime.Now.AddSeconds(fSeconds);

            //lblVTime.Text = DateTime.Now.ToString("HH;mm:ss");

            String my_depth = "";
            string path = getSqlConfig();
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
                    my_depth = "-" + t.ToString();
                }
                catch
                {
                    my_depth = "-2" ;
                }

            }
            m_dbConnection.Close();

            //my_depth = "-1";

            //lblVDeep.Text = fSonarDeep.ToString();
            lblVDeep.Text = my_depth;
            lblVFloor.Text = (Convert.ToSingle(my_depth) * 3).ToString();
            //lblVFloor.Text = (ran.Next(-500 * 10, -400 * 10) / 10.0).ToString();
            //lblVSpeed.Text = (ran.Next(0, 150) / 10.0).ToString();

            String date_now = DateTime.Now.ToString("yyyy-MM-dd") + " ";
            string speed_time = date_now + lblVTime.Text;
            float best_depth = fSonarDeep;
            float sea_depth = Convert.ToSingle(lblVFloor.Text);
            //float speed = Convert.ToSingle(lblVSpeed.Text);
            float speed = 0;

            speed2Sqlite(speed_time, best_depth, sea_depth, speed);

            DateTime sonar_time = getSonarTime();
            float sonar_deep = getSonarDeep();

            //chartMain.Series[1].Points.AddXY(dtSonar, fSonarDeep);
            chartMain.Series[1].Points.AddXY(dtSonar, sonar_deep * -1);

            //double minValue = dtNetTime.AddSeconds(-5).ToOADate();
            //double maxValue = dtNetTime.AddSeconds(20).ToOADate();
        }

        private float getSonarDeep()
        {
            string path = getSqlConfig();
            float t=-1;
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + path + ";Version=3;");
            if (m_dbConnection.State != System.Data.ConnectionState.Open)
            {
                m_dbConnection.Open();
                //String sql_message = "SELECT depth_cacl FROM main.estimate_depths ORDER BY time DESC LIMIT 1";
                String sql_message = "SELECT depth FROM main.paths ORDER BY time DESC LIMIT 1";
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = m_dbConnection;
                cmd.CommandText = sql_message;
                try {
                    SQLiteDataReader test = cmd.ExecuteReader();

                    while (test.Read())
                    {
                        //t = Convert.ToSingle(test["depth_cacl"].ToString());
                        t = Convert.ToSingle(test["depth"].ToString());
                    }
                    return t;
                }
                catch
                {
                    return -2;
                }
                
            }
            return (float)-1;

        }

        private DateTime getSonarTime()
        {
            string path = getSqlConfig();
            DateTime t = DateTime.Now;
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + path + ";Version=3;");
            if (m_dbConnection.State != System.Data.ConnectionState.Open)
            {
                DateTime dt = DateTime.Now;
                m_dbConnection.Open();
                String sql_message = "SELECT time FROM main.estimate_depths ORDER BY time DESC LIMIT 1";
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = m_dbConnection;
                cmd.CommandText = sql_message;
                SQLiteDataReader test = cmd.ExecuteReader();

                while (test.Read())
                {
                   String str_time = test["time"].ToString();
                   dt = Convert.ToDateTime(str_time.Split('.')[0]);
                }
                m_dbConnection.Close();
                return dt;
            }
            m_dbConnection.Close();
            return DateTime.Now;
        }

        private void speed2Sqlite(string speed_time, float best_depth, float sea_depth, float speed)
        {
            string path = getSqlConfig();
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + path + ";Version=3;");
            if (m_dbConnection.State != System.Data.ConnectionState.Open)
            {
                m_dbConnection.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = m_dbConnection;
                cmd.CommandText = String.Format("INSERT INTO main.speed_info VALUES(\"{0}\", {1}, {2}, {3})", speed_time, best_depth, sea_depth, speed);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("数据库插入失败!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("数据库打开失败", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

            Control.CheckForIllegalCrossThreadCalls = false;
            serialNetPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(netDataReceived);
            //serialNetPort.Open(); //打开串口
            if (!serialNetPort.IsOpen)
            {
                try
                {
                    serialNetPort.Open(); //打开串口
                }
                catch (Exception ex)
                {
                    MessageBox.Show("串行端口打开失败！具体原因：" + ex.Message, "提示信息");
                }
            }
            serialGpsPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(gpsDataRecivedHandle);
            //serialNetPort.Open(); //打开串口
            if (!serialGpsPort.IsOpen)
            {
                try
                {
                    serialGpsPort.Open(); //打开串口
                }
                catch (Exception ex)
                {
                    MessageBox.Show("串行端口打开失败！具体原因：" + ex.Message, "提示信息");
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

        private void netDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //sendAbledFlag = false;
            System.Threading.Thread.Sleep(20); // 这个延时是为了保证每次接收到的缓冲区的数据是完整的，避免出现高频错包
            
            if (sendingFlagNow){
                //已经点击了发送按钮
                sendAbledFlag = false;
                WriteLog("收到数据，且已经点击了发送按钮，2t+1后再允许发送");
                // 在新线程中做sendable全局变量的等待
                Thread thread_wait = new Thread(() => WaitForSendable());
                thread_wait.Start();
            }
            
            

            if (!serialNetPort.IsOpen)
            {
                try
                {
                    serialNetPort.Open();
                }
                catch(Exception ex)
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
                WriteLog("接收： " + strbuilder.ToString());
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
                        WriteLog("校验码检测正确");

                        angleValue = (Convert.ToInt32(angle_code) - 35).ToString();


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

                        WriteLog(angleValue + "°    " + depthValue + "m    "
                            + v_status.ToString() + "  " + lihe_status.ToString() + "  "
                            + code_status.ToString() + "  " + future_status.ToString() + "  "
                            + v_value.ToString());

                        // 更新UI
                        lblNAngle.Text = angleValue;
                        lblNDeep.Text = depthValue;

                        if (v_status == 0)
                        {
                            label37.Text = "正常";
                            label37.ForeColor = Color.Green;
                        }
                        else
                        {
                            label37.Text = "异常";
                            label37.ForeColor = Color.Red;
                        }

                        if (lihe_status == 0)
                        {
                            label38.Text = "正常";
                            label38.ForeColor = Color.Green;
                        }
                        else
                        {
                            label38.Text = "异常";
                            label38.ForeColor = Color.Red;
                        }

                        if (code_status == 0)
                        {
                            label39.Text = "正常";
                            label39.ForeColor = Color.Green;
                        }
                        else
                        {
                            label39.Text = "异常";
                            label39.ForeColor = Color.Red;
                        }

                        if (future_status == 0)
                        {
                            label40.Text = "正常";
                            label40.ForeColor = Color.Green;
                        }
                        else
                        {
                            label40.Text = "异常";
                            label40.ForeColor = Color.Red;
                        }

                        label42.Text = v_value_str;
                        label42.ForeColor = Color.Green;


                    }
                    else
                    {
                        WriteLog("校验码检测错误");
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
                        WriteLog("数据校验失败");
                        MessageBox.Show("数据校验失败！");
                        return;

                    }

                    WriteLog("收到数据时检查sending状态: " + sendingDataFlag.ToString());
                    // 有发送的数据待确认
                    if (sendingDataFlag)
                    {
                        rightFlag = false;
                        // 如果是发回的反馈，只改变相关信号量
                        if (strbuilder.ToString().Trim().Equals(sendedContent))
                        {
                            rightFlag = true;
                            sendingDataFlag = false;

                            WriteLog(strbuilder.ToString().Trim());
                            sendedContent = "";
                            strbuilder.Length = 0; //清空strbuilder
                        }
                        else
                        {
                            rightFlag = false;
                            sendingDataFlag = false;

                            sendedContent = "";
                            strbuilder.Length = 0; //清空strbuilder
                        }

                    }
                    else
                    {

                        rightFlag = false; // 规避掉前面验证成功结果的干扰
                        // 正常收到的22数据头 是发回的距离数据
                        if (data_header_code == 0x22)
                        {
                            string dis_str = (angle + depth).Trim();
                            int dis = int.Parse(dis_str, System.Globalization.NumberStyles.HexNumber);

                            // 换算深度（未完成）
                            // 更新UI
                            lblNDistance.Text = dis.ToString();
                            WriteLog(dis.ToString() + "m");
                        }
                    }
                }
                strbuilder.Length = 0; //清空strbuilder
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                //textBoxReceive.Text = "";//清空

            }
            
            System.Threading.Thread.Sleep(waitingMilliSecond);
            try
            {
                
                serialNetPort.DiscardInBuffer();
                
            }
            catch
            {

            }
        }

        private void WaitForSendable()
        {
            //Thread.Sleep(sendingTime * 2 + 1000);
            Thread.Sleep(sendingTime * 2);
            sendAbledFlag = true;
            WriteLog("开始允许发送...");
            System.Threading.Thread.CurrentThread.Abort();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            sendingDataFlag = true;
            sendingFlagNow = true;

            int angleSet = (int)numericUpDown1.Value;
            float depthSet = (float)numericUpDown2.Value;
            byte angleSetCode;
            byte depthSetCode;
            byte headers = 0x24;

            angleSetCode = BitConverter.GetBytes(Convert.ToInt32(angleSet + 35))[0];

            if (angleSetCode == 0xFF) // 发送的是调整时间间隔指令
            {
                if (depthSet % 0.5 != 0)
                {
                    //sendingDataFlag = false;
                    MessageBox.Show("时间间隔设置错误，应为0.5的整数倍！");
                    return;
                }
                int timeIntervbal = (int)(depthSet * 2);
                depthSetCode = BitConverter.GetBytes(timeIntervbal)[0];
            }
            else
            {
                depthSet = (int)depthSet;

                depthSetCode = BitConverter.GetBytes(Convert.ToInt32(depthSet))[0];
            }
                
            //int check = angleSet ^ depthSet;
            // 校验码
            int check = angleSetCode ^ depthSetCode;
            byte checkCode = BitConverter.GetBytes(check)[0];

            //SerialPort sendPort = new SerialPort("COM5");

            if (serialNetPort.IsOpen)
            {
                if (!sendAbledFlag) {
                    MessageBox.Show("当前程序正处于禁止发送状态，程序即将假死，直至收到数据后允许发送。");
                }
                while (!sendAbledFlag)
                {

                    buttonSendCom.Text = "等待信道空闲...";
                    WriteLog("暂时不允许发送...");
                    Thread.Sleep(500);

                }

            }
            else
            {
                MessageBox.Show("端口未打开，发送失败" , "错误！");
                return;
            }


            byte[] message = { headers, angleSetCode, depthSetCode, checkCode};
            if (!serialNetPort.IsOpen)
                try
                {
                    serialNetPort.Open();
                }catch (Exception error)
                {
                    MessageBox.Show("打开指定串口时出现错误：" + error.ToString(),"错误！");
                    serialNetPort.Close();
                    return;
                }

            
            serialNetPort.Write(message, 0, message.Length);
            WriteLog("需要发送值： " + angleSet.ToString() + "°   " + depthSet.ToString() + "m");
            //string[] str = BitConverter.ToString(message).Split('-');
            string message_str = "";
            foreach(byte x in message)
            {
                //message_str = message_str + Convert.ToString(Convert.ToInt32(x), 16).ToUpper() + " ";
                message_str = message_str + Convert.ToInt32(x).ToString("X2").ToUpper() + " ";
            }
            WriteLog("发送：   " + message_str);
            MessageBox.Show("发送成功！", "提示");
            sendingFlagNow = false;

            sendAbledFlag = false;
            WriteLog("本次发送完成，开始禁止发送，直到再次收到报文...");
            //sendPort.Write("\r\n");
            //serialNetPort.Close();
            buttonSendCom.Text = "发送";

            // 将发送的信息存储到sendedContent
            sendedContent = message_str.Trim();
            sendingDataFlag = true;

            Thread thread1 = new Thread(() => WaitForBack(sendedContent));
            thread1.Start();

        }

        private void WaitForBack(string sendedContent)
        {
            bool result = false;
            WriteLog("开始等待传回的确认消息...");
            int startTime = System.Environment.TickCount;
            while (System.Environment.TickCount - startTime < (8000))
            {
                
                WriteLog("当前等待时间:   " + (System.Environment.TickCount - startTime).ToString() + "       " + "最长等待时间:    " + (sendingTime * 2 + 1000).ToString());
                WriteLog("sendingDataFlag:    " + sendingDataFlag.ToString());
                WriteLog("rightFlag:    " + rightFlag.ToString());
                if (rightFlag)
                {
                    sendedContent = "";
                    result = true;

                    WriteLog("数据验证成功！本次验证共消耗：" + (System.Environment.TickCount - startTime).ToString() + "ms");
                    MessageBox.Show(sendedContent + "   下位机已返回确认！");

                    sendingDataFlag = false; //发送完毕
                    rightFlag = false;
                    break;
                    System.Threading.Thread.CurrentThread.Abort();
                    return;
                }
                else
                {
                    WriteLog("未确认， 继续等待...");
                    Thread.Sleep(500);
                }
            }
            if (!result) {
                sendedContent = "";

                sendingDataFlag = false; //发送完毕
                rightFlag = false;
                WriteLog("未接收到反馈或反馈错误，请手动重发...");
                MessageBox.Show("未接收到反馈或反馈错误，请手动重发...");
                System.Threading.Thread.CurrentThread.Abort();
            }
           


            //System.Threading.Thread.Sleep(sendingTime * 2 + 1); //  重发等待：2t + 1 
            //// 有数据待确认 且收到与发出信号不一致
            //if(sendingDataFlag && !rightFlag)
            //{
            //    MessageBox.Show("未接收到反馈或反馈错误，请手动重发...");
            //    // 再发一次
            //    //    if (sendedContent.StartsWith("22"))
            //    //    {
            //    //        btnAskDis_Click(null, null);
            //    //        return;
            //    //    }
            //    //    else if(sendedContent.StartsWith("24"))
            //    //    {
            //    //        button1_Click_1(null, null);
            //    //        return;
            //    //    }
            //    //    else
            //    //    {
            //    //        MessageBox.Show("发送的数据有误！确认后重发！", "ERROR");
            //    //        return;
            //    //    }

            //    //    WriteLog("未接收到反馈，开始重发：  " + sendedContent);
            //}
            //else if(sendingDataFlag && rightFlag)
            //{
            //    sendingDataFlag = false; //发送完毕
            //    MessageBox.Show(sendedContent + "   下位机已返回确认！");
            //}
            ////结束当前线程
            //System.Threading.Thread.CurrentThread.Abort();
        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void buttonChangeCOM_Click(object sender, EventArgs e)
        {
            serialGpsPort.Close();
            serialNetPort.Close();
            FormSetCom com = new FormSetCom();
            com.ShowDialog(this);
        }

        private void buttonRefreshComStatus_Click(object sender, EventArgs e)
        {
            CheckCOMStatus();
            MessageBox.Show("串口状态刷新成功！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAskDis_Click(object sender, EventArgs e)
        {
            //sendingDataFlag = true;
            byte[] message = { 0x22, 0x01, 0x02, 0x03 };
            string message_str = "22 01 02 03";
            // 
            

            if (!serialNetPort.IsOpen)
                try
                {
                    serialNetPort.Open();
                }
                catch (Exception error)
                {
                    MessageBox.Show("打开指定串口时出现错误：" + error.ToString(), "错误！");
                    //askPort.Close();
                    return;
                }
            else
            {
                serialNetPort.Close();
                serialNetPort.Open();
            }
            serialNetPort.Write(message, 0, message.Length);
            //sendPort.Write("\r\n");
            //serialNetPort.Close();
            sendedContent = message_str.Trim();
            MessageBox.Show("查询距离请求发送成功！" + message_str + serialNetPort.PortName);
            WriteLog("发送查询距离请求...");
            WriteLog("发送：" + message_str);


            /* 查询距离的指令，不会原样返回，不等反馈 */
            //sendingDataFlag = true;
            //Thread thread1 = new Thread(() => WaitForBack(sendedContent));
            //thread1.Start();
            //thread1.Abort();
        }

        private void WriteLog(string strLog)
        {
            string strDate = DateTime.Now.ToString("yyyyMMdd");
            string strLogDir = AppDomain.CurrentDomain.BaseDirectory + "Log\\SysLog\\";
            if (!Directory.Exists(strLogDir))
                Directory.CreateDirectory(strLogDir);

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strLog = string.Format("[{0}] {1}\r\n", strDateTime, strLog);

            string strLogFilePath = string.Format("{0}/{1}.txt", strLogDir, strDate);
            try { 
                File.AppendAllText(strLogFilePath, strLog);
            }
            catch
            {
                ;
            }
            
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            sendingTime = Convert.ToInt32(textBox1.Text);
            MessageBox.Show("修改成功！");
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("此操作将退出所有线程");
            System.Environment.Exit(0);
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            serialGpsPort.Close();
            serialNetPort.Close();
            serialSpeedPort.Close();

            Form1 anotherForm;
            anotherForm = new Form1();
            this.Hide();
            //this.Close();
            anotherForm.ShowDialog();
            Application.ExitThread();
        }

        private void chartMain_Click(object sender, EventArgs e)
        {

        }

        private void lblVSpeed_Click(object sender, EventArgs e)
        {

        }

        private void lblNVTime_Click(object sender, EventArgs e)
        {

        }
    }
}
