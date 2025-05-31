using Microsoft.Xaml.Behaviors;
using Schedule.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Schedule.Infrastracture {
	public class MouseEnterBehavior : Behavior<FrameworkElement> {
		protected override void OnAttached() {
			base.OnAttached();
			AssociatedObject.MouseMove += OnMouseEnter;
		}

		protected override void OnDetaching() {
			base.OnDetaching();
			AssociatedObject.MouseMove -= OnMouseEnter;
		}

		private void OnMouseEnter(object sender, MouseEventArgs e) {
			var element = sender as FrameworkElement;
			if (element == null)
				return;

			var dataGrid = sender as DataGrid;
			if (dataGrid == null)
				return;


			Point mousePos = e.GetPosition(dataGrid);

			// Пытаемся найти элемент под курсором
			DependencyObject depObj = dataGrid.InputHitTest(mousePos) as DependencyObject;
			int rowIndex = -1;
			int columnIndex = -1;
			while (depObj != null) {
				if (depObj is DataGridCell celll) {
					rowIndex = dataGrid.Items.IndexOf(celll.DataContext);
					columnIndex = dataGrid.Columns.IndexOf(celll.Column);

					//MessageBox.Show($"Позиция: строка {rowIndex}, столбец {columnIndex}");
					break;
				}

				depObj = VisualTreeHelper.GetParent(depObj);
			}
			if (rowIndex < 0 || columnIndex < 0)
				return;

			// Получаем ViewModel
			var viewModel = dataGrid.DataContext as DayScheduleViewModel;
			if (viewModel != null) {
				try {
					viewModel.HoveredRowIndex = rowIndex;
					viewModel.HoveredColumnIndex = columnIndex;
					viewModel.HoveredValue = viewModel.LessonModels2DTransposed[columnIndex][rowIndex];
				}
				catch (NullReferenceException) {
					viewModel.HoveredValue = null;
				}

			}

		}
	}
}
