using Schedule.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Schedule.DB.Entity.Base {
	public abstract class BaseEntity : IEntity {
		[Key]
		public int Id { get; set; }
	}
}
