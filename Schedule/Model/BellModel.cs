using Schedule.DB.Entity;
using Schedule.ViewModels.Base;

namespace Schedule.Model {
	public class BellModel : ViewModel {
		private string _bellName;

		public string BellName {
			get => Start.ToLongTimeString() + " - " + End.ToLongTimeString();
			set => Set(ref _bellName, value);
		}

		private TimeOnly _start;
		public TimeOnly Start {
			get => _start;
			set => Set(ref _start, value);
		}

		private TimeOnly _end;
		public TimeOnly End {
			get => _end;
			set => Set(ref _end, value);
		}
		public Bell Bell { get; set; }

		public BellModel(Bell bell) {
			Bell = bell;

		}
	}
}
