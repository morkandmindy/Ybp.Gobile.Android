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

namespace Ybp.Gobile.Android
{
    public static class Constants
    {
        public static string BASE_URL = "http://172.27.11.172/hx/ajax.ashx?location=";
        public static string LOGIN_URL = BASE_URL + "login";
    }
}