using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SonarKrill
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            MyChart1();

        }
        private void MyChart1()
        {
            //清空原来数据缓存
            chart1.Series[0].Points.Clear();

            //定义图表大小尺寸
            chart1.Width = Width - 100;
            chart1.Height = Height - 100;

            //定义X轴、Y轴数据
            double[] Ydata = { 20, 3, 23, 6 };
            DateTime[] Xdate = new DateTime[] { DateTime.Parse("09:10:02"), DateTime.Parse("09:10:10"), DateTime.Parse("09:10:15"), DateTime.Parse("09:10:20") };

            //以下按照先绘制chartArea、然后再绘制Series的步骤画图
            //chartArea背景颜色
            chart1.BackColor = Color.Azure;

            //X轴设置
            chart1.ChartAreas[0].AxisX.Title = "时间";
            chart1.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Near;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;//不显示竖着的分割线

            /************************************************************************/
            /* 本文重点讲解时间格式的设置
             * 如果想显示原点第一个时间坐标,需要设置最小时间,时间间隔类型，时间间隔值等三个参数*/
            /************************************************************************/
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss"; //X轴显示的时间格式，HH为大写时是24小时制，hh小写时是12小时制
            chart1.ChartAreas[0].AxisX.Minimum = DateTime.Parse("09:10:02").ToOADate();
            chart1.ChartAreas[0].AxisX.Maximum = DateTime.Parse("09:10:21").ToOADate();
            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;//如果是时间类型的数据，间隔方式可以是秒、分、时
            chart1.ChartAreas[0].AxisX.Interval = DateTime.Parse("00:00:02").Second;//间隔为2秒

            //Y轴设置
            chart1.ChartAreas[0].AxisY.Title = "数据点";
            chart1.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Center;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;//显示横着的分割线
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 25;
            chart1.ChartAreas[0].AxisY.Interval = 5;

            //Series绘制
            chart1.Series[0].LegendText = "温度点";
            chart1.Series[0].ChartType = SeriesChartType.Spline;
            chart1.Series[0].XValueType = ChartValueType.DateTime;
            chart1.Series[0].IsValueShownAsLabel = true;//显示数据点的值
            chart1.Series[0].MarkerStyle = MarkerStyle.Circle;

            //把数据点添加到Series图表中
            for (int i = 0; i < Xdate.Length; i++)
            {
                chart1.Series[0].Points.AddXY(Xdate[i], Ydata[i]);
            }
        }
        private void MyChart2()
        {
            chart1.Series[0].Points.Clear();

            //定义图表大小尺寸
            chart1.Width = Width - 100;
            chart1.Height = Height - 100;

            //定义X轴、Y轴数据
            double[] Ydata = { 20, 3, 23, 6 };
            DateTime[] Xdate = new DateTime[] { DateTime.Parse("09:10:02"), DateTime.Parse("09:10:10"), DateTime.Parse("09:10:15"), DateTime.Parse("09:10:20") };

            //以下按照先绘制chartArea、然后再绘制Series的步骤画图
            //chartArea背景颜色
            chart1.BackColor = Color.Azure;

            //X轴设置
            chart1.ChartAreas[0].AxisX.Title = "时间";
            chart1.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Near;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;//不显示竖着的分割线

            /************************************************************************/
            /* 本文重点讲解时间格式的设置
             * 如果想显示原点第一个时间坐标,需要设置最小时间,时间间隔类型，时间间隔值等三个参数*/
            /************************************************************************/
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss"; //X轴显示的时间格式，HH为大写时是24小时制，hh小写时是12小时制
            double minValue = DateTime.Parse("09:10:01").ToOADate();
            double maxValue = DateTime.Parse("09:10:22").ToOADate();
            chart1.ChartAreas[0].AxisX.Minimum = minValue;
            chart1.ChartAreas[0].AxisX.Maximum = maxValue;
            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;//如果是时间类型的数据，间隔方式可以是秒、分、时
            chart1.ChartAreas[0].AxisX.Interval = DateTime.Parse("00:00:02").Second;//间隔为2秒
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = DateTime.Parse("00:00:02").Second;//间隔为2秒
            chart1.ChartAreas[0].AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;

            chart1.ChartAreas[0].AxisX.LabelStyle.Interval = DateTime.Parse("00:00:02").Second;//间隔为2秒
            chart1.ChartAreas[0].AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;

            //Y轴设置
            chart1.ChartAreas[0].AxisY.Title = "数据点";
            chart1.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Center;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;//显示横着的分割线
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 25;
            chart1.ChartAreas[0].AxisY.Interval = 5;

            //Series绘制
            chart1.Series[0].LegendText = "温度点";
            chart1.Series[0].ChartType = SeriesChartType.Spline;
            chart1.Series[0].XValueType = ChartValueType.DateTime;
            chart1.Series[0].IsValueShownAsLabel = true;//显示数据点的值
            chart1.Series[0].MarkerStyle = MarkerStyle.Circle;

            //把数据点添加到Series图表中
            for (int i = 0; i < Xdate.Length; i++)
            {
                chart1.Series[0].Points.AddXY(Xdate[i], Ydata[i]);
            }
        }
    }
}
