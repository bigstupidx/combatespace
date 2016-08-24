using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FighterSelectorButton : MonoBehaviour {

    public Text username;
    public Text score;
    public int id;
    public PlayerData playerData;
    public ProfilePicture profilePicture;
    public RawImage rawImage;

    void Start()
    {
        SetOff();
    }
    public void Init(int id, PlayerData _playerData)
    {
        this.id = id;
        this.playerData = _playerData;
        username.text = playerData.nick;
        score.text = "" + _playerData.stats.score;
        profilePicture.setPicture(_playerData.facebookID);
	}
    public void Clicked()
    {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.swap1);
        Events.OnVerticalScrollSnapChanged(id);
    }
    public void SetOn()
    {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.swap2);
        rawImage.enabled = true;
    }
    public void SetOff()
    {
        rawImage.enabled = false;
    }
}
