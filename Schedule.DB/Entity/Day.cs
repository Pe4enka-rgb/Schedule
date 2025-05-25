using Schedule.DB.Entity.Base;
using System.ComponentModel.DataAnnotations;

namespace Schedule.DB.Entity {
	public class Day : BaseEntity {
		[Required]
		public SchoolClass SchoolClass { get; set; }
		public DayOfWeek DayOfWeek { get; set; }
		public virtual ICollection<Subject> Subjects { get; set; }

	}
}
