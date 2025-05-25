using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Schedule.DB;
using Schedule.DB.Context;

namespace Schedule.Data {
	static class DbRegistrator {
		public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration Configuration) => services
			.AddDbContext<ScheduleDbContext>(options => {
				var type = Configuration["Type"];
				switch (type) {
					case "PostgreSQL":
						options.UseNpgsql(Configuration.GetConnectionString(type));
						break;
					case "InMemory":
						options.UseInMemoryDatabase("Schedule.db");
						break;
					case null:
						throw new InvalidOperationException("Не определен тип Базы данных");

					default:
						throw new InvalidOperationException($"Тип подключения {type} не поддерживается");

				}
			})
			.AddTransient<DbInitializer>()
			.AddRepository();
	}
}
