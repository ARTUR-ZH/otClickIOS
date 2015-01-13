using System;
using UIKit;
using Foundation;
using System.Drawing;

namespace CompanyIOS
{
	public class RateTableVIewCellStyle : UITableViewCell
	{
		UILabel leftLabel, mainLabel;

		public RateTableVIewCellStyle (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
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
			Deselect ();
			leftLabel.Text = left;
			mainLabel.Text = main;
			if (main.ToUpper().Contains("Моя компания".ToUpper())) {
				SelectRow ();
			}
		}

		void SelectRow ()
		{
			BackgroundColor = UIColor.FromRGB(255, 110 ,23);
			leftLabel.BackgroundColor = UIColor.Clear;
			leftLabel.TextColor = UIColor.White;
			mainLabel.TextColor = UIColor.White;
			mainLabel.Font = UIFont.FromName ("HelveticaNeue-Bold", 18f);

		}

		void Deselect ()
		{
			BackgroundColor = UIColor.White;
			leftLabel.BackgroundColor = UIColor.LightGray;
			leftLabel.TextColor = UIColor.Gray;
			mainLabel.TextColor = UIColor.DarkGray;
			mainLabel.Font = UIFont.FromName ("HelveticaNeue-Light", 18f);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			leftLabel.Frame = new RectangleF (0, 0, 50, 50);
			mainLabel.Frame = new RectangleF (60, 0, 270, 50);
		}
	}
}

