using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;

namespace Nsoft_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : Controller
    {
        [HttpGet]
        public ActionResult<DeviceModel> Get()
        {
            string cacheStr = System.IO.File.ReadAllText("C:/cache.json");

            DeviceModel devConf = JsonConvert.DeserializeObject<DeviceModel>(cacheStr);

            return devConf;
        }

        [Route("api/start_application")]
        public ActionResult<string> Run()
        {
            string cacheStr = System.IO.File.ReadAllText("C:/cache.json");

            DeviceModel devConf = JsonConvert.DeserializeObject<DeviceModel>(cacheStr);

            Utils.Files.RunProccess("C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe", devConf.displays[0].applications[0].url);

            return "";
        }
    }
}