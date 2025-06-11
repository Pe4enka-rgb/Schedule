using Schedule.DB.Entity;
using Schedule.Services.Interfaces;
using Schedule.View.EntityEditView;
using Schedule.ViewModels.EntityEditViewModels;

namespace Schedule.Services {
	internal class UserDialogGradeService : IUserDialog<Grade> {

		public bool Edit(Grade item) {
			var gradeEditViewModel = new GradeEditViewModel(item);
			var gradeEditView = new GradeEditView() {
				DataContext = gradeEditViewModel
			};

			if (gradeEditView.ShowDialog() != false)
				return false;

			item.Year = gradeEditViewModel.GradeYear;
			item.Description = gradeEditViewModel.GradeName;

			return true;
		}


	}

	internal class UserDialogSchoolClassService : IUserDialog<SchoolClass> {
		public bool Edit(SchoolClass item) {
			var schoolClassEditViewModel = new SchoolClassEditViewModel(item);
			var schoolClassEditView = new SchoolClassEditView() {
				DataContext = schoolClassEditViewModel
			};

			if (schoolClassEditView.ShowDialog() != false)
				return false;

			item.Grade = schoolClassEditViewModel.Grade;
			item.Letter = schoolClassEditViewModel.Letter;

			return true;
		}
	}
}
