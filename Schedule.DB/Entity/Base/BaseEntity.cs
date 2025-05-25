using Schedule.Interfaces;

namespace Schedule.DB.Entity.Base {
	public abstract class BaseEntity : IEntity {
		public int Id { get; set; }
	}
}
