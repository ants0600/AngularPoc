namespace Domain.Model.Response
{
	/// <summary>
	/// Domain class for external API.
	/// Ex: to serialize from json string.
	/// </summary>
	public class OpenWeatherWind
	{
		public virtual double speed { get; set; }
		public virtual double deg { get; set; }

	}
}