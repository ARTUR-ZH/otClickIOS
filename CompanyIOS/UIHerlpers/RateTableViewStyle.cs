using System;
using UIKit;
using CoreGraphics;

namespace CompanyIOS
{
	public class RateTableViewStyle : UITableView
	{
		public RateTableViewStyle (CGRect frame)
		{
			Initialize (frame);
		}

		void Initialize (CGRect frame)
		{
			Frame = frame;
			AutoresizingMask = UIViewAutoresizing.All;
			ScrollEnabled = true;
			SectionFooterHeight = 0;
			AllowsSelection = false;
			AllowsMultipleSelection = false;
			SeparatorColor = UIColor.White;
			RowHeight = 50;
			SeparatorInset = UIEdgeInsets.Zero;
		}
	}
}

