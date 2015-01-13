using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using CoreGraphics;

namespace CompanyIOS
{
	public partial class CompaniesControl : UIViewController
	{
		ComapniesTableView table;
		LoadingOverlay loadingOverlay;

		public CompaniesControl (IntPtr handle) : base (handle)
		{

		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			table = new ComapniesTableView (new CGRect (0, 0 , 320, MainBackgroundComp.Frame.Height));
			UIButton but = new UIButton (UIButtonType.Custom);
			but.TouchUpInside += tableWork;
			but.SendActionForControlEvents (UIControlEvent.TouchUpInside);
		}
		async void tableWork (object sender, EventArgs e)
		{
			HttpServiceConn conn = new HttpServiceConn ();
			loadingOverlay = new LoadingOverlay (new CGRect (MainBackgroundComp.Frame.X, MainBackgroundComp.Frame.Y, MainBackgroundComp.Frame.Width, MainBackgroundComp.Frame.Height));
			Add (loadingOverlay);
			var rate = await conn.GetCompanies (GraphicsController.Token);
			table.Bounces = true;
			table.Source = new CompaniesTableFill (rate.Item2.Resource,this);
			Add (table);			
			Add (loadingOverlay);
			loadingOverlay.Hide ();

		}


	}
}
