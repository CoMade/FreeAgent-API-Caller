using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FreeAgentDataDownloader;
using FreeAgentDataDownloader.Model;
using FreeAgentDataDownloader.OAuth;


namespace TestFreeAgentApiCaller
{
	class Program
	{
		static void Main(string[] args)
		{
			if(args.Length == 0)
			{
				args = new string[2];
				args[0] = "2014-01-01";
				args[1] = "2014-03-01";
			}

			var controller = new Controller();
			controller.Go(args);
		}

		public class Controller
		{
			private string _dateFormat = "yyyy-MM-dd";

			private DateTime _from;
			private DateTime _to;

			public void Go(string[] args)
			{
				LoadParameters(args);

				LoadFaData<List<FaInvoice>>("invoices?", collection => collection.Where(x => x.DatedOn >= _from && x.DatedOn <= _to).ToList());
				LoadFaData<List<FaProject>>("projects?", null);
			}

			private void LoadFaData<T>(string url, Func<T, T> resultModifier) where T : IList, new()
			{
				Console.Write("Downloading {0} from FA ... ", typeof(T).Name);
				var objects = OAuthFreeAgentAccess.GetData<T>(url, resultModifier);

				var sb = new StringBuilder();

				foreach (var obj in objects)
				{
					sb.Append(obj.ToString());
				}

				Console.WriteLine(sb);

				Console.WriteLine("Done.");
			}

			public void LoadParameters(string[] args)
			{
				Console.Write("Checking parameters... ");

				if (args.Length != 2)
					throw new ApplicationException("Must supply two arguments, from date & to date, where dates are in the format yyyy-mm-dd");

				var provider = CultureInfo.InvariantCulture;
				DateTime fromDate;
				DateTime toDate;
				try
				{
					fromDate = DateTime.ParseExact(args[0], _dateFormat, provider);
					toDate = DateTime.ParseExact(args[1], _dateFormat, provider);
				}
				catch (FormatException)
				{
					throw new ApplicationException("From & To dates must be in the format yyyy-mm-dd");
				}

				if (toDate < fromDate)
					throw new ApplicationException("To date must be after From date");

				_from = fromDate;
				_to = toDate;

				Console.WriteLine("Done.");
			}
		}

		
	}
}
