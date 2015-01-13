using System;
using UIKit;
using System.Drawing;
using Foundation;

namespace CompanyIOS
{
	public class ClickedButtonAtIndex : UIAlertViewDelegate , IDisposable
	{
		protected string _number;

		public ClickedButtonAtIndex (string number)
		{
			_number = number;
		}

		public override void Clicked (UIAlertView alertview, nint buttonIndex)
		{
			if (buttonIndex == 1) {
				NSUrl url = new NSUrl (string.Format ("tel:{0}", _number));
				if (!UIApplication.SharedApplication.OpenUrl (url)) {
					UIAlertView av = new UIAlertView ("Ошибка",
						                 "Is not supported on this device",
						                 null,
						                 "ОК",
						                 null);
					av.Show ();
				}
					
			}
		}
		protected override void Dispose (bool disposing)
		{
			base.Dispose (true);
		}
		public new void Dispose()
		{
			Dispose (true);
		}

	}
	
}

