using Schedule.DB.Entity.Base;

namespace Schedule.DB.Entity {
	public class Subject : BaseEntity {
		public string Name { get; set; } = string.Empty;
		public int Difficulty { get; set; }
		public int HoursPerWeek { get; set; }
		public int HoursPerDay { get; set; }

	}
}
