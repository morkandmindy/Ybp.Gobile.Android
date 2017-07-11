using System.Linq;

using Android.App;
using Android.OS;
using Android.Widget;

using Newtonsoft.Json;

using Ybp.Gobile.Android.Models;

namespace Ybp.Gobile.Android.Resources.layout
{
    [Activity(Label = "Gobile")]
    public class BibDisplayActivity : Activity
    {
        private ImageView imageViewBookCover;
        private ListView listViewPurchaseOptions;
        private TextView textViewAuthor;
        private TextView textViewBinding;
        private TextView textViewPagination;
        private TextView textViewPublisher;
        private TextView textViewPubYear;
        private TextView textViewTitle;
        private Button Button1;

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
            listViewPurchaseOptions = FindViewById<ListView>(Resource.Id.listViewPurchaseOptions);
            Button1 = FindViewById<Button>(Resource.Id.button1);

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

            var x = searchResult.ItemVendors.Select(o => o.GOBI_DISPLAY_NAME + " " + o.PURCHASE_OPTION_DESC).ToArray();
            ArrayAdapter<string> purchaseOptions = new ArrayAdapter<string>(this,
                global::Android.Resource.Layout.SimpleListItem1,
                x);
            listViewPurchaseOptions.Adapter = purchaseOptions;


            //lets just pretend the user clicked on first item

            var putInCart = new PutInCartRequest()
                            {
                                ContainedItems =
                                    searchResult.ItemVendors[0]
                                    .PK_ITEM_VENDOR,
                                ContainerId =
                                    searchResult.Container
                            };
            Button1.Click += async (sender, e) =>
                                   {
                                       var response =
                                           await Utilities.MakeAjaxRequestAsync(
                                               Constants.BASE_URL + "app_putincart&cart=OrderCart",
                                               putInCart.ToRequest(),
                                               Constants.FORM_DATA);
                                   };
        }
    }
}
