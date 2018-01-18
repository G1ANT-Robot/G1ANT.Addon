using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
namespace G1ANT.Addon.Xls
{
    public class XlsWrapper
    {
        private XlsWrapper() { }

        public XlsWrapper(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
        private SpreadsheetDocument spreadsheetDocument = null;
        private Sheet sheet;
        private WorkbookPart wbPart;

        public Sheet GetSheetByName(string name)
        {
            var sheets = wbPart.Workbook.Sheets.Cast<Sheet>().ToList();
            return sheets.Find(x => x.Name == name);
        }


        public List<String> GetSheetsNames()
        {
            List<string> names = new List<string>();
            var sheets = wbPart.Workbook.Sheets.Cast<Sheet>().ToList();
            foreach (Sheet sh in sheets)
            {
                names.Add(sh.Name);
            }
            return names;
        }

        public int CountRows()
        {
            //WorksheetPart worksheetPart = wbPart.WorksheetParts.First();
            //SheetData sheetData = sheet.Descendants<SheetData>().First();
            int a = 0;// sheetData.Elements<Row>().Count();

            //IEnumerable<Row> row = sheetData.Elements<Row>();
            //a = row.Count();
            IEnumerable<WorksheetPart> worksheetPart = wbPart.WorksheetParts;
            WorksheetPart wsPart =
             (WorksheetPart)(wbPart.GetPartById(sheet.Id));
            Worksheet worksheet = wsPart.Worksheet;
            //find sheet data
            SheetData sheetData = worksheet.Elements<SheetData>().First();
            // Iterate through every sheet inside Excel sheet

            IEnumerable<Row> row = sheetData.Elements<Row>(); // Get the row IEnumerator
            a = row.Count(); // Will give you the count of rows

            return a;
        }

        public void SetValue(int row, string column, string value)
        {
            var Position = FormatInput(column, row);

            WorksheetPart wsPart =
             (WorksheetPart)(wbPart.GetPartById(sheet.Id));
            Cell theCell = wsPart.Worksheet.Descendants<Cell>().
         Where(c => c.CellReference == Position.ToUpper()).FirstOrDefault();

            if (theCell != null)
            {
                setCellValue(value, theCell);
                spreadsheetDocument.WorkbookPart.Workbook.CalculationProperties.ForceFullCalculation = true;
                spreadsheetDocument.WorkbookPart.Workbook.CalculationProperties.FullCalculationOnLoad = true;
            }
            else
            {
                Worksheet worksheet = wsPart.Worksheet;
                SheetData sheetData = worksheet.GetFirstChild<SheetData>();
                Row newrow;

                newrow = CheckForRow(Convert.ToUInt32(row), wsPart);
                theCell = CheckForCell(ColumnNumberToLetter(column), newrow);
                setCellValue(value, theCell);
                spreadsheetDocument.WorkbookPart.Workbook.CalculationProperties.ForceFullCalculation = true;
                spreadsheetDocument.WorkbookPart.Workbook.CalculationProperties.FullCalculationOnLoad = true;
            }
        }

        public string GetValue(int row, string column)
        {
            string Position = FormatInput(column, row);
            WorksheetPart wsPart = (WorksheetPart)(wbPart.GetPartById(sheet.Id));
            Cell theCell = wsPart.Worksheet.Descendants<Cell>().Where(c => c.CellReference == Position.ToUpper()).FirstOrDefault();

            if (theCell != null)
            {
                if (theCell.DataType != null && theCell.DataType.Value == CellValues.SharedString)
                {

                    return wbPart.SharedStringTablePart.SharedStringTable.ElementAt(Int32.Parse(theCell.CellValue.InnerText)).InnerText.ToString();
                }
                else if (theCell.StyleIndex == "3")
                {
                    double oaDateAsDouble;
                    if (double.TryParse(theCell.InnerText.ToString(), out oaDateAsDouble))
                        return DateTime.FromOADate(oaDateAsDouble).ToString();

                }
                else if (theCell.StyleIndex == "6")
                {

                    double intres = 0;
                    if (double.TryParse(theCell.InnerText.ToString(), out intres))
                        return ((intres * 100 + "%").ToString());

                }
                else
                {
                    return theCell?.CellValue?.InnerText?.ToString() ?? string.Empty;
                }
                return string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }

        public string FormatInput(string column, int row)
        {
            var position = string.Empty;
            position += ColumnNumberToLetter(column);
            position += row.ToString();
            return position;
        }
        public int[] FormatInput(string position)
        {
            int[] result = new int[2];
            var lettersOnly = position.TakeWhile(x => !Char.IsDigit(x)).ToArray();
            result[0] = FormatLetterToNumber(lettersOnly);
            var lol = position.SkipWhile(x => !Char.IsDigit(x)).ToArray();
            result[1] = Int32.Parse(new string(lol));
            return result;
        }
        private string ColumnNumberToLetter(string column)
        {
            var position = string.Empty;
            int columnToConvert = 0;
            var newBase = 26;
            if (Int32.TryParse(column, out columnToConvert))
            {
                var baseRange = Enumerable.Range('A', newBase).ToArray();
                do
                {
                    columnToConvert--;
                    position = (char)baseRange[columnToConvert % newBase] + position;
                    columnToConvert = columnToConvert / newBase;
                } while (columnToConvert > 0);
            }
            else
            {
                position += column.ToUpper();
            }
            return position;
        }
        private int FormatLetterToNumber(char[] position)
        {
            var oldBase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int column = 0; //because we have 1-based starting index
            var reversed = position.Reverse().ToArray();
            for (int i = reversed.Length - 1; i >= 0; i--)
            {
                column += (oldBase.IndexOf(reversed[i]) + 1) * (int)Math.Pow(26, i);
            }
            return column;
        }
        private Row CheckForRow(uint index, WorksheetPart wsPart)
        {

            if (wsPart.Worksheet.GetFirstChild<SheetData>().Elements<Row>().Where(r => r.RowIndex == index).Count() != 0)
            {
                return wsPart.Worksheet.GetFirstChild<SheetData>().Elements<Row>().Where(r => r.RowIndex == index).First();
            }
            else
            {
                Row row = new Row() { RowIndex = index };
                wsPart.Worksheet.GetFirstChild<SheetData>().Append(row);
                return row;
            }
        }
        private void setCellValue(string val, Cell cell)
        {
            int v = 0;
            decimal d = 0.0000M;
            const int maxCellLength = 32000;
            if (Int32.TryParse(val, out v))
            {
                cell.CellValue = new CellValue(val);
                cell.DataType = new DocumentFormat.OpenXml.EnumValue<CellValues>(CellValues.Number);
            }
            else if (decimal.TryParse(val, out d))
            {
                cell.CellValue = new CellValue(val);
                cell.DataType = new DocumentFormat.OpenXml.EnumValue<CellValues>(CellValues.Number);
            }
            else if (DateTime.TryParse(val, out DateTime date))
            {
                cell.CellValue = new CellValue(date.ToOADate().ToString());
                cell.StyleIndex = 1;
            }

            else
            {

                if (val.Length >= maxCellLength)
                {
                    cell.CellValue = new CellValue(val.Substring(0, maxCellLength));
                    cell.AddAnnotation("this text has been truncated to 32000 characters due to excel's cell limit");
                }
                else
                {
                    cell.CellValue = new CellValue(val);
                }
                cell.DataType = new DocumentFormat.OpenXml.EnumValue<CellValues>(CellValues.String);
            }
        }

        private Cell CheckForCell(string column, Row row)
        {
            string position = column + row.RowIndex;
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == position).Count() > 0)
            {
                Cell cell = row.Elements<Cell>().Where(c => c.CellReference.Value == position).First();
                return cell;
            }
            else
            {
                Cell refCell = null;
                foreach (Cell cella in row.Elements<Cell>())
                {
                    if (cella.CellReference.Value.Length == position.Length)
                    {
                        if (string.Compare(cella.CellReference.Value, position, true) > 0)
                        {
                            refCell = cella;
                            break;
                        }
                    }
                }
                Cell newCell = new Cell() { CellReference = position };
                row.InsertBefore(newCell, refCell);
                return newCell;
            }
        }

