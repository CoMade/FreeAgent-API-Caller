using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeAgentDataDownloader.Model
{
	public class FaTransaction
	{
		public int ID { get; set; }
		public string NominalCode { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
		public DateTime Date { get; set; }
	}
}
