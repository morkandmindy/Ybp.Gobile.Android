using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Ybp.Gobile.Android.Models;

namespace Ybp.Gobile.Android
{
    [Activity(Label = "Ybp.Gobile.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button loginButton = FindViewById<Button>(Resource.Id.loginButton);
            EditText loginIdText = FindViewById<EditText>(Resource.Id.loginidtext);
            EditText passwordText = FindViewById<EditText>(Resource.Id.passwordtext);
            EditText accountText = FindViewById<EditText>(Resource.Id.accountText);
            EditText responseText = FindViewById<EditText>(Resource.Id.responseText);



            loginButton.Click += async (sender, e) =>
            {
                
                    LoginCredentials loginCredentials = new LoginCredentials()
                    {
                        Account = accountText.Text,
                        Login = loginIdText.Text,
                        Password = passwordText.Text
                    };

                    var serializedCredentials = Newtonsoft.Json.JsonConvert.SerializeObject(loginCredentials);


                    var utils = new Utilities();
                    String response = await utils.MakeAjaxRequestAsync(Constants.LOGIN_URL, serializedCredentials);
                    responseText.Text = response;
            };
        }
    }
}

