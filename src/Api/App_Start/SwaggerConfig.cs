using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace RestfulApi
{
	/// <summary>
	/// 
	/// </summary>
	public class SwaggerConfig : ConfigurationSection
	{
		[ConfigurationProperty("", IsDefaultCollection = true, IsKey = false, IsRequired = true)]
		public SwaggerCollection Swagger
		{
			get { return base[""] as SwaggerCollection; }
			set { base[""] = value; }
		}
	}

	public class SwaggerCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new SwaggerSettings();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SwaggerSettings)element).FileName;
		}

		protected override string ElementName
		{
			get { return "swagger"; }
		}

		protected override bool IsElementName(string elementName)
		{
			return !String.IsNullOrEmpty(elementName) && elementName == "swagger";
		}

		public override ConfigurationElementCollectionType CollectionType
		{
			get { return ConfigurationElementCollectionType.BasicMap; }
		}
	}

	public class SwaggerSettings : ConfigurationElement
	{
		[ConfigurationProperty("fileName", DefaultValue = "None", IsRequired = true)]
		public String FileName
		{
			get { return (String)this["fileName"]; }
			set { this["fileName"] = value; }
		}
	}

}