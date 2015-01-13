using System;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace CompanyIOS
{
	public class InnerDataResourceHelperForComments
	{
		[JsonProperty("servicecost")]
		public nint ServiceCost {
			get;
			set;
		}
		[JsonProperty("servicequality")]
		public nint ServiceQuality {
			get;
			set;
		}
		[JsonProperty("service")]
		public nint Service {
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
	}



}

