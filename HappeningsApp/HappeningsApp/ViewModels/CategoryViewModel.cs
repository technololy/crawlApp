using HappeningsApp.Models;
using HappeningsApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HappeningsApp.ViewModels
{
    public class CategoryViewModel
    {
        public ObservableCollection<ImageItems> CategoryItems { get; set; }

        public CategoryViewModel()
        {
            MockImageList m = new MockImageList();
            CategoryItems = m.MockCategoryStore();
        }

    }
}
