using System.Linq;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Views;

using Newtonsoft.Json;

using Ybp.Gobile.Android.Models;
using System;

namespace Ybp.Gobile.Android.Resources.layout
{
    [Activity(Label = "Gobi2Go")]
    public class BibDisplayActivity : Activity
    {
        private ImageView imageViewBookCover;
        private TextView textViewAuthor;
        private TextView textViewBinding;
        private TextView textViewPagination;
        private TextView textViewPublisher;
        private TextView textViewPubYear;
        private TextView textViewTitle;
        private Button Button1;
        private ProgressBar progressBar;

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
            Button1 = FindViewById<Button>(Resource.Id.button1);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBarAddCart);

            imageViewBookCover = FindViewById<ImageView>(Resource.Id.imageViewBookCover);

            progressBar.Visibility = ViewStates.Invisible;

            SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(Intent.GetStringExtra("response"));

            textViewTitle.Text = searchResult.BibData.TITLE;
            textViewAuthor.Text = searchResult.BibData.AUTHOR_NAME;
            textViewPublisher.Text = "Publisher: " + searchResult.BibData.PUBLISHER_NAME;
            textViewPubYear.Text = "Pub Year: " + searchResult.BibData.PUBLICATION_YEAR;
            textViewBinding.Text = "Binding: " + searchResult.BibData.BINDING_TEXT;
            textViewPagination.Text = "Pagination: " + searchResult.BibData.PAGINATION;

            var bookCover = Utilities.GetImageBitmapFromUrl(Constants.BOOK_COVER_VIEW_URL + searchResult.BibData.ISBN_13);
            imageViewBookCover.SetImageBitmap(bookCover);

            //lets just pretend the user clicked on first item

            var putInCart = new PutInCartRequest()
                            {
                                ContainedItems =
                                    searchResult.ItemVendors[0]
                                    .PK_ITEM,
                                ContainerId =
                                    searchResult.Container
                            };

            Button1.Click += async (sender, e) =>
                                   {
                                       progressBar.Visibility = ViewStates.Visible;
                                       var response =
                                           await Utilities.MakeAjaxRequestAsync(
                                               Constants.BASE_URL + "app_putincart&cart=OrderCart",
                                               putInCart.ToRequest(),
                                               Constants.FORM_DATA);

                                       progressBar.Visibility = ViewStates.Invisible;

                                       
                                       try
                                       {
                                           CartResponseStatus cartResponseStatus = JsonConvert.DeserializeObject<CartResponseStatus>(response);
                                           if (cartResponseStatus.Status.Equals("success"))
                                           {
                                               DisplayToast(Constants.ADD_TO_CART_SUCCESS);
                                           }
                                           else
                                           {
                                               DisplayToast(Constants.ADD_TO_CART_ERROR);
                                           }
                                       }
                                       catch (Exception)
                                       {
                                           DisplayToast(Constants.ADD_TO_CART_ERROR);
                                       }
                                   };
        }

        private void DisplayToast(string message)
        {
            Toast toast = Toast.MakeText(this, message, ToastLength.Long);
            toast.Show();
        }
    }
}
