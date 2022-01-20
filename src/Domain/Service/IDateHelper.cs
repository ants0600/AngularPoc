using System;
using Domain.Service;

namespace Domain.Service
{
	public interface IDateHelper : IService
	{
		DateTime SqlMinimumDate { get; }
		DateTime SqlMaximumDate { get; }

		/// <summary>
		///     Gets the first date by day of week.
		/// </summary>
		/// <param name="dateFrom"> The date from. </param>
		/// <param name="dayOfWeek"> The day of week. </param>
		/// <param name="searchAfter"> if set to <c>true</c> [search after]. </param>
		/// <returns> </returns>
		DateTime GetFirstDateByDayOfWeek(DateTime dateFrom, DayOfWeek dayOfWeek, bool searchAfter);

		/// <summary>
		///     Gets the first date in month.
		/// </summary>
		/// <param name="inputDate"> The input date. </param>
		/// <returns> </returns>
		DateTime GetFirstDateInMonth(DateTime inputDate);

		/// <summary>
		///     Gets the end date in month.
		/// </summary>
		/// <param name="inputDate"> The input date. </param>
		/// <returns> </returns>
		DateTime GetLastDateInMonth(DateTime inputDate);

		/// <summary>
		///     Gets the first MONDAY in week.
		/// </summary>
		/// <param name="inputDate"> The input date. </param>
		/// <returns> </returns>
		DateTime GetFirstDateInWeek(DateTime inputDate);

		/// <summary>
		///     Gets the last date in week.
		/// </summary>
		/// <param name="inputDate"> The input date. </param>
		/// <returns> </returns>
		DateTime GetLastDateInWeek(DateTime inputDate);

		/// <summary>
		///     Gets the index of the week.
		/// </summary>
		/// <param name="sourceDate"> The source date. </param>
		/// <returns> </returns>
		int GetWeekIndex(DateTime sourceDate);

		int GetWeekNumber(DateTime source);
		DateTime GetSqlDate(DateTime source);
	}
}