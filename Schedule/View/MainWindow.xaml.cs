using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Schedule {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		private readonly string LoremIpsum = "Lorem ipsum dolor sit amet consectetur adipiscing elit. Quisque faucibus ex sapien vitae pellentesque sem placerat. In id cursus mi pretium tellus duis convallis. Tempus leo eu aenean sed diam urna tempor. Pulvinar vivamus fringilla lacus nec metus bibendum egestas. Iaculis massa nisl malesuada lacinia integer nunc posuere. Ut hendrerit semper vel class aptent taciti sociosqu. Ad litora torquent per conubia nostra inceptos himenaeos.";


		public MainWindow() {
			InitializeComponent();
		}
		private void lbl1_MouseDown(object sender, MouseButtonEventArgs e) {
			Label lbl = (Label)sender;
			DragDrop.DoDragDrop(lbl, lbl.Content, DragDropEffects.Copy);
		}

		private void txtTarget_Drop(object sender, DragEventArgs e) {
			if (sender.GetType() == typeof(Label)) {
				Label cell = (Label)sender;
				cell.Content = e.Data.GetData(DataFormats.Text).ToString();



			} else if (sender.GetType() == typeof(TextBlock)) {
				( (TextBlock)sender ).Text = e.Data.GetData(DataFormats.Text).ToString();

			}

		}


		//private void add_Click(object sender, RoutedEventArgs e) {

		//	ScheduleGrid.ColumnDefinitions.Add(new ColumnDefinition());
		//}

		//private void del_Click(object sender, RoutedEventArgs e) {
		//	if (ScheduleGrid.ColumnDefinitions.Count <= 2)
		//		return;
		//	ScheduleGrid.ColumnDefinitions.Remove(ScheduleGrid.ColumnDefinitions.LastOrDefault());
		//}
	}
}