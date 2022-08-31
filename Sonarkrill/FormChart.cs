using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;



namespace SonarKrill
{
    public partial class FormChart : Form
    {

        private Random random = new Random();
        private int pointIndex = 0;

        DateTime  minValue;
        DateTime maxValue ;
        private Random rand = new Random();

        public FormChart()
        {
            InitializeComponent();


            //Random random = new Random();
            //for (int pointIndex = 0; pointIndex < 10; pointIndex++)
            //{
            //    chart1.Series["Series1"].Points.AddY(random.Next(0, 100));
            //}

            //// Set point chart type
            //chart1.Series["Series1"].ChartType = SeriesChartType.Point;

            //// Enable data points labels
            //chart1.Series["Series1"].IsValueShownAsLabel = true;
            //chart1.Series["Series1"]["LabelStyle"] = "Center";

            //// Set marker size
            //chart1.Series["Series1"].MarkerSize = 15;

            //// Set marker shape
            //chart1.Series["Series1"].MarkerStyle = MarkerStyle.Diamond;

            //// Set to 3D
            //chart1.ChartAreas[0].AxisX.IsReversed=true;

            // Set Reverse value for Y axis
            //chart1.ChartAreas[0].AxisY.IsReversed = true;

            //chart1.ChartAreas[0].AxisY.Crossing = Double.MaxValue;
            //chart1.ChartAreas[0].AxisX.Crossing = Double.MaxValue;         



            //InitChart();


            // Predefine the viewing area of the chart
     



            // Reset number of series in the chart.
            chart1.Series.Clear();

            // create a line chart series
            Series newSeries = new Series("Series1");
            newSeries.ChartType = SeriesChartType.Point;
            newSeries.BorderWidth = 2;
            newSeries.Color = Color.OrangeRed;
            newSeries.XValueType = ChartValueType.DateTime;
            chart1.Series.Add(newSeries);

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss"; //X轴显示的时间格式，HH为大写时是24小时制，hh小写时是12小时制
            double minValue = DateTime.Now.AddSeconds(-10).ToOADate();
            double maxValue = DateTime.Now.AddSeconds(5).ToOADate();
            chart1.ChartAreas[0].AxisX.Minimum = minValue;
            chart1.ChartAreas[0].AxisX.Maximum = maxValue;
            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;//如果是时间类型的数据，间隔方式可以是秒、分、时
            chart1.ChartAreas[0].AxisX.Interval = DateTime.Parse("00:00:02").Second;//间隔为2秒
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = DateTime.Parse("00:00:02").Second;//间隔为2秒
            chart1.ChartAreas[0].AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;

            chart1.ChartAreas[0].AxisX.LabelStyle.Interval = DateTime.Parse("00:00:02").Second;//间隔为2秒
            chart1.ChartAreas[0].AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;

            chart1.Series[0].XValueType = ChartValueType.DateTime;
            chart1.ChartAreas[0].AxisY.Maximum = 0;
            //AddPoints();

        }

        private void timerRealTimeData_Tick0(object sender, System.EventArgs e)
        {
            // Define some variables
            int numberOfPointsInChart = 200;
            int numberOfPointsAfterRemoval = 150;

            // Simulate adding new data points
            int numberOfPointsAddedMin = 5;
            int numberOfPointsAddedMax = 10;
            for (int pointNumber = 0; pointNumber < random.Next(numberOfPointsAddedMin, numberOfPointsAddedMax); pointNumber++)
            {
                chart1.Series[0].Points.AddXY(pointIndex + 1, random.Next(-200, 0));
                ++pointIndex;
                chart1.Series[0].ChartType = SeriesChartType.Point;
            }

            // Adjust Y & X axis scale
            chart1.ResetAutoValues();

            // Keep a constant number of points by removing them from the left
            while (chart1.Series[0].Points.Count > numberOfPointsInChart)
            {
                // Remove data points on the left side
                while (chart1.Series[0].Points.Count > numberOfPointsAfterRemoval)
                {
                    chart1.Series[0].Points.RemoveAt(0);
                }
                chart1.Series[0].ChartType = SeriesChartType.Point;
                // Adjust X axis scale
                chart1.ChartAreas[0].AxisX.Minimum = pointIndex - numberOfPointsAfterRemoval;
                chart1.ChartAreas[0].AxisX.Maximum = chart1.ChartAreas[0].AxisX.Minimum + numberOfPointsInChart;
            }

            // Invalidate chart
            chart1.Invalidate();
        }

        private void timerRealTimeData_Tick1(object sender, System.EventArgs e)
        {
            DateTime timeStamp = DateTime.Now;

            foreach (Series ptSeries in chart1.Series)
            {
                AddNewPoint(timeStamp, ptSeries);
            }


        }

        private void timerRealTimeData_Tick(object sender, System.EventArgs e)
        {
            Random ran = new Random();
            int iValue = ran.Next(-200, 0);
            chart1.Series[0].Points.AddXY(DateTime.Now.AddSeconds(1), iValue);

            double minValue = DateTime.Now.AddSeconds(-10).ToOADate();
            double maxValue = DateTime.Now.AddSeconds(5).ToOADate();
            chart1.ChartAreas[0].AxisX.Minimum = minValue;
            chart1.ChartAreas[0].AxisX.Maximum = maxValue;

        }

        public void AddNewPoint(DateTime timeStamp, System.Windows.Forms.DataVisualization.Charting.Series ptSeries)
        {
            double newVal = 0;

            if (ptSeries.Points.Count > 0)
            {
                newVal = ptSeries.Points[ptSeries.Points.Count - 1].YValues[0] + ((rand.NextDouble() * 2) - 1);
            }

            if (newVal < 0)
                newVal = 0;

            // Add new data point to its series.
            ptSeries.Points.AddXY(timeStamp.ToOADate(), rand.Next(-200, 0));

            // remove all points from the source series older than 1.5 minutes.
            double removeBefore = timeStamp.AddSeconds((double)(90) * (-1)).ToOADate();
            //remove oldest values to maintain a constant number of data points
            while (ptSeries.Points[0].XValue < removeBefore)
            {
                ptSeries.Points.RemoveAt(0);
            }

            chart1.ChartAreas[0].AxisX.Minimum = ptSeries.Points[0].XValue;
            chart1.ChartAreas[0].AxisX.Maximum = DateTime.FromOADate(ptSeries.Points[0].XValue).AddMinutes(2).ToOADate();

            chart1.Invalidate();
        }
        private void AddPoints()
        {
            double[] Ydata = { 20, 3, 23, 6 };
            DateTime[] Xdate = new DateTime[] { DateTime.Now .AddSeconds(1), DateTime.Now.AddSeconds(3), DateTime.Now.AddSeconds(5), DateTime.Now.AddSeconds(7), };
            //把数据点添加到Series图表中
            for (int i = 0; i < Xdate.Length; i++)
            {
                chart1.Series[0].Points.AddXY(Xdate[i], Ydata[i]);
            }
        }

       
    }
}
