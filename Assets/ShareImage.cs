using UnityEngine;
using System.Collections;
using System;

public class ShareImage : MonoBehaviour
{
    public RenderTexture overviewTexture;
    public Camera camOV;
    public string path = "";


    void LateUpdate()
    {

        if (Input.GetKeyDown("f9"))
        {
            StartCoroutine(TakeScreenShot());
        }

    }

    // return file name
    string fileName(int width, int height)
    {
        return string.Format("screen_{0}x{1}_{2}.png",
                              width, height,
                              System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public IEnumerator TakeScreenShot()
    {
        yield return new WaitForEndOfFrame();

        RenderTexture currentRT = RenderTexture.active;

        RenderTexture.active = camOV.targetTexture;
        camOV.Render();
        Texture2D imageOverview = new Texture2D(camOV.targetTexture.width, camOV.targetTexture.height, TextureFormat.RGB24, false);
        imageOverview.ReadPixels(new Rect(0, 0, camOV.targetTexture.width, camOV.targetTexture.height), 0, 0);
        imageOverview.Apply();
        RenderTexture.active = currentRT;

        // Encode texture into PNG
        byte[] bytes = imageOverview.EncodeToPNG();

        // save in memory
        string filename = fileName(Convert.ToInt32(imageOverview.width), Convert.ToInt32(imageOverview.height));
        path = Application.persistentDataPath + "/Snapshots/" + filename;

        System.IO.File.WriteAllBytes(path, bytes);

        if (!Application.isEditor)
        {
            // block to open the file and share it ------------START
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + path);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "Which Football Club does this Rebus represent?\n" +
                                                 "Download the game on play store at " + "\nhttps://play.google.com/store/apps/details?id=com.TGC.RebusFC&pcampaignid=GPC_shareGame");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Which club is this?");
            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            // option one WITHOUT chooser:
            currentActivity.Call("startActivity", intentObject);

            // block to open the file and share it ------------END

        }
    }
}