        public void ActivateSheet(string name)
        {
            Sheet foundSheet = GetSheetByName(name);
            if (foundSheet == null)
            {
                throw new InvalidOperationException("Attempt to set null as active sheet");
            }
            sheet = GetSheetByName(name);
        }

        public bool Open(string filePath, string accessMode = "ReadWrite")
        {
            if (string.IsNullOrEmpty(accessMode))
            {
                accessMode = "ReadWrite";
            }

            FileAccess access;
            if (Enum.TryParse(accessMode, true, out access) == false)
            {
                throw new ArgumentOutOfRangeException(nameof(accessMode), accessMode, "Accessmode specified an invalid value");
            }

            Package spreadsheetPackage = Package.Open(filePath, FileMode.Open, access);
            try
            {
                spreadsheetDocument = SpreadsheetDocument.Open(spreadsheetPackage);
                wbPart = spreadsheetDocument.WorkbookPart;
                ActivateSheet(GetSheetsNames()[0]);
            }
            catch
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, access))
                {
                    UriFixer.FixInvalidUri(fs, brokenUri => FixUri(brokenUri));
                }
                spreadsheetPackage = Package.Open(filePath, FileMode.Open, access);
                spreadsheetDocument = SpreadsheetDocument.Open(spreadsheetPackage);
                wbPart = spreadsheetDocument.WorkbookPart;
                ActivateSheet(GetSheetsNames()[0]);
                remhyp();
            }

            if (spreadsheetDocument != null) return true;
            else return false;
        }

        /// <summary>
        /// Close underlying file and save changes if it was opened with write access.
        /// </summary>
        public void Close()
        {
            try
            {
                spreadsheetDocument.Close();
            }
            catch { }
        }

        private static Uri FixUri(string brokenUri)
        {
            return new Uri("http://broken-link/");
        }

        public string Find(string value)
        {
            WorksheetPart wsPart =
           (WorksheetPart)(wbPart.GetPartById(sheet.Id));
            int v = 0;
            if (Int32.TryParse(value, out v))
            {
                Cell cell = wsPart.Worksheet.Descendants<Cell>().
              Where(c => c.StyleIndex == 1 && c.CellValue.InnerText.ToString() == value).First();
                if (cell != null)
                    return cell.CellReference.ToString();
                else
                    return null;

            }
            else if (value.ToString().Contains("%"))
            {
                Cell cell = wsPart.Worksheet.Descendants<Cell>().
              Where(c => c.StyleIndex != null && c.StyleIndex == 6 || c.StyleIndex == 2 && c.CellValue.InnerText.ToString() == (double.Parse(value.ToString().TrimEnd('%')) / 100).ToString()).First();
                if (cell != null)
                {
                    return cell.CellReference.ToString();
                }
                else
                {
                    return null;
                }
            }
            else if (DateTime.TryParse(value, out DateTime d))
            {
                try
                {
                    Cell cell = wsPart.Worksheet.Descendants<Cell>().
                         Where(c => c.StyleIndex != null && c.StyleIndex == 1 && c.CellValue.InnerText.ToString() == d.ToOADate().ToString()).First();
                }
                catch (Exception ex)
                {
                    var va = wbPart.SharedStringTablePart.SharedStringTable.ToList();
                    if
                        (va.Exists(xk => xk.InnerText == value))
                    {
                        int i = 0;
                        foreach (SharedStringItem ssi in va)
                        {
                            if (ssi.InnerText == value)
                            {
                                Cell cell = wsPart.Worksheet.Descendants<Cell>().
                  Where(c => c.CellValue != null && c.CellValue.InnerText.ToString() == i.ToString() && c.DataType?.Value == CellValues.SharedString).First();
                                return cell.CellReference.ToString();
                            }
                            i++;
                        }

                    }
                    else { return null; }

                }
            }
            else
            {
                var va = wbPart.SharedStringTablePart.SharedStringTable.ToList();
                if
                    (va.Exists(xk => xk.InnerText == value))
                {
                    int i = 0;
                    foreach (SharedStringItem ssi in va)
                    {
                        if (ssi.InnerText == value)
                        {
                            Cell cell = wsPart.Worksheet.Descendants<Cell>().
              Where(c => c.CellValue != null && c.CellValue.InnerText.ToString() == i.ToString() && c.DataType?.Value == CellValues.SharedString).First();
                            return cell.CellReference.ToString();
                        }
                        i++;
                    }

                }
                else { return null; }
            }
            return null;
        }

        public void remhyp()
        {
            Uri z = new Uri("http://broken-link/");
            WorksheetPart wsPart =
            (WorksheetPart)(wbPart.GetPartById(sheet.Id));
            var hyperLinks = wsPart.Worksheet.Descendants<Hyperlinks>().First();
            var hyperRel = wsPart.HyperlinkRelationships.Where(c => c.Uri == z).FirstOrDefault();
            foreach (Hyperlink item in hyperLinks)
            {
                if (hyperRel.Id == item.Id)
                {
                    wsPart.DeleteReferenceRelationship(item.Id.ToString());

                    item.Remove();
                }
                if (hyperLinks.Count() == 0)

                    hyperLinks.Remove();

            }
            wsPart.Worksheet.Save();
        }
    }
}

