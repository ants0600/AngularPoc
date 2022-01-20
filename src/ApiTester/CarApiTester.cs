using System;
using Domain.Model.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestfulApiTester;

namespace ApiTester
{
	/// <summary>
	///     Not all API methods are tested, this is just a sample.
	///     Imagine we are testing API running in DEV or STAGING environments.
	/// </summary>
	[TestClass]
	public class CarApiTester : BaseApiTester
	{
		private const string CALCULATE_DISCOUNT = "/CalculateDiscount?api-version=1.0";

		/// <summary>
		///     ex: http://localhost:8624/CalculateDiscount?api-version=1.0
		/// </summary>
		[TestMethod]
		public void DiscountApi_MustReturnValue()
		{
			var request = new CalculateDiscountRequest
			{
				Quantity = 1,
				CarName = "BMW 1999"
			};
			var body = JsonConvert.SerializeObject(request, Formatting.Indented);

			var url = base._apiUrl + CALCULATE_DISCOUNT;
			var downloadString = PostUrl(url, body);
			Console.WriteLine(downloadString);
		}
	}
}