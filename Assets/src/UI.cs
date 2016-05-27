using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UI : MonoBehaviour {

    public Text debugField;

    static UI mInstance = null;

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
}
