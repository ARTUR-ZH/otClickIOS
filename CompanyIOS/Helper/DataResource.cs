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
		[JsonProperty("data")]
		public InnerDataResourceHelperForComments Data {
			get;
			set;
		}

	}


}

