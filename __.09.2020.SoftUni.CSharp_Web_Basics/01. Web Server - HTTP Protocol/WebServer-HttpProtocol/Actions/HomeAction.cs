using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer_HttpProtocol.Actions
{
    public class HomeAction : Action
    {
        public HomeAction((string code, string name) status, IDictionary<string, string> requestHeaders)
            : base(status)
        {
            if ("post".Equals(requestHeaders["Http-Method"].ToLowerInvariant()))
                PostTweet(requestHeaders).Wait();
        }

        public override async Task<string> FetchHtml()
            => $"<h1>Hello, tweet something! Current time: {DateTime.Now}</h1>" +
               "<form action=/ method=post>" +
               "    <input name=username />" +
               "    <input name=content />" +
               "    <input type=submit value='Post'/>" +
               "</form>" +
               "<ul>" +
               string.Join('\n', (await this.FetchRecords())
                   .Select(x => $"<li>{x.ElementAtOrDefault(0)} said: {x.ElementAtOrDefault(1)}</li>")) +
               "</ul>";

        public async Task<string> FetchHeaders()
            => base.Headers +
               "Content-Type: text/html; charset=utf-8" + CrLf +
               "Content-Lenght: " + (await FetchHtml()).Length + CrLf;

        private async Task<ICollection<string[]>> FetchRecords()
        {
            string fileName = @"tweets.txt";

            // Check if file already exists. If yes, delete it.     
            if (!File.Exists(fileName))
                File.Create(fileName);

            return (await File.ReadAllTextAsync(fileName))
                .Split('\n')
                .Select(x => x.Split('\t', StringSplitOptions.RemoveEmptyEntries))
                .Where(x => x.Any())
                .ToHashSet();
        }

        private async Task PostTweet(IDictionary<string, string> requestHeaders)
        {
            string fileName = "tweets.txt";

            if (!File.Exists(fileName))
                File.Create(fileName);

            //File contents: "username    Very short post!"
            //               "another     a bit longer post"

            var post = requestHeaders["Content"]
                .Split("&")
                .Select(kvp => Uri.UnescapeDataString(kvp.Replace('+', ' ')).Split("="))
                .ToDictionary(
                    kvp => kvp.ElementAtOrDefault(0),
                    kvp => kvp.ElementAtOrDefault(1));

            await File.AppendAllTextAsync(fileName, $"{post["username"]}\t{post["content"]}{CrLf}");
        }
    }
}