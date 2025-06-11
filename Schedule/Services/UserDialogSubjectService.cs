using Schedule.DB.Entity;
using Schedule.Services.Interfaces;
using Schedule.View.EntityEditView;
using Schedule.ViewModels.EntityEditViewModels;

namespace Schedule.Services;

internal class UserDialogSubjectService : IUserDialog<Subject> {
	public bool Edit(Subject item) {
		var subjectEditViewModel = new SubjectEditViewModel(item);
		var subjectEditView = new SubjectEditView() {
			DataContext = subjectEditViewModel
		};

		if (subjectEditView.ShowDialog() != false)
			return false;

		item.Name = subjectEditViewModel.Name;
		item.Difficulty = subjectEditViewModel.Difficulty;
		item.HoursPerDay = subjectEditViewModel.HoursPerDay;
		item.HoursPerWeek = subjectEditViewModel.HoursPerWeek;

		return true;
	}
}