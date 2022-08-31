using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SonarKrill
{
    public partial class FormSetCom : Form
    {
        //实例化串口对象   
        SerialPort serialPort = new SerialPort();

        String saveDataFile = null;
        FileStream saveDataFS = null;

        public FormSetCom()
        {
            InitializeComponent();
            //GetGPSData();
        }

        //初始化串口界面参数设置
        private void Init_Port_Confs()
        {
            /*------串口界面参数设置------*/

            //检查是否含有串口
            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                MessageBox.Show("本机没有串口！", "Error");
                this.Close();
            }
            //添加串口
            foreach (string s in str)
            {
                comboBoxCom.Items.Add(s);
            }
            //设置默认串口选项
            comboBoxCom.SelectedIndex = 0;

            /*------功能类型设置-------*/
            string[] functionType = { "GPS", "Speed", "Net", "数据发送" };
            foreach (string s in functionType)
            {
                comboBoxType.Items.Add(s);
            }
            comboBoxType.SelectedIndex = 0;

            /*------波特率设置-------*/
            string[] baudRate = { "9600", "19200", "38400", "57600", "115200" };
            foreach (string s in baudRate)
            {
                comboBoxBaudRate.Items.Add(s);
            }
            comboBoxBaudRate.SelectedIndex = 0;

            /*------数据位设置-------*/
            string[] dataBit = { "5", "6", "7", "8" };
            foreach (string s in dataBit)
            {
                comboBoxDataBit.Items.Add(s);
            }
            comboBoxDataBit.SelectedIndex = 3;


            /*------校验位设置-------*/
            string[] checkBit = { "None", "Even", "Odd", "Mask", "Space" };
            foreach (string s in checkBit)
            {
                comboBoxCheckBit.Items.Add(s);
            }
            comboBoxCheckBit.SelectedIndex = 0;


            /*------停止位设置-------*/
            string[] stopBit = { "1", "1.5", "2" };
            foreach (string s in stopBit)
            {
                comboBoxStopBit.Items.Add(s);
            }
            comboBoxStopBit.SelectedIndex = 0;

            /*------数据格式设置-------*/
            radioButtonSendDataASCII.Checked = true;
            radioButtonReceiveDataASCII.Checked = true;

            /*------加载时根据配置文件选择对应item----*/
            string gps_config = getComConfig("GPS");
            string[] config_items = gps_config.Split(',');

            int index_gps_com = comboBoxCom.FindString(config_items[0]);
            comboBoxCom.SelectedIndex = index_gps_com;

            int index_gps_baund = comboBoxBaudRate.FindString(config_items[1]);
            comboBoxBaudRate.SelectedIndex = index_gps_baund;

            int index_gps_data = comboBoxDataBit.FindString(config_items[2]);
            comboBoxDataBit.SelectedIndex = index_gps_data;

            int index_gps_check = comboBoxCheckBit.FindString(config_items[3]);
            comboBoxCheckBit.SelectedIndex = index_gps_check;

            int index_gps_stop = comboBoxStopBit.FindString(config_items[4]);
            comboBoxStopBit.SelectedIndex = index_gps_stop;

        }

        private string getComConfig(string key)
        {
            string s = ConfigurationManager.AppSettings[key];
            return s;
        }

        public string MidStrEx(string sourse, string startstr, string endstr)
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

        // 测试解析串口数据
        public string GetGPSData()
        {
            serialPort.PortName = "COM4";
            serialPort.Open();

            String input = serialPort.ReadLine();
            
            String GPRMC = MidStrEx(input, "$GPRMC,", "*");
            String[] paramss = GPRMC.Split(',');
            String status;
            try
            {
                status = paramss[1];
            }
            catch {
                status = "";
                serialPort.Close();
                GetGPSData();

            }
            
            if( status == "A") // 定位状态有效
            {
                String time = paramss[0].Substring(0, 2) + ":" + paramss[0].Substring(2, 2) + ":" + paramss[0].Substring(4, 2);
                
                double lat = Convert.ToDouble(paramss[2].Substring(0, 2)) + Convert.ToDouble(paramss[2].Substring(2, 7)) / 60;

                double lon = Convert.ToDouble(paramss[4].Substring(0, 2)) + Convert.ToDouble(paramss[4].Substring(2, 7)) / 60;
                float speed = Convert.ToSingle(paramss[6]); //地面速率
                float direct = Convert.ToSingle(paramss[7]); // 地面航向
                String date = "20" + paramss[8].Substring(4, 2) + "-" + paramss[8].Substring(2, 2) + "-" + paramss[8].Substring(0, 2);
                String date_time = date + " " + time;
                ;
            }
 

            return input;
            //return "123";
        }

        //加载主窗体
        private void MainForm_Load(object sender, EventArgs e)
        {

            Init_Port_Confs();

            Control.CheckForIllegalCrossThreadCalls = false;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);


            //准备就绪              
            serialPort.DtrEnable = true;
            serialPort.RtsEnable = true;
            //设置数据读取超时为1秒
            serialPort.ReadTimeout = 1;

            serialPort.Close();

            buttonSendData.Enabled = false;

        }



        //接收数据
        private void dataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            System.Threading.Thread.Sleep(20); //毫秒
            //MessageBox.Show("name:" + serialPort.PortName, "OK");
            if (serialPort.IsOpen)
            {
                //输出当前时间
                DateTime dateTimeNow = DateTime.Now;
                //dateTimeNow.GetDateTimeFormats();
                textBoxReceive.Text += string.Format("\r\n{0}   ", dateTimeNow);
                //dateTimeNow.GetDateTimeFormats('f')[0].ToString() + "\r\n";
                textBoxReceive.ForeColor = Color.Red;    //改变字体的颜色

                if (radioButtonReceiveDataASCII.Checked == true) //接收格式为
                                                                 //
                {
                    try
                    {
                        String input = serialPort.ReadLine();
                        textBoxReceive.Text += input + "\r\n";
                        // save data to file
                        if (saveDataFS != null)
                        {
                            byte[] info = new UTF8Encoding(true).GetBytes(input + "\r\n");
                            saveDataFS.Write(info, 0, info.Length);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message, "错误");
                        return;
                    }

                    textBoxReceive.SelectionStart = textBoxReceive.Text.Length;
                    textBoxReceive.ScrollToCaret();//滚动到光标处
                    try
                    {
                        serialPort.DiscardInBuffer(); //清空SerialPort控件的Buffer 

                    }
                    catch
                    {
                        MessageBox.Show("清空SerialPort控件的Buffer时出现错误！", "Error");
                        serialPort.Close();
                    }
                }
                else //接收格式为HEX
                {
                    try
                    {
                        StringBuilder strbuilder = new StringBuilder();
                        byte[] buf = new byte[10];//声明一个临时数组存储当前来的串口数据  (byte型 数据)
                        int n = serialPort.Read(buf, 0, 10);//读取缓冲数据  
                        Array.Resize(ref buf, n);
                        foreach (byte b in buf)
                        {
                            strbuilder.Append(b.ToString("X2") + " ");//一个字节一个字节的处理
                            //string hexOutput = String.Format("{0:X}", b);
                            //textBoxReceive.AppendText(hexOutput + " ");
                        }
                        textBoxReceive.SelectionStart = textBoxReceive.Text.Length;
                        textBoxReceive.ScrollToCaret();//滚动到光标处
                        textBoxReceive.Text += (strbuilder.ToString() + "\n");
                        textBoxReceive.SelectionStart = textBoxReceive.Text.Length;
                        textBoxReceive.ScrollToCaret();//滚动到光标处

                        //MessageBox.Show(n.ToString() + ": " + strbuilder.ToString());


                        ////MessageBox.Show("HEX");
                        //string input = serialPort.ReadLine();
                        //int input1 = serialPort.ReadChar();
                        //MessageBox.Show(input1.ToString());
                        //char[] values = input.ToCharArray();
                        //foreach (char letter in values)
                        //{
                        //    // Get the integral value of the character.
                        //    int value  = Convert.ToInt32(letter);
                        //    // Convert the decimal value to a hexadecimal value in string form.
                        //    string hexOutput = String.Format("{0:X}", value);
                        //    textBoxReceive.AppendText(hexOutput + " ");
                        //    textBoxReceive.SelectionStart = textBoxReceive.Text.Length;
                        //    textBoxReceive.ScrollToCaret();//滚动到光标处
                        //    textBoxReceive.Text += hexOutput + " ";

                        //}

                        //// save data to file
                        //if (saveDataFS != null)
                        //{
                        //    byte[] info = new UTF8Encoding(true).GetBytes(input + "\r\n");
                        //    saveDataFS.Write(info, 0, info.Length);
                        //}


                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                        textBoxReceive.Text = "";//清空
                    }
                }
            }
            else
            {
                MessageBox.Show("请打开某个串口", "错误提示");
            }
        }

        //发送数据
        private void buttonSendData_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                MessageBox.Show("请先打开串口", "Error");
                return;
            }

            String strSend = textBoxSend.Text;//发送框数据
            if (radioButtonSendDataASCII.Checked == true)//以字符串 ASCII 发送
            {
                serialPort.WriteLine(strSend);//发送一行数据 

            }
            else
            {
                //16进制数据格式 HXE 发送

                char[] values = strSend.ToCharArray();
                foreach (char letter in values)
                {
                    // Get the integral value of the character.
                    int value = Convert.ToInt32(letter);
                    // Convert the decimal value to a hexadecimal value in string form.
                    string hexIutput = String.Format("{0:X}", value);
                    serialPort.WriteLine(hexIutput);

                }

            }

        }

        //清空接收数据框
        private void buttonClearRecData_Click(object sender, EventArgs e)
        {

            textBoxReceive.Text = "";

        }


        //窗体关闭时
        private void MainForm_Closing(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();//关闭串口
            }

            if (saveDataFS != null)
            {
                saveDataFS.Close(); // 关闭文件
                saveDataFS = null;//释放文件句柄
            }

        }



        // 退出
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();//关闭串口
            }
            if (saveDataFS != null)
            {
                saveDataFS.Close(); // 关闭文件
                saveDataFS = null;//释放文件句柄
            }

            this.Close();
        }

        // 重置串口参数设置
        private void ResetPortConfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBoxCom.SelectedIndex = 0;
            comboBoxBaudRate.SelectedIndex = 0;
            comboBoxDataBit.SelectedIndex = 3;
            comboBoxCheckBit.SelectedIndex = 0;
            comboBoxStopBit.SelectedIndex = 0;
            radioButtonSendDataASCII.Checked = true;
            radioButtonReceiveDataASCII.Checked = true;

        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Txt |*.txt";
            saveFileDialog.Title = "保存接收到的数据到文件中";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != null)
            {
                saveDataFile = saveFileDialog.FileName;
            }
        }

        private void FormSetCom_Load(object sender, EventArgs e)
        {
            Init_Port_Confs();

            Control.CheckForIllegalCrossThreadCalls = false;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);


            //准备就绪              
            serialPort.DtrEnable = true;
            serialPort.RtsEnable = true;
            //设置数据读取超时为1秒
            serialPort.ReadTimeout = 1000;

            serialPort.Close();

            buttonSendData.Enabled = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            serialPort.Close();
            comboBoxCom.Text = "";
            comboBoxCom.Items.Clear();

            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                MessageBox.Show("本机没有串口！", "Error");
                return;
            }

            //添加串口
            foreach (string s in str)
            {
                comboBoxCom.Items.Add(s);
            }

            //设置默认串口
            comboBoxCom.SelectedIndex = 0;
            comboBoxBaudRate.SelectedIndex = 0;
            comboBoxDataBit.SelectedIndex = 3;
            comboBoxCheckBit.SelectedIndex = 0;
            comboBoxStopBit.SelectedIndex = 0;

        }

        //打开串口 关闭串口
        private void btnOpenCloseCom_Click(object sender, EventArgs e)
        {
            //serialPort.Close();
            if (!serialPort.IsOpen)//串口处于关闭状态
            {
                try
                {

                    if (comboBoxCom.SelectedIndex == -1)
                    {
                        MessageBox.Show("Error: 无效的端口,请重新选择", "Error");
                        return;
                    }
                    string strSerialName = comboBoxCom.SelectedItem.ToString();
                    string strBaudRate = comboBoxBaudRate.SelectedItem.ToString();
                    string strDataBit = comboBoxDataBit.SelectedItem.ToString();
                    string strCheckBit = comboBoxCheckBit.SelectedItem.ToString();
                    string strStopBit = comboBoxStopBit.SelectedItem.ToString();

                    Int32 iBaudRate = Convert.ToInt32(strBaudRate);
                    Int32 iDataBit = Convert.ToInt32(strDataBit);

                    serialPort.PortName = strSerialName;//串口号
                    serialPort.BaudRate = iBaudRate;//波特率
                    serialPort.DataBits = iDataBit;//数据位



                    switch (strStopBit)            //停止位
                    {
                        case "1":
                            serialPort.StopBits = StopBits.One;
                            break;
                        case "1.5":
                            serialPort.StopBits = StopBits.OnePointFive;
                            break;
                        case "2":
                            serialPort.StopBits = StopBits.Two;
                            break;
                        default:
                            MessageBox.Show("Error：停止位参数不正确!", "Error");
                            break;
                    }
                    switch (strCheckBit)             //校验位
                    {
                        case "None":
                            serialPort.Parity = Parity.None;
                            break;
                        case "Odd":
                            serialPort.Parity = Parity.Odd;
                            break;
                        case "Even":
                            serialPort.Parity = Parity.Even;
                            break;
                        default:
                            MessageBox.Show("Error：校验位参数不正确!", "Error");
                            break;
                    }



                    if (saveDataFile != null)
                    {
                        saveDataFS = File.Create(saveDataFile);
                    }

                    //打开串口
                    serialPort.Open();

                    //打开串口后设置将不再有效
                    comboBoxCom.Enabled = false;
                    comboBoxBaudRate.Enabled = false;
                    comboBoxDataBit.Enabled = false;
                    comboBoxCheckBit.Enabled = false;
                    comboBoxStopBit.Enabled = false;
                    radioButtonSendDataASCII.Enabled = false;
                    radioButtonSendDataHex.Enabled = false;
                    radioButtonReceiveDataASCII.Enabled = false;
                    radioButtonReceiveDataHEX.Enabled = false;
                    buttonSendData.Enabled = true;
                    btnRefresh.Enabled = false;

                    btnOpenCloseCom.Text = "关闭串口";

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Error");
                    return;
                }
            }
            else //串口处于打开状态
            {
                // MessageBox.Show("enter");
                serialPort.Close();//关闭串口
                
                //串口关闭时设置有效
                comboBoxCom.Enabled = true;
                comboBoxBaudRate.Enabled = true;
                comboBoxDataBit.Enabled = true;
                comboBoxCheckBit.Enabled = true;
                comboBoxStopBit.Enabled = true;
                radioButtonSendDataASCII.Enabled = true;
                radioButtonSendDataHex.Enabled = true;
                radioButtonReceiveDataASCII.Enabled = true;
                radioButtonReceiveDataHEX.Enabled = true;
                buttonSendData.Enabled = false;
                btnRefresh.Enabled = true;

                btnOpenCloseCom.Text = "打开串口";
                MessageBox.Show("关闭端口成功");

                if (saveDataFS != null)
                {
                    saveDataFS.Close(); // 关闭文件
                    saveDataFS = null;//释放文件句柄
                }

            }
        }

        private void btnSetCom_Click(object sender, EventArgs e)
        {
            string strComInfo =comboBoxCom.Text + "," +comboBoxBaudRate.Text + "," +comboBoxDataBit.Text + ","
                +comboBoxCheckBit.Text + "," +comboBoxStopBit.Text;
                SetConfigValue(comboBoxType.Text, strComInfo);
            MessageBox.Show("串口配置信息保存成功！\r\n "+comboBoxType.Text +"的配置信息保存为：" + strComInfo , "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                return false;
            }
        }

        /*--关联选项，选择类型后自动选择配置文件中的数据--*/
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index_now = comboBoxType.FindStringExact(comboBoxType.Text);
            switch (index_now)
            {
                case 0:
                    string gps_config = getComConfig("GPS");
                    string[] config_items = gps_config.Split(',');

                    int index_gps_com = comboBoxCom.FindString(config_items[0]);
                    comboBoxCom.SelectedIndex = index_gps_com;

                    int index_gps_baund = comboBoxBaudRate.FindString(config_items[1]);
                    comboBoxBaudRate.SelectedIndex = index_gps_baund;

                    int index_gps_data = comboBoxDataBit.FindString(config_items[2]);
                    comboBoxDataBit.SelectedIndex = index_gps_data;

                    int index_gps_check = comboBoxCheckBit.FindString(config_items[3]);
                    comboBoxCheckBit.SelectedIndex = index_gps_check;

                    int index_gps_stop = comboBoxStopBit.FindString(config_items[4]);
                    comboBoxStopBit.SelectedIndex = index_gps_stop;
                    break;
                case 1:
                    string speed_config = getComConfig("Speed");
                    string[] speed_config_items = speed_config.Split(',');

                    int index_speed_com = comboBoxCom.FindString(speed_config_items[0]);
                    comboBoxCom.SelectedIndex = index_speed_com;

                    int index_speed_baund = comboBoxBaudRate.FindString(speed_config_items[1]);
                    comboBoxBaudRate.SelectedIndex = index_speed_baund;

                    int index_speed_data = comboBoxDataBit.FindString(speed_config_items[2]);
                    comboBoxDataBit.SelectedIndex = index_speed_data;

                    int index_speed_check = comboBoxCheckBit.FindString(speed_config_items[3]);
                    comboBoxCheckBit.SelectedIndex = index_speed_check;

                    int index_speed_stop = comboBoxStopBit.FindString(speed_config_items[4]);
                    comboBoxStopBit.SelectedIndex = index_speed_stop;
                    break;
                case 2:
                    string net_config = getComConfig("Net");
                    string[] net_config_items = net_config.Split(',');

                    int index_net_com = comboBoxCom.FindString(net_config_items[0]);
                    comboBoxCom.SelectedIndex = index_net_com;

                    int index_net_baund = comboBoxBaudRate.FindString(net_config_items[1]);
                    comboBoxBaudRate.SelectedIndex = index_net_baund;

                    int index_net_data = comboBoxDataBit.FindString(net_config_items[2]);
                    comboBoxDataBit.SelectedIndex = index_net_data;

                    int index_net_check = comboBoxCheckBit.FindString(net_config_items[3]);
                    comboBoxCheckBit.SelectedIndex = index_net_check;

                    int index_net_stop = comboBoxStopBit.FindString(net_config_items[4]);
                    comboBoxStopBit.SelectedIndex = index_net_stop;
                    break;
                case 3:
                    string send_config = getComConfig("Send");
                    string[] send_config_items = send_config.Split(',');

                    int index_send_com = comboBoxCom.FindString(send_config_items[0]);
                    comboBoxCom.SelectedIndex = index_send_com;

                    int index_send_baund = comboBoxBaudRate.FindString(send_config_items[1]);
                    comboBoxBaudRate.SelectedIndex = index_send_baund;

                    int index_send_data = comboBoxDataBit.FindString(send_config_items[2]);
                    comboBoxDataBit.SelectedIndex = index_send_data;

                    int index_send_check = comboBoxCheckBit.FindString(send_config_items[3]);
                    comboBoxCheckBit.SelectedIndex = index_send_check;

                    int index_send_stop = comboBoxStopBit.FindString(send_config_items[4]);
                    comboBoxStopBit.SelectedIndex = index_send_stop;
                    break;

            }
        }
        public static void WriteLog(string strLog)
        {
            string strDate = DateTime.Now.ToString("yyyyMMdd");
            string strLogDir = AppDomain.CurrentDomain.BaseDirectory + "Log\\SysLog\\";
            if (!Directory.Exists(strLogDir))
                Directory.CreateDirectory(strLogDir);

            string strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strLog = string.Format("[{0}] {1}\r\n", strDateTime, strLog);

            string strLogFilePath = string.Format("{0}/{1}.txt", strLogDir, strDate);
            File.AppendAllText(strLogFilePath, strLog);
        }
    }
}
