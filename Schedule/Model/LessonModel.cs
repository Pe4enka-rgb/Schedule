using Schedule.DB.Entity;
using Schedule.Model.Base;
using Schedule.Services.Interfaces;

namespace Schedule.Model {	
	public class LessonModel : ModelBase<Lesson>, IModelBase<Lesson> {

		private Subject? _subject;
		public Subject? Subject {
			get => _subject;
			set {
				if (Set(ref _subject, value))
					this.Entity.Subject = value;
			}
		}

		private Bell _bell;
		public Bell Bell {
			get => _bell;
			set {
				if (Set(ref _bell, value))
					this.Entity.Bell = value;
			}
		}

		private Lesson _lesson;
		public override Lesson Entity {
			get => _lesson;
			set {
				if (Set(ref _lesson, value)) {
					Subject = value.Subject;
					Bell = value.Bell;
				}

			}
		}

		public LessonModel(Lesson lesson) {
			Entity = lesson;
			Subject = lesson.Subject;
			Bell = lesson.Bell;
		}
	}
}
