using System;
using System.Security.Cryptography;
using System.Text;

namespace CompanyIOS
{
	public class md5securitycobject
	{
		public static string ComputeHash(string   plainText)
		{
			MD5 md5 = System.Security.Cryptography.MD5.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(plainText);
			byte[] hash = md5.ComputeHash(inputBytes);

			// step 2, convert byte array to hex string
			StringBuilder sb = new StringBuilder();
			for (nint i = 0; i < hash.Length; i++)
			{
				sb.Append(hash[i].ToString("X2"));
			}
			return sb.ToString();
		}
	}
}

