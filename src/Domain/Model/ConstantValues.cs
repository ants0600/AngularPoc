namespace Domain.Model
{
	public class ConstantValues
	{
		public const string QUERY_INSERT_CART_ITEM = @"
EXEC dbo.usp_InsertCart @userName = @userName, @productId = @productId,
    @quantity = @quantity


";
		public const double FARENHEIT_TO_CELCIUS = 0.556;
		public const string ROWS_XML_FORMAT = "<rows>{0}</rows>";

		public const string ID_ROW_XML_FORMAT = "<row><Id>{0}</Id></row>";

		/// <summary>
		/// 
		/// </summary>
		public const string QUERY_DELETE_CARS_BY_IDS = @"
EXEC dbo.usp_DeleteCars @xml = @xml
";

		public const string QUERY_INSERT_CONTACT_US = @"
EXEC dbo.usp_InsertContactUs @FullName = @FullName, @Email = @Email,
    @message = @message
";

		public const string QUERY_GET_CAR_BY_ID = @"
SELECT TOP 1
        Id ,
        Name ,
        Year ,
        Price
FROM    dbo.Cars WITH ( NOLOCK )
WHERE   Id = @id

";
		public const int PERCENT = 100;

		/// <summary>
		///     invoke native TSQL
		/// </summary>
		public const string QUERY_GET_CAR_BY_NAME = @"
SELECT TOP 1
        Id ,
        Name ,
        Year ,
        Price
FROM    dbo.Cars WITH ( NOLOCK )
WHERE   Name = @name
";

		/// <summary>
		///     invoke stored procedure
		/// </summary>
		public const string QUERY_UPDATE_CAR_BY_ID = @"
EXEC dbo.usp_UpdateCar @id = @id, @name = @name, @year = @year,
    @price = @price

";

		/// <summary>
		///     invoke stored procedure
		/// </summary>
		public const string QUERY_INSERT_CAR = @"
EXEC dbo.usp_InsertCar @name = @name, @year = @year, @price = @price

";

		/// <summary>
		///     invoke native TSQL
		/// </summary>
		public const string QUERY_SELECT_CARS = @"
SELECT  Id ,
        Name ,
        Year ,
        Price
FROM    dbo.Cars WITH ( NOLOCK )
ORDER BY Id DESC
";

		public const string QUERY_DELAY_TEST = @"
WAITFOR DELAY '00:00:03'

SELECT TOP 1
        *
FROM    dbo.Cars
";

		public const string QUERY_SELECT_CARS_BY_PAGE_INDEX = @"
EXEC dbo.usp_SelectCars @pageIndex = @pageIndex, @pageSize = @pageSize,
    @orderBy = @orderBy, @isAscending = @isAscending

";

		public const string QUERY_GET_CARS_COUNT = @"
SELECT  COUNT(1)
FROM    dbo.Cars WITH ( NOLOCK )

";

		public const string QUERY_GET_COUNTRIES = @"
SELECT  Id ,
        Name
FROM    dbo.Countries
ORDER BY Name

";

		public const string QUERY_GET_CITIES_BY_COUNTRY_ID = @"
SELECT  Id ,
        Name ,
        CountryId
FROM    dbo.Cities
WHERE   CountryId = @id
ORDER BY Name ,
        Id

";

		public const string QUERY_GET_CITIES_BY_COUNTRY_NAME = @"
EXEC dbo.usp_getCitiesByCountryName @name = @name

";

		public const string ERROR_MESSAGE_FORMAT = @"
EXCEPTION: {0}

MESSAGE: {1}

STACK TRACE: {2} 
";

		public const string FORMAT_DD_MMM_YYYY_HH_MM_SS = "dd MMM yyyy HH:mm:ss";
		public const string QUERY_SELECT_PRODUCTS = @"
SELECT  Id ,
        Code ,
        Name ,
        Price ,
        UnitId ,
        MobileCardImageUrl ,
        UnitName
FROM    dbo.ProductView
";

		public const string QUERY_GET_PRODUCT_BY_ID = @"
SELECT TOP 1
        Id ,
        Code ,
        Name ,
        Price ,
        UnitId ,
        MobileCardImageUrl ,
        UnitName
FROM    dbo.ProductView WITH ( NOLOCK )
WHERE   Id = @id
";

		public const string COMMA = ", ";

		public const string QUERY_GET_CART_DETAILS_BY_USER_NAME = @"
SELECT  Id ,
        ProductId ,
        CartId ,
        UserName ,
        Quantity ,
        Code ,
        Name ,
        Price ,
        UnitId ,
        UnitName ,
        MobileCardImageUrl ,
        TotalPrice
FROM    dbo.CartDetailView
WHERE   UserName = @id

";

		public const string QUERY_GET_CART_TOTAL_PRICE_BY_USER_NAME = @"
SELECT  SUM(a.TotalPrice)
FROM    CartDetailView a WITH ( NOLOCK )
WHERE   a.UserName = @id


";
	}
}