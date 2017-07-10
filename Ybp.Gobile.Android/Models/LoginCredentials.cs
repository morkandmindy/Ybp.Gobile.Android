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

namespace Ybp.Gobile.Android.Models
{
    public class LoginCredentials
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Account { get; set; }
    }
}