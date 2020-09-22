using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer_HttpProtocol.Actions
{
    public class AdminAction : Action
    {
        public AdminAction((string code, string name) status, IDictionary<string, string> requestHeaders)
            : base(status)
        {
            if ("post".Equals(requestHeaders["Http-Method"].ToLowerInvariant()))
                DeleteRecords(requestHeaders).Wait();
        }

        public override Task<string> FetchHtml()
        {
            return Task.Factory.StartNew(()
                => $"<h1>Hello, admin! Delete something! </h1>" +
                   "<form action=/admin method=post>" +
                   "    <input name=username />" +
                   "    <input type=submit value='Delete'/>" +
                   "</form>" +
                   "<button onclick='window.location.href=window.location.origin'>Back to home</button>");
        }

        private async Task DeleteRecords(IDictionary<string, string> request)
        {
            var fileName = "tweets.txt";

            var target = request["Content"]
                .Split("&")
                .Select(kvp => Uri.UnescapeDataString(kvp.Replace('+', ' ')).Split("="))
                .ToDictionary(
                    kvp => kvp.ElementAtOrDefault(0),
                    kvp => kvp.ElementAtOrDefault(1));

            if ("*".Equals(target["username"].Trim()))
            {
                File.Delete(fileName);
                return;
            }

            var fileContent = string.Join(CrLf, (await File.ReadAllTextAsync(fileName))
                .Split(CrLf)
                .Select(x => x.Split('\t'))
                .Where(x => !target["username"].Equals(x.ElementAtOrDefault(0)))
                .Select(x => string.Join('\t', x)));

            await File.WriteAllTextAsync(fileName, fileContent);
        }
    }
}