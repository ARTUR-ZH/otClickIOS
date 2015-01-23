using System;
using UIKit;
using System.Collections.Generic;
using Foundation;

namespace CompanyIOS
{
	public class RateTableSourceFill : UITableViewSource
	{
		protected List<NewsData> tableItems;
		protected string cellIdentifier = "TableCell";
		UIViewController parent;
		public RateTableSourceFill (List<NewsData> items, UIViewController parentVU)
		{
			tableItems = items;
			parent = parentVU;
		}
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return tableItems.Count;
		}
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
			RateTableVIewCellStyle cell = tableView.DequeueReusableCell (cellIdentifier) as RateTableVIewCellStyle;
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new RateTableVIewCellStyle (cellIdentifier);
			cell.UpdateCell (tableItems [indexPath.Row]);
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow (indexPath, true); // iOS convention is to remove the highlight

			// parent is a reference to the UITableViewController - you will need to pass this into
			// your TableSource class.  Every ViewController has a NavigationController property - if
			// the VC is contained in a Nav controller this property will be set.
			parent.NavigationController.PushViewController(new NewsController(tableItems[indexPath.Row].TextUrl), true);
		}
	}
}

