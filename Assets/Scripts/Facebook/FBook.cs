using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using Facebook.Unity;
using System;

public class FBook : MonoBehaviour
{
	public string linkAndroid = "https://play.google.com/store/apps/details?id=com.vietbrain.SuperKongSaga";
	public string linkIOS = "https://itunes.apple.com/lt/app/super-kong-saga-magic-monkey/id1065736101";
	public string linkPicture = "https://scontent-hkg3-1.xx.fbcdn.net/hphotos-xtp1/v/t1.0-9/11667440_494793770676666_4943243402128542675_n.jpg?oh=dabd1ec68a80665a348c2d161f98bdbd&oe=578AC7AE";
	public string linkAppFb = "https://fb.me/843568162438930";

	string linkShare = "";
	void Awake ()
	{
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init (InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp ();
		}

		if(Application.platform == RuntimePlatform.Android)
		{
			linkShare = linkAndroid;
		}
		else
			if(Application.platform == RuntimePlatform.IPhonePlayer)
			{
			linkShare = linkIOS;
			}
	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp ();
			// Continue with Facebook SDK
			// ...
		} else {
			Debug.Log ("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
		} else {
			// Resume the game - we're getting focus again
		}
	}

	public void ShareLink ()
	{
		FB.FeedShare (
			string.Empty, //toId
			new System.Uri (linkShare), //link
			string.Empty, //linkName
			string.Empty, //linkCaption
			string.Empty, //linkDescription
			new System.Uri (linkPicture), //picture
			string.Empty, //mediaSource
			FeedCallback //callback
		);
	}

	private void FeedCallback (IShareResult result)
	{
		
	}

	public void Invite()
	{
		FB.Mobile.AppInvite (new Uri (linkAppFb), callback: InviteCallback);
	}

	private void InviteCallback (IAppInviteResult result)
	{

	}
}





