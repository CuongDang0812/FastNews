using System;
using System.Text;
using System.Text.RegularExpressions;

namespace FastNews.Common
{
    public class ConvertToUnSign
    {
        public static string utf8Convert(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').Replace(' ', '-').ToLower();
        }
    }
}