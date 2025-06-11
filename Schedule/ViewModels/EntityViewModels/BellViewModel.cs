using MathCore.WPF.Commands;
using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.Services.Interfaces;
using Schedule.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Schedule.ViewModels.EntityViewModels {
	internal class BellViewModel : ViewModel {
		private readonly IRepository<Bell> _bellRepository;
		private readonly IUserDialog<Bell> _bellUserDialog;

		#region Properies

		private ObservableCollection<Bell> _bells;
		public ObservableCollection<Bell> Bells {
			get => _bells;
			set {
				Set(ref _bells, value);
				OnPropertyChanged();
			}
		}


		private Bell _SelectedBell;
		public Bell SelectedBell {
			get => _SelectedBell;
			set => Set(ref _SelectedBell, value);
		}
		#endregion

		#region Commands
		private ICommand _LoadDataCommand;

		public ICommand LoadDataCommand =>
			_LoadDataCommand ??= new LambdaCommandAsync(OnLoadDataCommandExecuted);

		private async Task OnLoadDataCommandExecuted() {
			await Task.CompletedTask;

			Bells = new(_bellRepository.Items.ToArray());
		}

		#region Add command
		private ICommand _AddBellCommand;
		public ICommand AddBellCommand => _AddBellCommand
			??= new LambdaCommand(OnAddBellCommandExecuted);

		private void OnAddBellCommandExecuted() {
			Bell newBell = new();
			if (!_bellUserDialog.Edit(newBell)) {
				return;
			}
			_bellRepository.Add(newBell);
			Bells.Add(newBell);
			SelectedBell = newBell;
		}
		#endregion

		#region Edit command

		private ICommand _EditBellCommand;
		public ICommand EditBellCommand => _EditBellCommand
			??= new LambdaCommand<Bell>(OnEditBellCommandExecuted);

		private void OnEditBellCommandExecuted(Bell parametrBell) {
			if (parametrBell == null)
				return;
			if (!_bellUserDialog.Edit(parametrBell)) {
				return;
			}
			_bellRepository.Update(parametrBell);
			Bells = Bells.OrderBy(b => b.Start).ToObservableCollection();
			SelectedBell = parametrBell;
		}
		#endregion

		#region Delete command



		private ICommand _DeleteBellCommand;
		public ICommand DeleteBellCommand => _DeleteBellCommand
			??= new LambdaCommand<Bell>(OnDeleteBellCommandExecuted, CanDeleteBellCommandExecuted);
		private bool CanDeleteBellCommandExecuted(Bell bell) => bell != null || SelectedBell != null;

		private void OnDeleteBellCommandExecuted(Bell parametrBell) {

			var bellToRemove = parametrBell ?? SelectedBell;

			if (bellToRemove != null) {
				_bellRepository.Remove(bellToRemove);
				_bells.Remove(bellToRemove);
			}

			SelectedBell = null;
		}
		#endregion

		#endregion


		public BellViewModel(IRepository<Bell> bellRepository, IUserDialog<Bell> bellUserDialog) {
			this._bellRepository = bellRepository;
			_bellUserDialog = bellUserDialog;
		}
	}

}
