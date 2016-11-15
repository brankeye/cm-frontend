using System.ComponentModel;
using cm.frontend.core.Domain.Extensions.NotifyPropertyChanged;

namespace cm.frontend.core.Phone.ViewModels.Controls.PrettyListViewItems
{
    public class Test : Base.Core
    {
        public Test(Test model)
        {
            TestModel = model;
        }

        public Test TestModel
        {
            get { return _model; }
            set { this.SetProperty(ref _model, value, PropertyChanged); }
        }
        private Test _model;

        public new event PropertyChangedEventHandler PropertyChanged;
    }
}
