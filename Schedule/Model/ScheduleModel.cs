using Schedule.DB.Entity;

namespace Schedule.Model {
	internal class ScheduleModel {
		private string _className;

		public string ClassName {
			get {
				return SchoolClass.Grade.Year.ToString() + SchoolClass.Letter;
			}
		}

		public SchoolClass SchoolClass { get; set; }
		public IQueryable<Lesson> Lessons { get; set; }

	}
}
