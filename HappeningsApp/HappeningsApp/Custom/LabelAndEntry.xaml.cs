using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace HappeningsApp.Custom
{
    public partial class LabelAndEntry : ContentView
    {
        public LabelAndEntry()
        {
            InitializeComponent();
        }

        public string PlaceHolderName
        {
            get;
            set;
        }
        public static readonly BindableProperty PlaceHolderNameProperty =
           BindableProperty.Create(nameof(PlaceHolderName), 
                                   typeof(string), 
                                   typeof(LabelAndEntry),
                                   default(string),BindingMode.TwoWay,propertyChanged:OnPlaceHolderNameChange);

        private static void OnPlaceHolderNameChange(BindableObject bindable, object oldValue, object newValue)
        {
            var obj = (LabelAndEntry)bindable;
            obj.lblText.Placeholder = (string)newValue;
        }

       

      

    }
}
