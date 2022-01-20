namespace Domain.Model.Response
{
	public class UpdateCarResponse
	{
		public UpdateCarResponse(Car updated, bool status, ErrorEnum error, string errorMessage)
		{
			this.Updated = updated;
			this.Status = status;
			this.ErrorMessage = errorMessage;
			this.Error = error;
		}

		public ErrorEnum Error { get; }
		public Car Updated { get; }
		public bool Status { get; }
		public string ErrorMessage { get; }
	}
}