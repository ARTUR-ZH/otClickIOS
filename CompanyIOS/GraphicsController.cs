using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using CoreGraphics;

namespace CompanyIOS
{
	partial class GraphicsController : UIViewController
	{
		public UIButton _but;
		public GraphicsViewWorker _cost;
		public GraphicsViewWorker _quality;
		public GraphicsViewWorker _service;
		public Tuple<string,GraphicsCallback> _data;
		private bool _buttonAllCliked;
		private bool _buttonCostCliked;
		private bool _buttonQualityCliked;
		private bool _buttonServiceCliked;
		protected LoadingOverlay loadingOverlay;
		private UIView osXview;
		Tuple<string,QuestionHelper> _Questions;

		public static nint Company {
			get;
			set;
		}

		public static string Token {
			get;
			set;
		}

		public GraphicsController (IntPtr handle) : base (handle)
		{
			_buttonAllCliked = false;
			_buttonCostCliked = false;
			_buttonQualityCliked = false;
			_buttonServiceCliked = false;
		}

		public override async void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			btnMonth.TouchUpInside += btnTopMenuTouch;
			btnQuarter.TouchUpInside += btnTopMenuTouch;
			btnHalfYear.TouchUpInside += btnTopMenuTouch;
			btnYear.TouchUpInside += btnTopMenuTouch;
			btnCost.TouchUpInside += btnServicesTouch;

			btnQuality.TouchUpInside += btnServicesTouch;
			btnService.TouchUpInside += btnServicesTouch;
			btnAll.TouchUpInside += btnServicesTouch;

			HttpServiceConn conn = new HttpServiceConn ();
			_Questions = await conn.GetQuestions (Token, Company);
			btnCost.SetTitle (_Questions.Item2.Resource [0].QName, UIControlState.Normal);
			btnMonth.SendActionForControlEvents (UIControlEvent.TouchUpInside);
			UISwipeGestureRecognizer gest = new UISwipeGestureRecognizer();
			gest.Direction = UISwipeGestureRecognizerDirection.Right;
			gest.AddTarget (() => MoveBack (gest));
			MainBackGroubd.AddGestureRecognizer (gest);

		}

		void MoveBack (UISwipeGestureRecognizer gest)
		{
			if (gest.State != (UIGestureRecognizerState.Cancelled | UIGestureRecognizerState.Failed
				| UIGestureRecognizerState.Possible))
			{
				PerformSegue ("back", this);
			}
		}


