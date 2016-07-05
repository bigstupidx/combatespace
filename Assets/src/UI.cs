using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {
    
    static UI mInstance = null;
    public Text heroUsernameField;
    public Text characterUsernameField;


    public static UI Instance
    {
        get
        {
            return mInstance;
        }
    }
    void Awake()
    {
        if (!mInstance)
            mInstance = this;
    }
    void Start()
    {
        heroUsernameField.text = Data.Instance.playerSettings.heroData.nick;
        characterUsernameField.text = Data.Instance.playerSettings.characterData.nick;
    }
}
