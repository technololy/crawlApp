using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Auth.XamarinForms;

namespace HappeningsApp.Models
{

  
    public class CategoryRootobject
        {
            public string Message { get; set; }
            public ObservableCollection<Category> categories { get; set; }
        }

        public class Category
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
            public string Description { get; set; }
            public string ImagePath { get; set; }
            public DateTime Created { get; set; }
            public DateTime Modified { get; set; }
        }

    
}
