using Schedule.DB.Entity;
using Schedule.Model;

namespace Schedule.Data {
	public static class LessonListExtension {
		public static ICollection<LessonModel> ToLessonModelCollection(this ICollection<Lesson> lessons) {
			var lessonsModels = new List<LessonModel>();
			foreach (var lesson in lessons)
				lessonsModels.Add(new LessonModel(lesson));

			return lessonsModels;
		}
	}
}
