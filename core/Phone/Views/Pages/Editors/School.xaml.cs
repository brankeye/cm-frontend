namespace cm.frontend.core.Phone.Views.Pages.Editors
{
    public partial class School
    {
        public School()
        {
            InitializeComponent();
        }

        public void Initialize(string schoolName, bool isManaging)
        {
            ViewModel.SchoolModel.Name = schoolName;
            ViewModel.IsManaging = isManaging;
        }
    }
}
