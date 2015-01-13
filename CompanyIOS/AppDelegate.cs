using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.Threading;
using Security;

namespace CompanyIOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
//		string _deviceToken;

		// class-level declarations

		public override UIWindow Window {
			get;
			set;
		}

//		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
//		{
			// NOTE: Don't call the base implementation on a Model class
			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
//			UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
//			UIApplication.SharedApplication.RegisterForRemoteNotificationTypes (notificationTypes); 
//			return true;
//		}

		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
//			this._deviceToken = deviceToken.ToString ();
			// code to register with your server application goes here
		}

		public override void FailedToRegisterForRemoteNotifications (UIApplication application, NSError error)
		{
			new UIAlertView ("Error registering push notifications", error.LocalizedDescription, null, "OK", null).Show ();
		}
		// This method is invoked when the application is about to move from active to inactive state.
		// OpenGL applications should use this method to pause.
		public override void OnResignActivation (UIApplication application)
		{
		}
		
		// This method should be used to release shared resources and it should store the application state.
		// If your application supports background exection this method is called instead of WillTerminate
		// when the user quits.
		public override void DidEnterBackground (UIApplication application)
		{
		}
		
		// This method is called as part of the transiton from background to active state.
		public override void WillEnterForeground (UIApplication application)
		{
		}
		
		// This method is called when the application is about to terminate. Save data, if needed.
		public override void WillTerminate (UIApplication application)
		{
		}

	}
}

