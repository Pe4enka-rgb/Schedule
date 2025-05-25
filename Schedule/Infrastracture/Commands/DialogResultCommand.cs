using System.Windows.Input;

namespace Schedule.Infrastracture.Commands {
	internal class DialogResultCommand : ICommand {
		public event EventHandler? CanExecuteChanged;
		public bool DialogResult { get; set; }
		public bool CanExecute(object? parameter) => App.ActiveWindow != null;

		public void Execute(object? parameter) {
			if (!CanExecute(parameter))
				return;


			var dialog_result = DialogResult;
			if (parameter != null) {
				dialog_result = Convert.ToBoolean(parameter);
			}

			var window = App.CurrentWindow;


			window.DialogResult = DialogResult;
			window.Close();
		}

	}
}
