using Microsoft.Extensions.DependencyInjection;
using Schedule.DB.Entity;
using Schedule.DB.Repositories;
using Schedule.Interfaces;

namespace Schedule.DB {
	public static class RepositoryRegistrator {

		public static IServiceCollection AddRepository(this IServiceCollection services) => services
			.AddTransient<IRepository<Day>, DayRepository>()
			.AddTransient<IRepository<Bell>, BellRepository>()
			.AddTransient<IRepository<Grade>, DbRepository<Grade>>()
			.AddTransient<IRepository<Subject>, DbRepository<Subject>>()
			.AddTransient<IRepository<SchoolClass>, SchoolClassRepository>()
			.AddTransient<IRepository<Lesson>, LessonRepository>()
		;
	}
}
