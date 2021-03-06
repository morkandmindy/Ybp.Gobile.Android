﻿using System;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

using Newtonsoft.Json;

using Ybp.Gobile.Android.Models;
using Ybp.Gobile.Android.Resources.layout;

using ZXing.Mobile;

using Result = ZXing.Result;

namespace Ybp.Gobile.Android
{
    [Activity(Label = "Gobi2Go")]
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
            buttonSearch = this.FindViewById<Button>(Resource.Id.searchButton);
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

                                          if (result != null && !string.IsNullOrEmpty(result.Text))
                                          {
                                              isbnEditText.Text = result.Text;

                                              var isbn = new IsbnSearchRequest() { Isbn = isbnEditText.Text };
                                              var isbnRequest = JsonConvert.SerializeObject(isbn);
                                              var utils = new Utilities();
                                              var response = await Utilities.MakeAjaxRequestAsync(Constants.SEARCH_URL, isbnRequest, Constants.JSON_CONTENT);
                                              if (response == "Status: Not found")
                                              {
                                                  Toast toast = Toast.MakeText(this, "Title not found.", ToastLength.Long);
                                                  toast.Show();
                                              }
                                              else
                                              {
                                                  var intent = new Intent(this, typeof(BibDisplayActivity));
                                                  intent.PutExtra("response", response);
                                                  StartActivity(intent);
                                              }
                                              
                                          }
                                      };
            buttonSearch.Click += async (sender, e) =>
                                  {
                                      try
                                      {
                                          var isbn = new IsbnSearchRequest() { Isbn = isbnEditText.Text };
                                          var isbnRequest = JsonConvert.SerializeObject(isbn);
                                          var utils = new Utilities();
                                          var response = await Utilities.MakeAjaxRequestAsync(Constants.SEARCH_URL, isbnRequest, Constants.JSON_CONTENT);
                                          var intent = new Intent(this, typeof(BibDisplayActivity));
                                          intent.PutExtra("response", response);
                                          StartActivity(intent);
                                      }
                                      catch (Exception ex)
                                      {
                                          System.Diagnostics.Debug.WriteLine(ex.Message);
                                      }
                                  };
        }
        
    }
}
