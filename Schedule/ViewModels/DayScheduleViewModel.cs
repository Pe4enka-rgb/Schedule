using Schedule.ViewModels.Base;

namespace Schedule.ViewModels {
	public class DayScheduleViewModel : ViewModel {
		private List<List<string>> _array2DOfOfStrings;
		public List<List<string>> Array2DOfStrings {
			get => _array2DOfOfStrings;
			set => Set(ref _array2DOfOfStrings, value);
		}

		public DayScheduleViewModel() {
			//Array2DOfStrings = new string[11, 44];
			//for (int i = 0; i < 11; i++) {
			//	for (int j = 0; j < 44; j++) {
			//		Array2DOfStrings[i, j] = i.ToString() + " " + j.ToString();
			//	}
			//}
			Array2DOfStrings = new List<List<string>>();
			Array2DOfStrings.Add(new() { "1", "2", "3" });
			Array2DOfStrings.Add(new() { "4", "5", "6" });
			Array2DOfStrings.Add(new() { "7", "8", "9" });

			//Array2DOfStrings = new List<List<string>>() {
			//	{"1", "2", "3"},
			//	{"4", "5", "6"},
			//	{"7", "8", "9"}
			//};
		}

	}
}
