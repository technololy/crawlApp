using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HappeningsApp.Services
{
   public class MyToast
    {
        public ToastConfig ShowMyToast(Color color, string text)
        {
            var toastConfig = new ToastConfig(text);
            toastConfig.SetDuration(5000);
            toastConfig.SetPosition(ToastPosition.Top);
            toastConfig.SetBackgroundColor(color);
            return toastConfig;
        }

        public static ToastConfig DisplayToast(Color color, string text)
        {
            var toastConfig = new ToastConfig(text);
            toastConfig.SetDuration(5000);
            toastConfig.SetPosition(ToastPosition.Top);
            toastConfig.SetBackgroundColor(color);
            return toastConfig;
        }

        public static ToastConfig DisplayToastEnum(ToastType ttype, string text)
        {
            var toastConfig = new ToastConfig(text);
            toastConfig.SetDuration(5000);
            Color color = new Color();
            toastConfig.SetPosition(ToastPosition.Top);
            if (ttype == ToastType.Success)
            {
                color = Color.Green;
            }
            else if(ttype==ToastType.Failure)
            {
                color = Color.MediumVioletRed;

            }
            toastConfig.SetBackgroundColor(color);
            return toastConfig;
        }

        public enum ToastType
        {
            Success, Failure
        }
    }
}
