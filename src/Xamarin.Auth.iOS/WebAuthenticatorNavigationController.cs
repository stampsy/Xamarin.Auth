using System;
using MonoTouch.UIKit;

namespace Xamarin.Auth
{
	/// <summary>
	/// Navigation controller that the web controller will be wrapped into when presented to user.
	/// This class is public so clients can use it with UIAppearance.
	/// </summary>
	public class WebAuthenticatorNavigationController : UINavigationController
	{
		public WebAuthenticatorNavigationController (IntPtr handle)
			: base (handle)
		{
		}

		public WebAuthenticatorNavigationController (UIViewController rootViewController)
			: base (rootViewController)
		{
		}
	}
}