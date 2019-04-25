/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Net
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using System;
using RestSharp;

using G1ANT.Language;

namespace G1ANT.Addon.Net
{
    [Command(Name = "rest", Tooltip = "This command prepares a request to a desired URL with a selected method")]
    public class RestCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Required = true, Tooltip = "HTTP method of the `rest` request: `post` or `get`")]
            public TextStructure Method { get; set; }

            [Argument(Required = true, Tooltip = "URL of API method")]
            public TextStructure Url { get; set; }

            [Argument(DefaultVariable = "timeoutrest", Tooltip = "Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed")]
            public  override TimeSpanStructure Timeout { get; set; }

            [Argument(Tooltip = "Headers attached to the request. Separate headers using ❚ character (Ctrl+\\); their keys and values should be separated with colon (:)")]
            public ListStructure Headers { get; set; }

            [Argument(Tooltip = "Parameters attached to the request. Separate headers using ❚ character (Ctrl+\\); their keys and values should be separated with colon (:)")]
            public ListStructure Parameters { get; set; }

            [Argument(Tooltip = "Name of a variable which will store the data returned by the API (usually json or xml)")]
            public VariableStructure Result { get; set; } = new VariableStructure("result");

            [Argument(Tooltip = "Name of a variable which will store the request delivery status")]
            public VariableStructure Status { get; set; } = new VariableStructure("status");
        }
        public RestCommand(AbstractScripter scripter) : base(scripter)
        {
        }
        public void Execute(Arguments arguments)
        {
            RestClient client = new RestClient(arguments.Url.Value)
            {
                Timeout = Convert.ToInt32((int)arguments.Timeout.Value.TotalMilliseconds)
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
