namespace Domain.Model
{
	/// <summary>
	///     all kinds of exceptions, validation errors
	/// </summary>
	public enum ErrorEnum
	{
		None = 0,
		CarNameNotFound = 1,
		DuplicateName = 2,
		CarIdNotFound = 3,
		CarNameIsEmpty = 4,
		CarYearIsInvalid = 5,
		CarPriceIsInvalid = 6,
		UrlNotReachable = 7,
		CityNameIsEmpty = 8,
		CityNotFound = 9
	}
}