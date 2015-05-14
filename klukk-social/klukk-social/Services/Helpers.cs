﻿using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace klukk_social.Services
{
    public static class Helpers
    {
        public static string ToFriendlyDateString(this DateTime Date)
        {
            TimeSpan lengthOfTime = DateTime.Now.Subtract(Date);
            if (lengthOfTime.Minutes == 0)
                return "Fyrir minna en mínútu";
            else if (lengthOfTime.Hours == 0)
                return "Fyrir " + lengthOfTime.Minutes + " mínútum";
            else if (lengthOfTime.Days == 0)
                return "Fyrir " + lengthOfTime.Hours + " tímum";
            else
                return "Fyrir " + lengthOfTime.Days + " dögum";
        }
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
        public static string RenderViewToString(ControllerContext context, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = context.RouteData.GetRequiredString("action");

            ViewDataDictionary viewData = new ViewDataDictionary(model);

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                ViewContext viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}