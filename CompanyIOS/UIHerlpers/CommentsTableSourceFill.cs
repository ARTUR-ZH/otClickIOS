using System;
using UIKit;
using Foundation;
using System.Collections.Generic;

namespace CompanyIOS
{
	public class CommentsTableSourceFill : UITableViewSource
	{
		protected List<DataResource> tableItems;
		protected readonly string cellIdentifier = "TableCell";
		private CommentsController parentController;

		public CommentsTableSourceFill (List<DataResource> items, CommentsController parent)
		{
			tableItems = items;
			this.parentController = parent;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return tableItems.Count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
			CommentsStyleCell cell = tableView.DequeueReusableCell (cellIdentifier) as CommentsStyleCell;
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new CommentsStyleCell (cellIdentifier, tableItems [indexPath.Row].Promocode, tableItems [indexPath.Row].Data.Phone, tableItems [indexPath.Row].Data.ServiceCost, tableItems [indexPath.Row].Data.ServiceQuality, tableItems [indexPath.Row].Data.Service, tableItems [indexPath.Row].Data.Comment, parentController);
			else {
				cell.UpdateCell (tableItems [indexPath.Row].Promocode, tableItems [indexPath.Row].Data.Phone, tableItems [indexPath.Row].Data.ServiceCost, tableItems [indexPath.Row].Data.ServiceQuality, tableItems [indexPath.Row].Data.Service, tableItems [indexPath.Row].Data.Comment, parentController);
			}
			return cell;
		}

	}
}

