using MathCore.WPF.Commands;
using Microsoft.EntityFrameworkCore;
using Schedule.DB.Entity;
using Schedule.Interfaces;
using Schedule.Services.Interfaces;
using Schedule.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Schedule.ViewModels.EntityViewModels {
	class GradeViewModel : ViewModel {
		private readonly IRepository<Grade> _gradeRepository;
		private readonly IUserDialog<Grade> _userDialog;

		private ObservableCollection<Grade> _grades;

		#region Properies
		public ObservableCollection<Grade> Grades {
			get { return _grades; }
			set { Set(ref _grades, value); }
		}

		private Grade _SelectedGrade;
		public Grade SelectedGrade {
			get => _SelectedGrade;
			set => Set(ref _SelectedGrade, value);
		}


		#endregion
		#region LoadData Command

		private ICommand _LoadDataCommand;
		public ICommand LoadDataCommand => _LoadDataCommand
		?? new LambdaCommandAsync(OnLoadDataCommandExecuted);

		private async Task OnLoadDataCommandExecuted() {
			Grades = new(await _gradeRepository.Items.ToArrayAsync());
		}

		#endregion
		#region AddGrade Command

		private ICommand _AddGradeCommand;
		public ICommand AddGradeCommand => _AddGradeCommand
		??= new LambdaCommand(OnAddGradeCommandExecuted);

		private void OnAddGradeCommandExecuted() {
			Grade newGrade = new();
			if (!_userDialog.Edit(newGrade)) {
				return;
			}
			_gradeRepository.Add(newGrade);
			Grades.Add(newGrade);
			SelectedGrade = newGrade;
		}

		#endregion
		#region EditGrade Command
		private ICommand _EditGradeCommand;
		public ICommand EditGradeCommand => _EditGradeCommand
			??= new LambdaCommand<Grade>(OnEditGradeCommandExecuted);

		private void OnEditGradeCommandExecuted(Grade parametrGrade) {
			if (parametrGrade == null)
				return;
			if (!_userDialog.Edit(parametrGrade)) {
				return;
			}
			_gradeRepository.Update(parametrGrade);
			Grades.GroupBy(g => g.Year);
			SelectedGrade = parametrGrade;
		}

		#endregion
		#region DeleteGrade Command

		private ICommand _DeleteGradeCommand;
		public ICommand DeleteGradeCommand => _DeleteGradeCommand
		??= new LambdaCommand<Grade>(OnDeleteGradeCommandExecuted);

		private void OnDeleteGradeCommandExecuted(Grade parametrGrade) {

			var gradeToRemove = parametrGrade ?? SelectedGrade;

			if (gradeToRemove != null) {
				_gradeRepository.Remove(gradeToRemove);
				_grades.Remove(gradeToRemove);
			}

			SelectedGrade = null;
		}
		private bool CanDeleteGradeCommandExecuted(Grade grade) => grade != null || SelectedGrade != null;

		#endregion

		private void PreviewTextInput(object sender, TextCompositionEventArgs e) {
		}

		public GradeViewModel(
			IRepository<Grade> gradeRepository,
			IUserDialog<Grade> userDialog) {
			_gradeRepository = gradeRepository;
			_userDialog = userDialog;

		}
	}

}
