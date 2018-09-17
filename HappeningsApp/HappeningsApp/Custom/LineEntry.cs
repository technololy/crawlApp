using System;
using Xamarin.Forms;

namespace HappeningsApp.Custom
{
    public class LineEntry: Xamarin.Forms.Entry
    {
        public LineEntry()
        {
        }

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create<LineEntry, Color>(p => p.BorderColor, Color.Black);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public new static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create<LineEntry, double>(p => p.FontSize, Font.Default.FontSize);

        public new double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public new static readonly BindableProperty PlaceholderColorProperty =
            BindableProperty.Create<LineEntry, Color>(p => p.PlaceholderColor, Color.Default);

        public new Color PlaceholderColor
        {
            get { return (Color)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }


        public static BindableProperty ErrorColorProperty =
            BindableProperty.Create(
                nameof(ErrorColor),
                typeof(Color),
                typeof(LineEntry),
                Color.Default,
                BindingMode.TwoWay);
        public Color ErrorColor
        {
            get { return (Color)GetValue(ErrorColorProperty); }
            set { SetValue(ErrorColorProperty, value); }
        }

        public static BindableProperty IsValidProperty =
            BindableProperty.Create(
                nameof(IsValid),
                typeof(bool),
                typeof(LineEntry),
                true,
                BindingMode.TwoWay);
        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }
    }
}
