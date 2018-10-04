using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HappeningsApp.Models
{

   
    public class FavRootObject
    {
       
        public string Message
        {
            get;
            set;
        }
        public ObservableCollection<FavoriteModel> FavoriteList { get; set; }
    }

    public class FavoriteModel
    {
      
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string NickName
        {
            get;
            set;
        }
        public string Details
        {
            get;
            set;
        }
        public DateTime DateAdded
        {
            get;
            set;
        }
      
    }
}
