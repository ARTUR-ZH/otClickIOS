using System;
using UIKit;
using Foundation;
using System.Drawing;
using CoreGraphics;

namespace CompanyIOS
{
	public class RateTableVIewCellStyle : UITableViewCell
	{
		UILabel leftLabel, mainLabel, headerLabel;
		UIImageView callArrow;

		public RateTableVIewCellStyle (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			ContentView.BackgroundColor = UIColor.Clear;

			leftLabel = new UILabel () {
				Font = UIFont.FromName ("HelveticaNeue-Light", 12),
				TextAlignment = UITextAlignment.Left,
				TextColor = UIColor.Gray,
				BackgroundColor = UIColor.Clear
			};
			mainLabel = new UILabel () {
				Font = UIFont.FromName ("HelveticaNeue-Light", 12),
				TextColor = UIColor.DarkGray,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 2,
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear
			};

			headerLabel = new UILabel () {
				Font = UIFont.FromName ("HelveticaNeue-Light", 14),
				TextColor = UIColor.FromRGB (255, 110, 23),
				LineBreakMode = UILineBreakMode.WordWrap,
				Lines = 2,
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear
			};

			callArrow = new UIImageView (new UIImage ("стрелка.png"));
			callArrow.ContentMode = UIViewContentMode.ScaleAspectFit;
			callArrow.BackgroundColor = UIColor.Clear;
			ContentView.Add (callArrow);

			UIView horizontalLine = new UIView ();
			horizontalLine.Frame = new CGRect (0, 100, 320, 1);
			horizontalLine.BackgroundColor = UIColor.FromRGB (239, 243, 243);

			ContentView.Add (leftLabel);
			ContentView.Add (headerLabel);
			ContentView.Add (mainLabel);
			ContentView.Add (horizontalLine);
		}

		public void UpdateCell (NewsData data)
		{
			leftLabel.Text = data.Date;
			headerLabel.Text = data.Header;
			mainLabel.Text = data.Details;
		}



		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			leftLabel.Frame = new CoreGraphics.CGRect (10, 0, 80, 50);
			headerLabel.Frame = new CoreGraphics.CGRect (80, 0, 200, 50);
			mainLabel.Frame = new CoreGraphics.CGRect (80, 50, 200, 50);
			callArrow.Frame = new CGRect (300, 42.5f, 15, 15);
		}


	}
}

