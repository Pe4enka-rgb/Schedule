using GongSolutions.Wpf.DragDrop;
using Gu.Wpf.DataGrid2D;
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
	public class DayScheduleViewModel : ViewModel, IDropTarget {
		#region Fields

		private readonly IRepository<SchoolClass> _schoolClassRepository;
		private readonly IRepository<Subject> _subjectsRepository;
		private readonly IRepository<Bell> _bellRepository;
		private readonly IRepository<Day> _dayRepository;
		private readonly IRepository<Lesson> _lessonRepository;

		#endregion

		#region Properties
		private DayOfWeek _selectedDayOfWeek;
		public DayOfWeek SelectedDayOfWeek {
			get => _selectedDayOfWeek;
			set {
				if (Set(ref _selectedDayOfWeek, value))
					SetTableData(value);
			}
		}


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

		private ObservableCollection<Day> _days;
		public ObservableCollection<Day> Days {
			get { return _days; }
			set { Set(ref _days, value); }
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

		private List<Subject> _subjects;
		public List<Subject> Subjects {
			get { return _subjects; }
			set { Set(ref _subjects, value); }
		}

		#endregion

		#region Model

		private ObservableCollection<ObservableCollection<LessonModel>> _lessonModels2DTransposed;
		public ObservableCollection<ObservableCollection<LessonModel>> LessonModels2DTransposed {
			get => _lessonModels2DTransposed;
			set => Set(ref _lessonModels2DTransposed, value);
		}

		private List<BellModel> _bellModelsList;
		public List<BellModel> BellModelsList {
			get => _bellModelsList;
			set => Set(ref _bellModelsList, value);
		}

		#endregion

		#region Selected

		private LessonModel _selectedDataGridLesson;
		public LessonModel SelectedDataGridLesson {
			get => _selectedDataGridLesson;
			set => Set(ref _selectedDataGridLesson, value);
		}

		private RowColumnIndex _selectedDataGridLessonIndex;
		public RowColumnIndex SelectedDataGridLessonIndex {
			get => _selectedDataGridLessonIndex;
			set => Set(ref _selectedDataGridLessonIndex, value);
		}

		private Subject _selectedListBoxSubject;
		public Subject SelectedListBoxSubject {
			get => _selectedListBoxSubject;
			set => Set(ref _selectedListBoxSubject, value);
		}


		#endregion

		#endregion
		#region Commands

		private ICommand _loadDataCommand;
		public ICommand LoadDataCommand =>
			_loadDataCommand ??= new LambdaCommand(OnLoadDataCommandExecuted);
		private void OnLoadDataCommandExecuted() {
			Subjects = _subjectsRepository.Items.ToList();
			SchoolClasses = _schoolClassRepository.Items.ToList();
			Bells = _bellRepository.Items.ToList();
			Lessons = _lessonRepository.Items.ToList();
			Days = _dayRepository.Items.ToObservableCollection();

			DaysOfWeek = [];
			for (int i = 1; i < 7; i++) {
				DaysOfWeek.Add((DayOfWeek)i);
			}

			BellModelsList = Bells.Select(bell => new BellModel(bell)).ToList();
		}

		private void SetTableData(DayOfWeek dayOfWeek) {
			LessonModels2DTransposed = Days
					.Where(day => day.DayOfWeek == dayOfWeek)
					.Select(day => day.Lessons.ToLessonModelCollection().ToObservableCollection())

					.ToObservableCollection()
				;
		}

		private ICommand _deleteLessonCommand;
		public ICommand DeleteLessonCommand =>
			_deleteLessonCommand ??= new LambdaCommand(OnDeleteLessonCommandExecuted);

		private void OnDeleteLessonCommandExecuted() {
			LessonModels2DTransposed
					[SelectedDataGridLessonIndex.Column]
					[SelectedDataGridLessonIndex.Row]
					.Subject = null;
			// TODO Сделать расширение работающее для типов BaseModel,
			// TODO позволяющее обращаться к коллекции и обновлять изменения в базе данных
			//LessonModels2DTransposed[SelectedDataGridLessonIndex.Column]
			//	.UpdateEntity(SelectedDataGridLessonIndex.Row, _lessonRepository);

			//LessonModels2DTransposed.UpdateEntity(
			//	SelectedDataGridLessonIndex.Column,
			//	SelectedDataGridLessonIndex.Row,
			//	_lessonRepository
			//	);


			// Update Lesson
			_lessonRepository.Update(
				LessonModels2DTransposed
						[SelectedDataGridLessonIndex.Column]
						[SelectedDataGridLessonIndex.Row]
						.Entity
			);
		}

		private ICommand _drainTableCommand;
		public ICommand DrainTableCommand =>
			_drainTableCommand ??= new LambdaCommand(OnDrainTableCommandCommandExecuted);

		private void OnDrainTableCommandCommandExecuted() {
			LessonModels2DTransposed = new ObservableCollection<ObservableCollection<LessonModel>>();
			for (int i = 0; i < SchoolClasses.Count; i++) {
				ObservableCollection<LessonModel> temp = [];
				foreach (var bell in Bells) {
					temp.Add(new LessonModel(new Lesson { Bell = bell }));
				}
				LessonModels2DTransposed.Add(temp);
			}
		}

		private ICommand _saveCommand;
		public ICommand SaveCommand =>
			_saveCommand ??= new LambdaCommand(OnSaveCommandExecuted);

		private void OnSaveCommandExecuted() {

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

			LessonModels2DTransposed[HoveredColumnIndex][HoveredRowIndex]
				.Subject = draggedItem;

			SelectedDataGridLesson =
				LessonModels2DTransposed
						[HoveredColumnIndex]
					[HoveredRowIndex]
				;
			// Update Lesson
			_lessonRepository.Update(
				LessonModels2DTransposed
						[HoveredColumnIndex]
					[HoveredRowIndex]
					.Entity
			);

			dropInfo.VisualTarget.ReleaseMouseCapture();
		}
		#endregion

		#region Constructors
		/// <summary>
		/// DI constructor
		/// </summary>
		public DayScheduleViewModel() { }

		public DayScheduleViewModel(
			IRepository<SchoolClass> schoolClassRepository,
			IRepository<Bell> bellRepository,
			IRepository<Day> dayRepository,
			IRepository<Lesson> lessonRepository,
			IRepository<Subject> subjectsRepository) : base() {
			_schoolClassRepository = schoolClassRepository;
			_bellRepository = bellRepository;
			_dayRepository = dayRepository;
			_lessonRepository = lessonRepository;
			_subjectsRepository = subjectsRepository;
			//LessonModels2DObservableCollection = SchoolClasses
			//	.Select((_, i) => Bells
			//		.Select((_, j) => new LessonModel(
			//			Lessons.FirstOrDefault(l => l.Id == i * Bells.Count + j + 1)))
			//		.ToObservableCollection())
			//	.ToObservableCollection();

		}
		#endregion
	}
}
