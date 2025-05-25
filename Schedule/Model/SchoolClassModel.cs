using Schedule.DB.Entity;
using Schedule.ViewModels.Base;

namespace Schedule.Model {
	internal class SchoolClassModel : ViewModel {
		private string _className;
		private SchoolClass _schoolClass;

		public string ClassName {
			get => _className;
			set => Set(ref _className, value);
		}

		public SchoolClass SchoolClass {
			get => _schoolClass;
			set => Set(ref _schoolClass, value);
		}



		public SchoolClassModel(SchoolClass schoolClass) {
			SchoolClass = schoolClass;
			ClassName = SchoolClass.ToString();
		}

		public override string ToString() {
			return ClassName;
		}
	}
}
