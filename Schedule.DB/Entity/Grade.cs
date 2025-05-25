using Schedule.DB.Entity.Base;

namespace Schedule.DB.Entity {
	public class Grade : BaseEntity {
		public int Year { get; set; }
		public string Description { get; set; }

		public override string ToString() {
			return Year.ToString();
		}
	}
}
