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
}
