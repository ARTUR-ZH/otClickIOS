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
			var rate = await conn.GetRating (GraphicsController.Token);
			List<string> data = new List<string> ();
			foreach (var item in rate.Item2.Resource) {
				data.Add (item.Value);
			}
			for (int i = 0; i < data.Count; i++) {
				if (data [i].ToUpper ().Contains ("Моя компания".ToUpper ())) {
					index = i + 1;
				}
			}

			this.NavigationItem.Title = string.Format ("Ваш рейтинг: {0} из {1}", index, data.Count);
			table.Bounces = true;
			table.Source = new RateTableSourceFill (data);
			Add (table);
			Add (loadingOverlay);
			loadingOverlay.Hide ();
		}
	}
}
