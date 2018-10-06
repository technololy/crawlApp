using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HappeningsApp.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace HappeningsApp.Custom
{
    public partial class MultiSelectPopUp : PopupPage
    {
        public MultiSelectPopUp()
        {
            InitializeComponent();
            
        }


        void Reload()
        {
            var context = (MultiPicker)this.BindingContext;
            context.ReloadItemsSourceCommand.Execute(null);
        }

        public event EventHandler<MultiPickerItems> SelectedIndexChanged;

        public ICommand ReloadCommand { get; set; }

        public static BindableProperty ItemsSourceProperty = BindableProperty.Create(
                                                            propertyName: "ItemsSource",
                                                            returnType: typeof(IEnumerable<string>),
                                                            declaringType: typeof(MultiSelectPopUp),
                                                            defaultValue: null,
                                                            defaultBindingMode: BindingMode.TwoWay,
                                                            propertyChanged: ItemsSourceChanged);

        public static BindableProperty IsRefreshVisibleProperty = BindableProperty.Create(
                                                            propertyName: "IsRefreshVisible",
                                                            returnType: typeof(bool),
                                                            declaringType: typeof(MultiSelectPopUp),
                                                            defaultValue: false,
                                                            defaultBindingMode: BindingMode.TwoWay);

        public static BindableProperty PickerTitleProperty = BindableProperty.Create(
                                                            propertyName: "PickerTitleProperty",
                                                            returnType: typeof(string),
                                                            declaringType: typeof(MultiSelectPopUp),
                                                            defaultValue: string.Empty,
                                                            defaultBindingMode: BindingMode.TwoWay,
                                                            propertyChanged: PickerTitleChanged);


        public List<MultiPickerItems> Items { get => SetItems(); }

        public IEnumerable<string> ItemsSource
        {
            get { return (IEnumerable<string>)GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);

                if (ItemsSource == null || ItemsSource.Count() == 0)
                    IsRefreshVisible = true;
                else
                    IsRefreshVisible = false;
            }
        }

        public string PickerTitle
        {
            get { return (string)GetValue(PickerTitleProperty); }
            set { SetValue(PickerTitleProperty, value); }
        }

        public bool IsRefreshVisible
        {
            get { return (bool)GetValue(IsRefreshVisibleProperty); }
            set { SetValue(IsRefreshVisibleProperty, value); }
        }


        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MultiSelectPopUp)bindable;


            var items = control.ItemsSource;

            List<MultiPickerItems> MultiPickerItems = new List<MultiPickerItems>();

            foreach (var item in items)
            {
                MultiPickerItems.Add(new MultiPickerItems
                {
                    DisplayText = item,
                    SelectedIndex = items.IndexOf(item)
                });
            }

            control.PickerList.ItemsSource = MultiPickerItems;

            if (control.ItemsSource == null || control.ItemsSource.Count() == 0)
                control.RefreshView.IsVisible = true;
            else
                control.RefreshView.IsVisible = false;
        }

        private static void PickerTitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MultiSelectPopUp)bindable;

            control.PickerTitle = (string)newValue;
            control.HeaderTxt.Text = (string)newValue;
        }

        public List<MultiPickerItems> SetItems()
        {
            List<MultiPickerItems> MultiPickerItems = new List<MultiPickerItems>();

            foreach (var item in ItemsSource)
            {
                MultiPickerItems.Add(new MultiPickerItems
                {
                    DisplayText = item,
                    SelectedIndex = ItemsSource.IndexOf(item)
                });
            }


            return MultiPickerItems;
        }

        private void PickerList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (PickerList.SelectedItem == null)
                return;

            var item = (MultiPickerItems)PickerList.SelectedItem;

            Navigation.PopPopupAsync();
            SelectedIndexChanged?.Invoke(this, item);
        }

        private void Cancel(object sender, EventArgs e)
        {
           
           
            Navigation.PopPopupAsync();

        }

        public List<SelectableData<MultiPickerItems>> DataList { get; set; }

        public List<SelectableData<MultiPickerItems>> GetNewData()
        {
            var list = new List<SelectableData<MultiPickerItems>>();

            foreach (var data in Items)
                list.Add(new SelectableData<MultiPickerItems>() 
            { 
                Selected = data.SelectedSwitch,
                Data = new MultiPickerItems{DisplayText = data.DisplayText} 
            });

            return list;
        }

        void HandleSwitch_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
           try
            {
                var control = sender as Switch ;
                var item = control.BindingContext as MultiPickerItems ;


            }
            catch (Exception ex)
            {
                var log = ex;
            }
           
        }

        private void Refresh(object sender, EventArgs e)
        {
            var context = (MultiPicker)this.BindingContext;

            Task.Run(async () => {
                while (RefreshBtn.IsVisible)
                {
                    await RefreshBtn.RelRotateTo(360, 500);
                    await Task.Delay(100);
                }
            });

            context.RefreshContent?.Invoke();
        }

    }
    public class MultiPickerItems
    {
        public string DisplayText { get; set; }
        public int SelectedIndex { get; set; }
        public bool SelectedSwitch
        {
            get;
            set;
        }
    }
}
