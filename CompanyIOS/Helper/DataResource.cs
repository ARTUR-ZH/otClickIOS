using System;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace CompanyIOS
{
	public class DataResource
	{
		[JsonProperty("promocode")]
		public string Promocode {
			get;
			set;
		}
		[JsonProperty("datefrom")]
		public string Dateform {
			get;
			set;
		}

		[JsonProperty("comment")]
		public string Comment {
			get;
			set;
		}
		[JsonProperty("phone")]
		public string Phone {
			get;
			set;
		}

		[JsonProperty("data")]
		public List<string> Data {
			get;
			set;
		}

	}


}

