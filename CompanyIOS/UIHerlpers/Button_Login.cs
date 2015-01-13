using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace CompanyIOS
{
	partial class Button_Login : UIButton
	{
		public Button_Login (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		public override void AwakeFromNib ()
		{
			Initialize ();
		}

		void Initialize ()
		{
			Layer.CornerRadius = 3f;
			BackgroundColor = UIColor.FromRGB(255, 110 ,23);
		}
	}
}
