using System;
using System.Globalization;
using Infrastructure.Extensions;
using Domain.Resources;
using Domain.Service;

namespace Infrastructure.Extensions
{
	public class DateHelper : IDateHelper
	{
		private readonly TryParser _tryParser;

		public DateHelper(TryParser tryParser)
		{
			this._tryParser = tryParser;
		}

		#region properties

		public DateTime SqlMinimumDate => this._tryParser.ConvertToDateTime(UtilityResources.SqlMinimumDate).Date;

		public DateTime SqlMaximumDate => this._tryParser.ConvertToDateTime(UtilityResources.SqlMaximumDate).Date;

		#endregion

		#region user defined methods

		/// <summary>
		///     Gets the first date by day of week.
		/// </summary>
		/// <param name="dateFrom"> The date from. </param>
		/// <param name="dayOfWeek"> The day of week. </param>
		/// <param name="searchAfter"> if set to <c>true</c> [search after]. </param>
		/// <returns> </returns>
		public DateTime GetFirstDateByDayOfWeek(DateTime dateFrom, DayOfWeek dayOfWeek, bool searchAfter)
		{
			var value = dateFrom;
			if (value.DayOfWeek != dayOfWeek)
			{
				double addDays = searchAfter ? 1 : -1;

				while (value.DayOfWeek != dayOfWeek)
				{
					value = value.AddDays(addDays);
				}
			}

			value = value.Date;

			return value;
		}

		/// <summary>
		///     Gets the first date in month.
		/// </summary>
		/// <param name="inputDate"> The input date. </param>
		/// <returns> </returns>
		public DateTime GetFirstDateInMonth(DateTime inputDate)
		{
			var value = new DateTime(inputDate.Year, inputDate.Month, 1).Date;

			return value;
		}

		/// <summary>
		///     Gets the end date in month.
		/// </summary>
		/// <param name="inputDate"> The input date. </param>
		/// <returns> </returns>
		public DateTime GetLastDateInMonth(DateTime inputDate)
		{
			var value = inputDate.AddMonths(1);

			value = value.AddDays(-value.Day).Date;

			return value;
		}

		/// <summary>
		///     Gets the first MONDAY in week.
		/// </summary>
		/// <param name="inputDate"> The input date. </param>
		/// <returns> </returns>
		public DateTime GetFirstDateInWeek(DateTime inputDate)
		{
			var value = this.GetFirstDateByDayOfWeek(inputDate, DayOfWeek.Monday, false);

			return value;
		}

		/// <summary>
		///     Gets the last date in week.
		/// </summary>
		/// <param name="inputDate"> The input date. </param>
		/// <returns> </returns>
		public DateTime GetLastDateInWeek(DateTime inputDate)
		{
			var value = this.GetFirstDateByDayOfWeek(inputDate, DayOfWeek.Sunday, true);

			return value;
		}

		/// <summary>
		///     Gets the index of the week.
		/// </summary>
		/// <param name="sourceDate"> The source date. </param>
		/// <returns> </returns>
		public int GetWeekIndex(DateTime sourceDate)
		{
			var firstDateInMonth = this.GetFirstDateInMonth(sourceDate);
			var firstMonday = this.GetFirstDateByDayOfWeek(sourceDate, DayOfWeek.Monday, false);
			const int dateAdding = -7;
			var value = 0;
			for (var i = firstMonday; i > firstDateInMonth; i = i.AddDays(dateAdding))
			{
				++value;
			}

			value -= value > 0 ? 1 : 0;

			return value;
		}

		public int GetWeekNumber(DateTime source)
		{
			var currentInfo = DateTimeFormatInfo.CurrentInfo;
			if (currentInfo == null)
			{
				return 0;
			}
			var cal = currentInfo.Calendar;
			var calendarWeekRule = currentInfo.CalendarWeekRule;
			return cal.GetWeekOfYear(source, calendarWeekRule, currentInfo.FirstDayOfWeek);
		}

		public DateTime GetSqlDate(DateTime source)
		{
			source = source.Date;
			var sqlMinimumDate = this.SqlMinimumDate;
			var sqlMaximumDate = this.SqlMaximumDate;
			if (source < sqlMinimumDate)
			{
				return sqlMinimumDate;
			}
			if (source > sqlMaximumDate)
			{
				return sqlMaximumDate;
			}
			return source;
		}

		#endregion
	}
}