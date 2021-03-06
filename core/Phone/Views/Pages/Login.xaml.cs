﻿using System;
using cm.frontend.core.Phone.Views.Pages.Base;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Login
    {
        private Base.PageController<ViewModels.Pages.Login> PageController { get; }

        public Login()
        {
            InitializeComponent();
            PageController = new PageController<ViewModels.Pages.Login>(this);
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void NewUserSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            // if they are a new user then set the button to "Register", otherwise "Login"
            LoginRegisterButton.Text = e.Value ? "Register" : "Login";
        }

        private void PasswordVisibility_OnClicked(object sender, EventArgs e)
        {
            var button = (Button) sender;
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
            var visible = "ic_visibility_white_24dp.png";
            var notVisible = "ic_visibility_off_white_24dp.png";
            button.Image = PasswordEntry.IsPassword ? notVisible : visible;
        }
    }
}
