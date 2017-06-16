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
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            var username = Username.Text;
            var password = Password.Text;
            ApiHandler.Instance.Login(username, password);
        }
    }
}
