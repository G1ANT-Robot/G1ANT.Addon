using System.Collections.Generic;
using System.Linq;
using Spire.Pdf;
using Spire.Pdf.Widget;
using Spire.Pdf.Fields;

namespace G1ANT.Addon.Pdf
{
    static public class PdfManager
    {
        private static int lastId = 0;
        private static int currentId = 0;
        private static Dictionary<int, PdfDocument> pdfDocuments = new Dictionary<int, PdfDocument>();

        static public int Open(string path)
        {
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(path);
            lastId++;
            pdfDocuments.Add(lastId, doc);
            currentId = lastId;
            return lastId;
        }

        static public void Close()
        {
            if (pdfDocuments.ContainsKey((currentId)))
            {
                pdfDocuments[currentId].Close();
                pdfDocuments.Remove(currentId);
                if (pdfDocuments.Count > 0)
                    currentId = pdfDocuments.First().Key;
                else
                    currentId = 0;
            }
        }

        static public void SaveAs(string path)
        {
            if (pdfDocuments.ContainsKey((currentId)))
            {
                var doc = pdfDocuments[currentId];
                doc.SaveToFile(path);
            }
        }

        static public void Switch(int id)
        {
            if (pdfDocuments.ContainsKey(id))
                currentId = id;
        }

        static public void SetFormField(string name, string value)
        {
            if (pdfDocuments.ContainsKey(currentId))
            {
                var doc = pdfDocuments[currentId];
                PdfFormWidget formWidget = doc.Form as PdfFormWidget;

                for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                {
                    PdfField field = formWidget.FieldsWidget.List[i] as PdfField;
                    if (name == field.Name)
                    {
                        if (field is PdfTextBoxFieldWidget textField)
                        {
                            textField.Text = value;
                        }
                        else if (field is PdfListBoxWidgetFieldWidget listField)
                        {
                            listField.SelectedValue = value;
                        }
                        else if (field is PdfComboBoxWidgetFieldWidget comboField)
                        {
                            comboField.SelectedValue = value;
                        }
                        else if (field is PdfCheckBoxWidgetFieldWidget checkBoxField)
                        {
                            checkBoxField.Checked = IsTrue(value);
                        }
                    }
                }
            }
        }

        static public bool IsTrue(string value)
        {
            value = value.Replace("(bool)", "").Replace("(int)", "").Replace("(str)", "").Replace("(float)", "");
            return (value != "" && "0123456789".Contains(value[0]) && value != "0") ||
                   value == "true" || value == "yes" || value == "on";
        }

    }
}
