using Microsoft.EntityFrameworkCore;
using Schedule.DB.Entity;

namespace Schedule.DB.Context {
	public class ScheduleDbContext : DbContext {
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Bell> Bells { get; set; }
		public DbSet<Grade> Grades { get; set; }
		public DbSet<Day> Days { get; set; }
		public DbSet<SchoolClass> SchoolClasses { get; set; }
		public DbSet<Lesson> Lessons { get; set; }

		public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options) : base(options) {

		}


	}
}
