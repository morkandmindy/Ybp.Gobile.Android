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
    public class PutInCartRequest
    {
        public string ContainerType => "BusyPod";
        public string ContainerId { get; set; }
        // ContainedItems is a CSV consisting of the ItemVendor primary key prefixed with a 'p'
        public string ContainedItems { get; set; }
    }
}