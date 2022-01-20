using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Domain.Model;
using Domain.Repository;
using Domain.Service;
using Infrastructure.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace InfrastructureTester
{
	/// <summary>
	///     tester class for discount functions.
	///     Arrange, act, assert.
	///     <br></br>
	///     The data is mock up from JSON file.
	///     <br></br>
	///     As permutations are too many, I only put some of them.
	/// </summary>
	[TestClass]
	public class DiscountTester : BaseTester
	{
		private readonly IApplicationSettings _applicationSettings;
		private readonly ICarService _service;

		private readonly Assembly _domainAssembly = typeof(IRepository).Assembly;
		private readonly Assembly _infrastructureAssembly = typeof(BaseRepository).Assembly;

		public DiscountTester()
		{
			this._service = this._container.Resolve<ICarService>();
			this._applicationSettings = this._container.Resolve<IApplicationSettings>();
		}

		/// <summary>
		///     1. If total cost exceeds $100,000 apply 5% discount
		/// </summary>
		[TestMethod]
		public void BuyingExpensiveCar_MustGetDiscount()
		{
			//arrange
			const string CAR_NAME = "honda city 2005";
			const int QUANTITY = 1;

			//act, assert
			this.AssertDiscount(CAR_NAME, QUANTITY, 0);
		}

		private void AssertDiscount(string carName, int quantity, double priceLimit)
		{
			//act
			var carByName = this._service.GetCarByName(carName);
			var totalPrice = carByName.Price * quantity;
			var priceAfterDiscounts = this._service.GetPriceAfterDiscounts(carName, quantity);

			//assert
			Console.WriteLine(nameof(totalPrice));
			Console.WriteLine(totalPrice);
			Console.WriteLine(nameof(priceAfterDiscounts));
			Console.WriteLine(priceAfterDiscounts);

			if (priceLimit > 0)
			{
				Assert.IsTrue(priceLimit == priceAfterDiscounts);
			}

			Assert.IsTrue(totalPrice > priceAfterDiscounts);
		}

		/// <summary>
		///     2. If number of cars is more than 2, apply 3% discount
		/// </summary>
		[TestMethod]
		public void BuyingManyCars_MustGetDiscount()
		{
			//arrange
			const string CAR_NAME = "Karimun 2003";
			const int QUANTITY = 3;

			//act, assert
			this.AssertDiscount(CAR_NAME, QUANTITY, 0);
		}

		/// <summary>
		///     3. If cars year is before 2000, apply 10% discount
		/// </summary>
		[TestMethod]
		public void BuyingOldCar_MustGetDiscount()
		{
			//arrange
			const string CAR_NAME = "BMW 1999";
			const int QUANTITY = 1;
			this.AssertDiscount(CAR_NAME, QUANTITY, 0);
		}

		/// <summary>
		///     1. If total cost exceeds $100,000 apply 5% discount.
		///     2. If number of cars is more than 2, apply 3% discount.
		///     Ex: final price must be less than 92%
		/// </summary>
		[TestMethod]
		public void BuyingManyExpensiveCars_MustGetDiscount()
		{
			//arrange
			const string CAR_NAME = "honda city 2005";
			const int QUANTITY = 3;

			//act, assert
			var carByName = this._service.GetCarByName(CAR_NAME);
			var totalPrice = carByName.Price * QUANTITY;
			var costExceedDiscount = this._applicationSettings.GetCostExceedDiscount();
			var numberOfCarsDiscount = this._applicationSettings.GetNumberOfCarsDiscount();
			var totalDiscountPercent = costExceedDiscount + numberOfCarsDiscount;
			var priceLimit = totalPrice * (ConstantValues.PERCENT - totalDiscountPercent) / ConstantValues.PERCENT;

			this.AssertDiscount(CAR_NAME, QUANTITY, priceLimit);
		}

		[TestMethod]
		public void GetDomainTypes()
		{
			//interfaces
			var interfaces = _domainAssembly.GetTypes().Where(t => t.IsInterface).ToList();
			Console.WriteLine("Interfaces in domain");
			foreach (var t in interfaces)
			{
				Console.WriteLine(t.FullName);
			}

			//concrete classes
			var concreteClasses = _infrastructureAssembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract).ToList();
			Console.WriteLine();
			Console.WriteLine("Concrete classes in infrastructures");
			foreach (var t in concreteClasses)
			{
				Console.WriteLine(t.FullName + " " + t.ContainsGenericParameters);
			}

		}

	}
}