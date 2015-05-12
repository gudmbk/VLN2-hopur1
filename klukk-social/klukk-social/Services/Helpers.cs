using System.Text.RegularExpressions;

namespace klukk_social.Services
{
    public static class Helpers
    {
        public static string FormatUrls(string input)
        {
            string output = input;
            Regex regx = new Regex(@"(https?:)?//?[^'""<>]+?\.(jpg|jpeg|gif|png)", RegexOptions.IgnoreCase);

            MatchCollection mactches = regx.Matches(output);

            foreach (Match match in mactches)
            {
                output = output.Replace(match.Value, "<a href='" + match.Value + "' target='blank'>" + match.Value + "</a>");
            }
            return output;
        }

        public static string FormatImages(string input)
        {
            string output = input;
            Regex regx = new Regex(@"(https?:)?//?[^\'<>]+?\.(jpg|jpeg|gif|png)", RegexOptions.IgnoreCase);

            MatchCollection mactches = regx.Matches(output);

            foreach (Match match in mactches)
            {
                output = output.Replace(match.Value, "<img src='" + match.Value + "' alt='No image to display' />");
            }
            return output;
        }
    }
}