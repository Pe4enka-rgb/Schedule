using GongSolutions.Wpf.DragDrop;
using MathCore.WPF.Commands;
using Schedule.Data;
using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.Model;
using Schedule.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Schedule.ViewModels {
	internal class ScheduleViewModel : ViewModel, IDropTarget {
		private readonly IRepository<SchoolClass> _schoolClassRepository;
		private readonly IRepository<Bell> _bellRepository;
		private readonly IRepository<Day> _dayRepository;
		private readonly IRepository<Lesson> _lessonRepository;
		private readonly IRepository<Subject> _subjectsRepository;
		private readonly IRepository<Grade> _gradesRepository;

		#region Properies

		private ObservableCollection<DayOfWeek> _daysOfWeek;
		public ObservableCollection<DayOfWeek> DaysOfWeek {
			get => _daysOfWeek;
			set {
				Set(ref _daysOfWeek, value);
			}
		}

		#region Hovered

		private int _hoveredRowIndex;

		public int HoveredRowIndex {
			get => _hoveredRowIndex;
			set {
				_hoveredRowIndex = value;
				OnPropertyChanged();
			}
		}

		private int _hoveredColumnIndex;
		public int HoveredColumnIndex {
			get => _hoveredColumnIndex;
			set => Set(ref _hoveredColumnIndex, value);
		}

		private LessonModel _hoveredValue;
		public LessonModel HoveredValue {
			get => _hoveredValue;
			set => Set(ref _hoveredValue, value);
		}

		#endregion

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

		private LessonModel _selectedDataGridLesson;
		public LessonModel SelectedDataGridLesson {
			get => _selectedDataGridLesson;
			set => Set(ref _selectedDataGridLesson, value);
		}

		private SchoolClass _selectedSchoolClass;
		public SchoolClass SelectedSchoolClass {
			get => _selectedSchoolClass;
			set {
				if (Set(ref _selectedSchoolClass, value)) {
					LoadTableData(value);
				}

			}
		}



		#endregion

		#region Model

		private ObservableCollection<GradeWithSchoolClasses> _gradeWithSchoolClassesList;
		public ObservableCollection<GradeWithSchoolClasses> GradeWithSchoolClassesList {
			get => _gradeWithSchoolClassesList;
			set => Set(ref _gradeWithSchoolClassesList, value);
		}

		private ObservableCollection<ObservableCollection<LessonModel>> _lessonsList;
		public ObservableCollection<ObservableCollection<LessonModel>> LessonsList {
			get => _lessonsList;
			set => Set(ref _lessonsList, value);
		}
		#endregion

		#endregion

		#region Commands

		private ICommand _loadDataCommand;

		public ICommand LoadDataCommand =>
			_loadDataCommand ??= new LambdaCommand(OnLoadDataCommandExecuted);
		private void OnLoadDataCommandExecuted() {
			Grades = _gradesRepository.Items.ToList();

			SchoolClasses = _schoolClassRepository.Items.ToList();

			Bells = _bellRepository.Items.ToList();

			Lessons = _lessonRepository.Items.ToList();

			Subjects = _subjectsRepository.Items.ToList();

			Days = _dayRepository.Items.ToList();

			SelectedSchoolClass = SchoolClasses.FirstOrDefault()!;

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

			DaysOfWeek = new();
			for (int i = 1; i < 7; i++) {
				DaysOfWeek.Add((DayOfWeek)i);
			}



		}

		private void LoadTableData(SchoolClass newSchoolClass) {
			if (newSchoolClass == null)
				return;
			LessonsList = Days
					.Where(d => d.SchoolClass.Id == newSchoolClass.Id)
					.OrderBy(d => d.DayOfWeek)
					.Select(day => day.Lessons.ToLessonModelCollection().ToObservableCollection())
					.ToObservableCollection()
			;

		}

		void IDropTarget.DragOver(IDropInfo dropInfo) {

			if (!dropInfo.IsSameDragDropContextAsSource)
				return;
			if (dropInfo.TargetItem == null)
				return;

			dropInfo.VisualTarget.CaptureMouse();

			dropInfo.Effects = DragDropEffects.Copy;
			//dropInfo.DropTargetHintState = DropHintState.Active;
			//dropInfo.EffectText = "Поместить " + SelectedListBoxSubject.Name;
			dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;

		}

		void IDropTarget.Drop(IDropInfo dropInfo) {
			if (dropInfo.Data is not Subject draggedItem)
				return;
			dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
			dropInfo.Effects = DragDropEffects.Copy;

			LessonsList[HoveredColumnIndex][HoveredRowIndex]
				.Subject = draggedItem;

			SelectedDataGridLesson =
				LessonsList
					[HoveredColumnIndex]
					[HoveredRowIndex]
				;
			// Update Lesson
			_lessonRepository.Update(
				LessonsList
					[HoveredColumnIndex]
					[HoveredRowIndex]
					.Entity
			);

			dropInfo.VisualTarget.ReleaseMouseCapture();
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




			//GradeModels = Grades.Select(grade => new GradeModel(grade)).ToList();






		}

	}
}
