﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TogglDesktop
{
    class ExtendedTextBox : TextBox
    {
        public static readonly DependencyProperty IsTextChangingProgrammaticallyProperty =
            DependencyProperty.Register(
            "IsTextChangingProgrammatically", typeof(bool),
            typeof(ExtendedTextBox));
        public bool IsTextChangingProgrammatically 
        { 
            get => (bool)GetValue(IsTextChangingProgrammaticallyProperty);
            set { SetValue(IsTextChangingProgrammaticallyProperty, value); }
        }

        public bool SelectAllOnKeyboardFocus { get; set; }

        public ExtendedTextBox()
        {
            this.SelectAllOnKeyboardFocus = true;
        }

        public void SetText(string text)
        {
            this.IsTextChangingProgrammatically = true;
            this.Text = text;
            this.IsTextChangingProgrammatically = false;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (!this.IsFocused)
            {
                this.SelectionLength = 0;
            }

            if (e.ClickCount == 3)
            {
                this.SelectAll();
            }

            base.OnMouseDown(e);
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            if (!(Mouse.LeftButton == MouseButtonState.Pressed && this.IsMouseOver))
            {
                this.onFocusedByTab();
            }

            base.OnGotKeyboardFocus(e);
        }

        private void onFocusedByTab()
        {
            if (this.SelectAllOnKeyboardFocus)
            {
                this.SelectAll();
            }

            if (this.SelectedText == "")
            {
                this.CaretIndex = this.Text.Length;
            }
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            if (this.SelectAllOnKeyboardFocus)
            {
                this.SelectionLength = 0;
            }

            base.OnLostKeyboardFocus(e);
        }
    }
}
