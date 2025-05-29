using Schedule.DB.Context;
using Schedule.DB.Entity;

namespace Schedule.DB.Repositories;

internal class BellRepository : DbRepository<Bell> {
	public int BellCount {
		get => Items.Count();
	}
	public BellRepository(ScheduleDbContext db) : base(db) {
	}
}