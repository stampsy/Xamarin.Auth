using System;
using Xamarin.Auth;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Utilities;

namespace Xamarin.Auth
{
	/// <summary>
	/// Authenticator for obsolete specification OAuth Core 1.0.
	/// This specification was obsoleted by OAuth Core 1.0 Revision A on June 24th, 2009.
	/// 
	/// http://oauth.net/core/1.0/
	/// </summary>

	#if XAMARIN_AUTH_INTERNAL
	internal class OAuth1PreAAuthenticator : OAuth1Authenticator
	#else
	public class OAuth1PreAAuthenticator : OAuth1Authenticator
	#endif
	{
		public OAuth1PreAAuthenticator (string consumerKey, string consumerSecret, Uri requestTokenUrl, Uri authorizeUrl, Uri accessTokenUrl, Uri callbackUrl, GetUsernameAsyncFunc getUsernameAsync = null)
		: base (consumerKey, consumerSecret, requestTokenUrl, authorizeUrl, accessTokenUrl, callbackUrl, getUsernameAsync) { }

		public override Task<Uri> GetInitialUrlAsync () {
			var req = OAuth1.CreateRequest (
				"GET",
				requestTokenUrl,
				new Dictionary<string, string> (),
				consumerKey,
				consumerSecret,
				""
			);

			return req.GetResponseAsync ().ContinueWith (respTask => {

				var content = respTask.Result.GetResponseText ();

				var r = WebEx.FormDecode (content);

				token = r["oauth_token"];
				tokenSecret = r["oauth_token_secret"];

				string paramType = authorizeUrl.AbsoluteUri.IndexOf("?") >= 0 ? "&" : "?";

				var url = String.Format ("{0}{1}oauth_token={2}&oauth_callback={3}",
										 authorizeUrl.AbsoluteUri,
										 paramType,
										 Uri.EscapeDataString (token), 
										 Uri.EscapeDataString (callbackUrl.AbsoluteUri));

				return new Uri (url);
			});
		}

	}
}

