using System.Globalization;
using System.Windows.Data;

namespace Schedule.Data {
	public class DayOfWeekToLocalizedStringConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is DayOfWeek dayOfWeek) {
				// Получаем текущий язык приложения
				var ci = CultureInfo.CurrentCulture;

				var str = ci.DateTimeFormat.GetDayName(dayOfWeek).ToLower();
				char.ToUpper(str[0]);
				// Пытаемся получить локализованное название дня
				return str;
			}

			return string.Empty;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
