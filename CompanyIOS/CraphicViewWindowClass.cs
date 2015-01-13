using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using System.Drawing;


namespace CompanyIOS
{
	partial class CraphicViewWindowClass : UIView
	{
		public CraphicViewWindowClass (IntPtr handle) : base (handle)
		{
		}
		public override UIView HitTest (CoreGraphics.CGPoint point, UIEvent uievent)
		{
			UIButton but = null;
			foreach (UIView itemView in Subviews) {
				foreach (var item in itemView.Subviews) {
					if (item is UIButton) {
						if (item.Frame.Contains(point)) {
							but = item as UIButton;
						}
					}
				}
			}
			if (but != null) {
				but.SendActionForControlEvents (UIControlEvent.TouchUpInside);
			} else {
				if (GraphicsViewWorker.StaitcView != null) {
					GraphicsViewWorker.StaitcView.RemoveFromSuperview ();
					GraphicsViewWorker.StaitcView = null;
				}
			}
			return null;
		}
	}
}
