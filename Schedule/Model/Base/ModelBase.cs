using Schedule.DB.Entity.Base;
using Schedule.Services.Interfaces;
using Schedule.ViewModels.Base;

namespace Schedule.Model.Base {
	public abstract class ModelBase<T> : ViewModel, IModelBase<T> where T : BaseEntity {
		public virtual T Entity { get; set; }
	}
}
