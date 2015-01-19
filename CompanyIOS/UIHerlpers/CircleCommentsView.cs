using System;
using UIKit;
using CoreGraphics;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace CompanyIOS
{
	public class CircleCommentsView : UIView
	{

		List<string> _degreeInnfloat;
		UIColor[] _color;
		List<string> _labelForServiceName;

		public CircleCommentsView (Dictionary<string,string> data)
		{
			_color = new UIColor[] {
				UIColor.FromRGB (71, 201, 175),
				UIColor.FromRGB (255, 110, 23),
				UIColor.FromRGB (61, 147, 219)
			};
			BackgroundColor = UIColor.Clear;
			Opaque = true;
			_degreeInnfloat = data.Values.ToList();
			_labelForServiceName = GraphicsController.Questions.Select(x=>x.QName).ToList();
			ClearsContextBeforeDrawing = false;		
		}

		public override void Draw (CGRect rect)
		{
			base.Draw (rect);
			DrawingTableCellArcs ();

		}

		void DrawingTableCellArcs ()
		{
			CGPath path;
			for (int i = 0; i < _labelForServiceName.Count; i++) {
				using (var context = UIGraphics.GetCurrentContext ()) {
					context.SetLineCap (CGLineCap.Butt);
					context.SetLineWidth (4f);
					path = new CGPath ();
					path.AddArc (30, ((i * 2)+1) * 30, 20, ((nfloat)270).ToRadians (), ((nfloat)CountDegreeInPersentage (Convert.ToInt32(_degreeInnfloat [i]))).ToRadians (), false);
					context.SetAllowsAntialiasing (true);
					context.AddPath (path);
					context.SetFillColor (UIColor.Clear.CGColor);
					context.SetStrokeColor (_color [i].CGColor);
					context.DrawPath (CGPathDrawingMode.FillStroke);
					path = new CGPath ();
					path.AddArc (30, ((i * 2)+1) * 30, 18, 0, ((nfloat)360).ToRadians (), false);
					context.SetLineWidth (1f);
					context.AddPath (path);
					context.SetStrokeColor (UIColor.FromRGB (239, 243, 243).CGColor);
					context.DrawPath (CGPathDrawingMode.FillStroke);
				}
				UILabel labelForServiceName = new UILabel () {
					Frame = new CGRect (60, i * 60 , 200, 60),
					Font = UIFont.FromName ("HelveticaNeue-Light", 12f),
					TextAlignment = UITextAlignment.Left,
					TextColor = UIColor.Gray,
					LineBreakMode = UILineBreakMode.WordWrap,
					Lines = 2,
					BackgroundColor = UIColor.Clear,
					Text = _labelForServiceName [i]
				};
				UILabel labelForPersentage = new UILabel () {
					Frame = new CGRect (15, ((i * 4)+1) * 15, 30, 30),
					Font = UIFont.FromName ("HelveticaNeue-Bold", 12f),
					TextAlignment = UITextAlignment.Center,
					TextColor = UIColor.Gray,
					BackgroundColor = UIColor.Clear,
					Text = _degreeInnfloat [i]
				};
				UIView horizontalLine = new UIView ();
				horizontalLine.Frame = new CGRect (60, (i + 1) * 60, 320, 1);
				horizontalLine.BackgroundColor = UIColor.FromRGB (239, 243, 243);
				Add (labelForPersentage);
				Add (labelForServiceName);
				Add (horizontalLine);
				if (path != null) {
					path.Dispose ();
				}
				if (labelForPersentage != null) {
					labelForPersentage.Dispose ();
				}
				if (labelForServiceName != null) {
					labelForServiceName.Dispose ();
				}
			}
			if (_degreeInnfloat != null) {
				_degreeInnfloat = null;
			}
			if (_color != null) {
				_color = null;
			}
			if (_labelForServiceName != null) {
				_labelForServiceName = null;
			}
		}

		public nfloat CountDegreeInPersentage (nint value)
		{
			nfloat degree = 0;
			if (value == 0) {
				return degree;
			}
			return degree = 270 + (360f / 100f) * value;		 
		}

	}
}

