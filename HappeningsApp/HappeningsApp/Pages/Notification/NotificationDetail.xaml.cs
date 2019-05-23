using System;
using System.Collections.Generic;
using HappeningsApp.Models;
using Xamarin.Forms;

namespace HappeningsApp.Pages.Notification
{
    public partial class NotificationDetail : ContentPage
    {
        private Pushresp selected;

        public NotificationDetail()
        {
            InitializeComponent();

          var n =  new Pushresp
            {
                Message = "Image files can be added to each application project and referenced " +
                "from Xamarin.Forms shared code. This method of distributing images is required when images " +
                "are platform-specific, such as when using different resolutions on different platforms, " +
                "or slightly different designs",
                Picture = "",
                Status = "Hello01 Subject"
            };
            this.BindingContext = n;


        }




        public NotificationDetail(Pushresp selected)
        {
            InitializeComponent();

            try
            {
                this.selected = selected;
                this.BindingContext = this.selected;
            }
            catch (Exception ex)
            {
                var log = ex;
            }

        }
    }
}
