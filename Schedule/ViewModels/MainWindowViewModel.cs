using MathCore.WPF.Commands;
using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.Services.Interfaces;
using Schedule.ViewModels.Base;
using System.Windows.Input;

namespace Schedule.ViewModels {
	class MainWindowViewModel : ViewModel {
		#region CurrentViewModel - Текущая Модель Представления

		private ViewModel _currentViewModel;
		public ViewModel CurrentViewModel {
			get { return _currentViewModel; }
			set { Set(ref _currentViewModel, value); }
		}

		#endregion

		private readonly IUserDialog<Grade> _userDialog;
		private readonly IRepository<Subject> _subjectRepository;
		private readonly IRepository<Bell> _bellRepository;
		private readonly IRepository<SchoolClass> _schoolClassRepository;
		private readonly IRepository<Grade> _gradeRepository;
		private readonly IRepository<Day> _dayRepository;

		private string _Title = "окошко-лушошко";

		public string Title {
			get { return _Title; }
			private set { Set(ref _Title, value); }
		}


		#region Reference View Command

		private ICommand _ReferenceViewCommand;
		public ICommand ReferenceViewCommand =>
			_ReferenceViewCommand ??= new LambdaCommand(OnReferenceViewCommandExecuted);

		private void OnReferenceViewCommandExecuted() { CurrentViewModel = new ReferenceViewModel(); }
		#endregion

		#region DayScheduleView View Command

		private ICommand _DayScheduleViewCommand;
		public ICommand DayScheduleViewCommand =>
			_DayScheduleViewCommand ??= new LambdaCommand(OnDayScheduleViewCommandExecuted);

		private void OnDayScheduleViewCommandExecuted() { CurrentViewModel = new DayScheduleViewModel(); }
		#endregion

		#region Schedule View Command

		private ICommand _ScheduleViewCommand;

		public ICommand ScheduleViewCommand =>
			_ScheduleViewCommand ??= new LambdaCommand(OnScheduleViewCommandExecuted);
		private void OnScheduleViewCommandExecuted() { CurrentViewModel = new ScheduleViewModel(); }
		#endregion


		public MainWindowViewModel(
			IUserDialog<Grade> userDialog,
			Interfaces.IRepository<Subject> subjectRepository,
			Interfaces.IRepository<Bell> bellRepository,
			Interfaces.IRepository<SchoolClass> schoolClassRepository,
			Interfaces.IRepository<Grade> gradeRepository,
			Interfaces.IRepository<Day> dayRepository) {
			_userDialog = userDialog;
			_subjectRepository = subjectRepository;
			_bellRepository = bellRepository;
			_schoolClassRepository = schoolClassRepository;
			_gradeRepository = gradeRepository;
			_dayRepository = dayRepository;
		}


	}
}