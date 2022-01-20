using System;
using Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestfulApiTester;

namespace ApiTester
{
	[TestClass]
	public class ContactApiTester : BaseApiTester
	{
		private const string ADD_CONTACT_US = "/AddContactUs?api-version=1.0";

		[TestMethod]
		public void AddContactUsApi_MustReturnValue()
		{
			var url = this._apiUrl + ADD_CONTACT_US;
			var newGuid = Guid.NewGuid();

			const string FORMAT = "{0}{1}";
			string fullName = string.Format(FORMAT, nameof(fullName), newGuid);
			string email = string.Format(FORMAT, nameof(email), newGuid);
			string message = string.Format(FORMAT, nameof(message), newGuid);
			var inserted = new ContactUsItem()
			{
				FullName = fullName,
				Email = email,
				Message = message
			};
			var body = JsonConvert.SerializeObject(inserted);
			var downloadString = PostUrl(url, body);
			Console.WriteLine(downloadString);
			Assert.IsTrue(bool.Parse(downloadString));
		}
	}
}