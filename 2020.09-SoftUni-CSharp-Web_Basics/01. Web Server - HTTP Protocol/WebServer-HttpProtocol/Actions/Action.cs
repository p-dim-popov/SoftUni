using System;
using System.Collections;
using System.Threading.Tasks;

namespace WebServer_HttpProtocol.Actions
{
    public abstract class Action
    {
        protected const string CrLf = "\r\n";
        private const string ServerName = "DemoServer 2020";

        protected Action((string code, string name) status)
        {
            Status = status.name is null
                ? ("200", "OK")
                : (status.code, status.name);
        }

        public (string Code, string Name) Status { get; }

        public abstract Task<string> FetchHtml();

        public virtual string Headers
            => $"HTTP/1.1 {Status.Code} {Status.Name}" + CrLf +
               $"Server: {ServerName}" + CrLf;

        public async Task<string> ToStringAsync()
        {
            return Headers + CrLf + await FetchHtml();
        }
    }
}