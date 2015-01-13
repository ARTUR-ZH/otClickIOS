using System;
using UIKit;
using CoreGraphics;

namespace CompanyIOS
{
	public class ComapniesTableView : UITableView
	{
		public ComapniesTableView (CGRect frame)
		{
			Initialize (frame);
		}

		void Initialize (CGRect frame)
		{
			UserInteractionEnabled = true;
			Frame = frame;
			AutoresizingMask = UIViewAutoresizing.All;
			ScrollEnabled = true;
			SectionFooterHeight = 0;
			AllowsSelection = true;
			AllowsMultipleSelection = false;
			SeparatorColor = UIColor.White;
			RowHeight = 50;
			SeparatorInset = UIEdgeInsets.Zero;
			DelaysContentTouches = true;
			CanCancelContentTouches = true;
		}
	

		 
	}
}

