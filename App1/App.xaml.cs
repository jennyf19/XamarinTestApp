using Microsoft.Identity.Client;
using Microsoft.Identity.Client.AppConfig;
using System;
using Xamarin.Forms;

namespace App1
{
    public partial class App : Application
    {
        public static IPublicClientApplication MsalPublicClient;

        public static object AndroidActivity { get; set; }

        public const string ClientId = "4b0db8c2-9f26-4417-8bde-3f0e3656f8e0";

        public static string RedirectUriOnAndroid = "urn:ietf:wg:oauth:2.0:oob";

        public const string Authority = "https://login.microsoftonline.com/common";

        public static string[] Scopes = { "User.Read" };

        public static bool UseBroker;
        public App()
        {
            InitializeComponent();


            MainPage = new NavigationPage(new App1.MainPage());
            InitPublicClient();
        }
        public static void InitPublicClient()
        {
            var builder = PublicClientApplicationBuilder
                .Create(ClientId)
                .WithAuthority(new Uri(Authority), false);

            if (UseBroker)
            {
                //builder.WithBroker(true);
                builder.WithIosKeychainSecurityGroup("com.microsoft.adalcache");
            }

            else
            {
                builder.WithRedirectUri(RedirectUriOnAndroid);
            }

            MsalPublicClient = builder.Build();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