		async void btnTopMenuTouch (object sender, EventArgs e)
		{
			if (osXview != null) {
				osXview.RemoveFromSuperview ();
			}
			UIButton but = sender as UIButton;
			HttpServiceConn conn = new HttpServiceConn ();
			if (_buttonCostCliked && _buttonQualityCliked && _buttonServiceCliked) {
				_buttonAllCliked = true;
				_buttonCostCliked = false;
				_buttonQualityCliked = false;
				_buttonServiceCliked = false;
			} else {
				_buttonAllCliked = false;
			}
			if (_buttonAllCliked) {
				_cost.DeleteTouchView ();
				_cost.RemoveFromSuperview ();
				btnCost.SetImage (new UIImage ("стоимость_а.png"), UIControlState.Normal);
				_quality.DeleteTouchView ();
				_quality.RemoveFromSuperview ();
				btnQuality.SetImage (new UIImage ("качество_а.png"), UIControlState.Normal);
				_service.DeleteTouchView ();
				_service.RemoveFromSuperview ();
				btnService.SetImage (new UIImage ("сервис_а.png"), UIControlState.Normal);
			}
			if (_buttonCostCliked) {
				_cost.DeleteTouchView ();
				_cost.RemoveFromSuperview ();
				btnCost.SetImage (new UIImage ("стоимость_а.png"), UIControlState.Normal);
			}
			if (_buttonQualityCliked) {
				_quality.DeleteTouchView ();
				_quality.RemoveFromSuperview ();
				btnQuality.SetImage (new UIImage ("качество_а.png"), UIControlState.Normal);
			}
			if (_buttonServiceCliked) {
				_service.DeleteTouchView ();
				_service.RemoveFromSuperview ();
				btnService.SetImage (new UIImage ("сервис_а.png"), UIControlState.Normal);			
			}
			loadingOverlay = new LoadingOverlay (new CGRect (MainBackGroubd.Frame.X, MainBackGroubd.Layer.Frame.Y, MainBackGroubd.Frame.Width, MainBackGroubd.Layer.Frame.Height - BottomMenu.Layer.Frame.Height));
			 
			View.Add (loadingOverlay);
			_data = await conn.GetGraphicAsync (Token, DateTime.Now.ToString ("dd.MM.yyyy"), but.Title (UIControlState.Normal));
			loadingOverlay.Hide ();
			if (_data != null) {
				if (_data.Item2.Resource != null) {		
					CreateOsXMonthOrDaysVector (_data.Item2.Resource);
					but.Enabled = false;
					if (UIControlState.Disabled == but.State) {
						but.TitleEdgeInsets = new UIEdgeInsets (0, - 46, 0, 0);
					}
					if (_but == null) {
						_but = but;
					} else {
						_but.Enabled = true;
						if (UIControlState.Normal == _but.State) {
							_but.TitleEdgeInsets = new UIEdgeInsets (0, 0, 0, 0);
						}
						_but = but;
					}
					if (_buttonAllCliked) {
						_buttonAllCliked = false;
						btnAll.SendActionForControlEvents (UIControlEvent.TouchUpInside);
					} else {
						if (_buttonCostCliked) {
							_buttonCostCliked = false;
							btnCost.SendActionForControlEvents (UIControlEvent.TouchUpInside);
						} else {
							if (!_buttonAllCliked && !_buttonQualityCliked && !_buttonServiceCliked) {
								btnCost.SendActionForControlEvents (UIControlEvent.TouchUpInside);
							}
						}
						if (_buttonQualityCliked) {
							_buttonQualityCliked = false;
							btnQuality.SendActionForControlEvents (UIControlEvent.TouchUpInside);
						}
						if (_buttonServiceCliked) {
							_buttonServiceCliked = false;
							btnService.SendActionForControlEvents (UIControlEvent.TouchUpInside);
						}
					}
				} else {
					UIAlertView error = new UIAlertView ("Ошибка", _data.Item1, null, "Закрыть", null);
					error.Show ();
				}
			} 

		}

		void btnServicesTouch (object sender, EventArgs e)
		{
			UIButton button = sender as UIButton;
	
			switch (button.Title (UIControlState.Normal)) {
			case "cost":
				CostGraphicDraw ();
				break;
			case "quality":
				QualityGraphicDraw ();
				break;
			case "service":
				ServiceGraphicDraw ();
				break;
			case "all":
				AllGraphicDraw ();
				break;
			}

		}

		void CostGraphicDraw ()
		{

			if (_buttonCostCliked) {
				btnCost.SetImage (new UIImage ("стоимость_а.png"), UIControlState.Normal);
				_cost.DeleteTouchView ();
				_cost.RemoveFromSuperview ();
				_buttonCostCliked = false;
				_buttonAllCliked = false;
			} else {
				_cost = new GraphicsViewWorker (_data.Item2, WhatServiceWork.Cost, UIColor.FromRGB (71, 201, 175), GraphicViewWindow.Bounds,GraphicViewWindow);
				GraphicViewWindow.AddSubview (_cost);
				btnCost.SetImage (new UIImage ("стоимость_б.png"), UIControlState.Normal);
				_buttonCostCliked = true;
			}
		}

		void QualityGraphicDraw ()
		{
			if (_buttonQualityCliked) {
				btnQuality.SetImage (new UIImage ("качество_а.png"), UIControlState.Normal);
				_quality.DeleteTouchView ();
				_quality.RemoveFromSuperview ();
				_buttonQualityCliked = false;
				_buttonAllCliked = false;
			} else {
				_quality = new GraphicsViewWorker (_data.Item2, WhatServiceWork.Quality, UIColor.FromRGB (255, 110, 23), GraphicViewWindow.Bounds,GraphicViewWindow);
				GraphicViewWindow.AddSubview (_quality);
				_buttonQualityCliked = true;
				btnQuality.SetImage (new UIImage ("качество_б.png"), UIControlState.Normal);
			}
		}

