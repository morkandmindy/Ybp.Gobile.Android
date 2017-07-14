using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

using Android.Graphics;

namespace Ybp.Gobile.Android
{
    public class Utilities
    {
        public static async Task<String> MakeAjaxRequestAsync(string url, string requestBody, string requestType)
        {
            var responseText = "";
            var success = false;
            while (!success)//TODO: have it stop after n tries
            {
                try
                {
                    // Create an HTTP web request using the URL:
                    var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                    request.ContentType = "application/json; charset=utf-8";
                    request.Method = "POST";
                    request.CookieContainer = Constants.CookieContainer;
                    // this is important - make sure you specify type this way
                    request.ContentType = requestType;
                    request.Accept = "application/json";
                    request.Timeout = 2000;

                    using (var streamWriter = new StreamWriter(await request.GetRequestStreamAsync()))
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
                        success = true;
                    }
                }
                catch (Exception e)
                {
                    // Just catching this for now so we can see the exception.
                    Console.WriteLine("Exception e {0}", e.ToString());
                    //throw e;
                }
            }
            return responseText;
        }

        public static Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                byte[] imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }
}
