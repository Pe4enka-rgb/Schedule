using MathCore.WPF.Commands;
using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.Model;
using Schedule.ViewModels.Base;
using System.Windows.Input;

namespace Schedule.ViewModels {
	internal class ScheduleViewModel : ViewModel {
		private readonly IRepository<SchoolClass> _schoolClassRepository;
		private readonly Interfaces.IRepository<Bell> _bellRepository;
		private readonly Interfaces.IRepository<Day> _dayRepository;
		private readonly Interfaces.IRepository<Lesson> _lessonRepository;

		#region Properies

		private List<List<string>> _listOfStrings;
		public List<List<string>> ListOfStrings {
			get => _listOfStrings;
			set => Set(ref _listOfStrings, value);
		}

		private string[,] _array2DOfOfStrings;
		public string[,] Array2DOfStrings {
			get => _array2DOfOfStrings;
			set => Set(ref _array2DOfOfStrings, value);
		}

		private List<List<Subject>> _subjectsList;
		public List<List<Subject>> SubjectsList {

			get { return _subjectsList; }
			set { Set(ref _subjectsList, value); }
		}

		private List<Bell> _bells;
		public List<Bell> Bells {
			get { return _bells; }
			set { Set(ref _bells, value); }
		}

		private List<BellModel> _bellModels;
		public List<BellModel> BellModels {
			get { return _bellModels; }
			set { Set(ref _bellModels, value); }
		}

		private List<SchoolClass> _schoolClasses;
		public List<SchoolClass> SchoolClasses {
			get { return _schoolClasses; }
			set { Set(ref _schoolClasses, value); }
		}

		private List<SchoolClassModel> _schoolClassModels;
		public List<SchoolClassModel> SchoolClassModels {
			get { return _schoolClassModels; }
			set { Set(ref _schoolClassModels, value); }
		}

		private List<Lesson> _Lessons;
		public List<Lesson> Lessons {
			get { return _Lessons; }
			set { Set(ref _Lessons, value); }
		}

		#endregion

		#region Commands
		private ICommand _LoadDataCommand;

		public ICommand LoadDataCommand =>
			_LoadDataCommand ?? new LambdaCommand(OnLoadDataCommandExecuted);


		private void OnLoadDataCommandExecuted() {
			SchoolClasses = new(_schoolClassRepository.Items.ToList());

			Bells = new(_bellRepository.Items.ToList());

			Lessons = new(_lessonRepository.Items.ToList());

			BellModels = new();
			foreach (var bell in Bells) {
				BellModels.Add(new BellModel(bell));
			}

			SchoolClassModels = new();
			foreach (var schoolClass in SchoolClasses) {
				SchoolClassModels.Add(new SchoolClassModel(schoolClass));
			}


			int classNumber = SchoolClasses.Count;
			var temStrings = new List<string>();

			ListOfStrings = new List<List<string>>();
			for (int i = 0; i < Bells.Count; i++) {
				temStrings = new List<string>();
				for (int j = 0; j < classNumber; j++) {
					temStrings.Add(i.ToString() + " " + j.ToString());
				}
				ListOfStrings.Add(temStrings);
			}

			Array2DOfStrings = new string[Bells.Count, classNumber];
			for (int i = 0; i < Bells.Count; i++) {
				for (int j = 0; j < classNumber; j++) {
					Array2DOfStrings[i, j] = i.ToString() + " " + j.ToString();
				}
			}
		}

		#endregion

		public ScheduleViewModel() { }
		public ScheduleViewModel(
			Interfaces.IRepository<SchoolClass> schoolClassRepository,
			Interfaces.IRepository<Bell> bellRepository,
			Interfaces.IRepository<Day> dayRepository,
			IRepository<Lesson> lessonRepository
			) : base() {
			_schoolClassRepository = schoolClassRepository;
			_bellRepository = bellRepository;
			_dayRepository = dayRepository;
			_lessonRepository = lessonRepository;
		}
	}
}
