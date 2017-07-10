using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Ybp.Gobile.Android
{
    public class Utilities
    {

        public async Task<String> MakeAjaxRequestAsync(string url, string requestBody)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json; charset=utf-8";
            request.Method = "POST";

            // this is important - make sure you specify type this way
            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "application/json";
            //request.ContentLength = postBytes.Length;
            //request.CookieContainer = Cookies;
            //request.UserAgent = currentUserAgent;

            try
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(requestBody);
                    streamWriter.Flush();
                }
            }
            catch (Exception e)
            {
                // Just catching this for now so we can see the exception.
                System.Console.WriteLine("Exception e {0}", e.ToString());
                throw e;
            }

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    TextReader tr = new StreamReader(stream);
                    String str = tr.ReadToEnd();
                    return str;
                }
            }
        }
    }
}