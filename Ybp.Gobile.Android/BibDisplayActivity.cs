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
        private TextView textViewAuthor;
        private TextView textViewTitle;
        private ImageView imageViewBookCover;
        private TextView textViewPublisher;
        private TextView textViewPubYear;
        private TextView textViewBinding;
        private TextView textViewPagination;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.BibDisplay);

            textViewAuthor = FindViewById<TextView>(Resource.Id.textViewAuthor);
            textViewTitle = FindViewById<TextView>(Resource.Id.textViewTitle);
            textViewPublisher = FindViewById<TextView>(Resource.Id.textViewPublisher);
            textViewPubYear = FindViewById<TextView>(Resource.Id.textViewPubYear);
            textViewBinding = FindViewById<TextView>(Resource.Id.textViewBinding);
            textViewPagination = FindViewById<TextView>(Resource.Id.textViewPagination);

            imageViewBookCover = FindViewById<ImageView>(Resource.Id.imageViewBookCover);

            SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(Intent.GetStringExtra("response"));

            textViewTitle.Text = searchResult.BibData.TITLE;
            textViewAuthor.Text = searchResult.BibData.AUTHOR_NAME;
            textViewPublisher.Text = "Publisher: " + searchResult.BibData.PUBLISHER_NAME;
            textViewPubYear.Text = "Pub Year: " + searchResult.BibData.PUBLICATION_YEAR;
            textViewBinding.Text = "Binding: " + searchResult.BibData.BINDING_TEXT;
            textViewPagination.Text = "Pagination: " + searchResult.BibData.PAGINATION;

            var bookCover = Utilities.GetImageBitmapFromUrl(Constants.BOOK_COVER_VIEW_URL + searchResult.BibData.ISBN_13);
            imageViewBookCover.SetImageBitmap(bookCover);

        }
    }
}