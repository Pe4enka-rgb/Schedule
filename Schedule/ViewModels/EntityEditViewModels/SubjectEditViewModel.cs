using Schedule.DB.Entity;
using Schedule.ViewModels.Base;

namespace Schedule.ViewModels.EntityEditViewModels;

internal class SubjectEditViewModel(Subject item) : ViewModel {

	public int _id { get; } = item.Id;

	private int _difficulty = item.Difficulty;
	public int Difficulty {
		get => _difficulty;
		set => Set(ref _difficulty, Convert.ToInt32(value));
	}

	private int _hoursPerWeek = item.HoursPerWeek;
	public int HoursPerWeek {
		get => _hoursPerWeek;
		set => Set(ref _hoursPerWeek, Convert.ToInt32(value));
	}

	private int _hoursPerDay = item.HoursPerDay;
	public int HoursPerDay {
		get => _hoursPerDay;
		set => Set(ref _hoursPerDay, Convert.ToInt32(value));
	}

	private string _name = item.Name;
	public string Name {
		get => _name;
		set => Set(ref _name, value);
	}
}