﻿using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace cm.frontend.core.Phone.Views.Behaviors
{
    public abstract class RegexValidator : Behavior<Entry>
    {
        private string _regex;

        private static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(RegexValidator), false, propertyChanged: IsValidChanged);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        private static void IsValidChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var validator = (RegexValidator) bindable;
            validator.IsValid = (bool) newValue;
        }

        protected RegexValidator(string regex)
        {
            _regex = regex;
        }

        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            private set { SetValue(IsValidPropertyKey, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += HandleTextChanged;
            BindingContext = bindable.BindingContext;
        }

        protected void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = Regex.IsMatch(e.NewTextValue, _regex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= HandleTextChanged;
            bindable.BindingContext = null;
        }
    }
}