using System;

namespace cm.frontend.core.Phone.Views.Pages.Details
{
    public partial class CalendarClass
    {
        public CalendarClass()
        {
            InitializeComponent();
        }

        public void Initialize(int classLocalId, DateTimeOffset date)
        {
            ViewModel.Initialize(classLocalId, date);
        }
    }
}
