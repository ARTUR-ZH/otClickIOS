using System;
using UIKit;
using System.Drawing;
using Foundation;
using CoreGraphics;
using System.Collections.Generic;

namespace CompanyIOS
{
	public class CommentsStyleCell : UITableViewCell
	{
		protected UILabel leftLabelNumber;
		protected UIButton mainButton, showTextView;
		protected UIView verticalLine, horizontalLine;
		protected UIImageView callImage, callArrow;
		protected CircleCommentsView circleView;
		protected nint cost, quality, service;
		protected UITextView comment;
		protected ClickedButtonAtIndex clickedButtonAtIndex;
		CommentsController parent;
		Dictionary<string,string> _Data;

		public CommentsStyleCell (string cellId, String left, string main, string text, CommentsController parent, Dictionary<string,string> data) : base (UITableViewCellStyle.Default, cellId)
		{
			ClearsContextBeforeDrawing = false;
			ContentView.BackgroundColor = UIColor.Clear;
			SelectionStyle = UITableViewCellSelectionStyle.None;
			ContentView.Frame = new CGRect (0, 0, 320, 200);
			UserInteractionEnabled = true;
			CreateStyles (left, main, text, parent, data);
			_Data = data;
		}

		void CreateStyles (String left, string main, string text, CommentsController parent, Dictionary<string,string> data)
		{
			this.parent = parent;
			/*-----------Номер промокода----------*/
			/*-----------Номер промокода----------*/
			/*-----------Номер промокода----------*/
			leftLabelNumber = new UILabel () {
				Font = UIFont.FromName ("HelveticaNeue-Bold", 16f),
				TextAlignment = UITextAlignment.Center,
				TextColor = UIColor.Gray,
				BackgroundColor = UIColor.Clear,
				Text = left
			};
			ContentView.Add (leftLabelNumber);

			/*-----------Вертикальная линия----------*/
			/*-----------Вертикальная линия----------*/
			/*-----------Вертикальная линия----------*/
			verticalLine = new UIView ();
			verticalLine.BackgroundColor = UIColor.FromRGB (239, 243, 243);
			ContentView.Add (verticalLine);		

			if (!string.IsNullOrWhiteSpace (main)) {
				/*----------Номер телефона----------*/
				/*----------Номер телефона----------*/
				/*----------Номер телефона----------*/
				mainButton = new UIButton () {
					Font = UIFont.FromName ("HelveticaNeue-Light", 16f),
					BackgroundColor = UIColor.Clear,			
				};
				mainButton.SetTitle (main, UIControlState.Normal);
				mainButton.SetTitleColor (UIColor.FromRGB (255, 110, 23), UIControlState.Normal);
				mainButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
				mainButton.VerticalAlignment = UIControlContentVerticalAlignment.Center;
				mainButton.TouchUpInside += TouchPhoneNumber;
				ContentView.Add (mainButton);

				/*----------Картинка стрелочки номера----------*/
				/*----------Картинка стрелочки номера----------*/
				/*----------Картинка стрелочки номера----------*/
				callArrow = new UIImageView (new UIImage ("стрелка.png"));
				callArrow.ContentMode = UIViewContentMode.ScaleAspectFit;
				callArrow.BackgroundColor = UIColor.Clear;
				ContentView.Add (callArrow);

				/*-----------Картинка трубки телефона----------*/
				/*-----------Картинка трубки телефона----------*/
				/*-----------Картинка трубки телефона----------*/
				callImage = new UIImageView (new UIImage ("tel.png"));
				callImage.ContentMode = UIViewContentMode.ScaleAspectFit;
				callImage.BackgroundColor = UIColor.Clear;
				ContentView.Add (callImage);
			} else {
				/*----------Номер телефона----------*/
				/*----------Номер телефона----------*/
				/*----------Номер телефона----------*/
				mainButton = new UIButton () {
					Font = UIFont.FromName ("HelveticaNeue-Light", 16f),
					BackgroundColor = UIColor.Clear,			
				};
				mainButton.Hidden = true;
				mainButton.SetTitle (main, UIControlState.Normal);
				mainButton.SetTitleColor (UIColor.FromRGB (255, 110, 23), UIControlState.Normal);
				mainButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
				mainButton.VerticalAlignment = UIControlContentVerticalAlignment.Center;
				mainButton.TouchUpInside += TouchPhoneNumber;
				ContentView.Add (mainButton);

				/*----------Картинка стрелочки номера----------*/
				/*----------Картинка стрелочки номера----------*/
				/*----------Картинка стрелочки номера----------*/
				callArrow = new UIImageView (new UIImage ("стрелка.png"));
				callArrow.Hidden = true;
				callArrow.ContentMode = UIViewContentMode.ScaleAspectFit;
				callArrow.BackgroundColor = UIColor.Clear;
				ContentView.Add (callArrow);

				/*-----------Картинка трубки телефона----------*/
				/*-----------Картинка трубки телефона----------*/
				/*-----------Картинка трубки телефона----------*/
				callImage = new UIImageView (new UIImage ("tel.png"));
				callImage.Hidden = true;
				callImage.ContentMode = UIViewContentMode.ScaleAspectFit;
				callImage.BackgroundColor = UIColor.Clear;
				ContentView.Add (callImage);
			}


			/*----------Горизонтальная линия----------*/
			/*----------Горизонтальная линия----------*/
			/*----------Горизонтальная линия----------*/
			horizontalLine = new UIView ();
			horizontalLine.BackgroundColor = UIColor.FromRGB (239, 243, 243);
			ContentView.Add (horizontalLine);

			/*----------ГРАФИКИ----------*/
			/*----------ГРАФИКИ----------*/
			/*----------ГРАФИКИ----------*/
			circleView = new CircleCommentsView (data);
			ContentView.Add (circleView);

			/*----------КОММЕНТАРИИ И КНОПКА ДЛЯ ПЕРЕХОДА----------*/
			/*----------КОММЕНТАРИИ И КНОПКА ДЛЯ ПЕРЕХОДА----------*/
			/*----------КОММЕНТАРИИ И КНОПКА ДЛЯ ПЕРЕХОДА----------*/

			if (!string.IsNullOrWhiteSpace (text)) { //Пустая ли строка
				if (text.Length >= 115) { /* Если не пустая то больше чем 114 символов*/
					comment = new UITextView {
						Text = IfMyCommentsWillTruncated (text),
						UserInteractionEnabled = false,
						BackgroundColor = UIColor.Clear,
						TextColor = UIColor.Black,
						Font = UIFont.FromName ("HelveticaNeue-Light", 12f)
					};
					CommentsController.CommentsText = text;
				} else { // Если нет то не обрезаем 
					comment = new UITextView {
						Text = text,
						UserInteractionEnabled = false,
						BackgroundColor = UIColor.Clear,
						TextColor = UIColor.Black,
						Font = UIFont.FromName ("HelveticaNeue-Light", 12f)
					};
				}
			} else { // Если пустая то пишем дефолтный текст
				comment = new UITextView {
					Text = "Нету комментраия",
					UserInteractionEnabled = false,
					BackgroundColor = UIColor.Clear,
					TextColor = UIColor.LightGray,
					Font = UIFont.FromName ("HelveticaNeue-Light", 12f)
				};
			}
			ContentView.Add (comment);

			showTextView = new UIButton () {
				Font = UIFont.FromName ("HelveticaNeue-Light", 16f),
				BackgroundColor = UIColor.Clear,
				UserInteractionEnabled = true,
			};
			showTextView.Hidden = true;
			showTextView.SetTitle ("Далее >", UIControlState.Normal);
			showTextView.SetTitleColor (UIColor.FromRGB (152, 87, 162), UIControlState.Normal);
			showTextView.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
			showTextView.VerticalAlignment = UIControlContentVerticalAlignment.Center;
			if (!string.IsNullOrWhiteSpace (text)) {
				if (text.Length >= 115) {
					showTextView.Hidden = false;
					showTextView.TouchUpInside += PushComment;
				}
			}


			ContentView.Add (showTextView);
		}



