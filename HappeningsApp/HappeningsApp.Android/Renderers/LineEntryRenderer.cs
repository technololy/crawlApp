using Android.Graphics;
using HappeningsApp.Custom;
using HappeningsApp.Droid.Renderers;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LineEntry), typeof(LineEntryRenderer))]
namespace HappeningsApp.Droid.Renderers
{
    public class LineEntryRenderer: EntryRenderer
    {
        public LineEntryRenderer()
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control == null || Element == null || e.OldElement != null) return;

            if (e.NewElement != null)
            {
                var element = (LineEntry)Element;
                DrawAll(element);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var view = (LineEntry)Element;

            if (e.PropertyName.Equals("IsValid") || e.PropertyName.Equals("ErrorColor"))
                DrawAll(view);
            if (e.PropertyName.Equals("BorderColor"))
                DrawBorder(view);
            if (e.PropertyName.Equals("FontSize"))
                SetFontSize(view);
            if (e.PropertyName.Equals("PlaceholderColor"))
                SetPlaceholderTextColor(view);
        }

        protected void DrawBorder(LineEntry element)
        {
            Invalidate();
            var color = element.BorderColor.ToAndroid();

            // If the element is invalid, and the ErrorColor has been set, change the color
            if (!element.IsValid && element.ErrorColor != Xamarin.Forms.Color.Default)
                color = element.ErrorColor.ToAndroid();
            Control.Background.SetColorFilter(color, PorterDuff.Mode.SrcAtop);
        }

        protected void SetPlaceholderTextColor(LineEntry element)
        {
            Control.SetHintTextColor(element.PlaceholderColor.ToAndroid());
        }

        protected void SetFontSize(LineEntry element)
        {
            Control.SetTextSize(Android.Util.ComplexUnitType.Sp, (float)element.FontSize);
        }

        protected void DrawAll(LineEntry element)
        {
            DrawBorder(element);
            SetPlaceholderTextColor(element);
            SetFontSize(element);
        }
    }
}
