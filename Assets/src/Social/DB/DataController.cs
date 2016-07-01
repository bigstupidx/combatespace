using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class DataController : MonoBehaviour
{
    private string secretKey = "ranlogic2008";
    const string URL = "http://www.pontura.com/combateSpace/";
    private string getUserIdByFacebookID_URL = URL + "getUserIdByFacebookID.php?";
    private string createUser_URL = URL + "createUser.php?";
    private string updateUser_URL = URL + "updateUser.php?";
    private string updatePeleas_URL = URL + "updatePeleas.php?";
    private string getUsersByScore_URL = URL + "getUsersByScore.php?";
    
    void Start()
    {        
        Events.OnUpdatePlayerData += OnUpdatePlayerData;
        Events.OnSavePelea += OnSavePelea;

        SocialEvents.OnFacebookLogin += OnFacebookLogin;
        SocialEvents.OnGetUsersByScore += OnGetUsersByScore;
    }
    void OnFacebookLogin(string facebookID, string username, string email)
    {
        print("onfaecbooklogin");
        StartCoroutine(CheckIfUserExistsOnLocalDB(facebookID, username, email));
    }
    IEnumerator CheckIfUserExistsOnLocalDB(string _facebookID, string _username, string _email)
    {
        if (_facebookID == "")
            yield break;

        string post_url = getUserIdByFacebookID_URL + "facebookID=" + _facebookID;

        print(post_url);

        WWW receivedData = new WWW(post_url);
        yield return receivedData;
        if (receivedData.error != null)
            print("There was an error in CheckIfUserExistsOnLocalDB: " + receivedData.error);
        else
        {
            try
            {
                string[] userData = Regex.Split(receivedData.text, ":");
                int userID = System.Int32.Parse(userData[1]);
                string username = userData[2];

                Stats stats = new Stats();
                stats.Power = System.Int32.Parse(userData[3]);
                stats.Resistence = System.Int32.Parse(userData[4]);
                stats.Defense = System.Int32.Parse(userData[5]);
                stats.Speed = System.Int32.Parse(userData[6]);

                int score = System.Int32.Parse(userData[7]);
                
                Peleas peleas = new Peleas();
                peleas.peleas_g = System.Int32.Parse(userData[8]);
                peleas.peleas_p = System.Int32.Parse(userData[9]);
                peleas.retos_g = System.Int32.Parse(userData[10]);
                peleas.retos_p = System.Int32.Parse(userData[11]);

                Data.Instance.playerSettings.heroData.stats = stats;
                Data.Instance.playerSettings.heroData.peleas = peleas;

                SocialEvents.OnUserReady(_facebookID, _username, _email);
            }
            catch
            {
                Debug.Log("new user!");
                CreateUser(_facebookID, _username, _email);
            }
        }
    }
    public void CreateUser(string _facebookID, string _username, string _email)
    {
        Stats stats = new Stats();
        stats.Power = 10;  stats.Resistence = 10; stats.Defense = 10;  stats.Speed = 10;
        Data.Instance.playerSettings.heroData.stats = stats;

        StartCoroutine(CreateUserRoutine(_facebookID, _username, _email));
    }
    IEnumerator CreateUserRoutine(string _facebookID, string _username, string _email)
    {
       // username = username.Replace(" ", "_");
        string hash = Md5Test.Md5Sum(_facebookID + _username  + secretKey);
        string post_url = createUser_URL + "username=" + WWW.EscapeURL(_username) + "&facebookID=" + WWW.EscapeURL(_facebookID) + "&hash=" + hash;
        print("CreateUser : " + post_url);
        WWW hs_post = new WWW(post_url);
        yield return hs_post;
        if (hs_post.error != null)
            print("No pudo crear el nuevo user: " + hs_post.error);
        else
        {
            print("user agregado: " + hs_post.text);
            int userId = int.Parse(hs_post.text);
            SocialEvents.OnUserReady(_facebookID, _username, _email);
        }
    }

    void OnUpdatePlayerData(PlayerData playerData)
    {
        StartCoroutine(UpdateUserRoutine( playerData ));
    }
    IEnumerator UpdateUserRoutine(PlayerData playerData)
    {
       // username = username.Replace(" ", "_");
        string hash = playerData.facebookID;

        if (playerData.nick != "")
            hash += playerData.nick;

        hash = Md5Test.Md5Sum(hash + secretKey);
        string post_url = updateUser_URL + "nick=" + WWW.EscapeURL(playerData.nick) + "&facebookID=" + playerData.facebookID + "&hash=" + hash;
        print("UpdateUser : " + post_url);
        WWW hs_post = new WWW(post_url);
        yield return hs_post;
        if (hs_post.error != null)
            print("Error haciendo update de user");
        else
        {
            print("user updateado: " + hs_post.text);

        }
    }




    void OnSavePelea(string facebookID, Peleas peleas)
    {
        StartCoroutine(OnUpdatePeleasRoutine(facebookID, peleas.peleas_g, peleas.peleas_p, peleas.retos_g, peleas.retos_p));
    }
    IEnumerator OnUpdatePeleasRoutine(string facebookID, int peleas_g, int peleas_p, int retos_g, int retos_p)
    {
        string hash = facebookID + peleas_g + peleas_p + retos_g + retos_p;
        hash = Md5Test.Md5Sum(hash + secretKey);
        string post_url = updatePeleas_URL + "facebookID=" + facebookID + "&peleas_g=" + peleas_g + "&peleas_p=" + peleas_p + "&retos_g=" + retos_g + "&retos_p=" + retos_p + "&hash=" + hash;
        print("OnUpdatePeleas : " + post_url);
        WWW hs_post = new WWW(post_url);
        yield return hs_post;
        if (hs_post.error != null) print("Error haciendo update de user"); else{ print("user updateado: " + hs_post.text); }
    }


    private System.Action<string> OnGetUsersByScoreListener;
    public void OnGetUsersByScore(System.Action<string> OnGetUsersByScoreListener, int min, int max)
    {
        this.OnGetUsersByScoreListener = OnGetUsersByScoreListener;
        StartCoroutine(GetUsersByScoreRoutine(min, max));
    }
    IEnumerator GetUsersByScoreRoutine(int min, int max)
    {
        string post_url = getUsersByScore_URL + "min=" + min + "&max=" + max;
        print("OnGetUsersByScore : " + post_url);
        WWW hs_post = new WWW(post_url);
        yield return hs_post;
        if (hs_post.error != null) print("Error con: GetUsersByScoreRoutine: " + hs_post.error); else {OnGetUsersByScoreListener(hs_post.text);}
    }

}

