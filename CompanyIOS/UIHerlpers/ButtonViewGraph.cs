using System;
using UIKit;
using System.Drawing;
using CoreGraphics;

namespace CompanyIOS
{
	public class ButtonViewGraph : UIView
	{
		private UIColor _color;
		private nfloat _radius;

		public ButtonViewGraph(UIColor color,nfloat radius)
		{
			BackgroundColor = UIColor.Clear;
			Opaque = true;
			this._color = color;
			this._radius = radius;
		}
		public override void Draw (CGRect rect)
		{
			base.Draw (rect);
			using (var g = UIGraphics.GetCurrentContext ()) {
				g.SetShouldAntialias (true);
				_color.SetFill ();
				g.FillEllipseInRect (new CGRect (0, 0, _radius, _radius));
			}
		}
	}
}

