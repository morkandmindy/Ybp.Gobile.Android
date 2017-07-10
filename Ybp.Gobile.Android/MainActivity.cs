using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Ybp.Gobile.Android.Models;

namespace Ybp.Gobile.Android
{
    [Activity(Label = "Gobile - Login", MainLauncher = true, Icon = "@drawable/icon")]
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
            TextView responseText = FindViewById<TextView>(Resource.Id.responseText);


            loginButton.Click += async (sender, e) =>
                                       {
                                           LoginCredentials loginCredentials = new LoginCredentials()
                                                                               {
                                                                                   Account = accountText.Text,
                                                                                   Login = loginIdText.Text,
                                                                                   Password = passwordText.Text
                                                                               };

                                           var serializedCredentials = JsonConvert.SerializeObject(loginCredentials);


                                           var utils = new Utilities();
                                           var response =
                                               await utils.MakeAjaxRequestAsync(Constants.LOGIN_URL, serializedCredentials);
                                           
                                           JObject jsonResponse = JObject.Parse(response);
                                           if((string)jsonResponse["Login"]=="OK")
                                           {
                                               var intent = new Intent(this, typeof(SearchActivity));
                                               StartActivity(intent);
                                           }
                                           else
                                           {
                                               responseText.Text = response;
                                           }
                                       };
        }
    }
}
