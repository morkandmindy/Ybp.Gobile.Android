using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Ybp.Gobile.Android
{
    public class Utilities
    {
        public async Task<String> MakeAjaxRequestAsync(string url, string requestBody, string requestType)
        {
            string responseText = "";
            // Create an HTTP web request using the URL:
            var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "POST";
            request.CookieContainer = Constants.CookieContainer;
            // this is important - make sure you specify type this way
            request.ContentType = requestType;
            request.Accept = "application/json";
            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(requestBody);
                    streamWriter.Flush();
                }
                // Send the request to the server and wait for the response:
                using (var response = (HttpWebResponse)(await request.GetResponseAsync()))
                {
                    // Get a stream representation of the HTTP web response:
                    using (Stream stream = response.GetResponseStream())
                    {
                        TextReader tr = new StreamReader(stream);
                        responseText = tr.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                // Just catching this for now so we can see the exception.
                Console.WriteLine("Exception e {0}", e.ToString());
                //throw e;
            }
            return responseText;
        }
    }
}
