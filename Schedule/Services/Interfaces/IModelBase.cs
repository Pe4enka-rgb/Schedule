using Schedule.DB.Entity.Base;

namespace Schedule.Services.Interfaces {
	public interface IModelBase<out T> where T : BaseEntity {
		T Entity { get; }
	}
}