		void ServiceGraphicDraw ()
		{
			if (_buttonServiceCliked) {
				btnService.SetImage (new UIImage ("сервис_а.png"), UIControlState.Normal);
				_service.DeleteTouchView ();
				_service.RemoveFromSuperview ();
				_buttonServiceCliked = false;
				_buttonAllCliked = false;
			} else {
				_service = new GraphicsViewWorker (_data.Item2, WhatServiceWork.Service, UIColor.FromRGB (61, 147, 219), GraphicViewWindow.Bounds,GraphicViewWindow);
				GraphicViewWindow.AddSubview (_service);
				_buttonServiceCliked = true;
				btnService.SetImage (new UIImage ("сервис_б.png"), UIControlState.Normal);
			}
		}

		void AllGraphicDraw ()
		{
			if (_buttonCostCliked && _buttonQualityCliked && _buttonServiceCliked) {
				_buttonAllCliked = true;
				_buttonCostCliked = false;
				_buttonQualityCliked = false;
				_buttonServiceCliked = false;
			} else {
				_buttonAllCliked = false;
			}
			if (_buttonAllCliked) {
				_cost.DeleteTouchView ();
				_quality.DeleteTouchView ();
				_service.DeleteTouchView ();
				_cost.RemoveFromSuperview ();
				_quality.RemoveFromSuperview ();
				_service.RemoveFromSuperview ();
				_buttonAllCliked = false;
				_buttonCostCliked = false;
				_buttonQualityCliked = false;
				_buttonServiceCliked = false;
				btnCost.SetImage (new UIImage ("стоимость_а.png"), UIControlState.Normal);
				btnQuality.SetImage (new UIImage ("качество_а.png"), UIControlState.Normal);
				btnService.SetImage (new UIImage ("сервис_а.png"), UIControlState.Normal);
			} else {
				if (!_buttonCostCliked) {
					_cost = new GraphicsViewWorker (_data.Item2, WhatServiceWork.Cost, UIColor.FromRGB (71, 201, 175), GraphicViewWindow.Bounds,GraphicViewWindow);
					GraphicViewWindow.AddSubview (_cost);
				}
				if (!_buttonQualityCliked) {
					_quality = new GraphicsViewWorker (_data.Item2, WhatServiceWork.Quality, UIColor.FromRGB (255, 110, 23), GraphicViewWindow.Bounds,GraphicViewWindow);
					GraphicViewWindow.AddSubview (_quality);
				}
				if (!_buttonServiceCliked) {
					_service = new GraphicsViewWorker (_data.Item2, WhatServiceWork.Service, UIColor.FromRGB (61, 147, 219), GraphicViewWindow.Bounds,GraphicViewWindow);
					GraphicViewWindow.AddSubview (_service);
				}
				_buttonAllCliked = true;
				_buttonCostCliked = true;
				_buttonQualityCliked = true;
				_buttonServiceCliked = true;
				btnCost.SetImage (new UIImage ("стоимость_б.png"), UIControlState.Normal);
				btnQuality.SetImage (new UIImage ("качество_б.png"), UIControlState.Normal);
				btnService.SetImage (new UIImage ("сервис_б.png"), UIControlState.Normal);
			}
		}

		void CreateOsXMonthOrDaysVector (System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>> resource)
		{
			nfloat xFirstStep = 40f;
			nfloat xStep = (250) / (nfloat)(resource.Count - 1);
			osXview = new UIView (new CGRect(0,0,320,GraphicViewWindow.Frame.Height));
			GraphicViewWindow.Add (osXview);
			nfloat heightOfFrame = GraphicsViewWorker.CountFrameHeight(osXview.Frame.Height);
			osXview.BackgroundColor = UIColor.Clear;
			if (resource.Count < 15) {
				foreach (var item in resource) {
					UILabel label = new UILabel (new CGRect (xFirstStep - 12.5f, heightOfFrame, 25, 25));
					label.TextAlignment = UITextAlignment.Center;
					label.Font = UIFont.FromName ("HelveticaNeue-Light", 10f);
					label.TextColor = UIColor.LightGray;
					label.Text = Enum.GetName (typeof(Months), Convert.ToInt32(item.Key));
					osXview.Add (label);
					xFirstStep += xStep;
				}
			}

		}

	}
}
