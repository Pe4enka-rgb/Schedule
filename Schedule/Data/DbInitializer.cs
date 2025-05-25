using Microsoft.Extensions.Logging;
using Schedule.DB.Context;
using Schedule.DB.Entity;

namespace Schedule.Data {
	public class DbInitializer {
		private readonly ILogger<DbInitializer> _Logger;
		private readonly ScheduleDbContext _db;

		public DbInitializer(ScheduleDbContext db, ILogger<DbInitializer> Logger) {
			_Logger = Logger;
			_db = db;
		}

		public async Task InitializeAsync() {
			await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);


			await _db.Database.EnsureCreatedAsync().ConfigureAwait(false);

			await InitializeGrades();
			await InitializeSubjects();
			await InitializeBells();
			await InitializeSchoolClasses();
			await InitializeLessons();

		}

		private int _gradesNumber = 11;
		private Grade[] _grades;
		private async Task InitializeGrades() {
			_grades = new Grade[_gradesNumber];

			for (int i = 0; i < _gradesNumber; i++) {
				_grades[i] = new Grade() { Year = i + 1, Description = i.GetHashCode().ToString() };
			}

			await _db.Grades.AddRangeAsync(_grades);
			await _db.SaveChangesAsync();
		}

		private string subjectString =
			"    Математика\r\n    Алгебра        \r\n    Геометрия       \r\n    Физика          \r\n    Химия           \r\n    Информатика_и_ИКТ\r\n\r\n    Окружающий_мир  \r\n    География       \r\n    Биология        \r\n    Обществознание  \r\n    Литература      \r\n    История         \r\n    ОБЖ            \r\n    Технология     \r\n    Физ-ра          \r\n    Музыка          \r\n    ИЗО             \r\n    МХК             \r\n\r\n    Русский_язык    \r\n    Английский_язык \r\n    Немецкий_язык   \r\n    Иностранный_язык";
		private string[] _subjectsStrings;
		private Subject[] _subjects;

		private async Task InitializeSubjects() {
			subjectString = subjectString.Replace('\n', ' ');
			subjectString = subjectString.Replace("\r", " ");
			_subjectsStrings = subjectString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

			_subjects = new Subject[_subjectsStrings.Length];

			for (int i = 0; i < _subjectsStrings.Length; i++) {
				_subjects[i] = new Subject() { Name = _subjectsStrings[i] };
			}

			await _db.Subjects.AddRangeAsync(_subjects);
			await _db.SaveChangesAsync();
		}

		private int bellNumber = 11;
		private Bell[] _bells;
		private async Task InitializeBells() {
			_bells = new Bell[bellNumber];

			TimeOnly start = new TimeOnly(8, 0);
			TimeOnly end = new TimeOnly(8, 45);

			for (int i = 0; i < bellNumber; i++) {
				_bells[i] = new Bell() { Start = start, End = end };
				start = start.AddMinutes(55.0);
				end = end.AddMinutes(55.0);
			}

			await _db.Bells.AddRangeAsync(_bells);
			await _db.SaveChangesAsync();
		}

		private async Task InitializeDays() {
			DayOfWeek _dayOfWeek = 0;


		}

		private Lesson[] _lessons;

		private async Task InitializeLessons() {
			_lessons = new Lesson[_classes.Count * bellNumber];

			for (int i = 0; i < _classes.Count; i++) {
				for (int j = 0; j < bellNumber; j++) {
					_lessons[i + j] = new Lesson() { Bell = _bells[j], Subject = _subjects[j] };
				}
			}



			//await _db.Lessons.AddRangeAsync(_lessons);
			//await _db.SaveChangesAsync();
		}

		private List<SchoolClass> _classes;
		private async Task InitializeSchoolClasses() {
			_classes = new(_gradesNumber * 4);

			for (int i = 1; i <= _gradesNumber; i++) {
				_classes.Add(new SchoolClass() { Grade = _grades[i - 1], Letter = "a" });
				_classes.Add(new SchoolClass() { Grade = _grades[i - 1], Letter = "б" });
				_classes.Add(new SchoolClass() { Grade = _grades[i - 1], Letter = "в" });
				_classes.Add(new SchoolClass() { Grade = _grades[i - 1], Letter = "г" });
			}

			await _db.SchoolClasses.AddRangeAsync(_classes);
			await _db.SaveChangesAsync();
		}
	}
}
