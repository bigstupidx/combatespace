using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class LoginManager : MonoBehaviour {

    private string id;
    private string facebookID;
    private string username;

    void Awake()
    {
        FB.Init(SetInit, OnHideUnity);
    }
    void Start()
    {
        if (!FB.IsLoggedIn)
        {
            SocialEvents.OnFacebookLoginPressed += OnFacebookLoginPressed;
        }
    }
    void SetInit()
    {
        if (FB.IsLoggedIn) {
            Debug.Log ("FB is logged in");
            FB.API("/me?fields=name", HttpMethod.GET, LogInDone);
            SocialManager.Instance.facebookFriends.GetFriends();
        } else {
            Debug.Log ("FB is not logged in");
        }
    }

    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    public void OnFacebookLoginPressed()
    {
        if (FB.IsLoggedIn) return;

        List<string> permissions = new List<string> ();
        permissions.Add ("public_profile");

        FB.LogInWithReadPermissions (permissions, AuthCallBack);
    }

    void AuthCallBack(IResult result)
    {
        if (result.Error != null) {
            Debug.Log (result.Error);
        } else {
            SetInit();
        }
    }
    void LogInDone(IResult result)
    {
        facebookID = result.ResultDictionary["id"].ToString();
        username = result.ResultDictionary["name"].ToString();
        Debug.Log("facebookID: " + facebookID + " username:" + username  );
        SocialEvents.OnFacebookLogin(facebookID, username, "");
    }
}
