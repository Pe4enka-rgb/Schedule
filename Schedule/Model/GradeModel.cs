using Schedule.DB.Entity;
using Schedule.ViewModels.Base;

namespace Schedule.Model {
	internal class GradeModel : ViewModel {

		private int _year;
		public int Year {
			get => _year;
			set => Set(ref _year, value);
		}

		private String _descriprion;
		public String Descriprion {
			get => _descriprion;
			set => Set(ref _descriprion, value);
		}


		private Grade _grade;
		public Grade Grade {
			get => _grade;
			set => Set(ref _grade, value);
		}

		public GradeModel(Grade grade) {
			Grade = grade;
			Year = grade.Year;
			Descriprion = grade.Description;

		}

	}
}
