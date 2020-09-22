using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WebServer_HttpProtocol
{
    using Actions;

    public static class Responder
    {
        public static Action RespondTo(IDictionary<string, string> request)
        {
            switch (request["Request-URI"])
            {
                case "":
                case "/":
                    return new HomeAction(("200", "OK"), request);
                case "/admin":
                    return new AdminAction(("200", "OK"), request);
                default:
                    return new ErrorAction(("404", "Not Found"), request);
            }
        }
    }
}