using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Dashboard : ContentPage
    {
        public ViewModels.Pages.Dashboard ViewModel => _vm ?? (_vm = BindingContext as ViewModels.Pages.Dashboard);
        private ViewModels.Pages.Dashboard _vm;

        public Dashboard()
        {
            InitializeComponent();
            InitializeView(false);
            BindingContext = new ViewModels.Pages.Dashboard();
        }

        public void InitializeView(bool isStudent)
        {
            var calendarButton = new Button()
            {
                Text = "Calendar",
                CommandParameter = "Calendar"
            };
            calendarButton.SetBinding(Button.CommandProperty, new Binding("LaunchCommand"));

            var studentsButton = new Button()
            {
                Text = "Students",
                CommandParameter = "Students"
            };
            studentsButton.SetBinding(Button.CommandProperty, new Binding("LaunchCommand"));

            var profileButton = new Button()
            {
                Text = "Profile",
                CommandParameter = "Profile"
            };
            profileButton.SetBinding(Button.CommandProperty, new Binding("LaunchCommand"));

            var schoolButton = new Button()
            {
                Text = "School",
                CommandParameter = "School"
            };
            schoolButton.SetBinding(Button.CommandProperty, new Binding("LaunchCommand"));

            var signoutButton = new Button()
            {
                Text = "Signout",
                CommandParameter = "Signout"
            };
            signoutButton.SetBinding(Button.CommandProperty, new Binding("LaunchCommand"));

            if (isStudent)
            {
                var evaluationsButton = new Button()
                {
                    Text = "Evaluations",
                    CommandParameter = "Evaluations"
                };
                evaluationsButton.SetBinding(Button.CommandProperty, new Binding("LaunchCommand"));

                DashboardGrid.Children.Add(calendarButton, 0, 0);
                DashboardGrid.Children.Add(evaluationsButton, 1, 0);
                DashboardGrid.Children.Add(studentsButton, 0, 1);
                DashboardGrid.Children.Add(schoolButton, 1, 1);
                DashboardGrid.Children.Add(profileButton, 0, 2);
                DashboardGrid.Children.Add(signoutButton, 1, 2);
            }
            else
            {
                var classesButton = new Button()
                {
                    Text = "Classes",
                    CommandParameter = "Classes"
                };
                classesButton.SetBinding(Button.CommandProperty, new Binding("LaunchCommand"));

                DashboardGrid.Children.Add(calendarButton, 0, 0);
                DashboardGrid.Children.Add(classesButton, 1, 0);
                DashboardGrid.Children.Add(studentsButton, 0, 1);
                DashboardGrid.Children.Add(schoolButton, 1, 1);
                DashboardGrid.Children.Add(profileButton, 0, 2);
                DashboardGrid.Children.Add(signoutButton, 1, 2);
            }
        }
    }
}
