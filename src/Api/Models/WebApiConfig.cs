using System.Web.Http;

namespace RestfulApi.Models
{
    /// <summary>
    ///     todo: CANT place in separate folders
    /// </summary>
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            ////////// Web API routes
            ////////config.MapHttpAttributeRoutes();

            var routes = config.Routes;

            routes.MapHttpRoute(
                "ActionApi",
                "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }
            );

            routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );
        }
    }
}