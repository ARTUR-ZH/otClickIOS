using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CompanyIOS
{
	public class PointsData
	{
		[JsonProperty]
		public string TypeOfGraphic {
			get;
			set;
		}

		[JsonProperty]
		public Dictionary<string,nfloat> Points {
			get;
			set;
		}
	}

}

