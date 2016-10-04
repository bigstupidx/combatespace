using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class SharePopup : MonoBehaviour {

    public GameObject panel;
    public GameObject ShowPanels;
    public GameObject[] hidePanels;

    public Text usernameField;
    public Text characterField;

    private bool sharing;

	void Start () {
        panel.SetActive(false);
        Events.OnScreenShotReady += OnScreenShotReady;
	}
    void OnDestroy()
    {
        Events.OnScreenShotReady -= OnScreenShotReady;
    }
	public void SetOn() {
        if (Data.Instance.playerSettings.heroData.facebookID == "")
        {
            Events.OnRegisterPopup();
        }
        else
        {
            panel.SetActive(true);

            usernameField.text = Data.Instance.playerSettings.heroData.nick;
            characterField.text = Data.Instance.playerSettings.characterData.nick;

            if (sharing) return;
            sharing = true;

            ShowPanels.SetActive(true);

            foreach (GameObject go in hidePanels)
                go.SetActive(false);

            GetComponent<ShareScreenshot>().TakeScreenshot();
        }
	}
    

    void OnScreenShotReady()
    {
        sharing = false;
        ShowPanels.SetActive(false);
        foreach (GameObject go in hidePanels)
            go.SetActive(true);
    }
  
}
