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
        private string apiKey;
        private string url;

        public WatsonClassifyImageApi(string apiKey)
        {
            this.apiKey = apiKey;
            url = $"https://gateway-a.watsonplatform.net/visual-recognition/api/v3/classify?api_key={apiKey}&version=2017-10-13";
        }

        public string ClassifyImage(Bitmap image, int timeout, double threshold)
        {            
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
            var items = desiredValue.AsJEnumerable();
            foreach (var item in items)
            {
                var category = item["class"].ToString();
                var score = item["score"].ToString();
                output.Append($"{category}:{score};");
            }
            return output.ToString();
        }
    }
}
