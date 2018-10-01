using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using HappeningsApp.Custom;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace HappeningsApp.Services
{
    public class Utility
    {


        //public static string GetCurrency(string currencyCode, CurrencyReturnType returnType = CurrencyReturnType.CurrencySymbol)
        //{
        //    if (string.IsNullOrEmpty(currencyCode)) return string.Empty;
        //    currencyCode = currencyCode.ToUpper();
        //    string symbol = currencyCode;
        //    try
        //    {

        //        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Dashboard)).Assembly;
        //        Stream stream = assembly.GetManifestResourceStream("SterlingSwitch.Resources.Currencies.json");
        //        string text = "";
        //        using (var reader = new StreamReader(stream))
        //        {
        //            text = reader.ReadToEnd();
        //        }

        //        text = text.Replace(Environment.NewLine, string.Empty);
        //        text = text.Replace(" ", string.Empty);

        //        var jsonObject = JsonConvert.DeserializeObject<CurrencyType[]>(text);

        //        var item = jsonObject.Where(i => i.code == currencyCode).Single();

        //        if (item != null)
        //        {
        //            if (returnType == CurrencyReturnType.CurrencySymbol)
        //            {
        //                symbol = item.symbol_native;

        //                if (string.IsNullOrWhiteSpace(symbol))
        //                    return currencyCode;
        //            }
        //            else
        //                symbol = item.name;

        //        }
        //    }
        //    catch (Exception)
        //    {
        //        ;
        //    }

        //    return symbol;
        //}



        public class StringCaseConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {

                string param = System.Convert.ToString(parameter) ?? "u";

                switch (param.ToUpper())
                {
                    case "U":
                        return ((string)value).ToUpper();
                    case "L":
                        return ((string)value).ToLower();
                    default:
                        return ((string)value);
                }

            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotSupportedException();
            }
        }


        public bool CheckConnection()
        {
            if (CrossConnectivity.Current.IsConnected)
                return true;
            else
                return false;
        }


        public void PersistUsername()
        {
            
        }


    }
}
