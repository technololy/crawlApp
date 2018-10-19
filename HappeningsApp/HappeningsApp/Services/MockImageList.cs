﻿using HappeningsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HappeningsApp.Services
{
    public class MockImageList
    {
        ObservableCollection<ImageItems> imageItems;
        ObservableCollection<Deals> deals;

        public ObservableCollection<ImageItems> MockCategoryStore()
        {
            imageItems = new ObservableCollection<ImageItems>();
            var imageList = new ObservableCollection<ImageItems>()
            {
                new ImageItems{Name="Outdoor Sights",Image="lagoscathedral.jpg"},
                new ImageItems{Name="Date Spots",Image="nationaltheater.jpg"},
                new ImageItems{Name="Night Life",Image="quilox.jpg"},
                new ImageItems{Name="Sporting/Cruise",Image="boatcruise.jpg"},
            };

            foreach (var item in imageList)
            {
                imageItems.Add(item);

            }

            return imageItems;
        }

        internal ObservableCollection<ImageItems> GetDeals()
        {
            imageItems = new ObservableCollection<ImageItems>();
            var imageList = new ObservableCollection<ImageItems>()
            {
                 new ImageItems{Name="harvest Season 7",Image="harvest.jpg"},
                new ImageItems{Name="Club at VI",Image="nightlagos.jpg"},
                //new ImageItems{Name="Seminar and fun with all",Image="EkoHotel.jpg"},
                new ImageItems{Name="Sallah Deals",Image="salahgrill.jpg"},
               
           
            };

            foreach (var item in imageList)
            {

                imageItems.Add(item);

            }

            return imageItems;
        }




        internal ObservableCollection<Deals> GetDealsTest()
        {
            deals = new ObservableCollection<Deals>();
            var imageList = new ObservableCollection<Deals>()
            {
                 new Deals{Name="harvest Season 7",
                    ImagePath="harvest.jpg",
                    Details="fafafafafafafafafafafafafafafa",
                    Owner_Location="Lekki",
                    Expiration_Date=DateTime.Now.AddDays(-5)
                
                },
                new Deals
                {
                    Name="Club at VI",ImagePath="nightlagos.jpg",
                         Details="fafafafafafafafafafafafafafafa",
                    Owner_Location="Lekki",
                    Expiration_Date=DateTime.Now.AddDays(-5)

                },
                //new ImageItems{Name="Seminar and fun with all",Image="EkoHotel.jpg"},
                new Deals
                {
                    Name="Sallah Deals",
                    ImagePath="salahgrill.jpg",
                     Details="fafafafafafafafafafafafafafafa",
                    Owner_Location="Lekki",
                    Expiration_Date=DateTime.Now.AddDays(-5)

                },   new Deals
                {
                    Name="Sallah Deals",
                    ImagePath="salahgrill.jpg",
                     Details="fafafafafafafafafafafafafafafa",
                    Owner_Location="Lekki",
                    Expiration_Date=DateTime.Now.AddDays(-4)

                },   new Deals
                {
                    Name="Sallah Deals",
                    ImagePath="salahgrill.jpg",
                     Details="fafafafafafafafafafafafafafafa",
                    Owner_Location="Lekki",
                    Expiration_Date=DateTime.Now.AddDays(-4)

                },   new Deals
                {
                    Name="Sallah Deals",
                    ImagePath="salahgrill.jpg",
                     Details="fafafafafafafafafafafafafafafa",
                    Owner_Location="Lekki",
                    Expiration_Date=DateTime.Now.AddDays(-3)

                },


            };

            foreach (var item in imageList)
            {

                deals.Add(item);

            }

            return deals;
        }


        internal ObservableCollection<ImageItems> NearBy()
        {
            imageItems = new ObservableCollection<ImageItems>();
            var imageList = new ObservableCollection<ImageItems>()
            {
                new ImageItems{Name="Mega Food",Image="negafood.jpg"},
                new ImageItems{Name="Green Food",Image="greenfood.jpg"},
                //new ImageItems{Name="Seminar and fun with all",Image="EkoHotel.jpg"},
                new ImageItems{Name="Ice Cream",Image="icecream.jpg"},
               
           
                     //new ImageItems{Name="Happeing at Lagos Cathedral",Image="lagoscathedral.jpg"},
               
                
             
                //new ImageItems{Name="Check out exciting homes",Image="onethousandandfour.jpg"},
            };

            foreach (var item in imageList)
            {

                imageItems.Add(item);

            }

            return imageItems;
        }

        internal ObservableCollection<ImageItems> Category()
        {
            imageItems = new ObservableCollection<ImageItems>();
            var imageList = new ObservableCollection<ImageItems>()
            {
                new ImageItems{Name="Varieties",Image="negafood.jpg"},
                new ImageItems{Name="Greens",Image="greenfood.jpg"},
                //new ImageItems{Name="Seminar and fun with all",Image="EkoHotel.jpg"},
                new ImageItems{Name="Sweets",Image="icecream.jpg"},
                new ImageItems{Name="NightLife",Image="harvest.jpg"},
                new ImageItems{Name="Club",Image="nightlagos.jpg"},
                //new ImageItems{Name="Seminar and fun with all",Image="EkoHotel.jpg"},
                new ImageItems{Name="Festivities",Image="salahgrill.jpg"},
               
           
                     //new ImageItems{Name="Happeing at Lagos Cathedral",Image="lagoscathedral.jpg"},
               
                
             
                //new ImageItems{Name="Check out exciting homes",Image="onethousandandfour.jpg"},
            };

            foreach (var item in imageList)
            {

                imageItems.Add(item);

            }

            return imageItems;
        }

        internal ObservableCollection<ImageItems> MockTodayStore()
        {
            imageItems = new ObservableCollection<ImageItems>();
            var imageList = new ObservableCollection<ImageItems>()
            {
           
                new ImageItems{Name="Fun view and night party",Image="EkoCourts.jpg"},
                //new ImageItems{Name="Seminar and fun with all",Image="EkoHotel.jpg"},
                new ImageItems{Name="OnShore/OffShore Deals",Image="lagosoilrig.jpg"},
               
           
                     //new ImageItems{Name="Happeing at Lagos Cathedral",Image="lagoscathedral.jpg"},
               
                new ImageItems{Name="QUilox Season 7",Image="quilox.jpg"},
             
                //new ImageItems{Name="Check out exciting homes",Image="onethousandandfour.jpg"},
            };

            foreach (var item in imageList)
            {
                
                imageItems.Add(item);

            }

            return imageItems;
        }

        //internal ObservableCollection<CollectionsRootObject> GetCollections()
        //{
        //    var collec = new CollectionsRootObject();
        //    var co
        //}
    }
}
