using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FighterSelectorButton : MonoBehaviour {

    public Text username;
    public Text score;
    public int id;
    public PlayerData playerData;
    public ProfilePicture profilePicture;

    public void Init(int id, PlayerData _playerData)
    {
        this.id = id;
        this.playerData = _playerData;
        username.text = playerData.nick;
        score.text = "puntos: " + _playerData.stats.score;
        profilePicture.setPicture(_playerData.facebookID);
	}
    public void Clicked()
    {
        Events.OnVerticalScrollSnapChanged(id);
    }
}
