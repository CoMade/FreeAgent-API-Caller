﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace FreeAgentDataDownloader.Model
{
	
	[JsonObject("invoices")]
	public class FaInvoiceCollection : List<FaInvoice>
	{
		
	}
}
