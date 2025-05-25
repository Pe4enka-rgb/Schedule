using Schedule.DB.Entity;
using Schedule.ViewModels.Base;

namespace Schedule.ViewModels.EntityEditViewModels {
	internal class GradeEditViewModel : ViewModel {

		public int _gradeId { get; }

		private int _gradeYear;
		public int GradeYear {
			get => _gradeYear;
			set => Set(ref _gradeYear, Convert.ToInt32(value));
		}

		private string _gradeName;
		public string GradeName {
			get => _gradeName;
			set => Set(ref _gradeName, value);
		}

		public GradeEditViewModel(Grade grade) {
			_gradeId = grade.Id;
			_gradeYear = grade.Year;
			_gradeName = grade.Description;

		}


	}
}
