using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.ViewModels.Base;

namespace Schedule.ViewModels.EntityViewModels {
	class DayViewModel : ViewModel {
		private readonly IRepository<SchoolClass> schoolClassRepository;
		private readonly IRepository<Subject> subjectRepository;

		public DayViewModel(IRepository<SchoolClass> schoolClassRepository, IRepository<Subject> subjectRepository) {
			this.schoolClassRepository = schoolClassRepository;
			this.subjectRepository = subjectRepository;
		}
	}

}
