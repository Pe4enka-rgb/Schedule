using MathCore.WPF.Commands;
using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.Model;
using Schedule.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Schedule.ViewModels.EntityViewModels {
	class SchoolClassViewModel : ViewModel {
		private readonly IRepository<SchoolClass> _schoolClassRepository;
		private readonly IRepository<Grade> _gradeRepository;


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

		public SchoolClass _selectedSchoolClass;
		public SchoolClass SelectedSchoolClass {
			get => _selectedSchoolClass;
			set => Set(ref _selectedSchoolClass, value);
		}


		private ICommand _AddSchoolClassCommand;
		public ICommand AddSchoolClassCommand => _AddSchoolClassCommand
			??= new LambdaCommand(OnAddGradeCommandExecuted);
		private void OnAddGradeCommandExecuted() {

		}

		private ICommand _DeleteSchoolClassCommand;
		public ICommand DeleteSchoolClassCommand => _DeleteSchoolClassCommand
			??= new LambdaCommand(OnDeleteGradeCommandExecuted);
		private void OnDeleteGradeCommandExecuted() {

		}
		private ICommand _EditSchoolClassCommand;
		public ICommand EditSchoolClassCommand => _EditSchoolClassCommand
			??= new LambdaCommand(OnEditGradeCommandExecuted);
		private void OnEditGradeCommandExecuted() {

		}


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

		public SchoolClassViewModel(
			IRepository<SchoolClass> schoolClassRepository,
			IRepository<Grade> gradeRepository) {

			this._schoolClassRepository = schoolClassRepository;
			this._gradeRepository = gradeRepository;
		}
	}

}
