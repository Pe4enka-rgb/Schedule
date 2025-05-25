using Microsoft.Extensions.DependencyInjection;
using Schedule.ViewModels;

namespace Schedule.Infrastracture.DI {
	internal class ViewModelLocator {

		public MainWindowViewModel MainWindowModel =>
			App.Services.GetRequiredService<MainWindowViewModel>();
		public ReferenceViewModel ReferenceViewModel =>
			App.Services.GetRequiredService<ReferenceViewModel>();
		public ScheduleViewModel ScheduleViewModel =>
			App.Services.GetRequiredService<ScheduleViewModel>();
		public DayScheduleViewModel DayScheduleViewModel =>
			App.Services.GetRequiredService<DayScheduleViewModel>();
	}
}
