using System;
using UIKit;
using CoreGraphics;

namespace CompanyIOS
{
	public class CommentsTableViewStyle : UITableView
	{
		public CommentsTableViewStyle (CGRect frame)
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
			SeparatorColor = UIColor.FromRGB (239, 243, 243);
			SeparatorInset = UIEdgeInsets.Zero;
			DelaysContentTouches = true;
			CanCancelContentTouches = true;

		}


	}
}

