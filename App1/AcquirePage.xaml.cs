using Microsoft.Identity.Client;
using System;
using System.Globalization;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AcquirePage : ContentPage
    {
        private const string EmptyResult = "Result:";
        private const string SuccessfulResult = "Result: Success";
        public AcquirePage()
        {
            InitializeComponent();
        }

        private async void OnAcquireClickedAsync(object sender, EventArgs e)
        {
            try
            {
                acquireResponseTitleLabel.Text = EmptyResult;

                var result = await App.MsalPublicClient.AcquireTokenInteractive(App.Scopes, App.AndroidActivity).ExecuteAsync();
               
                var resText = GetResultDescription(result);

                if (resText.Contains("AccessToken"))
                {
                    acquireResponseTitleLabel.Text = SuccessfulResult;
                }

                acquireResponseLabel.Text = resText;
            }
            catch (Exception exception)
            {
                CreateExceptionMessage(exception);
            }
        }

        private static string GetResultDescription(AuthenticationResult result)
        {
            var sb = new StringBuilder();

            sb.AppendLine("AccessToken : " + result.AccessToken);
            sb.AppendLine("IdToken : " + result.IdToken);
            sb.AppendLine("ExpiresOn : " + result.ExpiresOn);
            sb.AppendLine("TenantId : " + result.TenantId);
            sb.AppendLine("Scope : " + string.Join(",", result.Scopes));
            sb.AppendLine("User :");
            sb.Append(GetAccountDescription(result.Account));

            return sb.ToString();
        }

        private static string GetAccountDescription(IAccount user)
        {
            var sb = new StringBuilder();

            sb.AppendLine("user.DisplayableId : " + user.Username);
            //sb.AppendLine("user.IdentityProvider : " + user.IdentityProvider);
            sb.AppendLine("user.Environment : " + user.Environment);

            return sb.ToString();
        }

        private void CreateExceptionMessage(Exception exception)
        {
            if (exception is MsalException msalException)
            {
                acquireResponseLabel.Text = string.Format(CultureInfo.InvariantCulture, "MsalException -\nError Code: {0}\nMessage: {1}",
                    msalException.ErrorCode, msalException.Message);
            }
            else
            {
                acquireResponseLabel.Text = "Exception - " + exception.Message;
            }

            System.Console.WriteLine(exception.Message);
        }
    }
}