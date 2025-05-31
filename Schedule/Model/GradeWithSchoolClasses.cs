using Schedule.DB.Entity;
using Schedule.ViewModels.Base;

namespace Schedule.Model {
	public class GradeWithSchoolClasses : ViewModel {
		List<SchoolClass> _schoolClasses;
		public List<SchoolClass> SchoolClasses {
			get => _schoolClasses;
			set => Set(ref _schoolClasses, value);
		}

		private Grade _grade;
		private readonly List<SchoolClass> _classes;

		public Grade Grade {
			get => _grade;
			set => Set(ref _grade, value);
		}

		public GradeWithSchoolClasses(Grade grade, List<SchoolClass> classes) {
			Grade = grade;
			SchoolClasses = classes;
		}
	}
}
