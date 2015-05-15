using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace klukk_social.Services
{
    public static class Helpers
    {
        /// <summary>
        /// Sends a given message as an email to a given email address
        /// </summary>
        /// <param name="message"></param>
        /// <param name="contactMail"></param>
        public static void LogMessage(string message, string contactMail)
        {
            string mailSubject = ConfigurationManager.AppSettings["ReportSubject"];

            using (MailMessage email = new MailMessage())
            {

                email.To.Add(contactMail);
                email.Subject = mailSubject;
                email.Body = message;
                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = true;
                    client.Send(email);
                }
            }
        }
        /// <summary>
        /// Takes a datetime and returns a string saying how long it has been since
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToFriendlyDateString(this DateTime date)
        {
            TimeSpan lengthOfTime = DateTime.Now.Subtract(date);
            if (lengthOfTime.Minutes == 0)
                return "Fyrir minna en mínútu";
            else if (lengthOfTime.Hours == 0)
                return "Fyrir " + lengthOfTime.Minutes + " mínútum";
            else if (lengthOfTime.Days == 0)
                return "Fyrir " + lengthOfTime.Hours + " tímum";
            else
                return "Fyrir " + lengthOfTime.Days + " dögum";
        }
        /// <summary>
        /// Used for parsing text for youTube and image links and embedding the links it finds
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
                output = output.Replace(match.Value, "<iframe width='480' height='390' src='http://www.youtube.com/embed/" + youTubeId + "'  frameborder='0' allowfullscreen></iframe>");
            }
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