using System;
using HappeningsApp.Interfaces;
using UIKit;

namespace HappeningsApp.iOS.Helpers
{
    public class CountBadge: ICountBadge
    {
        public CountBadge()
        {
        }

        public void SetPushNotificationCount(int p)
        {
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = p;

        }
    }
}
