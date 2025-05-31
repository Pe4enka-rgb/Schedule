using MathCore.WPF.Commands;
using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.Model;
using Schedule.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Schedule.ViewModels {
	internal class ScheduleViewModel : ViewModel {
		private readonly IRepository<SchoolClass> _schoolClassRepository;
		private readonly IRepository<Bell> _bellRepository;
		private readonly IRepository<Day> _dayRepository;
		private readonly IRepository<Lesson> _lessonRepository;
		private readonly IRepository<Subject> _subjectsRepository;
		private readonly IRepository<Grade> _gradesRepository;

		#region Properies



		#region Entity


		private List<Grade> _grades;
		public List<Grade> Grades {
			get => _grades;
			set => Set(ref _grades, value);
		}

		private List<Subject> _subjects;

		public List<Subject> Subjects {
			get { return _subjects; }
			set { Set(ref _subjects, value); }
		}

		private List<Bell> _bells;
		public List<Bell> Bells {
			get { return _bells; }
			set { Set(ref _bells, value); }
		}

		private List<SchoolClass> _schoolClasses;
		public List<SchoolClass> SchoolClasses {
			get { return _schoolClasses; }
			set { Set(ref _schoolClasses, value); }
		}

		private List<Lesson> _lessons;
		public List<Lesson> Lessons {
			get { return _lessons; }
			set { Set(ref _lessons, value); }
		}

		private List<Day> _days;
		public List<Day> Days {
			get { return _days; }
			set { Set(ref _days, value); }
		}

		#endregion

		#region SelectedItems

		private Lesson _selectedDataGridLesson;
		public Lesson SelectedDataGridLesson {
			get => _selectedDataGridLesson;
			set => Set(ref _selectedDataGridLesson, value);
		}

		private SchoolClass _selectedSchoolClass;
		public SchoolClass SelectedSchoolClass {
			get => _selectedSchoolClass;
			set => Set(ref _selectedSchoolClass, value);
		}

		#endregion

		#region Model

		private ObservableCollection<GradeWithSchoolClasses> _gradeWithSchoolClassesList;
		public ObservableCollection<GradeWithSchoolClasses> GradeWithSchoolClassesList {
			get => _gradeWithSchoolClassesList;
			set => Set(ref _gradeWithSchoolClassesList, value);
		}

		private List<BellModel> _bellModels;
		public List<BellModel> BellModels {
			get { return _bellModels; }
			set { Set(ref _bellModels, value); }
		}

		private List<ScheduleModel> _scheduleModels;
		public List<ScheduleModel> ScheduleModels {
			get { return _scheduleModels; }
			set { Set(ref _scheduleModels, value); }
		}


		private List<List<LessonModel>> _lessonsList;
		public List<List<LessonModel>> LessonsList {
			get { return _lessonsList; }
			set { Set(ref _lessonsList, value); }
		}
		#endregion

		#endregion

		#region Commands

		private ICommand _loadDataCommand;

		public ICommand LoadDataCommand =>
			_loadDataCommand ??= new LambdaCommand(OnLoadDataCommandExecuted);
		private void OnLoadDataCommandExecuted() {

			GradeWithSchoolClassesList = new();
			for (int i = 0; i < Grades.Count; i++) {
				GradeWithSchoolClassesList
					.Add(
						new GradeWithSchoolClasses(
							Grades[i],
							SchoolClasses.Where(s => s.Grade.Year == i + 1).ToList()
						)
					);
			}
		}

		#endregion

		public ScheduleViewModel() { }
		public ScheduleViewModel(
			IRepository<SchoolClass> schoolClassRepository,
			IRepository<Bell> bellRepository,
			IRepository<Day> dayRepository,
			IRepository<Lesson> lessonRepository,
			IRepository<Subject> subjectsRepository,
				IRepository<Grade> gradesRepository) : base() {
			_schoolClassRepository = schoolClassRepository;
			_bellRepository = bellRepository;
			_dayRepository = dayRepository;
			_lessonRepository = lessonRepository;
			_subjectsRepository = subjectsRepository;
			_gradesRepository = gradesRepository;

			Grades = new(_gradesRepository.Items.ToList());

			SchoolClasses = new(_schoolClassRepository.Items.ToList());

			Bells = new(_bellRepository.Items.ToList());

			Lessons = new(_lessonRepository.Items.ToList());

			BellModels = new();

			Subjects = new(_subjectsRepository.Items.ToList());

			Days = new(_dayRepository.Items.ToList());

			LessonsList = new List<List<LessonModel>>();

			//GradeModels = Grades.Select(grade => new GradeModel(grade)).ToList();






		}

	}
}
