using System;
using cm.frontend.core.Phone.Views.Pages.Base;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class CalendarClass
    {
        private Base.PageController<ViewModels.Pages.Details.CalendarClass> PageController { get; }

        public CalendarClass()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Details.CalendarClass>(this);
        }

        public void Initialize(int classLocalId, DateTimeOffset date)
        {
            PageController.ViewModel.Initialize(classLocalId, date);
        }
    }
}
