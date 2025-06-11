using Schedule.DB.Entity;
using Schedule.ViewModels.Base;

namespace Schedule.ViewModels.EntityEditViewModels;

internal class BellEditViewModel(Bell item) : ViewModel {

	public int _id { get; } = item.Id;

	private TimeOnly _start = item.Start;
	public TimeOnly Start {
		get => _start;
		set => Set(ref _start, value);
	}

	private TimeOnly _end = item.End;
	public TimeOnly End {
		get => _end;
		set => Set(ref _end, value);
	}
}