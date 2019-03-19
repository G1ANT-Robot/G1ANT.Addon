/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Watson
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;

namespace G1ANT.Addon.Watson.Api
{
    public class WatsonSpeechToTextApi
    {
        private readonly string apiKey;
        private const string ApiKeyName = "apikey";
        private readonly string serverUri;

        public WatsonSpeechToTextApi(string apiKey, string serverUri)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            this.apiKey = apiKey;
            this.serverUri = serverUri;
        }

        public string SpeechToText(string filePath, string language = "en-US", int timeout = 10000)
        {
            var url = $"{serverUri}/v1/recognize?model={language}_BroadbandModel";
            var jsonResponseString = SendAudioFileToWatson(filePath, url, timeout);
            return ConvertResponseJsonStringToProperResult(jsonResponseString);
        }

        private static string ConvertResponseJsonStringToProperResult(string jsonString)
        {
            var jResponse = JObject.Parse(jsonString);
            var transcripts = jResponse.SelectToken("['results'][0]['alternatives']");
            var result = string.Empty;
            foreach (var item in transcripts.AsJEnumerable())
            {
                result += $"{item["confidence"]} : {item["transcript"]};";
            }

            return result;
        }

        private string SendAudioFileToWatson(string filePath, string url, int timeout)
        {
            var credentials = new NetworkCredential(ApiKeyName, apiKey);
            var handler = new HttpClientHandler { Credentials = credentials };
            var fileBytes = ConvertFileToBytes(filePath);

            using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url)) { Content = new ByteArrayContent(fileBytes) })
            using (var client = new HttpClient(handler))
            {
                AddProperHeaders(client, filePath);
                client.Timeout = new TimeSpan(0, 0, 0, 0, timeout);
                var response = client.SendAsync(request).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }

            return string.Empty;
        }

        private void AddProperHeaders(HttpClient client, string filePath)
        {
            var authorizationValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ApiKeyName}:{apiKey}"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorizationValue);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", GetFormatType(filePath));
        }

        private static byte[] ConvertFileToBytes(string filePath)
        {
            try
            {
                return File.ReadAllBytes(filePath);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Unable to process file. Please verify, whether specified path is correct. {ex.Message}");
            }
        }

        private static string GetFormatType(string filePath)
        {
            var format = Path.GetExtension(filePath)?.Replace(".", "").ToLower();
            return $"audio/{format}";
        }
    }
}
