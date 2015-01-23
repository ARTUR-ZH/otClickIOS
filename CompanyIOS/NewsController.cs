
using System;
using System.Drawing;

using Foundation;
using UIKit;
using ObjCRuntime;

namespace CompanyIOS
{
	[Register("NewsController")]
	public partial class NewsController : UIViewController
	{
		NSUrlRequest _Url;
		UIBarButtonItem _CloseButton;
		UIWebView _GoogleWebView;
		private UIActivityIndicatorView _SearchIndicator; 
		public NewsController (string url)
		{
			var nsUrl = new NSUrl (url);
			this._Url = new NSUrlRequest (nsUrl);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			if (RespondsToSelector (new Selector ("edgesForExtendedLayout")))
				EdgesForExtendedLayout = UIRectEdge.Top;

			this._SearchIndicator = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.Gray);
			this.NavigationItem.RightBarButtonItems = new UIBarButtonItem[] { new UIBarButtonItem (this._SearchIndicator) };


			_GoogleWebView = new UIWebView (this.View.Frame);
			_GoogleWebView.ScalesPageToFit = true;
			this.Add (_GoogleWebView);
			_GoogleWebView.LoadRequest (_Url);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			_GoogleWebView.LoadStarted += HandleLoadStarted;
			_GoogleWebView.LoadFinished += HandleLoadFinished;
		}

		void HandleLoadFinished (object sender, EventArgs e)
		{
			_SearchIndicator.StopAnimating ();
		}

		void HandleLoadStarted (object sender, EventArgs e)
		{
			_SearchIndicator.StartAnimating ();
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			_GoogleWebView.LoadStarted -= HandleLoadStarted;
			_GoogleWebView.LoadFinished -= HandleLoadFinished;
		}

		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			_GoogleWebView.StopLoading ();
			_GoogleWebView.RemoveFromSuperview ();
		}		
	}
}

