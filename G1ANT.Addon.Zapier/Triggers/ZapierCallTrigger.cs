using G1ANT.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Language.Zapier.Triggers
{

    [Trigger]
    public class ZapierCallTrigger : Trigger
    {
        private HttpListener httpListener;

        public void Initialize()
        {
            httpListener = new HttpListener();
            string address = "localhost";// InputParameters["address"];
            string port = "60000";// InputParameters["port"];
            string listeningAddress = string.Format("http://{0}:{1}/", address, port);
            httpListener.Prefixes.Add(listeningAddress);
            httpListener.Start();
        }

        public void CheckAndRun()
        {
            var context = httpListener.GetContext();
            var address = context.Request.UserHostAddress;
            
            string message = context.Request.QueryString["message"] ?? string.Empty;
            
            string responseMessage = "OK" + message;
            int responseStatusCode = 200;
            if (string.IsNullOrEmpty(message))
            {
                responseMessage = "FAIL - message value not passed";
                responseStatusCode = 200;
            }

            var buffer = System.Text.Encoding.UTF8.GetBytes(responseMessage);
            context.Response.ContentLength64 = buffer.Length;
            var output = context.Response.OutputStream;
            context.Response.StatusCode = responseStatusCode;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        public void CleanUp()
        {
            httpListener.Stop();
        }
    }
}
