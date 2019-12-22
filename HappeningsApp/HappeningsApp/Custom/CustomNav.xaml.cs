
using HappeningsApp.Pages.Home;
using HappeningsApp.Views.Search;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickPolicy.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomNav : ContentView
    {

	                
        public new Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public new static readonly BindableProperty BackgroundColorProperty= BindableProperty.Create(
        "BackgroundColor", typeof(Color), typeof(CustomNav), default(Color), BindingMode.TwoWay);

        public bool IsVisibleLine
        {
            get => boxView.IsVisible;
            set => boxView.IsVisible = value;
        }

        //public bool BackBtnIsVisible
        //{
        //    get => backGrid.IsVisible;
        //    set => backGrid.IsVisible = value;
        //}

        public bool IsNotificationImgVisible
        {
            get => imgNotif.IsVisible;
            set => imgNotif.IsVisible = value;
        }

        //public bool MenuBtnIsVisible
        //{
        //    get => menuGrid.IsVisible;
        //    set => menuGrid.IsVisible = value;
        //}

        public string NavTitle
        {
            get { return GetValue(NavTitleProperty) as string; }
            set { SetValue(NavTitleProperty, value); }
        }

        public CustomNav()
        {
            InitializeComponent();
           
            nav.SetBinding(Grid.BackgroundColorProperty, new Binding("BackgroundColor", source: this));
        }

        private async void Back_Tapped(object sender, EventArgs e)
        {

            //backGrid.IsVisible = true;
            //backGrid.BackgroundColor = Color.FromHex("#80FFFFFF");
            //await backFrame.FadeTo(0, 300, Easing.SinIn);
            try
            {
                await Navigation.PopAsync(true);
            }
            catch (Exception)
            {
                await Navigation.PopModalAsync(true);


            }
            //backGrid.IsVisible = false;


        }

        void Search_Tapped(object sender, System.EventArgs e)
        {

            Navigation.PushAsync(new SearchPage(), true);
        }

        void Profiles_Tapped(object sender, System.EventArgs e)
        {
             Navigation.PushAsync(new Profile());

        }

        public static BindableProperty NavTitleProperty =
        BindableProperty.Create(propertyName: nameof(NavTitle),
            returnType: typeof(string),
            declaringType: typeof(CustomNav),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: NavTitlePropertyChanged

        );

        private static void NavTitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as CustomNav;

            if (control != null)
            {
                control.navTitle.Text = newValue as string;
            }
        }


        //    public static BindableProperty IsNotificationImgVisibleProperty =
        //BindableProperty.Create(propertyName: nameof(IsNotificationImgVisible),
        //    returnType: typeof(bool),
        //    declaringType: typeof(CustomNav),
        //    defaultValue: false,
        //    defaultBindingMode: BindingMode.TwoWay,
        //    propertyChanged: IsNotificationImgVisiblePropertyChanged);


        //    private static void IsNotificationImgVisiblePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        //    {
        //        var control = bindable as CustomNav;

        //        if (control != null)
        //        {
        //            control.IsNotificationImgVisible = newValue a;
        //        }
        //    }

        public bool MenuImgVisibility
        {
            get { return (bool) GetValue(MenuImgVisibleProperty) ; }
            set { SetValue(MenuImgVisibleProperty, value); }
        }

        public static BindableProperty MenuImgVisibleProperty =
    BindableProperty.Create(propertyName: nameof(MenuImgVisibility),
        returnType: typeof(bool),
        declaringType: typeof(CustomNav),
        defaultValue: true,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: MenuImgVisiblePropertyChanged);


        private static void MenuImgVisiblePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as CustomNav;

            if (control != null)
            {

            }
        }








        //group logo section start



        public bool GroupLogoVisibility
        {
            get { return (bool)GetValue(GroupLogoVisibleProperty); }
            set { SetValue(GroupLogoVisibleProperty, value); }
        }

        public static BindableProperty GroupLogoVisibleProperty =
    BindableProperty.Create(propertyName: nameof(GroupLogoVisibility),
        returnType: typeof(bool),
        declaringType: typeof(CustomNav),
        defaultValue: true,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: GroupLogoVisiblePropertyChanged);


        private static void GroupLogoVisiblePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as CustomNav;

            if (control != null)
            {
            }
        }
        //group logo section end




        //navtitleVisibility

        public bool NavTitleVisibility
        {
            get { return (bool) GetValue(NavTitleVisibilityProperty); }
            set { SetValue(NavTitleVisibilityProperty, value); }
        }




        public static BindableProperty NavTitleVisibilityProperty =
        BindableProperty.Create(propertyName: nameof(NavTitleVisibility),
            returnType: typeof(bool),
            declaringType: typeof(CustomNav),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: NavTitleVisibilityPropertyChanged

        );

        private static void NavTitleVisibilityPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as CustomNav;

            if (control != null)
            {
                control.navTitle.IsVisible = (bool) newValue;
            }
        }


        //end navtitlevisibility












        public bool BackButtonImgVisibility
        {
            get { return (bool) GetValue(BackButtonImgVisibleProperty); }
            set { SetValue(BackButtonImgVisibleProperty, value); }
        }

        public static BindableProperty BackButtonImgVisibleProperty =
    BindableProperty.Create(propertyName: nameof(BackButtonImgVisibility),
        returnType: typeof(bool),
        declaringType: typeof(CustomNav),
        defaultValue: true,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: BackButtonVisiblePropertyChanged);


        private static void BackButtonVisiblePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as CustomNav;

            if (control != null)
            {
            }
        }





    }
}