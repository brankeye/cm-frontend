using System;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Controls
{
    public partial class PrettyListView
    {
        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColorProperty), typeof(Color), typeof(PrettyListView), Color.Default);
        public static readonly BindableProperty QuickSelectProperty = BindableProperty.Create(nameof(SelectedColorProperty), typeof(bool), typeof(PrettyListView), false);

        public new event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

        private ViewModels.Controls.PrettyListViewItems.Base.Core _lastSelectedItem = null;

        public PrettyListView() : base(ListViewCachingStrategy.RecycleElement)
        {
            InitializeComponent();
            base.ItemSelected += OnBaseItemSelected;
            ItemSelected += OnItemSelected;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            if (_lastSelectedItem != null)
            {
                _lastSelectedItem.SelectedColor = Color.Default;
                _lastSelectedItem = null;
            }

            _lastSelectedItem = selectedItemChangedEventArgs.SelectedItem as ViewModels.Controls.PrettyListViewItems.Base.Core;

            var listView = (PrettyListView)sender;
            if (listView.QuickSelect)
            {
                listView.SelectedItem = null;
            }
        }

        private void OnBaseItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            if (selectedItemChangedEventArgs.SelectedItem != null)
            {
                var selectedItem = selectedItemChangedEventArgs.SelectedItem as ViewModels.Controls.PrettyListViewItems.Base.Core;
                if (selectedItem != null)
                {
                    selectedItem.SelectedColor = SelectedColor;
                }

                ItemSelected?.Invoke(sender, selectedItemChangedEventArgs);
            }
        }

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        public bool QuickSelect
        {
            get { return (bool)GetValue(QuickSelectProperty); }
            set { SetValue(QuickSelectProperty, value); }
        }
    }
}
