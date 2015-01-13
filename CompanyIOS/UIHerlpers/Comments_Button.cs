using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace CompanyIOS
{
	partial class Comments_Button : UIButton
	{
		public Comments_Button (IntPtr handle) : base (handle)
		{
			Initialize ();

		}
		public Comments_Button ()
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
			SetTitle ("", UIControlState.Normal);
			SetTitle ("", UIControlState.Disabled);
			SetImage (new UIImage ("2.png"), UIControlState.Normal);
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
