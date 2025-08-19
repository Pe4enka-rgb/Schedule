using Schedule.DB.Entity;
using Schedule.ViewModels.Base;

namespace Schedule.ViewModels.EntityEditViewModels;

internal class SubjectEditViewModel : ViewModel {

	public int _id { get; }

	private int _difficulty;
	public int Difficulty {
		get => _difficulty;
		set => Set(ref _difficulty, Convert.ToInt32(value));
	}

	private int _hoursPerWeek;
	public int HoursPerWeek {
		get => _hoursPerWeek;
		set => Set(ref _hoursPerWeek, Convert.ToInt32(value));
	}

	private int _hoursPerDay;
	public int HoursPerDay {
		get => _hoursPerDay;
		set => Set(ref _hoursPerDay, Convert.ToInt32(value));
	}

	private string _name;
	public string Name {
		get => _name;
		set => Set(ref _name, value);
	}

	public SubjectEditViewModel(Subject item) {
		_id = item.Id;
		Difficulty = item.Difficulty;
		HoursPerWeek = item.HoursPerWeek;
		HoursPerDay = item.HoursPerDay;
		Name = item.Name;
	}
}

