using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace ConnectUs.ServerSide.Application.Controls
{
    public class ConsoleLineTextBox : TextBox
    {
        private int _currentIndex = 0;
        private const string StartChar = ">";

        public override void BeginInit()
        {
            base.BeginInit();

            Text = StartChar + " ";
            _currentIndex = Text.Length;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (char.IsLetterOrDigit((char) e.Key) && SelectionStart < _currentIndex) {
                e.Handled = true;
            }
            base.OnPreviewKeyUp(e);

            if (e.Key == Key.Enter) {
                Text += Environment.NewLine + StartChar + " ";
                _currentIndex = Text.Length;
                SelectionStart = _currentIndex;
            }
        }
    }
}