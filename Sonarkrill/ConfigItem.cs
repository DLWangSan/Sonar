using System.IO.Ports;

namespace SonarKrill
{
    internal class ConfigItem
    {
        public string comName;
        public int baudRate;
        public int dataBits;
        public Parity parity;
        public StopBits stopBits;

        public ConfigItem(string comName, int baudRate, int dataBits, Parity parity, StopBits stopBits) {
            this.comName = comName;
            this.baudRate = baudRate;
            this.dataBits = dataBits;
            this.parity = parity;
            this.stopBits = stopBits;
        }

        public string getComName()
        {
            return this.comName;
        }
        public int getBaudRate()
        {
            return this.baudRate;
        }
        public int getDataBits() 
        {
            return this.dataBits;
        }
        public Parity getParity()
        {
            return this.parity;
        }
        public StopBits getStopBits()
        {
            return this.stopBits;
        }
    }
}