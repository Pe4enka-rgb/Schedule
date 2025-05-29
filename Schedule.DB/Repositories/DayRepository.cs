using Microsoft.EntityFrameworkCore;
using Schedule.DB.Context;
using Schedule.DB.Entity;

namespace Schedule.DB.Repositories {
	internal class DayRepository : DbRepository<Day> {
		public override IQueryable<Day> Items => base.Items
			.Include(day => day.Lessons)
			.Include(day => day.SchoolClass);
		public DayRepository(ScheduleDbContext db) : base(db) {

		}
	}
}
