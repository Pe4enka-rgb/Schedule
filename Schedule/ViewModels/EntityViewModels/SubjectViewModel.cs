using MathCore.WPF.Commands;
using Microsoft.EntityFrameworkCore;
using Schedule.DB.Entity;
using Schedule.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Schedule.ViewModels.EntityViewModels {
	class SubjectViewModel : Base.ViewModel {
		private readonly Interfaces.IRepository<Subject> _subjectRepository;
		private ObservableCollection<Subject> _subjects;

		#region Properies
		public ObservableCollection<Subject> Subjects {
			get { return _subjects; }
			set { Set(ref _subjects, value); }
		}
		#endregion

		#region LoadData Command

		private ICommand _LoadDataCommand;
		public ICommand LoadDataCommand =>
			_LoadDataCommand ?? new LambdaCommandAsync(OnLoadDataCommandExecuted);

		private async Task OnLoadDataCommandExecuted() {
			Subjects = new(await _subjectRepository.Items.ToArrayAsync());
		}

		#endregion

		public SubjectViewModel(IRepository<Subject> subjectRepository) {
			_subjectRepository = subjectRepository;
		}
	}
}