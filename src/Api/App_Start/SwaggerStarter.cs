using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using Infrastructure.Common;
using Microsoft.Web.Http;
using Microsoft.Web.Http.Routing;
using Swashbuckle.Application;

namespace RestfulApi
{
	/// <summary>
	///     the starting point for all swagger configurations
	/// </summary>
	public class SwaggerStarter
	{
		/// <summary>
		///     activate swagger ui functions here
		/// </summary>
		public static void Start()
		{
			// we only need to change the default constraint resolver for services that want urls with versioning like: ~/v{version}/{controller}
			var constraintResolver =
				new DefaultInlineConstraintResolver
				{
					ConstraintMap = { ["apiVersion"] = typeof(ApiVersionRouteConstraint) }
				};
			//var configuration = new HttpConfiguration();


			//directly set up the global configuration
			var configuration = GlobalConfiguration.Configuration;

			// reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
			configuration.AddApiVersioning(o =>
			{
				o.ReportApiVersions = true;
				o.AssumeDefaultVersionWhenUnspecified = true;
				o.DefaultApiVersion = new ApiVersion(1, 0);
			});
			configuration.MapHttpAttributeRoutes(constraintResolver);
			configuration.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());

			var apiExplorer = configuration.AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV");

			configuration.EnableSwagger(
					"{apiVersion}/swagger",
					swagger =>
					{
						// build a swagger document and endpoint for each discovered API version
						swagger.MultipleApiVersions(
							(apiDescription, version) => apiDescription.GetGroupName() == version,
							info =>
							{
								foreach (var group in apiExplorer.ApiDescriptions)
								{
									info.Version(group.Name, $"Sample Restful API {group.ApiVersion}");
								}
							});

						// add a custom operation filter which sets default values
						swagger.OperationFilter<SwaggerDefaultValues>();

						// integrate xml comments
						swagger.IncludeXmlComments(SwaggerStartup.XmlCommentsFilePath);
						swagger.DescribeAllEnumsAsStrings();
						swagger.DocumentFilter<RemoveVerbsFilter>();
					})
				.EnableSwaggerUi(swagger => swagger.EnableDiscoveryUrlSelector());
		}
	}
}