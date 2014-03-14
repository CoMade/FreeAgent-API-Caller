using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FreeAgentDataDownloader.Model
{
	[JsonObject("company")]
	class FaCompany
	{
		[JsonProperty("name")]
		public string Name {get;set;}

		[JsonProperty("sales_tax_registration_number")]
		public string TaxNumber {get;set;}
		
	}
}
