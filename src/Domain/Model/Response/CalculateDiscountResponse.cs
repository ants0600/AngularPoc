using Domain.Model.Request;

namespace Domain.Model.Response
{
	public class CalculateDiscountResponse
	{
		public CalculateDiscountResponse(CalculateDiscountRequest request, Car carData, double afterDiscount, ErrorEnum error,
			string errorMessage)
		{
			this.Request = request;
			this.CarData = carData;
			this.AfterDiscount = afterDiscount;
			this.Error = error;
			this.ErrorMessage = errorMessage;
		}

		/// <summary>
		///     auto calculate
		/// </summary>
		public double Discount
		{
			get
			{
				var carData = this.CarData;
				if (carData == null)
				{
					return 0;
				}

				var totalPrice = carData.Price * this.Request.Quantity;
				return totalPrice - this.AfterDiscount;
			}
		}

		public CalculateDiscountRequest Request { get; }
		public Car CarData { get; }
		public double AfterDiscount { get; }
		public ErrorEnum Error { get; }
		public string ErrorMessage { get; }
	}
}