/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Watson
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Addon.Watson
{
    public class WatsonSpeechToTextApi
    {
        private string baseUrl = "https://gateway-lon.watsonplatform.net/speech-to-text/api";
        private readonly string apiKey;
        private const string apikeyName = "apikey";
        private readonly string serverUri;
        public WatsonSpeechToTextApi(string apiKey, string serverUri)
        {
            this.apiKey = apiKey;
            this.serverUri = serverUri;
        }

        public string SpeechToText(string filePath, string language = "en-US", int timeout = 30000, int maxAlternatives = 1, double alternativeThreshold = 0.5f)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var result = Upload(filePath);
            return result;
            var includeConfidence = maxAlternatives > 1 ? "true" : "false";
            var url = $"{baseUrl}/v1/recognize";
            var formatType = GetFormatType(filePath);
            var fileBytes = ConvertFileToBytes(filePath);
            var request = CreateRequest(url, timeout, formatType, fileBytes);
            return GetResponse(request, fileBytes);
        }

        private string Upload(string filePath)
        {
            var url = $"{serverUri}/v1/recognize";
            var credentials = new NetworkCredential(apikeyName, apiKey);
            var handler = new HttpClientHandler { Credentials = credentials };
            var fileBytes = ConvertFileToBytes(filePath);

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url))
            {
                Content = new ByteArrayContent(fileBytes),
            };

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apikeyName}:{apiKey}")));

                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", GetFormatType(filePath));

                var response = client.SendAsync(request).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    var responseString = responseContent.ReadAsStringAsync().Result;
                    return responseString;
                }
            }

            return string.Empty;
        }

        private CredentialCache CreateCredentials(string url)
        {
            try
            {
                var credential = new NetworkCredential("apikey", apiKey);
                var credentials = new CredentialCache { { new Uri(url), "Basic", credential } };
                return credentials;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while creating network credentials. {ex.Message}", ex);
            }
        }

        private HttpWebRequest CreateRequest(string url, int timeout, string formatType, byte[] fileBytes)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = timeout;
                request.Credentials = CreateCredentials(url);
                request.PreAuthenticate = true;
                request.Method = "POST";
                request.ContentType = formatType;
                request.ContentLength = fileBytes.Length;
                return request;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occured while creating http request. {ex.Message}", ex);
            }
        }

        private string GetResponse(HttpWebRequest request, byte[] fileBytes)
        {
            try
            {
                JObject jResponse;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(fileBytes, 0, fileBytes.Length);
                    var response = (HttpWebResponse)request.GetResponse();
                    var jSerializer = new JsonSerializer();
                    var jTextReader = new JsonTextReader(new StreamReader(response.GetResponseStream()));
                    jResponse = (JObject)jSerializer.Deserialize(jTextReader);
                }

                var transcripts = jResponse.SelectToken("['results'][0]['alternatives']");

                var result = string.Empty;
                foreach (var item in transcripts.AsJEnumerable())
                {
                    result += $"{item["confidence"]}:{item["transcript"]};";
                }

                return result.Split(';')[0].Split(':')[1];
            }
            catch (Exception ex)
            {
                throw new WebException($"Error occured while obtaining response from Watson. {ex.Message}", ex);
            }
        }

        private byte[] ConvertFileToBytes(string filePath)
        {
            try
            {
                return File.ReadAllBytes(filePath);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Unable to process file. Please verify, whether specified path is correct. Message: {ex.Message}");
            }
        }

        private string GetFormatType(string filePath)
        {
            var format = Path.GetExtension(filePath)?.Replace(".", "")?.ToLower();
            if (format == null || (format != "wav" && format != "flac"))
            {
                throw new ArgumentException("Invalid file path. Only 'wav' files are supported.");
            }

            return $"audio/{format}";
        }
    }
}
