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
            ViewModel.Initialize(schoolName, isManaging);
        }
    }
}
