namespace Domain.Model.Request
{
	public class AddCartItemRequest
	{
		public string UserName { get; set; }

		public long ProductId { get; set; }

		public int Quantity { get; set; }
	}
}