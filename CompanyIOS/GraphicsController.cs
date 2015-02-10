using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using CoreGraphics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyIOS
{
	partial class GraphicsController : UIViewController
	{
		public UIButton _but;
		public GraphicsViewWorker _cost;
		public GraphicsViewWorker _quality;
		public GraphicsViewWorker _service;
		public GraphicsViewWorker _all;
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

		public static List<QuestionData> Questions {
			get;
			set;
		}

		public GraphicsController (IntPtr handle) : base (handle)
		{
		}

		public override async void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			TopMenuEvents (true,btnMonth,btnQuarter,btnHalfYear,btnYear);
			ClickServicesEvents (true, btnCost, btnQuality, btnService, btnAll);		
			btnCost.TouchUpInside += btnOneTouch;
			btnQuality.TouchUpInside += btnTwoTouch;
			btnService.TouchUpInside += btnThreeTouch;
			btnAll.TouchUpInside += btnFourTouch;

			HttpServiceConn conn = new HttpServiceConn ();
			try {
				_Questions = await conn.GetQuestions (Token, Company);
				Questions = _Questions.Item2.Resource;
				CreateButtonForGraphics (btnCost,btnQuality,btnService,btnAll);
			} catch (Exception ex) {
				UIAlertView error = new UIAlertView ("Ошибка", ex.Message, null, "Закрыть", null);
				error.Show ();
			}
		
			btnMonth.SendActionForControlEvents (UIControlEvent.TouchUpInside);

			UISwipeGestureRecognizer gest = new UISwipeGestureRecognizer();
			gest.Direction = UISwipeGestureRecognizerDirection.Right;
			gest.AddTarget (() => MoveBack (gest));
			MainBackGroubd.AddGestureRecognizer (gest);
		}

		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			TopMenuEvents (false,btnMonth,btnQuarter,btnHalfYear,btnYear);
			ClickServicesEvents (false, btnCost, btnQuality, btnService, btnAll);
			btnCost.TouchUpInside -= btnOneTouch;
			btnQuality.TouchUpInside -= btnTwoTouch;
			btnService.TouchUpInside -= btnThreeTouch;
			btnAll.TouchUpInside -= btnFourTouch;

		}

		void TopMenuEvents(bool add, params UIButton[] buttons)
		{
			if (add) {
				foreach (var item in buttons) {
					item.TouchUpInside += btnTopMenuTouch;
				}
			} else {
				foreach (var item in buttons) {
					item.TouchUpInside -= btnTopMenuTouch;
				}
			}
		}

		void ClickServicesEvents(bool add, params UIButton[] buttons)
		{
			if (add) {
				foreach (var item in buttons) {
					item.TouchDown += ChangeBackground;
					item.TouchDragExit += ChangeBackgroundBack;
				}
			} else {
				foreach (var item in buttons) {
					item.TouchDown -= ChangeBackground;
					item.TouchDragExit -= ChangeBackgroundBack;
				}
			}
		}

		void ChangeBackgroundBack (object sender, EventArgs e)
		{
			UIButton but = sender as UIButton;
			but.BackgroundColor = UIColor.FromRGB (246, 246, 246);
		}


		void ChangeBackground (object sender, EventArgs e)
		{
			UIButton but = sender as UIButton;
			but.BackgroundColor = UIColor.FromRGB (227, 209, 230);
		}

		void CreateButtonForGraphics (params UIButton[] buttons)
		{
			for (int i = 0; i < buttons.Length; i++) {
				try {
					string text = _Questions.Item2.Resource [i].QName;
					buttons [i].TitleEdgeInsets = new UIEdgeInsets(0,10,0,0);
					buttons [i].SetTitle (text, UIControlState.Normal);
					buttons [i].TitleLabel.TextAlignment = UITextAlignment.Left;
					buttons [i].VerticalAlignment = UIControlContentVerticalAlignment.Center;
					buttons [i].TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
					buttons [i].TitleLabel.Lines = 4;
					buttons [i].SetTitleColor (UIColor.FromRGB (150, 87, 162), UIControlState.Normal);
					buttons [i].BackgroundColor = UIColor.FromRGB (246, 246, 246);
					buttons [i].Font = UIFont.FromName (@"Helvetica-Light", 11);
				} catch (ArgumentOutOfRangeException) {
					buttons [i].SetTitle (string.Empty, UIControlState.Normal);
					buttons [i].SetTitleColor (UIColor.FromRGB (150, 87, 162), UIControlState.Normal);
					buttons [i].BackgroundColor = UIColor.FromRGB (246, 246, 246);
					buttons [i].Font = UIFont.FromName (@"Helvetica-Light", 11);
					buttons [i].Enabled = false;
				}
			}
		}

		private void CreateLeftView (string png, UITextField textField)
		{
			UIImageView viewLeftOverlay = new UIImageView (new UIImage (png));
			viewLeftOverlay.Frame = new CGRect (0, 0, 35, 15);
			viewLeftOverlay.ContentMode = UIViewContentMode.ScaleAspectFit;
			viewLeftOverlay.BackgroundColor = UIColor.Clear;		
			textField.LeftView = viewLeftOverlay;
		}

		void MoveBack (UISwipeGestureRecognizer gest)
		{
			if (gest.State != UIGestureRecognizerState.Cancelled && gest.State != UIGestureRecognizerState.Failed &&
				gest.State != UIGestureRecognizerState.Possible)
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
			if (_buttonAllCliked) {
				_all.DeleteTouchView ();
				_all.RemoveFromSuperview ();
			}
			if (_buttonCostCliked) {
				_cost.DeleteTouchView ();
				_cost.RemoveFromSuperview ();
			}
			if (_buttonQualityCliked) {
				_quality.DeleteTouchView ();
				_quality.RemoveFromSuperview ();
			}
			if (_buttonServiceCliked) {
				_service.DeleteTouchView ();
				_service.RemoveFromSuperview ();						
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
						but.TitleEdgeInsets = new UIEdgeInsets (0, -46, 0, 0);
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
					if (_buttonCostCliked) {
						_buttonCostCliked = false;
						btnCost.SendActionForControlEvents (UIControlEvent.TouchUpInside);
					}
					if (_buttonQualityCliked) {
						_buttonQualityCliked = false;
						btnQuality.SendActionForControlEvents (UIControlEvent.TouchUpInside);
					}
					if (_buttonServiceCliked) {
						_buttonServiceCliked = false;
						btnService.SendActionForControlEvents (UIControlEvent.TouchUpInside);
					}
					if (_buttonAllCliked) {
						_buttonAllCliked = false;
						btnAll.SendActionForControlEvents (UIControlEvent.TouchUpInside);
					}					
				} else {
					UIAlertView error = new UIAlertView ("Ошибка", _data.Item1, null, "Закрыть", null);
					error.Show ();
				}
			} 
		}

		void btnOneTouch (object sender, EventArgs e)
		{
			UIButton but = sender as UIButton;
			but.BackgroundColor = UIColor.FromRGB (246, 246, 246);
//			UIView.Animate(1,0,UIViewAnimationOptions.CurveEaseIn,
//				() => { but.BackgroundColor = UIColor.FromRGB (227, 209, 230); },
//				() => { but.BackgroundColor = UIColor.FromRGB (246, 246, 246); });
			CostGraphicDraw ();	
			but.SetImage (UIImage.FromBundle ("1chek.png"), UIControlState.Selected);
			but.ImageEdgeInsets = new UIEdgeInsets (0, 10, 50, 10);
			but.Selected = _buttonCostCliked;
		}
		void btnTwoTouch (object sender, EventArgs e)
		{
			UIButton but = sender as UIButton;
			but.BackgroundColor = UIColor.FromRGB (246, 246, 246);
//			UIView.Animate(0.5,0,UIViewAnimationOptions.CurveEaseIn,
//				() => { but.BackgroundColor = UIColor.FromRGB (227, 209, 230); }, 
//				() => { but.BackgroundColor = UIColor.FromRGB (246, 246, 246); });
			QualityGraphicDraw ();	
			but.SetImage (UIImage.FromBundle ("2chek.png"), UIControlState.Selected);
			but.ImageEdgeInsets = new UIEdgeInsets (0, 10, 50, 10);
			but.Selected = _buttonQualityCliked;
		}
		void btnThreeTouch (object sender, EventArgs e)
		{
			UIButton but = sender as UIButton;
			but.BackgroundColor = UIColor.FromRGB (246, 246, 246);
//			UIView.Animate(0.1,0,UIViewAnimationOptions.Autoreverse,
//				() => { but.BackgroundColor = UIColor.FromRGB (227, 209, 230); }, 
//				() => { but.BackgroundColor = UIColor.FromRGB (246, 246, 246); });
			ServiceGraphicDraw ();	
			but.SetImage (UIImage.FromBundle ("3chek.png"), UIControlState.Selected);
			but.ImageEdgeInsets = new UIEdgeInsets (0, 10, 50, 10);
			but.Selected = _buttonServiceCliked;
				
		}
		void btnFourTouch (object sender, EventArgs e)
		{
			UIButton but = sender as UIButton;
			but.BackgroundColor = UIColor.FromRGB (246, 246, 246);
//			UIView.Animate(0.1,0,UIViewAnimationOptions.Autoreverse,
//				() => { but.BackgroundColor = UIColor.FromRGB (227, 209, 230); }, 
//				() => { but.BackgroundColor = UIColor.FromRGB (246, 246, 246); });
			AllGraphicDraw ();
			but.SetImage (UIImage.FromBundle ("4chek.png"), UIControlState.Selected);
			but.ImageEdgeInsets = new UIEdgeInsets (0, 10, 50, 10);
			but.Selected = _buttonAllCliked;

		}

		void CostGraphicDraw ()
		{
			if (_buttonCostCliked) {
				_cost.DeleteTouchView ();
				_cost.RemoveFromSuperview ();
				_buttonCostCliked = false;
			} else {
				_cost = new GraphicsViewWorker (_data.Item2, WhatServiceWork.Cost, UIColor.FromRGB (71, 201, 175), GraphicViewWindow.Bounds,GraphicViewWindow);
				GraphicViewWindow.AddSubview (_cost);
				_buttonCostCliked = true;
			}
		}

		void QualityGraphicDraw ()
		{
			if (_buttonQualityCliked) {
				_quality.DeleteTouchView ();
				_quality.RemoveFromSuperview ();
				_buttonQualityCliked = false;
			} else {
				_quality = new GraphicsViewWorker (_data.Item2, WhatServiceWork.Quality, UIColor.FromRGB (255, 110, 23), GraphicViewWindow.Bounds,GraphicViewWindow);
				GraphicViewWindow.AddSubview (_quality);
				_buttonQualityCliked = true;
			}
		}

		void ServiceGraphicDraw ()
		{
			if (_buttonServiceCliked) {
				_service.DeleteTouchView ();
				_service.RemoveFromSuperview ();
				_buttonServiceCliked = false;
			} else {
				_service = new GraphicsViewWorker (_data.Item2, WhatServiceWork.Service, UIColor.FromRGB (61, 147, 219), GraphicViewWindow.Bounds,GraphicViewWindow);
				GraphicViewWindow.AddSubview (_service);
				_buttonServiceCliked = true;
			}
		}

		void AllGraphicDraw ()
		{
			if (_buttonAllCliked) {
				_all.DeleteTouchView ();
				_all.RemoveFromSuperview ();
				_buttonAllCliked = false;
			} else {
				_all = new GraphicsViewWorker (_data.Item2, WhatServiceWork.All, UIColor.FromRGB (51, 72, 93), GraphicViewWindow.Bounds,GraphicViewWindow);
				GraphicViewWindow.AddSubview (_all);
				_buttonAllCliked = true;
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
			if (resource.Values.Count < 15) {
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
