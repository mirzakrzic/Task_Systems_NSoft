using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Nsoft_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunController : ControllerBase
    {
        public ActionResult<string> Get()
        {
            string cacheStr = System.IO.File.ReadAllText("C:/cache.json");

            DeviceModel devConf = JsonConvert.DeserializeObject<DeviceModel>(cacheStr);

            Utils.Files.RunProccess("C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe", devConf.displays[0].applications[0].url);

            return "";
        }

    }
}