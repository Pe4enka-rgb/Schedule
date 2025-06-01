using Schedule.DB.Entity.Base;
using System.ComponentModel.DataAnnotations;

namespace Schedule.DB.Entity {
	public class SchoolClass : BaseEntity {
		[StringLength(1)]
		public string Letter { get; set; } = string.Empty;
		public virtual ICollection<Day> Days { get; set; } = new HashSet<Day>();
		[Required]
		public Grade Grade { get; set; }
	}
}
