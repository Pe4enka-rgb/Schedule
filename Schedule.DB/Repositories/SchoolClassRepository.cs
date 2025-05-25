using Microsoft.EntityFrameworkCore;
using Schedule.DB.Context;
using Schedule.DB.Entity;

namespace Schedule.DB.Repositories {
	internal class SchoolClassRepository : DbRepository<SchoolClass> {
		public override IQueryable<SchoolClass> Items => base.Items
			.Include(sc => sc.Grade)
			.Include(sc => sc.Days);
		public SchoolClassRepository(ScheduleDbContext db) : base(db) { }
	}
}
