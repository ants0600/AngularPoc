using System;
using System.Data;

namespace Infrastructure.Extensions
{
	public class TryParser
	{
		#region fields

		/// <summary>
		///   value = "'"
		/// </summary>
		public const string SingleQuoteString = "'";

		/// <summary>
		///   value = ''
		/// </summary>
		public const string SingleQuoteSqlString = "''";

		/// <summary>
		///   1 jan 1800
		/// </summary>
		public const string MinimumDateTimeString = "1 jan 1800";

		/// <summary>
		///   dd MMM yyyy
		/// </summary>
		public const string FormatDateDdMmmYyyy = "dd MMM yyyy";

		/// <summary>
		///   yyyy-MM-dd
		/// </summary>
		public const string FormatDateYyyyMmDd = "yyyy-MM-dd";

		/// <summary>
		///   MMM yyyy
		/// </summary>
		public const string FormatDateMmmYyyy = "MMM yyyy";

		#endregion

		/// <summary>
		///   method getIsNull seperti getIsNull( , ) di SQL Server
		/// </summary>
		/// <param name="testedValue"> input testedValue </param>
		/// <param name="nullValue"> value jika testedValue = null </param>
		/// <returns> </returns>
		public object GetIsNull(object testedValue, object nullValue)
		{
			return testedValue == DBNull.Value || testedValue == null || testedValue == Convert.DBNull
					   ? nullValue
					   : testedValue;
		}

		/// <summary>
		///   Converts to GUID.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <param name="nullValue"> The null value. </param>
		/// <returns> </returns>
		public Guid ConvertToGuid(object value, Guid nullValue)
		{
			try
			{
				string stringValue = ConvertToString(value);
				return string.IsNullOrEmpty(stringValue) ? nullValue : new Guid(stringValue);
			}
			catch
			{ }
			return nullValue;
		}

		/// <summary>
		///   Converts to GUID.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <returns> </returns>
		public Guid ConvertToGuid(object value)
		{
			return ConvertToGuid(value, Guid.Empty);
		}

		#region method convert to string

		/// <summary>
		///   Converts to string.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <returns> </returns>
		public string ConvertToString(object value)
		{
			return ConvertToString(value, string.Empty);
		}

		/// <summary>
		///   Converts to string.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <param name="nullValue"> The null value. </param>
		/// <returns> </returns>
		public string ConvertToString(object value, string nullValue)
		{
			////get null value
			value = GetIsNull(value, nullValue);

			////convert to string
			string s = value.ToString();

			return s;
		}

		#endregion

		#region method convert to datetime

		/// <summary>
		///   Converts to date time.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <returns> </returns>
		public DateTime ConvertToDateTime(object value)
		{
			return ConvertToDateTime(value, DateTime.Parse(MinimumDateTimeString));
		}

		/// <summary>
		///   Converts to date time.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <param name="nullValue"> The null value. </param>
		/// <returns> </returns>
		public DateTime ConvertToDateTime(object value, DateTime nullValue)
		{
			DateTime s;

			////get null value
			value = GetIsNull(value, nullValue);

			////convert to date
			DateTime.TryParse(value.ToString(), out s);

			return s;
		}

		#endregion

		#region method convert To Integer

		/// <summary>
		///   Converts to integer.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <returns> </returns>
		public int ConvertToInteger(object value)
		{
			return ConvertToInteger(value, 0);
		}

		/// <summary>
		///   Converts to integer.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <param name="nullValue"> The null value. </param>
		/// <returns> </returns>
		public int ConvertToInteger(object value, int nullValue)
		{
			int s;

			////get null value
			value = GetIsNull(value, nullValue);

			////try convert to integer
			int.TryParse(value.ToString(), out s);

			return s;
		}

		#endregion

		#region method convert to double

		/// <summary>
		///   Converts to double.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <returns> </returns>
		public double ConvertToDouble(object value)
		{
			return ConvertToDouble(value, 0);
		}

		/// <summary>
		///   Converts to double.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <param name="nullValue"> The null value. </param>
		/// <returns> </returns>
		public double ConvertToDouble(object value, double nullValue)
		{
			double s;

			////get null value
			value = GetIsNull(value, nullValue);

			////try convert to double
			double.TryParse(value.ToString(), out s);

			return s;
		}

		#endregion

		#region method convert to float

		/// <summary>
		///   Converts to float.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <returns> </returns>
		public float ConvertToFloat(object value)
		{
			return ConvertToFloat(value, 0);
		}

		/// <summary>
		///   Converts to float.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <param name="nullValue"> The null value. </param>
		/// <returns> </returns>
		public float ConvertToFloat(object value, float nullValue)
		{
			float s;

			////get null value
			value = GetIsNull(value, nullValue);

			////try convert to float
			float.TryParse(value.ToString(), out s);

			return s;
		}

		#endregion

