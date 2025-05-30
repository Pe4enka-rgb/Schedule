﻿using Schedule.DB.Entity.Base;
using System.ComponentModel.DataAnnotations;

namespace Schedule.DB.Entity {
	public class SchoolClass : BaseEntity {
		[StringLength(1)]
		public string Letter { get; set; } = string.Empty;
		public virtual ICollection<Day> Days { get; set; } = new HashSet<Day>();
		public Grade Grade { get; set; }

		public override string ToString() {
			return Grade.Year.ToString() + Letter;
		}
	}
}
