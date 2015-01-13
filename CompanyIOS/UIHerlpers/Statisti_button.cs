using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace CompanyIOS
{
	partial class Statisti_button : UIButton
	{
		public Statisti_button (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		public Statisti_button ()
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
			//Frame = new System.Drawing.RectangleF (0, 0, 106, 50);
			SetTitle ("", UIControlState.Normal);
			SetTitle ("", UIControlState.Disabled);
			SetImage (new UIImage ("1.png"), UIControlState.Normal);
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
