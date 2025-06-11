using Schedule.DB.Entity;
using Schedule.ViewModels.Base;

namespace Schedule.ViewModels.EntityEditViewModels;

internal class SchoolClassEditViewModel(SchoolClass item) : ViewModel {

	public int _id { get; } = item.Id;

	private Grade _grade = item.Grade;
	public Grade Grade {
		get => _grade;
		set => Set(ref _grade, value);
	}

	private string _letter = item.Letter;
	public string Letter {
		get => _letter;
		set => Set(ref _letter, value[0].ToString());
	}

	private ICollection<Day> _days = item.Days;
	public ICollection<Day> Days {
		get => _days;
		set => Set(ref _days, value);
	}
}