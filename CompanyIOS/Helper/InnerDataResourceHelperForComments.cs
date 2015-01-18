using System;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace CompanyIOS
{
	public class InnerDataResourceHelperForComments
	{
		[JsonProperty("servicecost")]
		public int ServiceCost {
			get;
			set;
		}
		[JsonProperty("servicequality")]
		public int ServiceQuality {
			get;
			set;
		}
		[JsonProperty("service")]
		public int Service {
			get;
			set;
		}
	}
}

