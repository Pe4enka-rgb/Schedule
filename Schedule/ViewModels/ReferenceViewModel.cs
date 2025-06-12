using MathCore.WPF.Commands;
using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.Services.Interfaces;
using Schedule.ViewModels.Base;
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
			_LoadDataCommand ?? new LambdaCommand(OnLoadDataCommandExecuted);

		private void OnLoadDataCommandExecuted() {
			//CurrentViewModel = new GradeViewModel(_gradeRepository, _gradeUserDialog);
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
