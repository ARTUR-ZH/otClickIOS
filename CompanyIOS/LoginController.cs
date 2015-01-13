using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using System.Drawing;
using ObjCRuntime;
using Security;
using CoreGraphics;

namespace CompanyIOS
{
	partial class LoginController : UIViewController
	{
		protected int _keyboardOffset;
		private SecRecord existingRec;
		protected LoadingOverlay loadingOverlay;

		public LoginController (IntPtr handle) : base (handle)
		{
			_keyboardOffset = 80;
		}

		Tuple<string,ServiceCallbackHelper> data;

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			Dispose (true);
			//_keyboardOffset = 0;
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			existingRec = new SecRecord (SecKind.GenericPassword) { 
				Service = KeyChain.Service, 
				Label = KeyChain.Label 
			};
			SecStatusCode code;
			SecRecord match = SecKeyChain.QueryAsRecord (existingRec, out code);

			if (code == SecStatusCode.Success) {
				txtLogin.Text = match.Account.ToString ();
				txtPassword.Text = match.ValueData.ToString ();
			}





			txtLogin.EditingDidBegin += textFieldDidBeginEditing;
			txtPassword.EditingDidBegin += textFieldDidBeginEditing;
			btnLogin.TouchUpInside += btnLoginTouchUpInside;


			CreateLeftView ("логин.png", txtLogin);
			CreateLeftView ("пароль.png", txtPassword);

		}

		private void CreateLeftView (string png, UITextField textField)
		{
			UIImageView viewLeftOverlay = new UIImageView (new UIImage (png));
			viewLeftOverlay.Frame = new RectangleF (0, 0, 35, 15);
			viewLeftOverlay.ContentMode = UIViewContentMode.ScaleAspectFit;
			viewLeftOverlay.BackgroundColor = UIColor.Clear;		
			textField.LeftView = viewLeftOverlay;
		}

		void textFieldDidBeginEditing (Object sender, EventArgs e)
		{
			if (sender == txtLogin || sender == txtPassword) {
				if (this.View.Frame.Y >= 0) {
					this.setViewMovedUp (true);
				}
			}
		}

		[Export ("keyboardWillShow")]
		void keyboardWillShow ()
		{
			if (this.View.Frame.Y >= 0) {
				this.setViewMovedUp (true);
			} else if (this.View.Frame.Y < 0) {
				this.setViewMovedUp (false);
			}
		}

		[Export ("keyboardWillHide")]
		void keyboardWillHide ()
		{
			if (this.View.Frame.Y >= 0) {
				this.setViewMovedUp (true);
			} else if (this.View.Frame.Y < 0) {
				this.setViewMovedUp (false);
			}
		}

		void setViewMovedUp (bool b)
		{
			UIView.BeginAnimations (null);
			UIView.SetAnimationDuration (0.3); 

			CGRect rect = this.View.Frame;

			if (b) {
				rect.Y -= (float)_keyboardOffset;//				
			} else {
				rect.Y += (float)_keyboardOffset;//				
			}
			this.View.Frame = rect;

			UIView.CommitAnimations ();
		}


		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
			this.View.EndEditing (true);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			NSNotificationCenter.DefaultCenter.AddObserver (this, new Selector ("keyboardWillShow"), (NSString)"UIKeyboardWillShowNotification", null);
			NSNotificationCenter.DefaultCenter.AddObserver (this, new Selector ("keyboardWillHide"), (NSString)"UIKeyboardWillHideNotification", null);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			NSNotificationCenter.DefaultCenter.RemoveObserver (this, "UIKeyboardWillShowNotification", null);
			NSNotificationCenter.DefaultCenter.RemoveObserver (this, "UIKeyboardWillSHideNotification", null);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		async void btnLoginTouchUpInside (Object sender, EventArgs e)
		{
			btnLogin.Enabled = false;

			if (txtPassword.Text.ToUpper () == "" || txtLogin.Text.ToUpper () == "") {
				UIAlertView error = new UIAlertView ("Ошибка", "Введите пожалуйста логин или пароль.", null, "Закрыть", null);
				error.Show ();
				btnLogin.Enabled = true;
			} else {
				HttpServiceConn newconn = new HttpServiceConn ();
				string md5hash = md5securitycobject.ComputeHash (txtLogin.Text + txtPassword.Text);
				loadingOverlay = new LoadingOverlay (new System.Drawing.RectangleF (0, 0, 320, 568));
				View.Add (loadingOverlay);
				data = await newconn.WebRequestServiceAsync (md5hash, txtLogin.Text, txtPassword.Text);
				loadingOverlay.Hide ();
				try {
					if ((string.IsNullOrWhiteSpace (data.Item1)) && data != null) {
						ServiceCallBack (data.Item2);
					} else {
						btnLogin.Enabled = true;
						if (data.Item2.Status == "error") {
							if (data.Item2.Error != null) {
								UIAlertView error = new UIAlertView ("Ошибка", data.Item2.Error.ErrorDescription, null, "Закрыть", null);
								error.Show ();
							} else {
								if (data.Item1 != null && data.Item1.Contains ("ConnectFailure")) {
									UIAlertView error = new UIAlertView ("Ошибка", "Пожалуйста проверьте ваше интернет-соединение.", null, "Закрыть", null);
									error.Show ();
								}else{
									if (!string.IsNullOrWhiteSpace(data.Item1)) {
										UIAlertView error = new UIAlertView ("Ошибка", data.Item1, null, "Закрыть", null);
										error.Show ();
									}
								}
							}
						} else {
							if (data.Item1 != null && data.Item1.Contains ("ConnectFailure")) {
								UIAlertView error = new UIAlertView ("Ошибка", "Пожалуйста проверьте ваше интернет-соединение.", null, "Закрыть", null);
								error.Show ();
							} else {
								UIAlertView error = new UIAlertView ("Ошибка", "Пожалуйста обратитесь к администратору.", null, "Закрыть", null);
								error.Show ();
							}
						}
					}
				} catch (Exception ex) {
					UIAlertView error = new UIAlertView ("Ошибка", ex.Message , null, "Закрыть", null);
					error.Show ();
				}

			}
		}

		public void ServiceCallBack (ServiceCallbackHelper data)
		{
			if (data.Status == "error") {
				UIAlertView error = new UIAlertView ("Ошибка", "Вы неправильно ввели логин или пароль.Попробуйте снова.", null, "Закрыть", null);
				error.Show ();
				btnLogin.Enabled = true;
			} else {
				var record = new SecRecord (SecKind.GenericPassword) {
					Description = KeyChain.Description,
					Comment = KeyChain.Comment,
					Service = KeyChain.Service, 
					Label = KeyChain.Label, 
					Account = txtLogin.Text,
					ValueData = NSData.FromString (txtPassword.Text),
					Generic = NSData.FromString(KeyChain.Generic),
					Accessible = SecAccessible.Always
				};
				SecStatusCode code = SecKeyChain.Add (record);
				if (code == SecStatusCode.DuplicateItem) {
					code = SecKeyChain.Remove (existingRec);
				}
				if (code == SecStatusCode.Success)
					code = SecKeyChain.Add (record);


				PerformSegue ("autherication", this);
			}
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);
			if (segue.Identifier.ToUpper ().Equals ("autherication".ToUpper ())) {
				GraphicsController.Token = data.Item2.Resource.Token;
			}
		}
	}
}
