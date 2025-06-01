using System.Globalization;
using System.Windows.Data;

namespace Schedule.Data {
	public class DayOfWeekToRussianStringConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is DayOfWeek dayOfWeek) {
				culture = new CultureInfo("ru-RU");
				return culture.DateTimeFormat.GetDayName(dayOfWeek);
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
