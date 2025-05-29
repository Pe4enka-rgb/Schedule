using MathCore.WPF.Commands;
using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Schedule.ViewModels.EntityViewModels {
	internal class BellViewModel : ViewModel {
		private readonly IRepository<Bell> _bellRepository;


		#region Properies

		private ObservableCollection<Bell> _bells;
		public ObservableCollection<Bell> Bells {
			get => _bells;
			set => Set(ref _bells, value);
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

		#endregion


		public BellViewModel(IRepository<Bell> bellRepository) {

			this._bellRepository = bellRepository;

		}
	}

}
