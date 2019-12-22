using System;
using System.Collections.Generic;
using HappeningsApp.Models;
using HappeningsApp.ViewModels;
using Xamarin.Forms;

namespace HappeningsApp.Pages.Notification
{
    public partial class Notifications : ContentPage
    {
        MessagesViewModel mvm;
        public Notifications()
        {
            InitializeComponent();

            mvm = new MessagesViewModel();
            mvm.PageTitle = "Messages";
            
            BindingContext = mvm;
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
