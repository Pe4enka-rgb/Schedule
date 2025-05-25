using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.ViewModels.Base;

namespace Schedule.ViewModels.EntityViewModels {
	class SchoolClassViewModel : ViewModel {
		private readonly IRepository<SchoolClass> _schoolClassRepository;
		private readonly IRepository<Grade> _gradeRepository;

		public SchoolClassViewModel(
			IRepository<SchoolClass> schoolClassRepository,
			IRepository<Grade> gradeRepository) {

			this._schoolClassRepository = schoolClassRepository;
			this._gradeRepository = gradeRepository;
		}
	}

}
