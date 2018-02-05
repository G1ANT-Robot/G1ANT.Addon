using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Vision.v1;
using Google.Apis.Vision.v1.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1ANT.Language.Ocr
{
    public class GoogleCloudApi
    {
        public static string JsonCredential {get; set;}

        public GoogleCloudApi()
        {
            if (JsonCredential == null)
            {
                throw new InvalidOperationException($"Before using this command, you need to login to Google text recognition service. Please, use ocr.login command first.");
            }
        }

        private GoogleCredential CreateCredential()
        {
            string json = "{'type': 'service_account', 'project_id': 'g1ant-robot-157411', 'private_key_id': '0b7239b2d48b60d4b5bc45c5297e57002f611e6c',  'private_key': '-----BEGIN PRIVATE KEY-----\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCdA1E6Yag0PdeP\nYwKGMbYPIjB0PrwJ7USq0rbujkG7MrdU54gKrnKztN4Kv+lxD8CVeOIXUohLW9xB\nMzv4UDKI656bcs1VbVV3WJTASnls/LKLHZeULFEuQ3IwPXBAgG34E8M2v5A71l4F\nEVTVNXegqt6pO6YrIRUxcEGj095qDRge5LFXKAMud17Z06KdYBGmkXi2U4l5W4RZ\nEnEAIZY1StUBYw24eK3ljL0NFghJrhPKlktO8BOpL4Q769b/diqEvaMs35InoYIQ\nZxkBamGE0RB0Sb+RvQrBHUXWJBoMmCInuNtlUGdc645WcGv0Rkeiv1N/j4T/m6pY\noRhbTx9HAgMBAAECggEBAJx4tNdIgRbm1k4XrmlLWxLD0KPX7Q67fGX8Lb+Y1xqW\nFSuwvNtgNACw62KtFBfPSe3rlO0HG7p15g633zi7u1DF9r8VblmG5thT15G15kJd\nphSLPga1D4XaQw6d+6LcGjtSfoWt7DAllnbYacE8UmY0QkdGoXdztaoaI9//ATSP\nHmhEgIkzpXK2wtB0uGvBDD//8XFuM/qK4CG6IIjiBCVrI9egtemcSZ3QA621Kv7Y\nIvl8r1h55AY0w0qD1wrByjxO47G5n3jm18fr3kyloZEhSMxLE74Iuf+ZpRuvs7gd\nUM7iZPJ2L8tU4rZASLWfsO8rSuEN/B2+zM6qztZyKLkCgYEAy5XgT4aOBnnQ2lZ4\nZQuar1i2/vjgw6QToJ0LQUSoDlWAIsSfc1sjQPGW3HDKZCTFjYixkNgJQ4eaMCbH\nZIUkqRAABpZjvrapdjuPr8EaZms7gqDeJ8WBUdUAnUWXrsFgbdGbwJt0lTr+gKI9\nt4TCB7cSYQJ76kUEf4NbJ78XsesCgYEAxW/kY+S5fb60JPYomzCUflRwEcazQhnB\nN7I4TqSh51RAxMBEoToLNX8ejd7aStFi/BHQ0CDH4nKVu/ElrgEKFLALnCN5bSKG\nyZrez4ixqr4yYbwXNpmx1bMT4yW0tEuP+P5BrxtcUdCg6783ZJhv0GbM744mv2IT\ndISRZGu/1RUCgYAhaNgl1NwL3gPTQOuDEZNyASoOz8L6+DssWXlWXbECoS9lBUVx\nhdxUNRFf3bapFovA/VS+g2WJyoAYNNT1/w+UGwcWDaoSyyEmQQIEgrkVY7Wha0rp\n1fSx/DWcmaYZzKxJeWOJCTnpn4PSkk10h3ncEEQY3XJ+cJMbZfAYprh5vwKBgHbP\npLLwWyducV2kvD7XdySmj2JDHnByqzFnEDdTHxLkQq2jvij/kus4ehyJtPc9FcH2\nvp/geiitJ6oXrcMOYRCd9/6X4mRoWJBNoDNnYPMMzai+1YFH8BpXT2prsFim+Baw\nerpzTpT1ZUE4qo7gpLjWhBDMjEizLl35wLybkq8ZAoGBAIf8OBhj9m5Z6C8eXjnW\nFO1J1dyYPYC4s+oAmTEVDWO0iv42nKVX8PRMTME/K5Z9ioO05grnxhKLSBl7nZwh\nDhSjV9RsNLszhk+1wtcbttFYROhijxd/armugZtil9IpwXV5DjVmFdEjwgL+D77T\npSsuYSAqUldsZ8wdOazxJ+Kn\n-----END PRIVATE KEY-----\n', 'client_email': '91763804-compute@developer.gserviceaccount.com', 'client_id': '110656280879165196440', 'auth_uri': 'https://accounts.google.com/o/oauth2/auth', 'token_uri': 'https://accounts.google.com/o/oauth2/token', 'auth_provider_x509_cert_url': 'https://www.googleapis.com/oauth2/v1/certs', 'client_x509_cert_url': 'https://www.googleapis.com/robot/v1/metadata/x509/91763804-compute%40developer.gserviceaccount.com'}";
            using (var stream = json.ConvertStringToStream())
            {
                string[] scopes = { VisionService.Scope.CloudPlatform };
                var credential = GoogleCredential.FromStream(stream);
                credential = credential.CreateScoped(scopes);
                return credential;
            }
        }

        public VisionService CreateService()
        {
            GoogleCredential credential = CreateCredential();
            var service = new VisionService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
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
