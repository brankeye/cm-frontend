using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Controls
{
    public partial class BindablePicker : Picker
    {
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(BindablePicker), null, BindingMode.TwoWay, null, OnSelectedItemChanged);
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(BindablePicker), null, BindingMode.OneWay, null, OnItemsSourceChanged);
        public static readonly BindableProperty DisplayPropertyProperty = BindableProperty.Create(nameof(DisplayProperty), typeof(string), typeof(BindablePicker), null, BindingMode.OneWay, null, OnDisplayPropertyChanged);

        public BindablePicker()
        {
            SelectedIndexChanged += OnSelectedIndexChanged;
        }

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set
            {
                SetValue(SelectedItemProperty, value);
                if (ItemsSource.Contains(SelectedItem))
                {
                    SelectedIndex = ItemsSource.IndexOf(SelectedItem);
                }
                else
                {
                    SelectedIndex = -1;
                }
            }
        }

        public string DisplayProperty
        {
            get { return (string)GetValue(DisplayPropertyProperty); }
            set { SetValue(DisplayPropertyProperty, value); }
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndex < 0) return;
            SelectedItem = ItemsSource[SelectedIndex];
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (BindablePicker)bindable;
            picker.SelectedItem = newValue;
            if (picker.ItemsSource == null || picker.SelectedItem == null) return;

            var count = 0;
            foreach (var item in picker.ItemsSource)
            {
                if (item == picker.SelectedItem)
                {
                    picker.SelectedIndex = count;
                    break;
                }
                count++;
            }
        }

        private static void OnDisplayPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (BindablePicker)bindable;
            picker.DisplayProperty = (string)newValue;
            LoadItemsAndSetSelected(bindable);
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (BindablePicker)bindable;
            picker.ItemsSource = (IList)newValue;

            var observableCollection = newValue as INotifyCollectionChanged;

            if (observableCollection != null)
            {
                observableCollection.CollectionChanged += (oldValueVar, newValueVar) =>
                {
                    LoadItemsAndSetSelected(bindable);
                };
            }

            LoadItemsAndSetSelected(bindable);
        }

        private static void LoadItemsAndSetSelected(BindableObject bindable)
        {
            var picker = (BindablePicker)bindable;
            if (picker.ItemsSource == null) return;

            picker.Items.Clear();
            var count = 0;
            foreach (var item in picker.ItemsSource)
            {
                var value = string.Empty;
                if (picker.DisplayProperty != null)
                {
                    var prop = item.GetType().GetRuntimeProperties().FirstOrDefault(p => string.Equals(p.Name, picker.DisplayProperty, StringComparison.OrdinalIgnoreCase));
                    if (prop != null)
                    {
                        value = prop.GetValue(item).ToString();
                    }
                }
                else
                {
                    value = item.ToString();
                }
                picker.Items.Add(value);
                if (picker.SelectedItem != null)
                {
                    if (picker.SelectedItem == item)
                    {
                        picker.SelectedIndex = count;
                    }
                }
                count++;
            }
        }
    }
}
