using System.ComponentModel;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Controls.PrettyListViewItems.Base
{
    public class Core : INotifyPropertyChanged
    {
        public Color SelectedColor
        {
            get { return _selectedColor; }
            set { this.SetProperty(ref _selectedColor, value, PropertyChanged); }
        }
        private Color _selectedColor;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
