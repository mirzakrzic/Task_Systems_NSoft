using Newtonsoft.Json;
using SocketIOClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Nsoft_Test
{
    public interface IReqService
    {
        Task InitializeWSAsync();
        Task InitializeAsync();
    }

    public class ReqService : IReqService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ReqService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;

            InitializeAsync();

            InitializeWSAsync();
        }

        public async Task InitializeAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "http://localhost:3000/api/device/configuration");
            request.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient();


            var response = client.SendAsync(request);

            response.Wait();


            if (response.IsCompletedSuccessfully)
            {
                DeviceModel devConf = await response.Result.Content.ReadAsAsync<DeviceModel>();

                string stringifiedJson = JsonConvert.SerializeObject(devConf);


                File.WriteAllText("C:/cache.json", stringifiedJson);
            }

        }
        public async Task InitializeWSAsync()
        {
            var client = new SocketIO("http://localhost:3001");
            client.OnClosed += async reason =>
            {
                //await Task.Delay(60000);
                //await client.ConnectAsync();
                //await client.EmitAsync("test", "test");
                if (reason == ServerCloseReason.ClosedByServer)
                {
                    // ...
                }
                else if (reason == ServerCloseReason.Aborted)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        try
                        {
                            await client.ConnectAsync();
                            break;
                        }
                        catch (WebSocketException ex)
                        {
                            // show tips
                            Console.WriteLine(ex.Message);
                            await Task.Delay(2000);
                        }
                    }
                    // show tips
                    Console.WriteLine("Tried to reconnect 3 times, unable to connect to the server");
                }
            };

            client.OnReceivedEvent += (eventName, args) =>
            {
                string en1 = eventName;
                string text1 = args.Text;

                if(eventName == "state" && args.Text != null)
                {
                    File.WriteAllText("C:/cache.json", args.Text);

                    DeviceWrapperModel devConf = JsonConvert.DeserializeObject<DeviceWrapperModel>(args.Text);
                    if(devConf.data != null)
                    {
                        File.WriteAllText("C:/cache.json", args.Text);
                        Utils.Files.RunProccess("C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe", devConf.data.displays[0].applications[0].url);
                    }
                }
            };
            await client.ConnectAsync();

        }

        private void Client_OnConnected()
        {
            Console.WriteLine("Connected to server");
        }

    }
}
