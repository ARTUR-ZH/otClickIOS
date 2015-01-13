using System;

namespace CompanyIOS
{
	public static class NumericExtensions
	{
		public static nfloat ToRadians (this nfloat angle)
		{
			return (((nfloat)Math.PI / 180) * angle);
		}
	}
}

