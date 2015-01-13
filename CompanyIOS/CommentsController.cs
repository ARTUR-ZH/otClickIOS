using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreGraphics;

namespace CompanyIOS
{

	public partial class CommentsController : UIViewController
	{
		protected CommentsTableViewStyle table;
		protected Tuple<string,CommentsHelper> data;
		protected LoadingOverlay loadingOverlay;

		public static string CommentsText {
			get;
			set;
		}
		public CommentsController (IntPtr handle) : base (handle)
		{

		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.NavigationController.NavigationBar.TintColor = UIColor.FromRGB (152, 87, 162);
			UIButton but = new UIButton (UIButtonType.Custom);
			but.TouchUpInside += tableWork;
			but.SendActionForControlEvents (UIControlEvent.TouchUpInside);	
			table = new CommentsTableViewStyle (new CGRect (0, 64, 320, MainBackground.Frame.Height - (64 + BottomMenu.Frame.Height)));
		}


		async void tableWork (object sender, EventArgs e)
		{
			HttpServiceConn conn = new HttpServiceConn ();
			loadingOverlay = new LoadingOverlay (new CGRect (MainBackground.Frame.X, MainBackground.Layer.Frame.Y, MainBackground.Frame.Width, MainBackground.Layer.Frame.Height - BottomMenu.Layer.Frame.Height));
			Add (loadingOverlay);
			data = await conn.GetComments (GraphicsController.Token, DateTime.Now.ToString ("ddMMyyyy"));
			table.Bounces = true;

			table.Source = new CommentsTableSourceFill (data.Item2.Resource,this);
			Add (table);
			Add (loadingOverlay);
			loadingOverlay.Hide ();
		}
		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);
			if (segue.Identifier != null) {
				if (segue.Identifier.ToUpper ().Equals ("comments".ToUpper ())) {
					CommentsTextViewController.CommentsTextForView = CommentsText;
				}
			}
		}
	}
}