		public void UpdateCell (String left, string main, string text, CommentsController parent,Dictionary<string,string> data)
		{	
			showTextView.Hidden = true;
			leftLabelNumber.Text = left;
			if (!string.IsNullOrWhiteSpace (main)) {
				mainButton.Hidden = false;
				callImage.Hidden = false;
				callArrow.Hidden = false;
				mainButton.SetTitle (main, UIControlState.Normal);
				mainButton.TouchUpInside -= TouchPhoneNumber;
				mainButton.TouchUpInside += TouchPhoneNumber;
			} else {
				mainButton.Hidden = true;
				callImage.Hidden = true;
				callArrow.Hidden = true;
				mainButton.TouchUpInside -= TouchPhoneNumber;
			}

			circleView.RemoveFromSuperview ();
			circleView = new CircleCommentsView (data);
			ContentView.Add (circleView);

			if (!string.IsNullOrWhiteSpace (text)) {
				if (text.Length >= 115) {
					comment.Text = IfMyCommentsWillTruncated (text);
					comment.TextColor = UIColor.Black;
					CommentsController.CommentsText = text;
					showTextView.Hidden = false;
					showTextView.TouchUpInside -= PushComment;
					showTextView.TouchUpInside += PushComment;					
				} else {
					comment.Text = text;
					comment.TextColor = UIColor.Black;
				}
			} else {
				comment.Text = "Нету комментария";
				comment.TextColor = UIColor.LightGray;
			}

		}

		void PushComment (object sender, EventArgs e)
		{
			parent.PerformSegue ("comments", parent);
		}

		void TouchPhoneNumber (object sender, EventArgs e)
		{
			mainButton = sender as UIButton;
			try {
				string main = mainButton.Title(UIControlState.Normal);
				clickedButtonAtIndex = new ClickedButtonAtIndex (main);
				UIAlertView av = new UIAlertView ("Предупреждение",
					string.Format ("Вы точно хотите набрать {0}", main),
					clickedButtonAtIndex,
					"Отменить",
					"Позвонить"
				);
				av.Show ();
			} catch (NullReferenceException ex) {
				Console.WriteLine (ex.Message);
			}
		}
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			leftLabelNumber.Frame = new CGRect (0, 0, 54, 50);
			mainButton.Frame = new CGRect (100, 0, 200, 50);
			verticalLine.Frame = new CGRect (60, 5, 1, 40);
			horizontalLine.Frame = new CGRect (0, 50, 320, 1);
			circleView.Frame = new CGRect (0, 50, 320, _Data.Count * 60);
			callImage.Frame = new CGRect (70, 15, 20, 20);
			callArrow.Frame = new CGRect (300, 17.5f, 15, 15);
			comment.Frame = new CGRect (20, _Data.Count * 60 + 50, 280, 60);
			showTextView.Frame = new CGRect (240, _Data.Count * 60 + 60, 70, 20);
		}

		string IfMyCommentsWillTruncated (string text)
		{
			return text = string.Format (text.Substring (0, 115) + "...");
		}
	}
}

