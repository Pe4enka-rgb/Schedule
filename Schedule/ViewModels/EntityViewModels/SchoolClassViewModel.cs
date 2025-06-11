using MathCore.WPF.Commands;
using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.Model;
using Schedule.Services.Interfaces;
using Schedule.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Schedule.ViewModels.EntityViewModels {
	class SchoolClassViewModel : ViewModel {
		private readonly IRepository<SchoolClass> _schoolClassRepository;
		private readonly IRepository<Grade> _gradeRepository;
		private readonly IUserDialog<SchoolClass> _schoolClassUserDialog;

		#region Properties

		private ObservableCollection<SchoolClass> _classes;
		public ObservableCollection<SchoolClass> Classes {
			get { return _classes; }
			set { Set(ref _classes, value); }
		}
		private ObservableCollection<SchoolClassModel> _classesModels;
		public ObservableCollection<SchoolClassModel> ClassesModels {
			get { return _classesModels; }
			set { Set(ref _classesModels, value); }
		}

		public SchoolClassModel _selectedSchoolClass;
		public SchoolClassModel SelectedSchoolClass {
			get => _selectedSchoolClass;
			set => Set(ref _selectedSchoolClass, value);
		}
		#endregion

		#region Add

		private ICommand _AddSchoolClassCommand;
		public ICommand AddSchoolClassCommand => _AddSchoolClassCommand
			??= new LambdaCommand(OnAddSchoolClassCommandExecuted);
		private void OnAddSchoolClassCommandExecuted() {
			SchoolClass newSchoolClass = new();
			if (!_schoolClassUserDialog.Edit(newSchoolClass)) {
				return;
			}
			_schoolClassRepository.Add(newSchoolClass);
			Classes.Add(newSchoolClass);
			SchoolClassModel scmodel = new(newSchoolClass);
			ClassesModels.Add(scmodel);
			ClassesModels.OrderBy(cm => cm.Grade.Year);
			SelectedSchoolClass = scmodel;

		}
		#endregion

		#region Delete

		private ICommand _DeleteSchoolClassCommand;
		public ICommand DeleteSchoolClassCommand => _DeleteSchoolClassCommand
			??= new LambdaCommand<SchoolClassModel>(OnDeleteSchoolClassCommandExecuted, CanDeleteSchoolClassCommandExecuted);
		private bool CanDeleteSchoolClassCommandExecuted(SchoolClassModel schoolClass) => schoolClass != null || SelectedSchoolClass != null;
		private void OnDeleteSchoolClassCommandExecuted(SchoolClassModel parametrSchoolClass) {
			var schoolClassToRemove = parametrSchoolClass ?? SelectedSchoolClass;

			if (schoolClassToRemove != null) {
				_schoolClassRepository.Remove(schoolClassToRemove.SchoolClass);
				Classes.Remove(schoolClassToRemove.SchoolClass);
			}

			SelectedSchoolClass = null;
		}
		#endregion

		#region Edit



		private ICommand _EditSchoolClassCommand;
		public ICommand EditSchoolClassCommand => _EditSchoolClassCommand
			??= new LambdaCommand<SchoolClassModel>(OnEditSchoolClassCommandExecuted);

		private void OnEditSchoolClassCommandExecuted(SchoolClassModel parametrSchoolClass) {

			if (parametrSchoolClass == null)
				return;
			if (!_schoolClassUserDialog.Edit(parametrSchoolClass.SchoolClass)) {
				return;
			}
			_schoolClassRepository.Update(parametrSchoolClass.SchoolClass);
			Classes.GroupBy(g => g.Grade.Year);
			SelectedSchoolClass = parametrSchoolClass;
		}
		#endregion

		#region LoadDataCommand

		private ICommand _loadDataCommand;
		public ICommand LoadDataCommand => _loadDataCommand
			??= new LambdaCommand(OnLoadDataCommandExecuted);


		private void OnLoadDataCommandExecuted() {
			Classes = new(_schoolClassRepository.Items.ToObservableCollection());
			ClassesModels = new ObservableCollection<SchoolClassModel>();
			foreach (var schoolClass in Classes) {
				ClassesModels.Add(new SchoolClassModel(schoolClass));
			}
		}
		#endregion

		public SchoolClassViewModel(
			IRepository<SchoolClass> schoolClassRepository,
			IRepository<Grade> gradeRepository,
			IUserDialog<SchoolClass> schoolClassUserDialog) {

			this._schoolClassRepository = schoolClassRepository;
			this._gradeRepository = gradeRepository;
			_schoolClassUserDialog = schoolClassUserDialog;
		}
	}

}
