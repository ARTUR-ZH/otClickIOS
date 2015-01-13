// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace CompanyIOS
{
	[Register ("LoginController")]
	partial class LoginController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Button_Login btnLogin { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel loginLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView LogoView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		LoginPage_TextFields txtLogin { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		LoginPage_TextFields txtPassword { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnLogin != null) {
				btnLogin.Dispose ();
				btnLogin = null;
			}
			if (loginLabel != null) {
				loginLabel.Dispose ();
				loginLabel = null;
			}
			if (LogoView != null) {
				LogoView.Dispose ();
				LogoView = null;
			}
			if (txtLogin != null) {
				txtLogin.Dispose ();
				txtLogin = null;
			}
			if (txtPassword != null) {
				txtPassword.Dispose ();
				txtPassword = null;
			}
		}
	}
}
