using System.Globalization;

namespace Schedule.Data {
	public static class DayOfWeekExtension {
		public static string GetDayName(this DayOfWeek value) {
			return DateTimeFormatInfo.CurrentInfo.GetDayName(value);
		}

		public static string GetAbbreviatedDayName(this DayOfWeek value) {
			return DateTimeFormatInfo.CurrentInfo.GetAbbreviatedDayName(value);
		}
	}
}
