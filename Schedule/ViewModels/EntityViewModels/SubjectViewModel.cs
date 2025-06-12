using MathCore.WPF.Commands;
using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Schedule.ViewModels.EntityViewModels {
	class SubjectViewModel : Base.ViewModel {
		private readonly IRepository<Subject> _subjectRepository;
		private readonly IUserDialog<Subject> _userDialog;

		private ObservableCollection<Subject> _subjects;

		#region Properies
		public ObservableCollection<Subject> Subjects {
			get { return _subjects; }
			set { Set(ref _subjects, value); }
		}

		private Subject _SelectedSubject;
		public Subject SelectedSubject {
			get => _SelectedSubject;
			set => Set(ref _SelectedSubject, value);
		}
		#endregion

		#region LoadData Command

		private ICommand _LoadDataCommand;
		public ICommand LoadDataCommand =>
			_LoadDataCommand ?? new LambdaCommand(OnLoadDataCommandExecuted);

		private void OnLoadDataCommandExecuted() {
			Subjects = _subjectRepository.Items.ToObservableCollection();
		}

		private ICommand _AddGradeCommand;
		public ICommand AddGradeCommand => _AddGradeCommand
			??= new LambdaCommand(OnAddGradeCommandExecuted);
		private void OnAddGradeCommandExecuted() {
			Subject newSubject = new();
			if (!_userDialog.Edit(newSubject)) {
				return;
			}
			_subjectRepository.Add(newSubject);
			Subjects.Add(newSubject);
			SelectedSubject = newSubject;

		}

		private ICommand _EditGradeCommand;
		public ICommand EditGradeCommand => _EditGradeCommand
			??= new LambdaCommand<Subject>(OnEditGradeCommandExecuted);
		private void OnEditGradeCommandExecuted(Subject parametrSubject) {
			if (parametrSubject is null)
				return;
			_subjectRepository.Update(parametrSubject);
			Subjects.GroupBy(s => s.Name);
			SelectedSubject = parametrSubject;
		}

		private ICommand _DeleteGradeCommand;
		public ICommand DeleteGradeCommand => _DeleteGradeCommand
			??= new LambdaCommand<Subject>(OnDeleteGradeCommandExecuted, CanDeleteGradeCommandExecuted);
		private bool CanDeleteGradeCommandExecuted(Subject subject) => subject is not null || SelectedSubject is not null;
		private void OnDeleteGradeCommandExecuted(Subject parametrSubject) {
			var subjectToRemove = parametrSubject ?? SelectedSubject;

			if (subjectToRemove != null) {
				_subjectRepository.Remove(subjectToRemove);
				Subjects.Remove(subjectToRemove);
			}

			SelectedSubject = null;
		}
		#endregion

		public SubjectViewModel(
			IRepository<Subject> subjectRepository,
			IUserDialog<Subject> userDialog) {
			_subjectRepository = subjectRepository;
			_userDialog = userDialog;
		}
	}
}