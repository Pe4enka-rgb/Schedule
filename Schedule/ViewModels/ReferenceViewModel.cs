using MathCore.WPF.Commands;
using Schedule.DB.Entity;
using Schedule.Services.Interfaces;
using Schedule.ViewModels.Base;
using Schedule.ViewModels.EntityViewModels;
using System.Windows.Input;

namespace Schedule.ViewModels {
	internal class ReferenceViewModel : ViewModel {
		private readonly Interfaces.IRepository<Subject> _subjectRepository;
		private readonly Interfaces.IRepository<Bell> _bellRepository;
		private readonly Interfaces.IRepository<SchoolClass> _schoolClassRepository;
		private readonly Interfaces.IRepository<Grade> _gradeRepository;
		private readonly IUserDialog<Grade> _userDialog;

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
			CurrentViewModel = new GradeViewModel(_gradeRepository, _userDialog);
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

			CurrentViewModel = new SubjectViewModel(_subjectRepository);
		}
		#endregion

		#region Bell View Command
		private ICommand _bellViewCommand;
		public ICommand BellViewCommand => _bellViewCommand
		?? new LambdaCommand(OnBellViewCommandExecuted);
		private void OnBellViewCommandExecuted() {
			if (!CurrentViewModel.GetType().Equals(typeof(BellViewModel)))
				CurrentViewModel = new BellViewModel(_bellRepository);
		}
		#endregion

		#region SchoolClass View Command
		private ICommand _schoolClassViewCommand;
		public ICommand SchoolClassViewCommand => _schoolClassViewCommand
		??= new LambdaCommand(OnSchoolClassViewCommandExecuted);


		private void OnSchoolClassViewCommandExecuted() {
			CurrentViewModel = new SchoolClassViewModel(
				_schoolClassRepository,
				_gradeRepository
			);
		}
		#endregion


		#region Grade View Command
		private ICommand _gradeViewCommand;
		public ICommand GradeViewCommand => _gradeViewCommand
		??= new LambdaCommand(OnGradeViewCommandExecuted);
		private void OnGradeViewCommandExecuted() {
			if (!CurrentViewModel.GetType().Equals(typeof(GradeViewModel)))
				CurrentViewModel = new GradeViewModel(_gradeRepository, _userDialog);
		}

		#endregion


		#region Day View Command
		private ICommand _dayViewCommand;
		public ICommand DayViewCommand => _dayViewCommand
		?? new LambdaCommand(OnDayViewCommandExecuted, CanDayViewCommandExecute);
		private bool CanDayViewCommandExecute() => true;
		private void OnDayViewCommandExecuted() {
			CurrentViewModel = new DayViewModel(
				_schoolClassRepository,
				_subjectRepository
			);
		}
		#endregion

		public ReferenceViewModel() { }
		public ReferenceViewModel(
			Interfaces.IRepository<Subject> subjectRepository,
			Interfaces.IRepository<Bell> bellRepository,
			Interfaces.IRepository<SchoolClass> schoolClassRepository,
			Interfaces.IRepository<Grade> gradeRepository,
			IUserDialog<Grade> userDialog) {

			_subjectRepository = subjectRepository;
			_bellRepository = bellRepository;
			_schoolClassRepository = schoolClassRepository;
			_gradeRepository = gradeRepository;
			_userDialog = userDialog;
		}
	}
}
