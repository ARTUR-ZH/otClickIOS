using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreGraphics;

namespace CompanyIOS
{
	partial class RateController : UIViewController
	{
		protected RateTableViewStyle table;
		protected nint index;
		protected LoadingOverlay loadingOverlay;

		protected string Token {
			get;
			set;
		}

		public RateController (IntPtr handle) : base (handle)
		{

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			table = new RateTableViewStyle (new CGRect (0, MainLabel.Frame.Height + MainLabel.Frame.Y, 320, MainBackground.Frame.Height - (MainLabel.Frame.Height + MainLabel.Frame.Y + BottomMenu.Frame.Height)));
			UIButton but = new UIButton (UIButtonType.Custom);
			but.TouchUpInside += tableWork;
			but.SendActionForControlEvents (UIControlEvent.TouchUpInside);	

		}

		async void tableWork (object sender, EventArgs e)
		{
			HttpServiceConn conn = new HttpServiceConn ();
			loadingOverlay = new LoadingOverlay (new CGRect (MainBackground.Frame.X, MainBackground.Frame.Y, MainBackground.Frame.Width, MainBackground.Frame.Height - BottomMenu.Frame.Height));
			Add (loadingOverlay);
			var rate = await conn.GetNews ();
			List<NewsData> data = new List<NewsData> ();
			foreach (var item in rate.Item2.Resource) {
				data.Add (item.Value);
			}


			this.NavigationItem.Title = "Новости";
			table.Bounces = true;
			table.Source = new RateTableSourceFill (data,this);
			Add (table);
			Add (loadingOverlay);
			loadingOverlay.Hide ();
		}
	}
}
