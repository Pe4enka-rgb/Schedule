using Schedule.DB.Entity.Base;
using Schedule.Interfaces;

namespace Schedule.ViewModels.Interfaces {
	interface Interface1<T> where T : BaseEntity, IEntity, new() {
		//private ObservableCollection<T> 
	}
}
