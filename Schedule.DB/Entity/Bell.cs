using Schedule.DB.Entity.Base;
using System.ComponentModel.DataAnnotations;

namespace Schedule.DB.Entity {
	public class Bell : BaseEntity {
		public TimeOnly Start { get; set; }
		public TimeOnly End { get; set; }
	}
}
