using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HappeningsApp.Services;
using HappeningsApp.ViewModels;
using Xamarin.Forms;

namespace HappeningsApp.Views.Messages
{
    public partial class MessagesLanding : ContentPage
    {
        public MessagesLanding()
        {
            InitializeComponent();
            BindingContext = new MessagesViewModel();
        }

      
    }
}
