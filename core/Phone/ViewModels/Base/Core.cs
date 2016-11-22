using System.ComponentModel;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.ViewModels.Base
{
    public class Core
    {
        public virtual void OnAppearing()
        {
            
        }

        public virtual void OnDisappearing()
        {

        }

        public INavigation Navigation { get; set; }
    }
}
