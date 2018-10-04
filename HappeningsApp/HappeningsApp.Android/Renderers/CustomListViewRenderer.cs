using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;

using HappeningsApp.Custom;
using HappeningsApp.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomListView), typeof(CustomListViewRenderer))]
namespace HappeningsApp.Droid.Renderers
{
   
    public class CustomListViewRenderer : ListViewRenderer
    {
        private CustomListView element;

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.VerticalScrollBarEnabled = false;
            }
        }

    }



}