using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using System.Threading.Tasks;

namespace CompanyIOS
{
	public class GraphicsViewWorker : UIView
	{
		private UILabel _labelText;
		private UIColor _color;
		private CALayer _animationLayer;
		private CAShapeLayer _pathLayer, _cicleLayer, _circleLayerWhite;
		private static UIView view;
		private CGPoint[] _points;
		private readonly nfloat _rad = 8;
		private readonly nfloat _osXLength = 260f;
		private readonly nfloat _oSx = 0;
		private readonly nfloat _oSy = 0;
		private readonly nfloat _mainFrameHeight;
		private readonly nfloat _oSyStep;
		private UIView parentView;

		public static UIView StaitcView {
			get {
				return view;
			}
			set {
				view = value;
			}
		}

		public GraphicsViewWorker (GraphicsCallback dataFromService, WhatServiceWork numberOfService, UIColor colorservice, CGRect frame, UIView views)
		{
			Frame = frame;
			BackgroundColor = UIColor.Clear;
			ClearsContextBeforeDrawing = false;
			UserInteractionEnabled = true;
			Opaque = true;		
			this._color = colorservice;
			this._mainFrameHeight = CountFrameHeight (this.Layer.Frame.Height);
			this._oSyStep = CountOsyStep (this.Layer.Frame.Height);
			this._points = CreatePoints (dataFromService, numberOfService);
			DeleteTouchView ();
			this.parentView = views;
		}

		private CGPoint[] CreatePoints (GraphicsCallback data, WhatServiceWork service)
		{
			nfloat xFirstStep = 40f;
			nfloat xStep = (_osXLength - 10) / (nfloat)(data.Resource.Count - 1);						
			List<Tuple<nfloat,nfloat>> listOfData = new List<Tuple<nfloat,nfloat>> ();
			string numberOfEnum = service.ToString ();
			var keyNumber = (int)((WhatServiceWork)Enum.Parse (typeof(WhatServiceWork), numberOfEnum));
			string key = keyNumber.ToString ();
			foreach (var keys in data.Resource) {
				listOfData.Add (Tuple.Create (xFirstStep, _mainFrameHeight - _oSyStep * ((nfloat)(Convert.ToInt32(keys.Value [key])))));
				xFirstStep += xStep;
			}
			nint c = 0;
			CGPoint[] points = new CGPoint[listOfData.Count];
			foreach (var item in listOfData) {
				points [c] = new CGPoint (item.Item1, item.Item2);
				c = c + 1;
			}
			return points;
		}

		private nfloat CountOsyStep (nfloat yStep)
		{
			return (yStep - ((yStep / 100f) * 20f)) / 100f;
		}

		public static nfloat CountFrameHeight (nfloat height)
		{
			return height - (height / 100f) * 10f;
		}

		public override void Draw (CGRect rect)
		{
			base.Draw (rect);
			using (var g = UIGraphics.GetCurrentContext ()) {			
				DrawButtonsOnGraphics (_points);
				this._animationLayer = new CALayer ();
				this._animationLayer.Frame = new CGRect (0, 0, 320, 250);
				this.Layer.AddSublayer (this._animationLayer);
				this.CreateAnimation (_points, g);
				this.StartAnimation ();
			}
		}

		private void CreateAnimation (CGPoint[] points, CGContext ctx)
		{
			CGPath path = new CGPath ();
			path.AddLines (points);

			CAShapeLayer pathLayer = new CAShapeLayer ();
			pathLayer.Frame = new CGRect (0, 0, 320, 250);
			pathLayer.Path = path;
			pathLayer.StrokeColor = _color.CGColor;
			pathLayer.FillColor = UIColor.Clear.CGColor;
			pathLayer.LineWidth = 2.2f;
			pathLayer.LineJoin = CAShapeLayer.JoinBevel;
			this._animationLayer.AddSublayer (pathLayer);
			this._pathLayer = pathLayer;

			CGPath circlePath = new CGPath ();
			foreach (var item in points) {
				circlePath.AddEllipseInRect (new CGRect (item.X - _rad / 2, item.Y - _rad / 2, _rad, _rad));
			}
			CAShapeLayer cicleLayer = new CAShapeLayer ();
			cicleLayer.Frame = new CGRect (0, 0, 320, 250);
			cicleLayer.Path = circlePath;
			cicleLayer.StrokeColor = _color.CGColor;
			cicleLayer.FillColor = UIColor.Clear.CGColor;
			cicleLayer.LineWidth = 2.2f;
			cicleLayer.LineJoin = CAShapeLayer.JoinBevel;
			this._animationLayer.AddSublayer (cicleLayer);
			this._cicleLayer = cicleLayer;

			CGPath circleLayerWhitePath = new CGPath ();
			foreach (var item in points) {
				circleLayerWhitePath.AddEllipseInRect (new CGRect (item.X - 1.5f, item.Y - 1.5f, 3, 3));
			}
			CAShapeLayer circleLayerWhite = new CAShapeLayer ();
			circleLayerWhite.Frame = new CGRect (0, 0, 320, 250);
			circleLayerWhite.Path = circleLayerWhitePath;
			circleLayerWhite.StrokeColor = UIColor.White.CGColor;
			circleLayerWhite.FillColor = UIColor.Clear.CGColor;
			circleLayerWhite.LineWidth = 3f;
			circleLayerWhite.LineJoin = CAShapeLayer.JoinBevel;
			this._animationLayer.AddSublayer (circleLayerWhite);
			this._circleLayerWhite = circleLayerWhite;
		
		}

