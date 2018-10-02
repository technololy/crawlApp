using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace HappeningsApp.Custom
{
    public partial class MultiPicker : ContentView, INotifyPropertyChanged
    {
        public MultiPicker()
        {
            InitializeComponent();
            SetTap();

            if (string.IsNullOrWhiteSpace(this.Placeholder))
            {
                this.Placeholder = "Select an option";

            }

            this.BindingContext = this;
        }


        public event EventHandler SelectedIndexChanged;
        public Action RefreshContent { get; set; }

        public ICommand ReloadItemsSourceCommand { get; set; }

        public static readonly BindableProperty ItemsProperty = BindableProperty.Create("Items", typeof(IEnumerable<string>), typeof(ExtendedEntry), null);

        public IEnumerable<string> Items
        {
            get => (IEnumerable<string>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public static BindableProperty ItemsSourceProperty = BindableProperty.Create(
                                                            propertyName: "ItemsSource",
                                                            returnType: typeof(IEnumerable<string>),
                                                            declaringType: typeof(MultiPicker),
                                                            defaultValue: null,
                                                            defaultBindingMode: BindingMode.TwoWay,
                                                            propertyChanged: ItemsSourcePropertyChanged);

        public static BindableProperty SelectedItemProperty = BindableProperty.Create(
                                                            propertyName: "SelectedItem",
                                                            returnType: typeof(string),
                                                            declaringType: typeof(MultiPicker),
                                                            defaultValue: null,
                                                            defaultBindingMode: BindingMode.TwoWay,
                                                            propertyChanged: SelectedItemPropertyChanged);

        public static BindableProperty SelectedIndexProperty = BindableProperty.Create(
                                                            propertyName: "SelectedIndex",
                                                            returnType: typeof(int),
                                                            declaringType: typeof(MultiPicker),
                                                            defaultValue: -1,
                                                            defaultBindingMode: BindingMode.TwoWay,
                                                            propertyChanged: SelectedIndexPropertyChanged);

        public static BindableProperty TitleProperty = BindableProperty.Create(
                                                            propertyName: "TitleProperty",
                                                            returnType: typeof(string),
                                                            declaringType: typeof(MultiPicker),
                                                            defaultValue: string.Empty,
                                                            defaultBindingMode: BindingMode.TwoWay,
                                                            propertyChanged: TitlePropertyChanged);

        public static BindableProperty PlaceholderProperty = BindableProperty.Create(
                                                            propertyName: "PlaceholderProperty",
                                                            returnType: typeof(string),
                                                            declaringType: typeof(MultiPicker),
                                                            defaultValue: string.Empty,
                                                            defaultBindingMode: BindingMode.TwoWay,
                                                            propertyChanged: PlaceholderPropertyChanged);

        public static BindableProperty PlaceHolderColorProperty = BindableProperty.Create(
                                                            propertyName: "PlaceHolderColor",
                                                            returnType: typeof(Color),
                                                            declaringType: typeof(MultiPicker),
                                                            defaultValue: Color.Gray,
                                                            defaultBindingMode: BindingMode.TwoWay,
                                                            propertyChanged: PlaceHolderColorPropertyChanged);

        private static void PlaceHolderColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MultiPicker)bindable;

            if (control != null)
            {
                control.PlaceHolderColor = (Color)newValue;
            }
        }

        public static BindableProperty TextColorProperty = BindableProperty.Create(
                                                            propertyName: "TextColor",
                                                            returnType: typeof(Color),
                                                            declaringType: typeof(MultiPicker),
                                                            defaultValue: Color.White,
                                                            defaultBindingMode: BindingMode.TwoWay,
                                                            propertyChanged: TextColorPropertyChanged);


        private static void TextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MultiPicker)bindable;

            if (control != null)
            {
                control.TextColor = (Color)newValue;
            }
        }

        private static void SelectedItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MultiPicker)bindable;

            if (control != null)
            {
                control.SelectedItem = (string)newValue;
                control.SelectedTxt.Text = control.SelectedItem;
            }
        }

        private static void TitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MultiPicker)bindable;

            if (control != null)
            {
                control.Title = (string)newValue;
                control.HeaderTxt.Text = control.Title;
            }
        }

        private static void PlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MultiPicker)bindable;

            if (control != null)
            {
                control.Placeholder = (string)newValue;
                control.SelectedTxt.Text = control.Placeholder;
                control.SelectedTxt.TextColor = Color.Gray;
            }
        }

        private static void SelectedIndexPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MultiPicker)bindable;

            if (control != null)
            {
                control.SelectedIndex = (int)newValue;
            }
        }



        private static void ItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MultiPicker)bindable;

            if (control != null)
            {
                control.ItemsSource = (IEnumerable<string>)newValue;
            }
        }

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }


        public Color PlaceHolderColor
        {
            get { return (Color)GetValue(PlaceHolderColorProperty); }
            set { SetValue(PlaceHolderColorProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public bool IsUnderLineVisible
        {
            get => BoxViewLine.IsVisible;
            set => BoxViewLine.IsVisible = value;
        }

        public bool IsArrowVisible
        {
            get => arrow.IsVisible;
            set => arrow.IsVisible = value;
        }

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public string SelectedItem
        {
            get
            {
                return (string)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        public int SelectedIndex
        {
            get
            {
                return (int)GetValue(SelectedIndexProperty);
            }
            set
            {
                SetValue(SelectedIndexProperty, value);
            }
        }

        public IEnumerable<string> ItemsSource
        {
            get { return (IEnumerable<string>)base.GetValue(ItemsSourceProperty); }
            set { base.SetValue(ItemsSourceProperty, value); }
        }


        private void SetTap()
        {
            TapGestureRecognizer tgr = new TapGestureRecognizer();
            tgr.NumberOfTapsRequired = 1;
            tgr.Tapped += (s, e) =>
            {
                var pickerPop = new MultiSelectPopUp();
                pickerPop.BindingContext = this;

                pickerPop.SetBinding(MultiSelectPopUp.ItemsSourceProperty, "ItemsSource");
                pickerPop.SetBinding(MultiSelectPopUp.PickerTitleProperty, "Title");

                pickerPop.SelectedIndexChanged += (p, t) =>
                {
                    this.SelectedIndex = t.SelectedIndex;
                    this.SelectedItem = t.DisplayText;
                    //SelectedTxt.TextColor = this.TextColor;
                    SelectedTxt.TextColor = Color.White;

                    SelectedIndexChanged?.Invoke(this, null);
                };

                Navigation.PushPopupAsync(pickerPop, true);
            };

            this.GestureRecognizers.Add(tgr);
        }
    }
}
