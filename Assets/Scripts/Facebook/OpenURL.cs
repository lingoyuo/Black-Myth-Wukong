using UnityEngine;
using System.Collections;

[System.Serializable]
public class UrlPlatform{
	public string ios;
	public string android;

	public void OpenURL()
	{
		if(Application.platform == RuntimePlatform.Android)
		{
			Application.OpenURL (android);
		}
		else if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			Application.OpenURL (ios);
		}
	}
}

public class OpenURL : MonoBehaviour {
	public UrlPlatform moreGame;
	public UrlPlatform rateGame;
	public UrlPlatform likeGame;

	public void RateGame()
	{
		rateGame.OpenURL ();
	}

	public void MoreGame()
	{
		moreGame.OpenURL ();
	}

	public void LikeGame()
	{
		likeGame.OpenURL ();
	}
}
