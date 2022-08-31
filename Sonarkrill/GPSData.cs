namespace SonarKrill
{
    internal class GPSData
    {
        private string date_time;
        private double lat;
        private double lon;
        private float speed;
        private float direct;
        private string time;

        public GPSData(string date_time, double lat, double lon, float speed, float direct)
        {
            this.date_time = date_time;
            this.lat = lat;
            this.lon = lon;
            this.speed = speed;
            this.direct = direct;
            if (date_time != "ERROR")
                this.time = date_time.Split(' ')[1];
            else
                this.time = "Waiting...";
        }

        public string getDtatime()
        {
            return this.date_time;
        }

        public double getLat()
        {
            return this.lat;
        }

        public double getLon()
        {
            return this.lon;
        }

        public float getSpeed()
        {
            return this.speed;
        }

        public float getDirect()
        {
            return this.direct;
        }

        public string getTime()
        {
            return this.time;
        }

    }
}