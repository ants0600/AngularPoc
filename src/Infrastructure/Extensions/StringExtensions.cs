using System;

namespace Infrastructure.Extensions
{
	public class StringExtensions
	{
		private const string ANCHOR_STARTING = "<a>";
		private const string ANCHOR_ENDING = "</a>";
		private const string ANCHOR_SEPARATOR = ANCHOR_ENDING + ANCHOR_STARTING;
		private readonly TryParser _tryParser;

		public StringExtensions(TryParser tryParser)
		{
			this._tryParser = tryParser;
		}

		/// <summary>
		///     default constructing parameter for xml parameter.
		///     Can return NULL value, since we want to pass sql parameter
		/// </summary>
		public string ConstructXmlParameterWithAnchor(string[] values)
		{
			if (values == null || values.Length <= 0)
			{
				return null;
			}

			var value = this.Join(values, ANCHOR_SEPARATOR, ANCHOR_STARTING, ANCHOR_ENDING);
			return value;
		}

		/// <summary>
		///     Gets the file extension.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <returns> </returns>
		public string GetFileExtension(string value)
		{
			value = this._tryParser.ConvertToString(value).Trim();

			var lastIndexOf = value.LastIndexOf(".", StringComparison.Ordinal);

			return lastIndexOf >= 0 ? (value.Length > 0 ? value.Substring(lastIndexOf) : string.Empty) : string.Empty;
		}

		/// <summary>
		///     Gets the trim string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public string GetTrimString(string value)
		{
			return this._tryParser.ConvertToString(value).Trim();
		}


		/// <summary>
		///     Gets the trim string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="length">The length.</param>
		/// <returns></returns>
		public string GetTrimString(string value, int length)
		{
			value = this.GetTrimString(value);
			if (value.Length > length)
			{
				value = value.Substring(0, length);
			}
			return value;
		}


		/// <summary>
		///     Gets the trim string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="length">The length.</param>
		/// <returns></returns>
		public string GetTrimString(string value, string length)
		{
			var intLength = this._tryParser.ConvertToInteger(length);
			return this.GetTrimString(value, intLength);
		}

		public string Join(string[] values, string separator, string starting, string ending)
		{
			var value = string.Join(separator, values);
			const string FORMAT = "{0}{1}{2}";
			value = string.Format(FORMAT, starting, value, ending);
			return value;
		}

		public string[] Split(string separated, string separator, bool isReturningSelfIfNoSeparator)
		{
			var indexOfSeparator = separated.IndexOf(separator, StringComparison.OrdinalIgnoreCase);
			var isContainingSeparator = indexOfSeparator >= 0;
			if (!isContainingSeparator && isReturningSelfIfNoSeparator)
			{
				return new[] {separated};
			}

			if (!isContainingSeparator)
			{
				return new string[] { };
			}

			var values = separated.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries);
			return values;
		}
	}
}