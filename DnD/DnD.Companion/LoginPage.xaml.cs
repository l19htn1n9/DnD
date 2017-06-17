using DnD.Companion.Helper;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DnD.Companion
{
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
            SetEnabled(true);
            CheckRememberMe();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            SetEnabled(false);
            var username = Username.Text;
            var password = Password.Text;
            var rememberMe = Switch.IsToggled;
            if(rememberMe)
            {
                Application.Current.Properties["username"] = username;
                Application.Current.Properties["password"] = password;
            }
            ApiHandler.Instance.Login(username, password);
        }

        private void SetEnabled(bool enabled)
        {
            Username.IsEnabled = enabled;
            Password.IsEnabled = enabled;
            Switch.IsEnabled = enabled;
            Login.IsEnabled = enabled;

            ActivityIndicator.IsEnabled = !enabled;
            ActivityIndicator.IsVisible = !enabled;
        }

        private void CheckRememberMe()
        {
            if(Application.Current.Properties.ContainsKey("username") && Application.Current.Properties.ContainsKey("password"))
            {
                var username = (string)Application.Current.Properties["username"];
                var password = (string)Application.Current.Properties["password"];
                SetEnabled(false);
                ApiHandler.Instance.Login(username, password);
            }
        }
    }
}
