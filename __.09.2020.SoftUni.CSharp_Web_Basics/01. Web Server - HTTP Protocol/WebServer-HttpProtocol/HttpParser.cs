using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WebServer_HttpProtocol
{
    public class HttpParser
    {
        private const string CrLf = "\r\n";

        private static readonly ICollection<string> HttpMethod =
            new HashSet<string> {"get", "post", "delete", "put", "patch"};

        public IDictionary<string, string> Parse(string response)
        {
            var splittedResponse = response
                .Split(CrLf);

            var httpInfo = splittedResponse
                .First()
                .Split(" ")
                .Select((x, i) => i == 0 ? new[] {"Http-Method", x}
                    : i == 1 ? new[] {"Request-URI", x}
                    : new[] {"Http-Version", x});
            
            var content = splittedResponse
                .TakeLast(1)
                .Select(x => new[] {"Content", x});

            return splittedResponse
                .Skip(1)
                .SkipLast(1)
                .Select(x => x.Split(": "))
                .Concat(httpInfo)
                .Concat(content)
                .ToDictionary(
                    kvp => kvp.ElementAtOrDefault(0), 
                    kvp => kvp.ElementAtOrDefault(1));
        }
    }
}