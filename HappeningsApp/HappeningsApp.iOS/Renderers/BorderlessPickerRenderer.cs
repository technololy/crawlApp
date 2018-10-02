using System;
using System.ComponentModel;
using HappeningsApp.Custom;
using HappeningsApp.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(BorderlessPickerRenderer), typeof(BorderlessPicker))]
namespace HappeningsApp.iOS.Renderers
{
    public class BorderlessPickerRenderer: PickerRenderer
    {
        public static void Init() { }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}
