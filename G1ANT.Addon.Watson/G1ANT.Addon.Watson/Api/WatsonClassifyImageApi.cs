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
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using G1ANT.Language;
using Newtonsoft.Json.Linq;

namespace G1ANT.Addon.Watson.Api
{
    public class WatsonClassifyImageApi
    {
        private readonly string apiKey;
        private readonly string url;

        public WatsonClassifyImageApi(string apiKey, string url)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            this.apiKey = apiKey;
            this.url = url;
        }

        public string ClassifyImage(string imagePath, int timeout, double threshold)
        {
            var image = Image.FromFile(imagePath);
            var jsonResult = SendRequest(image, timeout, threshold);
            return ConvertResponseToOutput(jsonResult);            
        }

        private string SendRequest(Image image, int timeout, double threshold)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = new TimeSpan(0, 0, 0, 0, timeout);
                    var form = new MultipartFormDataContent();
                    var imageData = image.ImageToBytes();
                    var imageContent = new ByteArrayContent(imageData, 0, imageData.Length);
                    var thresholdParam = new StringContent("{\"threshold\":" + threshold.ToString().Replace(',', '.') + "}");
                    AddProperHeaders(httpClient);
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                    form.Add(imageContent, "images_file", "file.jpeg");
                    thresholdParam.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    form.Add(thresholdParam, "parameters");

                    var response = httpClient.PostAsync(url, form).Result;
                    response.EnsureSuccessStatusCode();
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                throw new WebException($"Problem occured while sending request. {ex.Message}", ex);
            }            
        }


        private void AddProperHeaders(HttpClient client)
        {
            var authorizationValue = Convert.ToBase64String(Encoding.ASCII.GetBytes($"apikey:{apiKey}"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorizationValue);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "image/jpeg");
        }

        private static string ConvertResponseToOutput(string responseContent)
        {
            var jsonResultObject = JObject.Parse(responseContent);
            var desiredValue = jsonResultObject.SelectToken("['images'][0]['classifiers'][0]['classes']");
            if (desiredValue?.AsJEnumerable() == null)
            {
                var status = jsonResultObject.SelectToken("['status']")?.ToString() ?? string.Empty;
                var statusInfo = jsonResultObject.SelectToken("['statusInfo']")?.ToString() ?? string.Empty;
                throw new WebException($"Wrong data received. Status: '{status}' Info: '{statusInfo}'");
            }
            var output = new StringBuilder();
            var tokens = desiredValue.AsJEnumerable();
            foreach (var token in tokens)
            {
                var category = token["class"].ToString();
                var score = token["score"].ToString();
                output.Append($"{category}:{score};");
            }
            return output.ToString();
        }
    }
}
