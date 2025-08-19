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
			Subjects = _subjectRepository.Items.OrderBy(s => s.Name).ToObservableCollection();
		}

		private ICommand _AddSubjectCommand;
		public ICommand AddSubjectCommand => _AddSubjectCommand
			??= new LambdaCommand(OnAddSubjectCommandExecuted);
		private void OnAddSubjectCommandExecuted() {
			Subject newSubject = new();
			if (!_userDialog.Edit(newSubject)) {
				return;
			}
			_subjectRepository.Add(newSubject);
			Subjects.Add(newSubject);
			SelectedSubject = newSubject;

		}

		private ICommand _EditSubjectCommand;
		public ICommand EditSubjectCommand => _EditSubjectCommand
			??= new LambdaCommand<Subject>(OnEditSubjectCommandExecuted);
		private void OnEditSubjectCommandExecuted(Subject parametrSubject) {
			if (parametrSubject is null)
				return;
			if (!_userDialog.Edit(parametrSubject)) {
				return;
			}
			_subjectRepository.Update(parametrSubject);
			Subjects = Subjects.OrderBy(s => s.Name).ToObservableCollection();
			SelectedSubject = parametrSubject;
		}

		private ICommand _DeleteSubjectCommand;
		public ICommand DeleteSubjectCommand => _DeleteSubjectCommand
			??= new LambdaCommand<Subject>(OnDeleteSubjectCommandExecuted, CanDeleteSubjectCommandExecuted);
		private bool CanDeleteSubjectCommandExecuted(Subject subject) => subject is not null || SelectedSubject is not null;
		private void OnDeleteSubjectCommandExecuted(Subject parametrSubject) {
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