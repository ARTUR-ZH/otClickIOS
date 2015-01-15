using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CompanyIOS
{
	public class QuestionData
	{
		[JsonProperty("ID")]
		public string Id {
			get;
			set;
		}
		[JsonProperty("QuestionName")]
		public string QName {
			get;
			set;
		}
	}

}

