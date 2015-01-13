using System;
using UIKit;
using System.Collections.Generic;
using Foundation;

namespace CompanyIOS
{
	public class RateTableSourceFill : UITableViewSource
	{
		protected List<string> tableItems;
		protected string cellIdentifier = "TableCell";
		public RateTableSourceFill (List<string> items)
		{
			tableItems = items;
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
			cell.UpdateCell ((indexPath.Row + 1).ToString (), tableItems [indexPath.Row]);
			return cell;
		}
	}
}

