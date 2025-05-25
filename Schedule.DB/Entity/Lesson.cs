using Schedule.DB.Entity.Base;

namespace Schedule.DB.Entity {
	public class Lesson : BaseEntity {
		public Subject Subject { get; set; }
		public Bell Bell { get; set; }
	}
}
