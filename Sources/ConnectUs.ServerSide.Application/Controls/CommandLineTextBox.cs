using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ConnectUs.ServerSide.Application.Controls
{
    public class CommandLineTextBox : TextBox
    {
        private const string CommandCharacter = ">";
        private int _currentReadyOnlyIndex = 0;
        private readonly Key[] _allowedKeysWhenReadOnly = {Key.Up, Key.Down, Key.Left, Key.Right};

        public static readonly DependencyProperty CommandLinesProperty =
            DependencyProperty.Register("CommandLines", typeof (ObservableCollection<CommandLine>), typeof (CommandLineTextBox), new PropertyMetadata(new ObservableCollection<CommandLine>()));

        // ----- Properties
        private bool IsCurrentSelectionInReadOnlyPart
        {
            get { return SelectionStart < _currentReadyOnlyIndex; }
        }
        public ObservableCollection<CommandLine> CommandLines
        {
            get { return (ObservableCollection<CommandLine>) GetValue(CommandLinesProperty); }
            set { SetValue(CommandLinesProperty, value); }
        }

        // ----- Overides
        public override void BeginInit()
        {
            base.BeginInit();
            AppendCommandCharacter();
        }
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (IsCurrentSelectionInReadOnlyPart) {
                if (_allowedKeysWhenReadOnly.Contains(e.Key) == false) {
                    if (char.IsLetterOrDigit((char) e.Key)) {
                        SetSelectionAddTheEnd();
                    }
                    else {
                        e.Handled = true;
                    }
                }
            }
            else {
                switch (e.Key) {
                    case Key.Enter:
                        CreateNewCommand();
                        break;
                    case Key.Back:
                        if (SelectionStart == _currentReadyOnlyIndex) {
                            e.Handled = true;
                        }
                        break;
                }
            }

            base.OnPreviewKeyDown(e);
        }

        // ----- Internal logics
        private void CreateNewCommand()
        {
            var command = Text.Substring(_currentReadyOnlyIndex, Text.Length - _currentReadyOnlyIndex);
            var commandLine = new CommandLine(command);
            CommandLines.Add(commandLine);
            if (string.IsNullOrEmpty(commandLine.Result) == false) {
                Text += Environment.NewLine;
                Text += commandLine.Result;
            }
            Text += Environment.NewLine;
            AppendCommandCharacter();
        }
        private void AppendCommandCharacter()
        {
            Text += CommandCharacter + " ";
            UpdateReadOnlyPart();
            SetSelectionAddTheEnd();
        }
        private void UpdateReadOnlyPart()
        {
            _currentReadyOnlyIndex = Text.Length;
        }
        private void SetSelectionAddTheEnd()
        {
            SelectionStart = Text.Length;
            SelectionLength = 0;
        }
    }
}