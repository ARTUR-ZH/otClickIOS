using System;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace CompanyIOS
{
	public class HttpServiceConn
	{

		public async Task<Tuple<string,ServiceCallbackHelper>> WebRequestServiceAsync (string md5string, string Login, string Password)
		{
			ServiceCallbackHelper data;
			try {
				LoginHelper jsonHelper = new LoginHelper ();
				jsonHelper.Login = Login;
				jsonHelper.Password = Password;
				string jsonSerial = JsonConvert.SerializeObject (jsonHelper);
				byte[] postBytes = Encoding.UTF8.GetBytes (jsonSerial);
				var request = (HttpWebRequest)WebRequest.Create (string.Format (@"http://188.225.32.223/applications/webservice/index.php?r=TerminalApi/authTerminalDevice"));
				request.ContentType = "application/json; charset=utf-8";
				request.Method = "POST";
				request.Accept = "application/json";
				request.ContentLength = postBytes.Length;
				using (Stream newStream = await request.GetRequestStreamAsync ()) {
					newStream.Write (postBytes, 0, postBytes.Length);
					newStream.Flush ();
					newStream.Close ();
				}
				var task = request.GetResponseAsync ();
				var resp = (HttpWebResponse)await task;
				if (resp.StatusCode != HttpStatusCode.OK) {
					return Tuple.Create ((string.Format (@"Error fetching data. Server returned status code: {0}", resp.StatusCode)), new ServiceCallbackHelper (){ Status = "error" });
				}
				using (StreamReader sr = new StreamReader (resp.GetResponseStream ())) {		
					string text = sr.ReadToEnd ();
					nint index = text.IndexOf ("}}");
					if (index > 0)
						text = text.Substring (0, (int)index + 2);

					data = JsonConvert.DeserializeObject<ServiceCallbackHelper> (text);
				}
				if (data == null) {
					return Tuple.Create ("Error", new ServiceCallbackHelper (){ Status = "error" });
				}
				return Tuple.Create ("", data);
			} catch (Exception ex) {
				return Tuple.Create ((ex.Message), new ServiceCallbackHelper (){ Status = "error" });
			}
		}

		public async Task<Tuple<string,GraphicsCallback>> GetGraphicAsync (string token, string datetime, string dateof)
		{
			GraphicsCallback graphics;
			try {
				HttpWebRequest request;
				TokenValidation newjson = new TokenValidation ();
				newjson.Token = token;
				newjson.DateTime = datetime;
				newjson.Objid = GraphicsController.Company.ToString();
				string jsonser = JsonConvert.SerializeObject (newjson);
				byte[] jsonencode = Encoding.UTF8.GetBytes (jsonser);
				switch (dateof) {
				case "Месяц":
					request = (HttpWebRequest)WebRequest.Create (string.Format (@"http://188.225.32.223/applications/webservice/index.php?r=TerminalApi/getByMonth2"));
					break;
				case "Квартал":
					request = (HttpWebRequest)WebRequest.Create (string.Format (@"http://188.225.32.223/applications/webservice/index.php?r=TerminalApi/getByQuarter2"));
					break;
				case "Полугодие":
					request = (HttpWebRequest)WebRequest.Create (string.Format (@"http://188.225.32.223/applications/webservice/index.php?r=TerminalApi/getByHalfyear2"));
					break;
				case "Год":
					request = (HttpWebRequest)WebRequest.Create (string.Format (@"http://188.225.32.223/applications/webservice/index.php?r=TerminalApi/getByYear2"));
					break;
				default:
					return null;
				}
				request.ContentType = "application/json; charset=utf-8";
				request.Method = "POST";
				request.Accept = "application/json";
				request.ContentLength = jsonencode.Length;
				using (Stream newStream = await request.GetRequestStreamAsync ()) {
					newStream.Write (jsonencode, 0, jsonencode.Length);
					newStream.Flush ();
					newStream.Close ();
				}
				var task = request.GetResponseAsync ();
				var resp = (HttpWebResponse)await task;
				if (resp.StatusCode != HttpStatusCode.OK) {
					return Tuple.Create ((string.Format (@"Error fetching data. Server returned status code: {0}", resp.StatusCode)), new GraphicsCallback (){ Status = "error" });
				}
				using (StreamReader stream = new StreamReader (resp.GetResponseStream ())) {
					Task<string> reader = stream.ReadToEndAsync ();
					string text = (string)await reader;
					nint index = text.IndexOf ("}}");
					if (index > 0)
						text = text.Substring (0, (int)index + 3);

					graphics = JsonConvert.DeserializeObject<GraphicsCallback> (text);
				}
				if (graphics.Resource == null) {
					return Tuple.Create ("Error", new GraphicsCallback (){ Status = "error" });
				}
				return Tuple.Create ("", graphics);
			} catch (Exception ex) {
				return Tuple.Create (ex.Message, new GraphicsCallback (){ Status = "error" }); 
			}
		}

		public async Task<Tuple<string,NewsHelper>> GetNews ()
		{
			NewsHelper news;
			try {
				var request = (HttpWebRequest)WebRequest.Create (string.Format (@"http://188.225.32.223/applications/webservice/index.php?r=TerminalApi/getNews2"));
				request.ContentType = "application/json; charset=utf-8";
				request.Method = "POST";
				request.Accept = "application/json";

				using (Stream newStream = await request.GetRequestStreamAsync ()) {
				}				var task = request.GetResponseAsync ();
				
				var resp = (HttpWebResponse)await task;
				if (resp.StatusCode != HttpStatusCode.OK) {
					return Tuple.Create ((string.Format (@"Error fetching data. Server returned status code: {0}", resp.StatusCode)), new NewsHelper (){ Status = "error" });
				}
				using (StreamReader stream = new StreamReader (resp.GetResponseStream ())) {
					Task<string> reader = stream.ReadToEndAsync ();
					string text = (string)await reader;
					nint index = text.IndexOf ("}}");
					if (index > 0)
						text = text.Substring (0, (int)index + 3);

					news = JsonConvert.DeserializeObject<NewsHelper> (text);
				}
			
				if (news == null) {
					return Tuple.Create ("Error", new NewsHelper (){ Status = "error" });
				}
				return Tuple.Create ("", news);
			} catch (Exception ex) {
				return Tuple.Create (ex.Message, new NewsHelper (){ Status = "error" }); 
			}

		}
		public async Task<Tuple<string,CompaniesResource>> GetCompanies (string token)
		{
			CompaniesResource rate;
			try {
				TokenValidation tokenVald = new TokenValidation ();
				tokenVald.Token = token;
				string createJson = JsonConvert.SerializeObject (tokenVald);
				byte[] jsonByte = Encoding.UTF8.GetBytes (createJson);
				var request = (HttpWebRequest)WebRequest.Create (string.Format (@"http://188.225.32.223/applications/webservice/index.php?r=TerminalApi/getOrganizations"));
				request.ContentType = "application/json; charset=utf-8";
				request.Method = "POST";
				request.Accept = "application/json";
				request.ContentLength = jsonByte.Length;
				using (Stream newStream = await request.GetRequestStreamAsync ()) {
					newStream.Write (jsonByte, 0, jsonByte.Length);
					newStream.Flush ();
					newStream.Close ();
				}
				var task = request.GetResponseAsync ();

				var resp = (HttpWebResponse)await task;
				if (resp.StatusCode != HttpStatusCode.OK) {
					return Tuple.Create ((string.Format (@"Error fetching data. Server returned status code: {0}", resp.StatusCode)), new CompaniesResource (){ Status = "error" });
				}
				using (StreamReader stream = new StreamReader (resp.GetResponseStream ())) {
					Task<string> reader = stream.ReadToEndAsync ();
					string text = (string)await reader;
					text = text.Replace("},{",",");
					text = text.Replace(']',' ');
					text = text.Replace('[',' ');

					rate =  JsonConvert.DeserializeObject<CompaniesResource> (text,new NintConverter());
				}

				if (rate == null) {
					return Tuple.Create ("Error", new CompaniesResource (){ Status = "error" });
				}
				return Tuple.Create ("", rate);
			} catch (Exception ex) {
				return Tuple.Create (ex.Message, new CompaniesResource (){ Status = "error" }); 
			}

		}
		public async Task<Tuple<string,CommentsHelper>> GetComments(string token, string ondate)
		{
			CommentsHelper dataString;
			try {
				TokenValidation newToken = new TokenValidation ();
				newToken.Token = token;
				newToken.DateTime = ondate;
				newToken.Objid = GraphicsController.Company.ToString();
				string tokenJson = JsonConvert.SerializeObject (newToken);
				byte[] tokenByte = Encoding.UTF8.GetBytes (tokenJson);
				var request = (HttpWebRequest)WebRequest.Create (string.Format (@"http://188.225.32.223/applications/webservice/index.php?r=TerminalApi/getComments2"));
				request.ContentType = "application/json; charset=utf-8";
				request.Method = "POST";
				request.Accept = "application/json";
				request.ContentLength = tokenByte.Length;
				using (Stream stream = await request.GetRequestStreamAsync()) {
					stream.Write (tokenByte, 0, tokenByte.Length);
					stream.Flush ();
					stream.Close ();
				}
				Task<WebResponse> task = request.GetResponseAsync ();
				var response = (HttpWebResponse)await task;
				if (response.StatusCode != HttpStatusCode.OK) {
					return Tuple.Create ((string.Format (@"Error fetching data. Server returned status code: {0}", response.StatusCode)), new CommentsHelper (){ Status = "error" });
				}
				using (StreamReader reader = new StreamReader(response.GetResponseStream ())) {
					Task<string> textReader = reader.ReadToEndAsync ();
					string textJson = (string)await textReader;
					dataString = JsonConvert.DeserializeObject<CommentsHelper> (textJson, new DictionStringConverter());

				}
				if (dataString == null) {
					return Tuple.Create ("Error", new CommentsHelper (){ Status = "error" });
				}
				return Tuple.Create ("",dataString);
			} catch (Exception ex) {
				return Tuple.Create (ex.Message, new CommentsHelper (){ Status = "error" });
			}

		}

		public async Task<Tuple<string,QuestionHelper>> GetQuestions (string token,nint compId)
		{
			QuestionHelper QR;
			try {
				TokenValidation tokenVald = new TokenValidation ();
				tokenVald.Token = token;
				tokenVald.Objid = compId.ToString();
				string createJson = JsonConvert.SerializeObject (tokenVald);
				byte[] jsonByte = Encoding.UTF8.GetBytes (createJson);
				var request = (HttpWebRequest)WebRequest.Create (string.Format (@"http://www.otclick.com/applications/webservice/?r=TerminalApi/getQuestions"));
				request.ContentType = "application/json; charset=utf-8";
				request.Method = "POST";
				request.Accept = "application/json";
				request.ContentLength = jsonByte.Length;
				using (Stream newStream = await request.GetRequestStreamAsync ()) {
					newStream.Write (jsonByte, 0, jsonByte.Length);
					newStream.Flush ();
					newStream.Close ();
				}
				var task = request.GetResponseAsync ();

				var resp = (HttpWebResponse)await task;
				if (resp.StatusCode != HttpStatusCode.OK) {
					return Tuple.Create ((string.Format (@"Error fetching data. Server returned status code: {0}", resp.StatusCode)), new QuestionHelper (){ Status = "error" });
				}
				using (StreamReader stream = new StreamReader (resp.GetResponseStream ())) {
					Task<string> reader = stream.ReadToEndAsync ();
					string text = (string)await reader;


					QR = JsonConvert.DeserializeObject<QuestionHelper> (text);
				}

				if (QR == null) {
					return Tuple.Create ("Error", new QuestionHelper (){ Status = "error" });
				}
				return Tuple.Create ("", QR);
			} catch (Exception ex) {
				return Tuple.Create (ex.Message, new QuestionHelper (){ Status = "error" }); 
			}
		}
//		public async Task<Tuple<string,QuestionHelper>> GetNews (string token,nint compId)
//		{
//			QuestionHelper QR;
//			try {
//				var request = (HttpWebRequest)WebRequest.Create (string.Format (@"http://www.otclick.com/applications/webservice/?r=TerminalApi/getQuestions"));
//				request.ContentType = "application/json; charset=utf-8";
//				request.Method = "POST";
//				request.Accept = "application/json";
//				request.ContentLength = jsonByte.Length;
//				using (Stream newStream = await request.GetRequestStreamAsync ()) {
//					newStream.Write (jsonByte, 0, jsonByte.Length);
//					newStream.Flush ();
//					newStream.Close ();
//				}
//				var task = request.GetResponseAsync ();
//
//				var resp = (HttpWebResponse)await task;
//				if (resp.StatusCode != HttpStatusCode.OK) {
//					return Tuple.Create ((string.Format (@"Error fetching data. Server returned status code: {0}", resp.StatusCode)), new QuestionHelper (){ Status = "error" });
//				}
//				using (StreamReader stream = new StreamReader (resp.GetResponseStream ())) {
//					Task<string> reader = stream.ReadToEndAsync ();
//					string text = (string)await reader;
//
//
//					QR = JsonConvert.DeserializeObject<QuestionHelper> (text);
//				}
//
//				if (QR == null) {
//					return Tuple.Create ("Error", new QuestionHelper (){ Status = "error" });
//				}
//				return Tuple.Create ("", QR);
//			} catch (Exception ex) {
//				return Tuple.Create (ex.Message, new QuestionHelper (){ Status = "error" }); 
//			}
//
	}
}

