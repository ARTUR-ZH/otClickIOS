using System;
using UIKit;
using CoreGraphics;
using System.Drawing;

namespace CompanyIOS
{
	public class CircleCommentsView : UIView
	{

		nint[] _degreeInnfloat;
		UIColor[] _color;
		string[] _labelForServiceName;
		nfloat[] _oSx;

		public CircleCommentsView (nint cost, nint quality, nint service)
		{
			_color = new UIColor[] {
				UIColor.FromRGB (71, 201, 175),
				UIColor.FromRGB (255, 110, 23),
				UIColor.FromRGB (61, 147, 219)
			};
			_oSx = new nfloat[] { 50f, 156f, 262f };
			BackgroundColor = UIColor.Clear;
			Opaque = true;
			_degreeInnfloat = new nint[] { cost, quality, service };
			_labelForServiceName = new String[] { "Стоимость", "Качество", "Сервис" };
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
			for (int i = 0; i < 3; i++) {
				using (var context = UIGraphics.GetCurrentContext ()) {
					context.SetLineCap (CGLineCap.Butt);
					context.SetLineWidth (4f);
					path = new CGPath ();
					path.AddArc (_oSx [i], 30, 20, ((nfloat)270).ToRadians (), ((nfloat)CountDegreeInPersentage (_degreeInnfloat [i])).ToRadians (), false);
					context.SetAllowsAntialiasing (true);
					context.AddPath (path);
					context.SetFillColor (UIColor.Clear.CGColor);
					context.SetStrokeColor (_color [i].CGColor);
					context.DrawPath (CGPathDrawingMode.FillStroke);
					path = new CGPath ();
					path.AddArc (_oSx [i], 30, 18, 0, ((nfloat)360).ToRadians (), false);
					context.SetLineWidth (1f);
					context.AddPath (path);
					context.SetStrokeColor (UIColor.FromRGB (239, 243, 243).CGColor);
					context.DrawPath (CGPathDrawingMode.FillStroke);
				}
				UILabel labelForServiceName = new UILabel () {
					Frame = new CGRect ((nfloat)i * (106.6f - 2f), 50f, 106, 30),
					Font = UIFont.FromName ("HelveticaNeue-Light", 12f),
					TextAlignment = UITextAlignment.Center,
					TextColor = UIColor.Gray,
					BackgroundColor = UIColor.Clear,
					Text = _labelForServiceName [i]
				};
				UILabel labelForPersentage = new UILabel () {
					Frame = new CGRect (_oSx [i] - 15, 15f, 30, 30),
					Font = UIFont.FromName ("HelveticaNeue-Bold", 12f),
					TextAlignment = UITextAlignment.Center,
					TextColor = UIColor.Gray,
					BackgroundColor = UIColor.Clear,
					Text = _degreeInnfloat [i].ToString ()
				};
				Add (labelForPersentage);
				Add (labelForServiceName);
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
			if (_oSx != null) {
				_oSx = null;
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

