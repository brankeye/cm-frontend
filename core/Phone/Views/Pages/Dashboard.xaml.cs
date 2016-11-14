using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            InitializeView(true);
            BindingContext = new ViewModels.Pages.Dashboard();
        }

        public void InitializeView(bool isStudent)
        {
            var classesButton = new Button()
            {
                Text = "Classes"
            };
            var studentsButton = new Button()
            {
                Text = "Students"
            };
            var profileButton = new Button()
            {
                Text = "Profile"
            };
            var schoolButton = new Button()
            {
                Text = "School"
            };
            var signoutButton = new Button()
            {
                Text = "Signout"
            };

            if (isStudent)
            {
                var evaluationsButton = new Button()
                {
                    Text = "Evaluations"
                };
                DashboardGrid.Children.Add(classesButton, 0, 0);
                DashboardGrid.Children.Add(evaluationsButton, 1, 0);
                DashboardGrid.Children.Add(studentsButton, 0, 1);
                DashboardGrid.Children.Add(schoolButton, 1, 1);
                DashboardGrid.Children.Add(profileButton, 0, 2);
                DashboardGrid.Children.Add(signoutButton, 1, 2);
            }
            else
            {
                DashboardGrid.Children.Add(classesButton, 0, 0);
                DashboardGrid.Children.Add(studentsButton, 1, 0);
                DashboardGrid.Children.Add(schoolButton, 0, 1);
                DashboardGrid.Children.Add(profileButton, 1, 1);
                DashboardGrid.Children.Add(signoutButton, 0, 2);
            }
        }
    }
}
