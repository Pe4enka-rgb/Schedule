using Microsoft.Extensions.DependencyInjection;
using Schedule.DB.Entity;
using Schedule.Services.Interfaces;

namespace Schedule.Services {
	static class ServicesRegistrator {
		public static IServiceCollection AddServices(this IServiceCollection services) =>
			services
				.AddSingleton(services)
				.AddTransient<IUserDialog<Grade>, UserDialogGradeService>()
		;
	}
}
