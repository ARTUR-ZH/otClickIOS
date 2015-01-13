using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace CompanyIOS
{
	partial class TopMenu_Button : UIButton
	{
		public TopMenu_Button (IntPtr handle) : base (handle)
		{
			Initialize ();

		}

		public TopMenu_Button ()
		{
			Initialize ();
		}

		void Initialize ()
		{
			nfloat edges = GetCenterOfFrame (Frame.Width);
			nfloat edgHeight = GetHeightOfFrame (Frame.Height);
			SetTitleColor (UIColor.FromRGB (152, 163, 163), UIControlState.Normal);
			ImageEdgeInsets = new UIEdgeInsets (edgHeight, edges, 0, edges);
			UIImage image = new UIImage ("триугла.png");	
			SetImage (image, UIControlState.Disabled);

			SetTitleColor (UIColor.FromRGB (150, 87, 162), UIControlState.Disabled);

		}
		protected nfloat GetHeightOfFrame (nfloat height)
		{
			return (height - (height / 100) * 11.1f);
		}

		protected nfloat GetCenterOfFrame (nfloat width)
		{
			return (width - 23f) / 2f;
		}
	}
}
