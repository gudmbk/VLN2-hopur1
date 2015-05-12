using System.Text.RegularExpressions;

namespace klukk_social.Services
{
    public static class Helpers
    {
        public static string ParseText(string input)
        {
            string output = input;
            Regex youTube = new Regex(@"(https?:)?//?www.?youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");
            Regex imgRegx = new Regex(@"(https?:)?//?[^\'<>]+?\.(jpg|jpeg|gif|png)", RegexOptions.IgnoreCase);

            MatchCollection mactches = youTube.Matches(output);
            MatchCollection imgMactches = imgRegx.Matches(output);

            foreach (Match match in mactches)
            {
                var youTubeId = Regex.Match(input, @"(?:youtube\.com\/(?:[^\/]+\/.+\/|(?:v|e(?:mbed)?)\/|.*[?&amp;]v=)|youtu\.be\/)([^""&amp;?\/ ]{11})").Groups[1].Value;
                output = output.Replace(match.Value, "<iframe width='480' height='390' src='http://www.youtube.com/embed/" + youTubeId + "'  frameborder='0' allowfullscreen></iframe>");            }
            foreach (Match match in imgMactches)
            {
                output = output.Replace(match.Value, "<img src='" + match.Value + "' alt='No image to display' />");
            }
            return output;
        }
    }
}