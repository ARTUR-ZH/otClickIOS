using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using CoreGraphics;

namespace CompanyIOS
{
	partial class CommentsTextViewController : UIViewController
	{
		public static string CommentsTextForView {
			get;
			set;
		}

		public CommentsTextViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			UITextView utv = new UITextView (new CGRect (15, 0, 290, 400));
			utv.ScrollEnabled = true;
			utv.Font = UIFont.FromName ("HelveticaNeue-Light", 16f);
			utv.TextColor = UIColor.DarkGray;
			utv.Text = CommentsTextForView;
			this.View.Add (utv);
		}
	}
}
