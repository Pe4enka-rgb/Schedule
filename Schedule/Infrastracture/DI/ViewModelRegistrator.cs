using Microsoft.Extensions.DependencyInjection;
using Schedule.ViewModels;

namespace Schedule.Infrastracture.DI {
	internal static class ViewModelRegistrator {
		public static IServiceCollection AddViewModels(this IServiceCollection services) => services
				.AddSingleton<MainWindowViewModel>()
				.AddSingleton<ReferenceViewModel>()
				.AddSingleton<ScheduleViewModel>()
				.AddSingleton<DayScheduleViewModel>()

		;

	}
}
