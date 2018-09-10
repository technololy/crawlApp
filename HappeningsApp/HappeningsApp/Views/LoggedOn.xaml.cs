using System;
using System.Collections.Generic;
using UIKit;
using Xamarin.Forms;

namespace HappeningsApp.Views
{
    public partial class LoggedOn : ContentPage
    {
        public LoggedOn()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.OrangeRed;
            //this.NavigationController.NavigationBar.BarTintColor = UIColor.Yellow;

        }
    }
}
