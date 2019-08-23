using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Nsoft_Test.Utils
{
    public static class Files
    {
        public static void RunProccess(string procName, string url)
        {
            Process proc = new Process();

            proc.StartInfo.FileName = procName;
            proc.StartInfo.Arguments = "--new-window " + url;

            proc.Start();
        }
    }
}

/*

    1. Handle invalide response HTTP request on initialization
    2. WEB Socket client hardening (vise provjera itd)
    3. ON Handle web socket poruke, pamti zadnji state i gledati jel novi URL, samo ako jeste novi onda da
    4. Frontend Run Dugme i lista stvari
 
*/