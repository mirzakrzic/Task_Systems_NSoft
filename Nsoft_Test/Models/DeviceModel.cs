using System.Collections.Generic;

namespace Nsoft_Test
{
    public class DeviceWrapperModel
    {
        public DeviceModel data { get; set; }
    }
    public class DeviceModel
    {
        public string hostname { get; set; }
        public string motherbord { get; set; }
        public string hdd { get; set; }
        public string network { get; set; }
        public string ip_address { get; set; }

        public List<Display> displays { get; set; }
    }

    public class Display
    {
        public int id { get; set; }
        public string refresh_rate { get; set; }
        public string manufacturer { get; set; }
        public List<Application> applications { get; set; }
    }

    public class Application
    {
        public string name { get; set; }
        public string runtime { get; set; }
        public string url { get; set; }
    }
}