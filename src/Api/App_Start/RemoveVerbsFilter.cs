using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace RestfulApi
{
	/// <summary>
	/// 
	/// </summary>
	public class RemoveVerbsFilter : IDocumentFilter
	{
		public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
		{
			updateJson(swaggerDoc);
		}

		[Conditional("DEBUG")]
		public static void updateJson(SwaggerDocument swaggerDoc)
		{
			var swaggerVersion = swaggerDoc.info.version;
			var json = JsonHelper.SerializeToMinimalJson(swaggerDoc);
			string FilePath;
			FilePath = HttpContext.Current.Server.MapPath("/swagger" + swaggerVersion + ".json");
			if (!File.Exists(FilePath) || string.Compare(File.ReadAllText(FilePath), json, true) != 0)
			{
				File.WriteAllText(FilePath, string.Empty);
				File.WriteAllText(FilePath, json);
			}
		}
	}
}