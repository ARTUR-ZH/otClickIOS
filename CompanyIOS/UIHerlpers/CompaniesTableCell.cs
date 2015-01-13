using System;
using UIKit;
using Foundation;
using System.Drawing;
using CoreGraphics;

namespace CompanyIOS
{
	public class CompaniesTableCell : UITableViewCell
	{
		UILabel leftLabel, mainLabel;

		public CompaniesTableCell (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;
			UserInteractionEnabled = true;

			ContentView.BackgroundColor = UIColor.Clear;

			leftLabel = new UILabel () {
				Font = UIFont.FromName ("HelveticaNeue-Bold", 18f),
				TextAlignment = UITextAlignment.Center,
				TextColor = UIColor.Gray,
				BackgroundColor = UIColor.LightGray
			};
			mainLabel = new UILabel () {
				Font = UIFont.FromName ("HelveticaNeue-Light", 18f),
				TextColor = UIColor.DarkGray,
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.Clear
			};

			ContentView.Add (leftLabel);
			ContentView.Add (mainLabel);
		}

		public void UpdateCell (string left, string main)
		{
			leftLabel.Text = left;
			mainLabel.Text = main;
		}

		void SelectRow ()
		{
			BackgroundColor = UIColor.FromRGB(255, 110 ,23);
			leftLabel.BackgroundColor = UIColor.Clear;
			leftLabel.TextColor = UIColor.White;
			mainLabel.TextColor = UIColor.White;
			mainLabel.Font = UIFont.FromName ("HelveticaNeue-Bold", 18f);

		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			leftLabel.Frame = new CGRect (0, 0, 50, 50);
			mainLabel.Frame = new CGRect (60, 0, 270, 50);
		}



	}
}