		private void DrawButtonsOnGraphics (CGPoint[] points)
		{
			foreach (var item in points) {
				UIButton button = new UIButton (UIButtonType.Custom);
				button.BackgroundColor = UIColor.Clear;
				button.Frame = new CGRect (item.X - 10, item.Y - 10, 20, 20);
				button.TouchUpInside += GraphicButtonClick;
				AddSubview (button);
			}
		}

		void GraphicButtonClick (object sender, EventArgs e)
		{
			UIButton buttonSender = (UIButton)sender;
			if (_oSx == buttonSender.Frame.X && _oSy == buttonSender.Frame.Y) {
				if (view != null) {
					DeleteTouchView ();
				} else {
					view = new UIView (new CGRect (buttonSender.Frame.X - 10, buttonSender.Frame.Y - 22, 40, 25));
					_labelText = new UILabel (new CGRect (5, 0, 30, 20));
					_labelText.Font = UIFont.FromName ("Helvetica", 12f);
					_labelText.Text = ((_mainFrameHeight - buttonSender.Frame.Y - 10) / _oSyStep).ToString ();
					_labelText.AdjustsFontSizeToFitWidth = false;
					_labelText.TextColor = UIColor.Gray;
					_labelText.TextAlignment = UITextAlignment.Center;

					UIImageView imageView = new UIImageView (new UIImage ("buttongraph.png"));
					imageView.Frame = new CGRect (0, 0, 40, 25);
					ButtonViewGraph butNew = new ButtonViewGraph (_color, _rad);
					butNew.Frame = new CGRect (16, 25, 20, 20);
					view.AddSubview (butNew);
					ButtonViewGraph butNewOval = new ButtonViewGraph (_color.ColorWithAlpha (0.5f), _rad + 6);
					butNewOval.Frame = new CGRect (butNewOval.Frame.X - 3, butNewOval.Frame.Y - 3, 20, 20);
					view.AddSubview (butNewOval);
					view.AddSubview (imageView);
					view.AddSubview (_labelText);
					view.BackgroundColor = UIColor.Clear;
					AddSubview (view);

				}

			} else {
				DeleteTouchView ();	
				view = new UIView (new CGRect (buttonSender.Frame.X - 10f, buttonSender.Frame.Y - 22, 40, 25));
				_labelText = new UILabel (new CGRect (5, 0, 30, 20));
				_labelText.Font = UIFont.FromName ("HelveticaNeue-Bold", 12f);
				_labelText.TextColor = UIColor.FromRGB (152, 163, 163);
				_labelText.Text = ((_mainFrameHeight - buttonSender.Frame.Y - 10) / _oSyStep).ToString ();
				_labelText.AdjustsFontSizeToFitWidth = false;
				_labelText.TextColor = UIColor.Gray;
				_labelText.TextAlignment = UITextAlignment.Center;


				UIImageView imageView = new UIImageView (new UIImage ("buttongraph.png"));
				imageView.Frame = new CGRect (0, 0, 40, 25);
				view.AddSubview (imageView);
				ButtonViewGraph butNew = new ButtonViewGraph (_color, _rad);
				butNew.Frame = new CGRect (16, 28, 20, 20);
				view.AddSubview (butNew);
				ButtonViewGraph butNewOval = new ButtonViewGraph (_color.ColorWithAlpha (0.5f), _rad + 7);
				butNewOval.Frame = new CGRect (butNew.Frame.X - 3.5f, butNew.Frame.Y - 3.5f, 20, 20);
				view.AddSubview (butNewOval);
				view.AddSubview (_labelText);
				view.BackgroundColor = UIColor.Clear;
				parentView.Add (view);
			}
		}

		void StartAnimation ()
		{
			this._pathLayer.RemoveAllAnimations ();

			CABasicAnimation pathAnimation = CABasicAnimation.FromKeyPath (@"strokeEnd");
			pathAnimation.Duration = 2f;
			pathAnimation.From = NSNumber.FromNFloat (0);
			pathAnimation.To = NSNumber.FromNFloat (1);
			this._pathLayer.AddAnimation (pathAnimation, @"strokeEnd");
			this._cicleLayer.RemoveAllAnimations ();

			CABasicAnimation circleAnimation = CABasicAnimation.FromKeyPath (@"strokeEnd");
			circleAnimation.Duration = 2f;
			circleAnimation.From = NSNumber.FromNFloat (0);
			circleAnimation.To = NSNumber.FromNFloat (1);
			this._cicleLayer.AddAnimation (circleAnimation, @"strokeEnd");

			this._circleLayerWhite.RemoveAllAnimations ();

			CABasicAnimation circleAnimationWhite = CABasicAnimation.FromKeyPath (@"strokeEnd");
			circleAnimationWhite.Duration = 2f;
			circleAnimationWhite.From = NSNumber.FromNFloat (0);
			circleAnimationWhite.To = NSNumber.FromNFloat (1);
			this._circleLayerWhite.AddAnimation (circleAnimationWhite, @"strokeEnd");
		
		}



		public void DeleteTouchView ()
		{
			if (view != null) {
				view.RemoveFromSuperview ();
				view = null;
			}
		}

		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);

		}
	}
}

