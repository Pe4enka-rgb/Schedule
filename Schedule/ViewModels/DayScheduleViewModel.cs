using GongSolutions.Wpf.DragDrop;
using Gu.Wpf.DataGrid2D;
using MathCore.WPF.Commands;
using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.Model;
using Schedule.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Schedule.ViewModels {
	public class DayScheduleViewModel : ViewModel, IDropTarget {
		private readonly IRepository<SchoolClass> _schoolClassRepository;
		private readonly IRepository<Subject> _subjectsRepository;
		private readonly IRepository<Bell> _bellRepository;
		private readonly IRepository<Day> _dayRepository;
		private readonly IRepository<Lesson> _lessonRepository;

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

		private List<List<LessonModel>> _lessonModels2DList;
		public List<List<LessonModel>> LessonModels2DList {
			get => _lessonModels2DList;
			set => Set(ref _lessonModels2DList, value);
		}

		private ObservableCollection<ObservableCollection<LessonModel>> _lessonModels2DObservableCollection;
		public ObservableCollection<ObservableCollection<LessonModel>> LessonModels2DObservableCollection {
			get => _lessonModels2DObservableCollection;
			set {
				if (Set(ref _lessonModels2DObservableCollection, value))
					OnPropertyChanged();
			}
		}

		private List<BellModel> _bellModelsList;
		public List<BellModel> BellModelsList {
			get => _bellModelsList;
			set => _bellModelsList = value;
		}

		#endregion

		#region Selected

		private LessonModel _selectedDataGridLesson;
		public LessonModel SelectedDataGridLesson {
			get { return _selectedDataGridLesson; }
			set { Set(ref _selectedDataGridLesson, value); }
		}

		private RowColumnIndex _selectedDataGridLessonIndex;
		public RowColumnIndex SelectedDataGridLessonIndex {
			get { return _selectedDataGridLessonIndex; }
			set { Set(ref _selectedDataGridLessonIndex, value); }
		}

		private Subject _selectedListBoxSubject;
		public Subject SelectedListBoxSubject {
			get { return _selectedListBoxSubject; }
			set { Set(ref _selectedListBoxSubject, value); }
		}

		#endregion

		private ICommand _loadDataCommand;

		public ICommand LoadDataCommand =>
			_loadDataCommand ??= new LambdaCommand(OnLoadDataCommandExecuted);

		private void OnLoadDataCommandExecuted() {
			LessonModels2DList = SchoolClasses
				.Select((_, i) => Bells
					.Select((_, j) => new LessonModel(
						Lessons.FirstOrDefault(l => l.Id == i * Bells.Count + j + 1)!))
					.ToList())
				.ToList();

			BellModelsList = Bells.Select(bell => new BellModel(bell)).ToList();
		}


		void IDropTarget.DragOver(IDropInfo dropInfo) {

			if (!dropInfo.IsSameDragDropContextAsSource)
				return;
			if (dropInfo.TargetItem == null)
				return;

			dropInfo.VisualTarget.CaptureMouse();


			dropInfo.Effects = DragDropEffects.Copy;
			dropInfo.DropTargetHintState = DropHintState.Active;
			dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
			dropInfo.EffectText = dropInfo.TargetItem.ToString();

		}

		void IDropTarget.Drop(IDropInfo dropInfo) {

			if (dropInfo.Data is Subject draggedItem && dropInfo.TargetItem is ListRowView targetRow) {

				if (targetRow != null) {
					dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
					dropInfo.Effects = DragDropEffects.Copy;
				}

				LessonModels2DList[HoveredColumnIndex][HoveredRowIndex]
						.Subject = draggedItem;
				LessonModels2DObservableCollection[HoveredColumnIndex][HoveredRowIndex]
					.Subject = draggedItem;
				SelectedDataGridLesson =
					LessonModels2DList[HoveredColumnIndex][HoveredRowIndex];


				dropInfo.VisualTarget.ReleaseMouseCapture();
			}
		}


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

			Subjects = new(_subjectsRepository.Items.ToList());
			SchoolClasses = new(_schoolClassRepository.Items.ToList());
			Bells = new(_bellRepository.Items.ToList());
			Lessons = new(_lessonRepository.Items.ToList());

			//LessonModels2DList = new List<List<LessonModel>>();
			//for (int i = 0; i < SchoolClasses.Count; i++) {
			//	List<LessonModel> temp = new();
			//	for (int j = 0; j < Bells.Count; j++) {
			//		temp.Add(new LessonModel(Lessons.FirstOrDefault(l => l.Id == i * Bells.Count + j + 1)));
			//	}
			//	LessonModels2DList.Add(temp);
			//}
			LessonModels2DList = SchoolClasses
				.Select((_, i) => Bells
					.Select((_, j) => new LessonModel(
						Lessons.FirstOrDefault(l => l.Id == i * Bells.Count + j + 1)!))
					.ToList())
				.ToList();

			LessonModels2DObservableCollection = SchoolClasses
				.Select((_, i) => Bells
					.Select((_, j) => new LessonModel(
						Lessons.FirstOrDefault(l => l.Id == i * Bells.Count + j + 1)))
					.ToObservableCollection())
				.ToObservableCollection();

			BellModelsList = new List<BellModel>();
			foreach (var bell in Bells) {
				BellModelsList.Add(new BellModel(bell));
			}

		}

	}
}
