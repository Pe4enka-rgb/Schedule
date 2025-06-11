using Schedule.DB.Entity;
using Schedule.Services.Interfaces;
using Schedule.View.EntityEditView;
using Schedule.ViewModels.EntityEditViewModels;

namespace Schedule.Services;

internal class UserDialogBellService : IUserDialog<Bell> {
	public bool Edit(Bell item) {
		var bellEditViewModel = new BellEditViewModel(item);
		var bellEditView = new BellEditView() {
			DataContext = bellEditViewModel
		};

		if (bellEditView.ShowDialog() != false)
			return false;

		item.Start = bellEditViewModel.Start;
		item.End = bellEditViewModel.End;

		return true;
	}
}