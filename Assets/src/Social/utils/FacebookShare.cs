using System;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;


public class FacebookShare : MonoBehaviour
{

    string linkName = "Combate Space";
   // private string feedLink = "https://itunes.apple.com/us/app/running-tap-challenges/id987539773?ls=1&mt=8";
    private string feedLink = "";
    private string feedTitle = "Combate Space";
    private string feedCaption = "Combate Space";
    private string feedDescription = "Compite contra tus amigos en la liga Combate Space";
   // private string feedImage = "http://tipitap.com/running-icon.jpg";
    private string feedImage = "";
    private string feedMediaSource = string.Empty;


    public void ShareToFriend(string friend_facebookID, string linkCaption)
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("ShareToFriend: " + friend_facebookID + " linkCaption: " + linkCaption + " linkName: " + linkName);

            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            string feedLink = "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag=" + (FB.IsLoggedIn ? aToken.UserId : "guest");

            FB.FeedShare(
                    friend_facebookID,
                    string.IsNullOrEmpty(this.feedLink) ? null : new Uri(feedLink),
                    linkName,
                  //  linkCaption,
                    null,
                    this.feedDescription,
                    string.IsNullOrEmpty(this.feedImage) ? null : new Uri(this.feedImage),
                    this.feedMediaSource,
                    this.HandleResult);
        }
    }
    public void WinTo(string linkCaption)
    {
        if (FB.IsLoggedIn)
        {
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            string feedLink = "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag=" + (FB.IsLoggedIn ? aToken.UserId : "guest");
            FB.ShareLink(
                   new Uri(feedLink),
                   "Challenges",
                   linkCaption,
                   string.IsNullOrEmpty(this.feedImage) ? null : new Uri(this.feedImage),
                   this.HandleResult);
        }
    }
    public void NewHiscore(string linkCaption)
    {
        if (FB.IsLoggedIn)
        {
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            string feedLink = "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag=" + (FB.IsLoggedIn ? aToken.UserId : "guest");


            FB.ShareLink(
                   new Uri(feedLink),
                   "New Hiscore",
                   linkCaption,
                   string.IsNullOrEmpty(this.feedImage) ? null : new Uri(this.feedImage),
                   this.HandleResult);
        }
    }
    protected void HandleResult(IResult result)
    {
        Debug.Log(result);
        if (result == null)
        {

        }
    }
}
