using HappeningsApp.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HappeningsApp.Custom
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MultiSelectListPopUp : PopupPage
    {
       public static List<SelectableData<MultiPickerListItems>> AllSelectedData = new List<SelectableData<MultiPickerListItems>>();
        public event EventHandler<List<SelectableData<MultiPickerListItems>>> SelectedIndexChanged;

        public MultiSelectListPopUp ()
		{
			InitializeComponent ();
            BindingContext = new MMultiSelectPopUpViewModel();

        }

        public IEnumerable<string> ItemsSource
        {
            get { return (IEnumerable<string>)GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);

             
            }
        }
        public static BindableProperty ItemsSourceProperty = BindableProperty.Create(
                                                            propertyName: "ItemsSource",
                                                            returnType: typeof(IEnumerable<string>),
                                                            declaringType: typeof(MultiSelectPopUp),
                                                            defaultValue: null,
                                                            defaultBindingMode: BindingMode.TwoWay,
                                                            propertyChanged: ItemsSourceChanged);

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MultiSelectListPopUp)bindable;
            List<SelectableData<MultiPickerListItems>> SelectedData = new List<SelectableData<MultiPickerListItems>>();

              var items = control.ItemsSource;


            foreach (var item in items)
            {
                SelectedData.Add(new SelectableData<MultiPickerListItems>
                {
                    Data = new MultiPickerListItems { DisplayText = item}
                });
            }
            AllSelectedData = SelectedData;
            control.ListViewControl.ItemsSource = SelectedData;

       
        }

        private void Finished_Clicked(object sender, EventArgs e)
        {
            var select = GetNewData();
            Navigation.PopPopupAsync();
            SelectedIndexChanged?.Invoke(this, select);
        }
        private void Cancel(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
        public List<SelectableData<MultiPickerListItems>> GetNewData()
        {
            var list = new List<SelectableData<MultiPickerListItems>>();

            foreach (var data in AllSelectedData)
                list.Add(new SelectableData<MultiPickerListItems>() { Data = data.Data, Selected = data.Selected });

            return list;
        }
    }
}