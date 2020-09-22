using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebServer_HttpProtocol.Actions
{
    public class ErrorAction : Action
    {
        public ErrorAction((string code, string name) status,
            IDictionary<string, string> requestHeaders)
            : base(status)
        {
        }

        public override Task<string> FetchHtml()
            => Task.Factory.StartNew(()
                => $"<h1>Error {Status.Code}</h1>" +
                   $"<h2>{Status.Name}!</h2>");
    }
}