using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using System.Drawing;


namespace CompanyIOS
{
	partial class LoginPage_TextFields : UITextField
	{
		public LoginPage_TextFields (IntPtr handle) : base (handle)
		{
			Layer.CornerRadius = 3f;
			LeftViewMode = UITextFieldViewMode.Always;
			ShouldReturn = x => { 
				x.ResignFirstResponder ();
				return true;
			};
		}
	}
}
