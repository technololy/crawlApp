using System;
using System.ComponentModel;
using CoreAnimation;
using CoreGraphics;
using HappeningsApp.Custom;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using static HappeningsApp.iOS.Renderers.BorderlessEntry;

[assembly:ExportRenderer(typeof(BorderlessEntry),typeof(BorderlessEntryRenderer))]
namespace HappeningsApp.iOS.Renderers
{
    public class BorderlessEntry
    {
        public BorderlessEntry()
        {
        }
        public class BorderlessEntryRenderer : EntryRenderer
        {
            protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                base.OnElementPropertyChanged(sender, e);

                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;


                //var borderLayer = new CALayer(); 
                //borderLayer.MasksToBounds = true; 
                //borderLayer.Frame = new CGRect(0f, 15f, Control.Layer.Frame.Width, 1f); 
                //borderLayer.BorderColor = UIColor.Magenta.CGColor;
                //borderLayer.BorderWidth = 1.0f; 
                //Control.Layer.AddSublayer(borderLayer); 
                //Control.BorderStyle = UITextBorderStyle.None;

            }
        }
    }
}
