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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            chart1.ChartAreas[0].AxisY.Minimum = -200;
            chart1.ChartAreas[0].AxisY.Maximum = 0;


            //chart1.ChartAreas[0].AxisX2.Minimum = 0;
            //chart1.ChartAreas[0].AxisX2.Maximum = 10;
            chart1.ChartAreas[0].AxisY.Crossing = Double.MaxValue;
            chart1.Series[0].ChartType = SeriesChartType.Point;

            chart1.Series[0].Points.AddXY(5, -50);
        }
    }
}
