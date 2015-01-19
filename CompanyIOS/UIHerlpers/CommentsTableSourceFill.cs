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

//		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
//		{
//			throw new System.NotImplementedException ();
//		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// request a recycled cell to save memory
			CommentsStyleCell cell = tableView.DequeueReusableCell (cellIdentifier) as CommentsStyleCell;
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new CommentsStyleCell (cellIdentifier, tableItems [indexPath.Row].Promocode, tableItems [indexPath.Row].Phone, tableItems [indexPath.Row].Comment, parentController, tableItems [indexPath.Row].Data);
			else {
				cell.UpdateCell (tableItems [indexPath.Row].Promocode, tableItems [indexPath.Row].Phone, tableItems [indexPath.Row].Comment, parentController, tableItems [indexPath.Row].Data);
			}
			return cell;
		}

	}
}

