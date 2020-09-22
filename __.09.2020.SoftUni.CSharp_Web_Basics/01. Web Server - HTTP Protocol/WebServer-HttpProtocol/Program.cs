using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebServer_HttpProtocol
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            TcpListener tcpListener = new TcpListener(
                IPAddress.Loopback, 8081);
            tcpListener.Start();

            while (true)
            {
                var client = await tcpListener.AcceptTcpClientAsync();
                await using var stream = client.GetStream();
                
                var buffer = new byte[1000000];
                var length = stream.Read(buffer, 0, buffer.Length);

                var requestString =
                    Encoding.UTF8.GetString(buffer, 0, length);
                
                if (string.IsNullOrWhiteSpace(requestString.Trim()))
                    continue;
                Console.WriteLine(requestString);
                
                var httpParser = new HttpParser();
                var headers = httpParser.Parse(requestString);
                var response = Responder.RespondTo(headers);

                var responseBytes = Encoding.UTF8.GetBytes(await response.ToStringAsync());
                stream.Write(responseBytes);
                Console.WriteLine(new string('=', 64));
            }
        }

        public static async Task ReadData()
        {
            string url = "https://softuni.bg/courses/csharp-web-basics";
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(string.Join(Environment.NewLine,
                response.Headers.Select(x => x.Key + ": " + x.Value.First())));

            // var html = await httpClient.GetStringAsync(url);
            // Console.WriteLine(html);
        }
    }
}