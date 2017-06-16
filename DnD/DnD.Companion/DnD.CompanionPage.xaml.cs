using System;
using Xamarin.Forms;

namespace DnD.Companion
{
    public partial class DnD_CompanionPage : ContentPage
    {
        public DnD_CompanionPage()
        {
            InitializeComponent();

        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new LoginPage());
        }
    }
}
