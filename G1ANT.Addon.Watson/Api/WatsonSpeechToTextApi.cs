using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;

namespace G1ANT.Language.Watson
{
    public class WatsonSpeechToTextApi
	{
        private static string baseUrl = "https://stream.watsonplatform.net/speech-to-text/api/v1/recognize";
        private static string username = "a9fa31a8-d53f-48b2-ad63-6e97f748ae16";
        private static string password = "IAEdZHDd7Z6V";

        public string SpeechToText(string filePath, string language = "en-US", int timeout = 30000, int maxAlternatives = 1, double alternativeThreshold = 0.5f)
		{
			string includeConfidence = maxAlternatives > 1 ? "true" : "false";
            string url = $"{baseUrl}?model={language}_BroadbandModel&max_alternatives={maxAlternatives.ToString()}&word_alternatives_threshold={ alternativeThreshold.ToString()}&word_confidence={includeConfidence}";
            string formatType = GetFormatType(filePath);
            byte[] fileBytes = ConvertFileToBytes(filePath);
            HttpWebRequest request = CreateRequest(url, timeout, formatType, fileBytes);  
            return GetResponse(request, fileBytes);
		}

        private CredentialCache CreateCredentials(string url)
        {
            try
            {
                NetworkCredential credential = new NetworkCredential(username, password);
                CredentialCache credentials = new CredentialCache();
                credentials.Add(new Uri(url), "Basic", credential);
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
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Timeout = timeout;
                request.Credentials = CreateCredentials(url);
                request.PreAuthenticate = true;
                request.Method = "POST";
                request.SendChunked = true;
                request.AllowWriteStreamBuffering = true;
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
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    var jSerializer = new JsonSerializer();
                    var jTextReader = new JsonTextReader(new StreamReader(response.GetResponseStream()));
                    jResponse = (JObject)jSerializer.Deserialize(jTextReader);
                }
                JToken transcripts = jResponse.SelectToken("['results'][0]['alternatives']");

                string result = string.Empty;
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
                throw new ArgumentException($"Unable to process file. Please verify, whether specified path is correct. Message: {ex.Message}");
            }
        }

        private string GetFormatType(string filePath)
        {
            string format = Path.GetExtension(filePath)?.Replace(".", "")?.ToLower();
            if (format == null || format != "wav")
            {
                throw new ArgumentException("Invalid file path. Only 'wav' files are supported.");
            }
            return $"audio/{format}";
        }
    }
}
