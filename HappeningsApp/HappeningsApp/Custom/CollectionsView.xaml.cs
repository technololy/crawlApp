using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace HappeningsApp.Custom
{
    public partial class CollectionsView : ContentView
    {
        public CollectionsView()
        {
            InitializeComponent();
        }

        public string HeaderText
        {
            get
            {
                return (string)GetValue(HeaderTextProperty);
            }
            set
            {
                SetValue(HeaderTextProperty, value);
            }
        }

        public ImageSource ImageSource
        {
            get;
            set;
        }
        public static readonly BindableProperty HeaderTextProperty = 
            BindableProperty.Create(nameof(HeaderText),
                                    typeof(string),
                                    typeof(CollectionsView), 
                                    defaultValue:string.Empty,

                                    defaultBindingMode: BindingMode.OneWay, 
                                    propertyChanged:OnHeaderTextPropertyChanged);

        private static void OnHeaderTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ctl = (CollectionsView)bindable;
            ctl.txtHeader.Text = (string)newValue;

            //if (!string.IsNullOrEmpty(ctl.txtHeader.Text))
            //{
            //}
        }
        public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource),
                                typeof(ImageSource),
                                typeof(CollectionsView),
                                defaultValue: null,

                                defaultBindingMode: BindingMode.TwoWay,
                                propertyChanged: OnImageSourcepropertyChanged);

        private static void OnImageSourcepropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var obj = bindable as CollectionsView;
            obj.img.Source = (ImageSource)newValue;
            
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
           
        }

    }
}
