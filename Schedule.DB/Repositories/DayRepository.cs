using Microsoft.EntityFrameworkCore;
using Schedule.DB.Context;
using Schedule.DB.Entity;

namespace Schedule.DB.Repositories {
	internal class DayRepository : DbRepository<Day> {
		public override IQueryable<Day> Items => base.Items
			.Include(day => day.Subjects)
			.Include(day => day.SchoolClass);
		public DayRepository(ScheduleDbContext db) : base(db) {

		}
	}

	internal class BellRepository : DbRepository<Bell> {
		public int BellCount {
			get => Items.Count();
		}
		public BellRepository(ScheduleDbContext db) : base(db) {
		}
	}
}
