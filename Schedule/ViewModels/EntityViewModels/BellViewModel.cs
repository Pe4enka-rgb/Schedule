using MathCore.WPF.Commands;
using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Schedule.ViewModels.EntityViewModels {
	internal class BellViewModel : ViewModel {
		private readonly IRepository<Bell> _bellRepository;

		private ObservableCollection<Bell> _bells;

		#region Properies
		public ObservableCollection<Bell> Bells {
			get { return _bells; }
			set { Set(ref _bells, value); }
		}
		#endregion


		#region TEST

		private string[,] _strings;
		public string[,] Strings {
			get { return _strings; }
			set { Set(ref _strings, value); }
		}

		private string[] _columnHeaderStrings;
		public string[] ColumnHeaderStrings {
			get { return _columnHeaderStrings; }
			set { Set(ref _columnHeaderStrings, value); }
		}


		#endregion

		#region
		private ICommand _LoadDataCommand;

		public ICommand LoadDataCommand =>
			_LoadDataCommand ??= new LambdaCommandAsync(OnLoadDataCommandExecuted);

		private async Task OnLoadDataCommandExecuted() {
			await Task.CompletedTask;

			int classNumber = 10;

			Strings = new string[Bells.Count, classNumber];
			for (int i = 0; i < Bells.Count; i++) {
				for (int j = 0; j < classNumber; j++) {
					Strings[i, j] = i.ToString() + " " + j.ToString();
				}

			}

			ColumnHeaderStrings = new string[classNumber];
			for (int i = 0; i < classNumber; i++) {
				ColumnHeaderStrings[i] = i.ToString();
			}
		}

		#endregion


		public BellViewModel(IRepository<Bell> bellRepository) {

			this._bellRepository = bellRepository;
			Bells = new(_bellRepository.Items.ToArray());




		}
	}

}
