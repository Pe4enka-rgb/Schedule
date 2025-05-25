using Schedule.DB.Entity;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Schedule.Model {
	internal class BellModel : INotifyPropertyChanged {
		private string _bellName;

		public string BellName {
			get => _bellName;
			set {
				if (_bellName != value) {
					_bellName = value;
					OnPropertyChanged();
				}
			}
		}

		public Bell Bell { get; set; }


		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public BellModel(Bell bell) {
			Bell = bell;
			BellName = bell.Start.ToShortTimeString() + " - " + bell.End.ToShortTimeString();
		}

		public override string ToString() {
			return BellName;
		}
	}
}
