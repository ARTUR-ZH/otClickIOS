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
	[Register ("CommentsController")]
	partial class CommentsController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView BottomMenu { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Comments_Button btnComments { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Rate_button btnRates { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		Statisti_button btnStatistic { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView MainBackground { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UINavigationItem NavigationBar { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (BottomMenu != null) {
				BottomMenu.Dispose ();
				BottomMenu = null;
			}
			if (btnComments != null) {
				btnComments.Dispose ();
				btnComments = null;
			}
			if (btnRates != null) {
				btnRates.Dispose ();
				btnRates = null;
			}
			if (btnStatistic != null) {
				btnStatistic.Dispose ();
				btnStatistic = null;
			}
			if (MainBackground != null) {
				MainBackground.Dispose ();
				MainBackground = null;
			}
			if (NavigationBar != null) {
				NavigationBar.Dispose ();
				NavigationBar = null;
			}
		}
	}
}
