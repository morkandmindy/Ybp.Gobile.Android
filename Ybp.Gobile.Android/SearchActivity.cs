using System.Threading;

using Android.App;
using Android.OS;
using Android.Widget;

using Newtonsoft.Json;

using ZXing.Mobile;

using Result = ZXing.Result;

namespace Ybp.Gobile.Android
{
    [Activity(Label = "Gobile - Scan ISBN")]
    public class SearchActivity : Activity
    {
        private Button buttonScan;
        private Button buttonSearch;
        private EditText isbnEditText; 

        private MobileBarcodeScanner scanner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            MobileBarcodeScanner.Initialize(Application);

            SetContentView(Resource.Layout.Search);

            scanner = new MobileBarcodeScanner();
            scanner.UseCustomOverlay = false;
            scanner.TopText = "Hold the camera up to the barcode\nAbout 6 inches away";
            scanner.BottomText = "Wait for the barcode to automatically scan!";
            isbnEditText = this.FindViewById<EditText>(Resource.Id.isbnEditText);
            buttonScan = this.FindViewById<Button>(Resource.Id.scanButton);
            buttonScan.Click += async delegate
                                      {
                                          Result result = null;
                                          new Thread(new ThreadStart(delegate
                                                                     {
                                                                         while (result == null)
                                                                         {
                                                                             scanner.AutoFocus();
                                                                             Thread.Sleep(1000);
                                                                         }
                                                                     })).Start();

                                          result = await scanner.Scan();

                                          HandleScanResult(result);
                                      };
            buttonSearch = this.FindViewById<Button>(Resource.Id.searchButton);
            buttonSearch.Click += async (sender, e) =>
            {
                //try
                //{
                //    var isbn = new IsbnSearchRequest() { Isbn = IsbnText.Text };
                //    var isbnRequest = JsonConvert.SerializeObject(isbn);
                //    var utils = new Utilities();
                //    var response = await utils.MakeAjaxRequestAsync(Constants.SEARCH_URL, isbnRequest);
                //}
                //catch (Exception ex)
                //{
                //    System.Diagnostics.Debug.WriteLine(ex.Message);
                //}
            };
        }

        void HandleScanResult(Result result)
        {
            if (result != null && !string.IsNullOrEmpty(result.Text))
            {
                isbnEditText.Text = result.Text;
            }

            //string msg = "";

            //if (result != null && !string.IsNullOrEmpty(result.Text))
            //    msg = "Found Barcode: " + result.Text;
            //else
            //    msg = "Scanning Canceled!";

            //this.RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());
        }
    }
}
