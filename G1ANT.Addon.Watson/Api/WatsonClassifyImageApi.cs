using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace G1ANT.Language.Watson
{
    public class WatsonClassifyImageApi
    {
        private static string apiKey = "2c25bc7b0be22091cb1dc9373a026837703cdcf6";
        private static string url = $"https://gateway-a.watsonplatform.net/visual-recognition/api/v3/classify?api_key={apiKey}&version=2017-10-13";

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
