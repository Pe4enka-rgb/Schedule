using MathCore.WPF.Commands;
using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Schedule.ViewModels {
	public class SubjectListToDragViewModel : ViewModel {
		private readonly IRepository<Subject> _subjectRepository;


		private ObservableCollection<Subject> _subjects;
		public ObservableCollection<Subject> Subjects {
			get => _subjects;
			private set => Set(ref _subjects, value);
		}

		private Subject _selectedSubject;
		public Subject SelectedSubject {
			get => _selectedSubject;
			set => Set(ref _selectedSubject, value);
		}

		public ICommand _loadDataCommand;

		public ICommand LoadDataCommand =>
			_loadDataCommand ??= new LambdaCommand(OnLoadDataCommandExecuted);

		private void OnLoadDataCommandExecuted() {
			Subjects = _subjectRepository
				.Items
				.OrderBy(s => s.Name)
				.ToObservableCollection();
		}

		public SubjectListToDragViewModel() { }
		public SubjectListToDragViewModel(
			IRepository<Subject> subjectRepository
		) {
			_subjectRepository = subjectRepository;
		}

	}
}
