using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace FreeAgentDataDownloader.Model
{
	
	[JsonObject("invoice")]
	public class FaInvoice
	{
		[JsonProperty("id")]
		public int ID { get; set; }

		[JsonProperty("reference")]
		public string Reference { get; set; }

		[JsonProperty("project_id")]
		public int ProjectID { get; set; }

		[JsonProperty("dated_on")]
		public DateTime DatedOn { get; set; }

		[JsonProperty("net_value")]
		public double NetValue { get; set; }
	}
}
