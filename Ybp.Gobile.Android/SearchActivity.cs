using System.Threading;

using Android.App;
using Android.OS;
using Android.Widget;

using ZXing.Mobile;

using Result = ZXing.Result;

namespace Ybp.Gobile.Android
{
    [Activity(Label = "Gobile - Scan ISBN")]
    public class SearchActivity : Activity
    {
        private Button buttonScan;

        private MobileBarcodeScanner scanner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            MobileBarcodeScanner.Initialize(Application);

            SetContentView(Resource.Layout.Search);

            scanner = new MobileBarcodeScanner();

            buttonScan = this.FindViewById<Button>(Resource.Id.scanButton);
            buttonScan.Click += async delegate
                                      {
                                          scanner.UseCustomOverlay = false;
                                          scanner.TopText = "Hold the camera up to the barcode\nAbout 6 inches away";
                                          scanner.BottomText = "Wait for the barcode to automatically scan!";

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
        }

        void HandleScanResult(Result result)
        {
            string msg = "";

            if (result != null && !string.IsNullOrEmpty(result.Text))
                msg = "Found Barcode: " + result.Text;
            else
                msg = "Scanning Canceled!";

            this.RunOnUiThread(() => Toast.MakeText(this, msg, ToastLength.Short).Show());
        }
    }
}
