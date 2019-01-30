/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.Ocr
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Vision.v1;
using Google.Apis.Vision.v1.Data;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace G1ANT.Language.Ocr
{
    public class GoogleCloudApi
    {
        public GoogleCredential JsonCredential { get; private set; }
        private static GoogleCloudApi instance;
        public static GoogleCloudApi Instance
        {
            get
            {
                if (instance == null)
                    throw new InvalidOperationException($"Before using this command, you need to login to Google text recognition service. Please, use ocr.login command first.");
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        public GoogleCloudApi(string jsonCredential)
        {
            if (instance == null)
            {
                CreateCredential(jsonCredential);
                instance = this;
            }
            else
                throw new Exception("An instance of this class already exists.");
        }

        public void CleanUp()
        {
            instance = null;
            JsonCredential = null;
        }

        public void CreateCredential(string jsonCredential)
        {
            using (var stream = jsonCredential.ConvertStringToStream())
            {
                string[] scopes = { VisionService.Scope.CloudPlatform };
                this.JsonCredential = GoogleCredential.FromStream(stream);
                this.JsonCredential = this.JsonCredential.CreateScoped(scopes);
            }
        }

        private VisionService CreateService()
        {
            var service = new VisionService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = this.JsonCredential,
                ApplicationName = "G1ANT-Robot",
                GZipEnabled = true,
            });
            return service;
        }

        public string RecognizeText(Bitmap image, List<string> languages, int timeout)
        {
            BatchAnnotateImagesRequest batchRequest = new BatchAnnotateImagesRequest();
            batchRequest.Requests = new List<AnnotateImageRequest>();
            batchRequest.Requests.Add(new AnnotateImageRequest()
            {
                Features = new List<Feature>() { new Feature() { Type = "TEXT_DETECTION", MaxResults = 1 }, },
                ImageContext = new ImageContext() { LanguageHints = languages },
                Image = new Google.Apis.Vision.v1.Data.Image() { Content = Convert.ToBase64String(image.ImageToBytes()) },
            });

            string output = string.Empty;
            using (var visioService = this.CreateService())
            {
                visioService.HttpClient.Timeout = new TimeSpan(0, 0, 0, 0, timeout);
                var annotate = visioService.Images.Annotate(batchRequest);
                BatchAnnotateImagesResponse batchAnnotateImagesResponse = annotate.Execute();
                var annotations = batchAnnotateImagesResponse.Responses;
                if (annotations.Count > 0 &&
                    annotations[0].TextAnnotations != null &&
                    annotations[0].TextAnnotations.Count > 0 &&
                    annotations[0].TextAnnotations[0].Description != null)
                {
                    output = annotations[0].TextAnnotations[0].Description.TrimEnd().TrimStart();
                }
            }
            return output;
        }

        public Rectangle RecognizeText(Bitmap image, string searchword, List<string> languages, int timeout)
        {
            Rectangle rect = new Rectangle();
            BatchAnnotateImagesRequest batchRequest = new BatchAnnotateImagesRequest();
            batchRequest.Requests = new List<AnnotateImageRequest>();
            batchRequest.Requests.Add(new AnnotateImageRequest()
            {
                Features = new List<Feature>() { new Feature() { Type = "TEXT_DETECTION", MaxResults = 1 }, },
                ImageContext = new ImageContext() { LanguageHints = languages },
                Image = new Google.Apis.Vision.v1.Data.Image() { Content = Convert.ToBase64String(image.ImageToBytes()) },
            });

            string output = string.Empty;
            using (var visioService = this.CreateService())
            {

                visioService.HttpClient.Timeout = new TimeSpan(0, 0, 0, 0, timeout);
                var annotate = visioService.Images.Annotate(batchRequest);
                BatchAnnotateImagesResponse batchAnnotateImagesResponse = annotate.Execute();
                var annotations = batchAnnotateImagesResponse.Responses;
                if (annotations.Count > 0 &&
                    annotations[0].TextAnnotations != null &&
                    annotations[0].TextAnnotations.Count > 0 &&
                    annotations[0].TextAnnotations[0].Description != null)
                {
                    output = annotations[0].TextAnnotations[0].Description.TrimEnd().TrimStart();
                }

                if (annotations.Count > 0 &&
                    annotations[0].TextAnnotations != null &&
                    annotations[0].TextAnnotations.Count > 0)
                {
                    for (int i = 0; i < annotations[0].TextAnnotations.Count; i++)
                    {
                        var itemDescription = annotations[0].TextAnnotations[i].Description.ToLower();
                        var item = annotations[0].TextAnnotations[i].BoundingPoly.Vertices;
                        if (itemDescription.Equals(searchword.ToLower()))
                        {
                            rect = new Rectangle(item[0].X.Value, item[0].Y.Value, item[1].X.Value - item[0].X.Value, item[2].Y.Value - item[1].Y.Value);
                            break;
                        }
                        else
                        {

                            rect = new Rectangle(-1, -1, -1, -1);
                        }
                    }
                }
            }
            return rect;
        }
    }
}
