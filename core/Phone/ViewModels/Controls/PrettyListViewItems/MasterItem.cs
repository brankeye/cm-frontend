using System;
using System.ComponentModel;
using Xamarin.Forms;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;

namespace cm.frontend.core.Phone.ViewModels.Controls.PrettyListViewItems
{
    public class MasterItem : Base.Core
    {
        public MasterItem()
        {
            
        }

        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }
    }
}
