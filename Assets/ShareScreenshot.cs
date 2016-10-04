using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
 
public class ShareScreenshot : MonoBehaviour {
  
    private bool isProcessing = false;

    private string shareText  = "Which Hollywood Movie does this PICTURE represent?\n";
    private string gameLink = "Download the game on play store at "+"\nhttps://play.google.com/store/apps/details?id=com.TGC.guessthemovie&pcampaignid=GPC_shareGame";
    private string subject = "Rebus Guess The Movie Game";
    private string imageName = "Mypic"; // without the extension, for iinstance, MyPic 

    public void TakeScreenshot()
    { 
        if(!isProcessing)
            StartCoroutine( ShareScreenshotNow() );  
    }

    private IEnumerator ShareScreenshotNow()
    {
        isProcessing = true;
        yield return new WaitForEndOfFrame();

        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        byte[] bytes = tex.EncodeToPNG();
        Object.Destroy(tex);
   
        string destination = Path.Combine(Application.persistentDataPath,System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
        Debug.Log(destination);

        File.WriteAllBytes(destination, bytes);


        Data.Instance.GetComponent<NativeShare>().Share(shareText + gameLink, destination, destination, "");
        isProcessing = false;
        Events.OnScreenShotReady();

        //if(!Application.isEditor)
        //{
    
            // AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            // AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            // intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            // AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            // AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","file://" + destination);
            // intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            // intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareText + gameLink);
            // intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
            // intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            // AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            // AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
    
            // currentActivity.Call("startActivity", intentObject);
    
        //}
   
  
 
    }
  
}