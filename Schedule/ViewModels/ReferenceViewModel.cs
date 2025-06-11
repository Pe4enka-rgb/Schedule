using MathCore.WPF.Commands;
using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.Services.Interfaces;
using Schedule.ViewModels.Base;
using Schedule.ViewModels.EntityViewModels;
using System.Windows.Input;

namespace Schedule.ViewModels {
	internal class ReferenceViewModel : ViewModel {
		private readonly IRepository<Subject> _subjectRepository;
		private readonly IRepository<Bell> _bellRepository;
		private readonly IRepository<SchoolClass> _schoolClassRepository;
		private readonly IRepository<Grade> _gradeRepository;
		private readonly IUserDialog<Grade> _gradeUserDialog;
		private readonly IUserDialog<Subject> _subjectUserDialog;
		private readonly IUserDialog<Bell> _bellUserDialog;
		private readonly IUserDialog<SchoolClass> _schoolClassUserDialog;

		#region CurrentViewModel - Текущая Модель Представления

		private ViewModel _currentViewModel;
		public ViewModel CurrentViewModel {
			get { return _currentViewModel; }
			set { Set(ref _currentViewModel, value); }
		}

		#endregion

		#region LoadData Command

		private ICommand _LoadDataCommand;
		public ICommand LoadDataCommand =>
			_LoadDataCommand ?? new LambdaCommandAsync(OnLoadDataCommandExecuted);

		private async Task OnLoadDataCommandExecuted() {
			CurrentViewModel = new GradeViewModel(_gradeRepository, _gradeUserDialog);
		}

		#endregion

		#region Grade View Command
		private ICommand _gradeViewCommand;
		public ICommand GradeViewCommand => _gradeViewCommand
			??= new LambdaCommand(OnGradeViewCommandExecuted);
		private void OnGradeViewCommandExecuted() {
			if (!CurrentViewModel.GetType().Equals(typeof(GradeViewModel)))
				CurrentViewModel = new GradeViewModel(
					_gradeRepository,
					_gradeUserDialog
				);
		}

		#endregion

		#region Subject View Command
		private ICommand _subjectViewCommand;
		public ICommand SubjectViewCommand => _subjectViewCommand
		?? new LambdaCommand(OnSubjectViewCommandExecuted, CanSubjectViewCommandExecute);

		private bool CanSubjectViewCommandExecute() => true;

		private void OnSubjectViewCommandExecuted() {
			if (CurrentViewModel.GetType().Equals(typeof(SubjectViewModel)))
				return;

			CurrentViewModel = new SubjectViewModel(
				_subjectRepository,
				_subjectUserDialog
			);
		}
		#endregion

		#region Bell View Command
		private ICommand _bellViewCommand;
		public ICommand BellViewCommand => _bellViewCommand
		?? new LambdaCommand(OnBellViewCommandExecuted);
		private void OnBellViewCommandExecuted() {
			if (!CurrentViewModel.GetType().Equals(typeof(BellViewModel)))
				CurrentViewModel = new BellViewModel(
					_bellRepository,
					_bellUserDialog
					);
		}
		#endregion

		#region SchoolClass View Command
		private ICommand _schoolClassViewCommand;
		public ICommand SchoolClassViewCommand => _schoolClassViewCommand
		??= new LambdaCommand(OnSchoolClassViewCommandExecuted);


		private void OnSchoolClassViewCommandExecuted() {
			CurrentViewModel = new SchoolClassViewModel(
				_schoolClassRepository,
				_gradeRepository,
				_schoolClassUserDialog
			);
		}
		#endregion


		public ReferenceViewModel() { }
		public ReferenceViewModel(
			IRepository<Subject> subjectRepository,
			IRepository<Bell> bellRepository,
			IRepository<SchoolClass> schoolClassRepository,
			IRepository<Grade> gradeRepository,

			IUserDialog<Grade> gradeUserDialog,
			IUserDialog<Subject> subjectUserDialog,
			IUserDialog<Bell> bellUserDialog,
			IUserDialog<SchoolClass> schoolClassUserDialog
			) {
			_subjectRepository = subjectRepository;
			_bellRepository = bellRepository;
			_schoolClassRepository = schoolClassRepository;
			_gradeRepository = gradeRepository;

			_gradeUserDialog = gradeUserDialog;
			_subjectUserDialog = subjectUserDialog;
			_bellUserDialog = bellUserDialog;
			_schoolClassUserDialog = schoolClassUserDialog;
		}
	}
}
