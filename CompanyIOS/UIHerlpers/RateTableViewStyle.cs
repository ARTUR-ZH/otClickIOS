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
			AllowsSelection = true;
			AllowsMultipleSelection = false;
			SeparatorStyle = UITableViewCellSeparatorStyle.None;
			SeparatorColor = UIColor.LightGray;
			RowHeight = 100;
			SeparatorInset = UIEdgeInsets.Zero;
		}
	}
}

