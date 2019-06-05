using System;
using System.Collections.Generic;
using HappeningsApp.Models;
using HappeningsApp.ViewModels;
using Xamarin.Forms;

namespace HappeningsApp.Pages.Notification
{
    public partial class Notifications : ContentPage
    {
        public Notifications()
        {
            InitializeComponent();
            BindingContext = new MessagesViewModel();
            RefreshMessage();
        }

        private void RefreshMessage()
        {
            msgListView.RefreshCommand = new Command(

                () =>
                {
                    BindingContext = new MessagesViewModel();
                    msgListView.IsRefreshing = false;
                }



            );
        }

        void Notif_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            try
            {
                if (!(msgListView.SelectedItem is Pushresp selected))
                {
                    return;
                }

                //Application.Current.MainPage.Navigation.PushAsync(new NotificationDetail(selected));
                msgListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                var log = ex;
            }

        }
    }
}
