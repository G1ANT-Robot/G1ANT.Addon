/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.GoogleDocs
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/
using G1ANT.Language;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Download;
using Google.Apis.Drive.v3;

namespace G1ANT.Addon.GoogleDocs
{
    public class SheetsWrapper
    {
        private SheetsWrapper() { }
        public SheetsWrapper(int id)
        {
            this.Id = id;
        }
        public int Id { get; set; }
        static string[] scopes = { SheetsService.Scope.Spreadsheets, SheetsService.Scope.Drive, SheetsService.Scope.DriveFile};
        static string ApplicationName = "G1ANT Robot";
        UserCredential credential;
        SheetsService service;
        string spreadSheetId = null;
        string range = null;
        string[] rangeMultiple = null;
        SpreadsheetsResource.ValuesResource.GetRequest Request;
        public string spreadSheetName = null;
        Spreadsheet spreadsheet;
        public List<Sheet> sheets;

        private void CreateCredential(bool isShared = true)
        {
            string json = "{'installed':{'client_id':'823414569447-orhf4brgs1r9puic2jg73n98ei2qp7ph.apps.googleusercontent.com','project_id':'docsapi-168607','auth_uri':'https://accounts.google.com/o/oauth2/auth','token_uri':'https://accounts.google.com/o/oauth2/token','auth_provider_x509_cert_url':'https://www.googleapis.com/oauth2/v1/certs','client_secret':'7y8Z8zfHELMrNLEPGh8Dh6PZ','redirect_uris':['urn:ietf:wg:oauth:2.0:oob','http://localhost']}}";
            using (var stream = json.ConvertStringToStream())
            {
                string credPath = System.Environment.GetFolderPath(
                        System.Environment.SpecialFolder.Personal);
                switch (isShared)
                {
                    case false:
                        credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-dotnet-quickstart.json");
                        break;
                    default:
                        break;
                }

                try
                {
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                      GoogleClientSecrets.Load(stream).Secrets,
                      scopes,
                      "G1ANT_Robot",
                      CancellationToken.None,
                      new FileDataStore(credPath, true)).Result;
                }
                catch (AggregateException e)
                {

                }

            }
        }
        public string GetSpreadSheetId()
        {
            return spreadSheetId;
        }
        private void CreateService()
        {
            
                service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
            
        }
        private Spreadsheet GetSpreadsheetName()
        {
            
           var spreadShitTemp = service.Spreadsheets.Get(spreadSheetId);
           return spreadShitTemp.Execute();

        }
        private List<Sheet> GetSheetsList()
        {
            List<Sheet> sheets = new List<Sheet>();
            Spreadsheet tempSpreadSheet = GetSpreadsheetName();
            for (int i = 0; i < tempSpreadSheet.Sheets.Count; i++)
            {
                sheets.Add(tempSpreadSheet.Sheets[i]);
            }
            return sheets;
        }
        public void AssignSpreadSheet()
        {
          
            spreadsheet = GetSpreadsheetName();
            sheets = GetSheetsList();
            spreadSheetName = spreadsheet.Properties.Title;
        }
        public void SetNewTitle(string title)
        {
            Request mainReq = new Request();
            UpdateSpreadsheetPropertiesRequest updSpreadSheetReq = new UpdateSpreadsheetPropertiesRequest();
            updSpreadSheetReq.Properties = new SpreadsheetProperties();
            mainReq.UpdateSpreadsheetProperties = updSpreadSheetReq;

            BatchUpdateSpreadsheetRequest batchUpdate = new BatchUpdateSpreadsheetRequest()
            {
                Requests = new List<Request>() { mainReq }
            };
            batchUpdate.Requests[0].UpdateSpreadsheetProperties.Fields = "Title";
            batchUpdate.Requests[0].UpdateSpreadsheetProperties.Properties.Title = title;

            new SpreadsheetsResource.BatchUpdateRequest(service, batchUpdate, spreadSheetId).Execute();

            spreadSheetName = title;
            spreadsheet.Properties.Title = title;


        }
        public void Open(string spreadSheetID, bool isShared = true)
        {
            spreadSheetId = spreadSheetID;
            CreateCredential(isShared);
            CreateService();
            AssignSpreadSheet();
        }
        public ValueRange GetValueOLD(string cell, string sheetName = "")
        {
            range = sheetName + "!" + cell;
            Request = service.Spreadsheets.Values.Get(spreadSheetId, range);
            return Request.Execute();
        }
        public BatchGetValuesResponse GetValue(string cell, string sheetName = "")
        {
            string[] cells = cell.Split('&');
            string[] rangMult = new string[cells.Length];
            for (int i = 0; i < cells.Length; i++)
            {
                rangMult[i] = sheetName + "!" + cells[i];
            }
            //
            SpreadsheetsResource.ValuesResource.BatchGetRequest BatchGetRequest = service.Spreadsheets.Values.BatchGet(spreadSheetId);
            BatchGetRequest.Ranges = rangMult;
            BatchGetValuesResponse response= BatchGetRequest.Execute();
            return response;
        }
        public string FindValue(string value, string sheetName = "")
        {
            range = sheetName + "!A1:A200";
            Request = service.Spreadsheets.Values.Get(spreadSheetId, range);
            var val = Request.Execute();
            
            if(val.Values != null)
            {
                for(int i = 1; i <= val.Values.Count;i++)
                {
                    string s = val.Values[i][0].ToString();

                    if(s == value)
                    {
                        return "A" + (i+1).ToString();
                    }
                }
            }
            
            return string.Empty ;
        }
        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }
        public string FindAll(string in_value, string sheetName = "")
        {
            range = sheetName;
            Request = service.Spreadsheets.Values.Get(spreadSheetId, range);
            var val = Request.Execute();
            IList<IList<Object>> values = val.Values;
            IList<Object> rows; 
            List<string> addr = new List<string>();
            string res=null;
            //int ind=1;
            if (values != null && values.Count > 0)
            {
                //foreach (var row in values)
                for (int i1=0;i1<values.Count;i1++)
                {
                    rows = values[i1];
                    //foreach (var cell in row)
                    for (int i2 = 0; i2 < rows.Count; i2++)
                    {
                        
                        //string s = val.Values[i][0].ToString();
                        if (rows[i2].ToString() == (in_value))
                        {
                            res = (res==null)?(GetExcelColumnName(i2 + 1) + (i1 + 1).ToString()):(res+"&"+GetExcelColumnName(i2 + 1) + (i1 + 1).ToString());
                        }

                    }
                    
                }
                return (res == null) ? string.Empty : res;

            }

            return string.Empty;
        }
        public string FindFirst(string in_value, string sheetName = "")
        {
            range = sheetName;
            Request = service.Spreadsheets.Values.Get(spreadSheetId, range);
            var val = Request.Execute();
            IList<IList<Object>> values = val.Values;
            IList<Object> rows;
            List<string> addr = new List<string>();
            string res = null;
            //int ind=1;
            if (values != null && values.Count > 0)
            {
                //foreach (var row in values)
                for (int i1 = 0; i1 < values.Count; i1++)
                {
                    rows = values[i1];
                    //foreach (var cell in row)
                    for (int i2 = 0; i2 < rows.Count; i2++)
                    {
                        //string s = val.Values[i][0].ToString();
                        if (rows[i2].ToString() == (in_value))
                        {
                            return GetExcelColumnName(i2 + 1) + (i1 + 1).ToString();
                        }

                    }

                }
                return (res==null)?string.Empty:res;

            }

            return string.Empty;
        }
        public void SetValue(string cell, string value,string sheetName = null, bool numeric = false )
        {
            object v = value;
            if (numeric)
            {
                value = value.Contains("£") ? value.Replace("£", "") : value;
                value = value.Contains(",") ? value.Replace(",", ""): value;
                v = double.Parse(value);
            }


            range = sheetName + "!" + cell;
            var tempBody = new List<object>() { v };
            ValueRange bodyTemp = new ValueRange();
            bodyTemp.Values= new List<IList<object>> { tempBody };
            
            SpreadsheetsResource.ValuesResource.UpdateRequest update = service.Spreadsheets.Values.Update(bodyTemp, spreadSheetId, range);
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
           // update.ResponseValueRenderOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ResponseValueRenderOptionEnum.FORMATTEDVALUE;
           // update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.INPUTVALUEOPTIONUNSPECIFIED;
            
            //UpdateValuesResponse result2 = update.Execute();
            update.Execute();
        }
        public string DownloadFile(string path, string type = @"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            string format = null;
            if (type.Contains("pdf"))
            {
                format = @"application/pdf";
            } else
             
            {
                format = @"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            }

            var fileId = spreadSheetId;

            string result = null;
            var serviceDownload = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            //var req = serviceDownload.Files.Get(fileId);
            var req = serviceDownload.Files.Export(fileId, format);

            using (var stream = new System.IO.MemoryStream())
            {
                req.MediaDownloader.ProgressChanged +=
                   (IDownloadProgress progress) =>
                   {
                       switch (progress.Status)
                       {
                           case DownloadStatus.Downloading:
                               {
                                   result = progress.BytesDownloaded.ToString();
                                   break;
                               }
                           case DownloadStatus.Completed:
                               {
                                   result = "Download complete.";
                                   break;
                               }
                           case DownloadStatus.Failed:
                               {
                                   result = "Download failed.";
                                   break;
                               }
                       }
                   };
                req.Download(stream);
                using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    stream.WriteTo(file);
                }
            }

            return result;
        }

    }
}