		#region method convert to long

		/// <summary>
		///   Converts to long.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <returns> </returns>
		public long ConvertToLong(object value)
		{
			return ConvertToLong(value, 0);
		}

		/// <summary>
		///   Converts to long.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <param name="nullValue"> The null value. </param>
		/// <returns> </returns>
		public long ConvertToLong(object value, long nullValue)
		{
			long s;

			////get null value
			value = GetIsNull(value, nullValue);

			////try convert to long
			long.TryParse(value.ToString(), out s);

			return s;
		}

		#endregion

		#region method convert to boolean

		/// <summary>
		///   Converts to boolean.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <param name="nullValue"> if set to <c>true</c> [null value]. </param>
		/// <returns> </returns>
		public bool ConvertToBoolean(object value, bool nullValue)
		{
			int valInteger;

			////get null value
			value = GetIsNull(value, nullValue);

			////try convert to integer
			bool s = int.TryParse(value.ToString(), out valInteger);

			////jika convert to integer suxes
			if (s) ////convert to integer suxes
			{
				////set return value
				s = (valInteger > 0);
			}
			else ////convert to integer gagal
			{
				////try convert to boolean
				bool.TryParse(value.ToString(), out s);
			}
			return s;
		}


		/// <summary>
		///   Converts to boolean.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <returns> </returns>
		public bool ConvertToBoolean(object value)
		{
			return ConvertToBoolean(value, false);
		}

		#endregion

		#region method convert To Short

		/// <summary>
		///   Converts to short.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <returns> </returns>
		public short ConvertToShort(object value)
		{
			return ConvertToShort(value, 0);
		}

		/// <summary>
		///   Converts to short.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <param name="nullValue"> The null value. </param>
		/// <returns> </returns>
		public short ConvertToShort(object value, byte nullValue)
		{
			short s;

			////get null value
			value = GetIsNull(value, nullValue);

			////try convert to integer
			short.TryParse(value.ToString(), out s);

			return s;
		}

		#endregion

		#region method ConvertToDecimal

		/// <summary>
		///   Converts to decimal.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <returns> </returns>
		public decimal ConvertToDecimal(object value)
		{
			return ConvertToDecimal(value, 0);
		}

		/// <summary>
		///   Converts to decimal.
		/// </summary>
		/// <param name="value"> The value. </param>
		/// <param name="nullValue"> The null value. </param>
		/// <returns> </returns>
		public decimal ConvertToDecimal(object value, decimal nullValue)
		{
			decimal s;

			////get null value
			value = GetIsNull(value, nullValue);

			////try convert to decimal
			decimal.TryParse(value.ToString(), out s);

			return s;
		}

		#endregion

		#region method getSqlString

		/// <summary>
		///   Converts to query value. ex: if databaseType = VarChar: abc -> 'abc',
		/// </summary>
		/// <param name="inputValue"> The input value. </param>
		/// <param name="databaseType"> Type of the database. only accept; VarChar, Int, DateTime </param>
		/// <param name="formatString"> The format string. </param>
		/// <returns> </returns>
		public string ConvertToQueryValue(object inputValue, SqlDbType databaseType, string formatString)
		{
			////initial value
			string value = ConvertToString(inputValue);

			////cek type
			switch (databaseType)
			{
				case SqlDbType.VarChar: ////jika type = text
					{
						////////'abc ---> ''abc
						////replace (') menjadi ('')
						value = value.Replace(SingleQuoteString, SingleQuoteSqlString);

						////''abc ----> '''abc'
						return string.Format("'{0}'", value);
					}
				case SqlDbType.Int: ////jika type = int
					{
						////parse menjadi double
						double doubleResult;

						bool isValid = double.TryParse(value, out doubleResult);

						////jika bukan number
						if (!isValid) ////bukan number
						{
							////create error
							value = string.Format("({0}) is not valid number", value);

							throw (new Exception(value));
						}

						break;
					}
				case SqlDbType.DateTime: ////jika type = datetime
					{
						DateTime dateTimeResult;

						////parse menjadi datetime
						bool isValid = DateTime.TryParse(value, out dateTimeResult);

						////jika bukan date
						if (!isValid) ////bukan date
						{
							////create error
							value = string.Format("({0}) is not valid date", value);

							throw (new Exception(value));
						}

						if (formatString != null)
							value = string.Format("'{0}'", dateTimeResult.ToString(formatString));

						break;
					}
			}

			return value;
		}

		#endregion

		#region method getDateTimeString

		/// <summary>
		///   Gets the date time string.
		/// </summary>
		/// <param name="dateValue"> The date value. </param>
		/// <param name="formatString"> The format string. </param>
		/// <returns> </returns>
		public string GetDateTimeString(DateTime dateValue, string formatString)
		{
			////initial value
			string s = dateValue.ToString(formatString);

			return s;
		}

		#endregion
	}
}