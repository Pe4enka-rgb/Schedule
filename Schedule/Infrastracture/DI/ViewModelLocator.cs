using Microsoft.Extensions.DependencyInjection;
using Schedule.ViewModels;
using Schedule.ViewModels.EntityViewModels;

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
		public SubjectListToDragViewModel SubjectListToDragViewModel =>
			App.Services.GetRequiredService<SubjectListToDragViewModel>();

		public GradeViewModel GradeViewModel =>
			App.Services.GetRequiredService<GradeViewModel>();
		public BellViewModel BellViewModel =>
			App.Services.GetRequiredService<BellViewModel>();
		public SubjectViewModel SubjectViewModel =>
			App.Services.GetRequiredService<SubjectViewModel>();
		public SchoolClassViewModel SchoolClassViewModel =>
			App.Services.GetRequiredService<SchoolClassViewModel>();
	}
}
