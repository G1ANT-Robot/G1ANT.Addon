using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace G1ANT.UiFramework.Utils
{/// <summary>
/// Class that represents access path of the control
/// </summary>
    public static class WPath
    {
        private static bool IsSlash(String s)
        {
            return (s.Equals("\\"));
        }
        public static string MakeWPath(XmlDocument doc, TreeNode node)
        {
            string wPath = string.Empty;
            Regex regex = new Regex("<(.*?)>");
            MatchCollection matches = Regex.Matches(node.FullPath, "<(.*?)>");
            for (int i = 1; i < matches.Count; i++)
            {
                string wpathnode = string.Empty;
                regex = new Regex("<(.*?) ");
                Group typename = regex.Match(matches[i].Value).Groups[1];
                regex = new Regex("Id=\"(.*?)\"");
                Group id = regex.Match(matches[i].Value).Groups[1];
                regex = new Regex("Name=\"(.*?)\"");
                Group name = regex.Match(matches[i].Value).Groups[1];
                regex = new Regex("Type=\"(.*?)\"");
                Group controltype = regex.Match(matches[i].Value).Groups[1];
                regex = new Regex("Index=\"(.*?)\"");
                Group index = regex.Match(matches[i].Value).Groups[1];
                if (typename.Success)
                {
                    wpathnode = $"{typename.Value}[";
                }
                if (name.Success)
                {
                    wpathnode = wpathnode + $"Name=\"{name.Value}\" ";
                }
                if (id.Success)
                {
                    wpathnode = wpathnode + $"Id=\"{id.Value}\" ";
                }
                if (controltype.Success)
                {
                    wpathnode = wpathnode + $"Type=\"{controltype.Value}\" ";
                }
                if (index.Success)
                {
                    wpathnode = wpathnode + $"Index=\"{index.Value}\" ";
                }
                wpathnode = wpathnode.TrimEnd(' ');
                wpathnode = wpathnode + "]";
                if (wPath == string.Empty)
                    wPath = $"{wpathnode}";
                else
                    wPath = $"{wPath}\\{wpathnode}";
            }
            return wPath;
        }
    }
}