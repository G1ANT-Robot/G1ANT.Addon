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
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using G1ANT.Language;
namespace G1ANT.Addon.Watson
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
            string jsonResult = SendRequest(image, timeout, threshold);
            return ConvertResponseToOutput(jsonResult);            
        }

        private string SendRequest(Bitmap image, int timeout, double threshold)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.Timeout = new System.TimeSpan(0, 0, 0, 0, timeout);
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    byte[] imageData = image.ImageToBytes();
                    var imageContent = new ByteArrayContent(imageData, 0, imageData.Length);
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                    form.Add(imageContent, "images_file", "file.jpeg");

                    var thresholdParam = new StringContent("{\"threshold\":" + threshold.ToString().Replace(',', '.') + "}");
                    thresholdParam.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    form.Add(thresholdParam, "parameters");
                    HttpResponseMessage response = httpClient.PostAsync(url, form).Result;
                    response.EnsureSuccessStatusCode();
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                throw new WebException($"Problem occured while sending request. {ex.Message}", ex);
            }            
        }

        private string ConvertResponseToOutput(string responseContent)
        {
            JObject jsonResultObject = JObject.Parse(responseContent);
            JToken desiredValue = jsonResultObject.SelectToken("['images'][0]['classifiers'][0]['classes']");
            if (desiredValue == null || desiredValue?.AsJEnumerable() == null)
            {
                string status = jsonResultObject.SelectToken("['status']")?.ToString() ?? string.Empty;
                string statusInfo = jsonResultObject.SelectToken("['statusInfo']")?.ToString() ?? string.Empty;
                throw new WebException($"Wrong data received. Status: '{status}' Info: '{statusInfo}'");
            }
            StringBuilder output = new StringBuilder();
            var items = desiredValue.AsJEnumerable();
            foreach (var item in items)
            {
                string category = item["class"].ToString();
                string score = item["score"].ToString();
                output.Append($"{category}:{score};");
            }
            return output.ToString();
        }
    }
}
