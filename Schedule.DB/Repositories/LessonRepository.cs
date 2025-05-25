using Microsoft.EntityFrameworkCore;
using Schedule.DB.Context;
using Schedule.DB.Entity;

namespace Schedule.DB.Repositories;

internal class LessonRepository : DbRepository<Lesson> {
	public override IQueryable<Lesson> Items => base.Items
		.Include(lesson => lesson.Subject)
		.Include(lesson => lesson.Bell);
	public LessonRepository(ScheduleDbContext db) : base(db) {

	}
}