using System;
using UIKit;
using System.Drawing;
using System.Threading.Tasks;
using CoreGraphics;

namespace CompanyIOS
{
	public class LoadingOverlay : UIView
	{
		// control declarations
		UIActivityIndicatorView activitySpinner;
		UILabel loadingLabel;

		public LoadingOverlay (CGRect frame) : base (frame)
		{
			
			BackgroundColor = UIColor.Black;
			Alpha = (nfloat)0.75;
			AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

			nfloat labelHeight = 22;
			nfloat labelWidth = Frame.Width - 20;

			// derive the center x and y
			nfloat centerX = Frame.Width / 2;
			nfloat centerY = Frame.Height / 2;

			// create the activity spinner, center it horizontall and put it 5 points above center x
			activitySpinner = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.WhiteLarge);
			activitySpinner.Frame = new CGRect (
				centerX - (activitySpinner.Frame.Width / 2),
				centerY - activitySpinner.Frame.Height - 20,
				activitySpinner.Frame.Width,
				activitySpinner.Frame.Height);
			activitySpinner.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			AddSubview (activitySpinner);
			activitySpinner.StartAnimating ();

			// create and configure the "Loading Data" label				
		}

		/// <summary>
		/// Fades out the control and then removes it from the super view
		/// </summary>
		public void Hide ()
		{
			UIView.Animate (
				0.75, // duration
				() => {
					Alpha = 0;
				},
				() => {
					RemoveFromSuperview ();
				}
			);

		}
	};
}

