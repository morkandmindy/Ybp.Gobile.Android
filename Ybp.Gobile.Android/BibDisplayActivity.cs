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

using Newtonsoft.Json;

using Ybp.Gobile.Android.Models;

namespace Ybp.Gobile.Android.Resources.layout
{
    [Activity(Label = "Gobile")]
    public class BibDisplayActivity : Activity
    {
        private TextView textViewIsbn;
        private TextView textViewTitle;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.BibDisplay);
            textViewIsbn = FindViewById<TextView>(Resource.Id.textViewIsbn);
            textViewTitle = FindViewById<TextView>(Resource.Id.textViewTitle);
            SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(Intent.GetStringExtra("response"));
            textViewTitle.Text = searchResult.BibData.TITLE;
            textViewIsbn.Text = searchResult.BibData.ISBN_13;
        }
    }
}