﻿using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Pages
{
    public partial class Login
    {
        public Login()
        {
            InitializeComponent();
        }

        private void NewUserSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            // if they are a new user then set the button to "Register", otherwise "Login"
            ActionButton.Text = e.Value ? "Register" : "Login";
        }
    }
}
