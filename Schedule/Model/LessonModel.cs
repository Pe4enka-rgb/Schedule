using Schedule.DB.Entity;
using Schedule.ViewModels.Base;

namespace Schedule.Model {
	public class LessonModel : ViewModel {

		private Subject _subject;
		public Subject Subject {
			get => _subject;
			set {
				if (Set(ref _subject, value))
					this.Lesson.Subject = value;
			}
		}

		private Bell _bell;
		public Bell Bell {
			get => _bell;
			set {
				if (Set(ref _bell, value))
					this.Lesson.Bell = value;
			}
		}

		private Lesson _lesson;
		public Lesson Lesson {
			get => _lesson;
			set {
				if (Set(ref _lesson, value)) {
					Subject = value.Subject;
					Bell = value.Bell;
				}

			}
		}

		public LessonModel(Lesson lesson) {
			Lesson = lesson;
			Subject = lesson.Subject;
			Bell = lesson.Bell;
		}
	}
}
