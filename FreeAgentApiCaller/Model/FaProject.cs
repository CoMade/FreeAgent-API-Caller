using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace FreeAgentDataDownloader.Model
{
	
	[JsonObject("project")]
	public class FaProject
	{
		[JsonProperty("id")]
		public int ID { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
