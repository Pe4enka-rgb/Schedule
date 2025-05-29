using Schedule.DB.Entity;
using Schedule.ViewModels.Base;

namespace Schedule.Model {
	public class LessonModel : ViewModel {

		private Subject _subject;
		public Subject Subject {
			get => _subject;
			set {
				_subject = value;
				OnPropertyChanged();
			}
		}

		private Bell _bell;
		public Bell Bell {
			get => _bell;
			set {
				_bell = value;
				OnPropertyChanged();
			}
		}

		private Lesson _lesson;

		public Lesson Lesson {
			get => _lesson;
			set {
				_lesson = value;
				OnPropertyChanged();
			}
		}

		public LessonModel(Lesson lesson) {
			Lesson = lesson;
			Subject = lesson.Subject;
			Bell = lesson.Bell;
		}

		public override string ToString() {
			return Subject.Name;
		}
	}
}
