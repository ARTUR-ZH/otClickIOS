using System;
using UIKit;
using System.Collections.Generic;
using Foundation;

namespace CompanyIOS
{
	public class CompaniesTableFill : UITableViewSource
	{
		protected SortedList<nint,string> tableItems;
		protected string cellIdentifier = "TableCell";
		CompaniesControl control;

		public CompaniesTableFill (SortedList<nint,string> items,CompaniesControl cc)
		{
			tableItems = items;
			control = cc;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return tableItems.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
			CompaniesTableCell cell = tableView.DequeueReusableCell (cellIdentifier) as CompaniesTableCell;
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new CompaniesTableCell (cellIdentifier);

			cell.UpdateCell ((indexPath.Row + 1).ToString (), tableItems.Values[indexPath.Row]);
			return cell;
		}
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var obid = tableItems.Keys [indexPath.Row];
			GraphicsController.Company = obid;
			control.PerformSegue ("SelectComp", this);		
		}
	}
}

