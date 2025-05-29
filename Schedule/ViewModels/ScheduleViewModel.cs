using MathCore.WPF.Commands;
using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.Model;
using Schedule.ViewModels.Base;
using System.Windows.Input;

namespace Schedule.ViewModels {
	internal class ScheduleViewModel : ViewModel {
		private readonly IRepository<SchoolClass> _schoolClassRepository;
		private readonly IRepository<Bell> _bellRepository;
		private readonly IRepository<Day> _dayRepository;
		private readonly IRepository<Lesson> _lessonRepository;
		private readonly IRepository<Subject> _subjectsRepository;

		#region Properies

		#region Entity




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

		private Subject _selectedLisBoxLesson;
		public Subject SelectedLisBoxLesson {
			get => _selectedLisBoxLesson;
			set => Set(ref _selectedLisBoxLesson, value);
		}

		#endregion

		#region Model




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


			foreach (var bell in Bells) {
				BellModels.Add(new BellModel(bell));
			}



			for (int i = 0; i < SchoolClasses.Count; i++) {
				List<LessonModel> temp = new();
				for (int j = 0; j < Bells.Count; j++) {
					temp.Add(new LessonModel(Lessons.FirstOrDefault(l => l.Id == i * Bells.Count + j + 1)));
				}
				LessonsList.Add(temp);
			}

		}

		#endregion

		public ScheduleViewModel() { }
		public ScheduleViewModel(
			Interfaces.IRepository<SchoolClass> schoolClassRepository,
			Interfaces.IRepository<Bell> bellRepository,
			Interfaces.IRepository<Day> dayRepository,
			IRepository<Lesson> lessonRepository,
			IRepository<Subject> subjectsRepository) : base() {
			_schoolClassRepository = schoolClassRepository;
			_bellRepository = bellRepository;
			_dayRepository = dayRepository;
			_lessonRepository = lessonRepository;
			_subjectsRepository = subjectsRepository;


			SchoolClasses = new(_schoolClassRepository.Items.ToList());

			Bells = new(_bellRepository.Items.ToList());

			Lessons = new(_lessonRepository.Items.ToList());

			BellModels = new();

			Subjects = new(_subjectsRepository.Items.ToList());

			Days = new(_dayRepository.Items.ToList());

			LessonsList = new List<List<LessonModel>>();








		}
	}
}
