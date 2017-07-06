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
    class SearchResult
    {
        public string Container { get; set; }
        public string ContainerType => "BusyPod";
        public List<ItemVendor> ItemVendors { get; set; }

        public BibData BibData { get; set; }
    }
}