using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SharpMap;
using SharpMap.CoordinateSystems;

using SharpMap.Forms;
using SharpMap.Forms.Tools;
using SharpMap.Layers;
using System.Drawing.Drawing2D;
using SharpMap.Data;
using SharpMap.Rendering.Decoration;
using GeoAPI.Geometries;
using SharpMap.Styles;

namespace SonarKrill
{
    public partial class FormMap : Form
    {
        private SharpMap.Data.Providers.GeometryProvider geoProvider;
        public FormMap()
        {
            InitializeComponent();
            mapKrill.Map = ShapefileSample.InitializeMap(0);
            mapKrill.Refresh();
            mapKrill.ActiveTool = MapBox.Tools.Pan;

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
            mapKrill.Map.Layers.Add(vl);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mapKrill.Map = ShapefileSample.InitializeMap(0);
            mapKrill.Refresh();
            mapKrill.ActiveTool = MapBox.Tools.Pan;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            GeoAPI.Geometries.IGeometry geometry =mapKrill.Map .Factory.ToGeometry(mapKrill.Map.Envelope);
            Coordinate coord = new Coordinate(1, 1);
            var point = mapKrill.Map.Factory.CreatePoint(coord);
            geoProvider.Geometries.Add(point);
            Coordinate[] coords =  new Coordinate[] { new Coordinate(0, 30), new Coordinate(120, 10), new Coordinate(150, -30) };
            var line = mapKrill.Map.Factory.CreateLineString(coords);            
            geoProvider.Geometries.Add(line);
            this.mapKrill.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
            mapKrill.Refresh();
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
    }
}
