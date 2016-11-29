using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Master
    {
        public Master()
        {
            InitializeComponent();
        }

        public ListView MasterListView => ItemsListView;
    }
}
