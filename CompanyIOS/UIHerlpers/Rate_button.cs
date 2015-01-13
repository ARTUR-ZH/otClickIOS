using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace CompanyIOS
{
	partial class Rate_button : UIButton
	{
		public Rate_button (IntPtr handle) : base (handle)
		{
			Initialize ();

		}
		public Rate_button ()
		{
			Initialize ();
		}
		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
			Initialize ();
		}
		void Initialize ()
		{
			//Frame = new System.Drawing.CGRect (214, 0, 106, 50);
			SetTitle ("", UIControlState.Normal);
			SetTitle ("", UIControlState.Disabled);
			SetImage (new UIImage ("3.png"), UIControlState.Normal);
			ImageEdgeInsets = new UIEdgeInsets (15, 43, 15, 43);
			ContentMode = UIViewContentMode.Center;
			if ((UIControlState.Disabled|UIControlState.Selected) == this.State) {
				BackgroundColor = UIColor.FromRGB (109, 61, 119);
			} else {
				BackgroundColor = UIColor.FromRGB (152, 87, 162);
			}
		}
	}
}
