using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HappeningsApp.Models;
using Xamarin.Forms;

namespace HappeningsApp.Pages.Home
{
    public partial class AllInnerDetail : ContentPage
    {
        private NewCategoryDetailModel.Deal selected;

        public AllInnerDetail()
        {
            InitializeComponent();
            
        }


        public AllInnerDetail(NewCategoryDetailModel.Deal selected)
        {
            InitializeComponent();

            this.selected = selected;
           // ObservableCollection<NewCategoryDetailModel.Deal> d =  new ObservableCollection<NewCategoryDetailModel.Deal>();
            //d.Add(this.selected);
            BindingContext = this.selected;
        }
      
    }
}
