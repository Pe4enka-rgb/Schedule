using Schedule.DB.Entity;
using Schedule.ViewModels.Base;

namespace Schedule.ViewModels.EntityEditViewModels {
	internal class GradeEditViewModel(Grade grade) : ViewModel {

		public int _gradeId { get; } = grade.Id;

		private int _gradeYear = grade.Year;
		public int GradeYear {
			get => _gradeYear;
			set => Set(ref _gradeYear, Convert.ToInt32(value));
		}

		private string _gradeName = grade.Description;
		public string GradeName {
			get => _gradeName;
			set => Set(ref _gradeName, value);
		}
	}
}
