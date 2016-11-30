using System;

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
