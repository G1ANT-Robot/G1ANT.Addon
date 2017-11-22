using System;
using RestSharp;

using G1ANT.Language;

namespace G1ANT.Addon.Net
{
    [Command(Name = "rest", Tooltip = "This command prepares a request to the desired url with selected method. \nYou can pass multiple parameters and all of them will be attached as a request body.")]
    public class RestCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "http method of rest request ( post / get / put / delete / patch …)")]
            public TextStructure Method { get; set; }

            [Argument(Required = true, Tooltip = "url of api method")]
            public TextStructure Url { get; set; }

            [Argument(DefaultVariable = "timeoutrest", Tooltip = "Defines time duration for command to wait, default for rest is 5000ms")]
            public  override TimeSpanStructure Timeout { get; set; }

            [Argument(Tooltip = "Headers attached to rest request")]
            public ListStructure Headers { get; set; }

            [Argument(Tooltip = "Parameters attached to rest request")]
            public ListStructure Parameters { get; set; }

            [Argument]
            public TextStructure Result { get; set; } = new TextStructure("result");

            [Argument(Tooltip = "Name of variable which will return http status responsed by the API")]
            public TextStructure Status { get; set; } = new TextStructure("status");
        }
        public RestCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            RestClient client = new RestClient(arguments.Url.Value)
            {
                Timeout = Convert.ToInt32(arguments.Timeout.Value.Milliseconds)
            };

            string method = arguments.Method.Value;
            Method currentMethod = ParseRestMethod(method);

            RestRequest request = new RestRequest(string.Empty, currentMethod);

            char separator = ':';

            AddRequestData(request, arguments.Headers, separator, true);
            AddRequestData(request, arguments.Parameters, separator, false);

            IRestResponse response = client.Execute(request);
            string content = response.Content;
            if (response.ResponseStatus == ResponseStatus.TimedOut)
            {
                throw new TimeoutException("Request Timed Out");
            }
            Scripter.Variables.SetVariableValue(arguments.Result.Value, new TextStructure(content));
            Scripter.Variables.SetVariableValue(arguments.Status.Value, new TextStructure(response.ResponseStatus.ToString()));
        }

        private Method ParseRestMethod(string method)
        {
            Method currentMethod = Method.GET;
            try
            {
                currentMethod = (Method)Enum.Parse(typeof(Method), method, true);
            }
            catch
            {
                throw new NotSupportedException($"Given method [{method}] is not supported in rest");
            }

            return currentMethod;
        }

        private void AddRequestData(RestRequest request, ListStructure list, char separator, bool toHeader)
        {
            if (list != null)
            {
                foreach (var listData in list.Value)
                {
                    string data = listData.ToString();
                    var separatedData = data.Split(separator);
                    if (separatedData.Length != 2)
                    {
                        throw new FormatException($" Bad separator in [{data}], use [{separator}] instead ");
                    }

                    if (toHeader)
                    {
                        request.AddHeader(separatedData[0], separatedData[1]);
                    }
                    else
                    {
                        request.AddParameter(separatedData[0], separatedData[1]);
                    }
                }
            }
        }
    }
}
