using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Fabric;

public class Data : MonoBehaviour
{
    const string PREFAB_PATH = "Data";
    static Data mInstance = null;
    public Settings settings;
    public PlayerSettings playerSettings;
    public FightersManager fightersManager;
    public PeleasManager peleasManager;
    public HistorialManager hostorialManager;
    public CustomizerData customizerData;
    public string lastScene;
    public string actualScene;
	public MusicManager music;
	public InterfaceSfx interfaceSfx;

    public static Data Instance
    {
        get
        {
            mInstance = FindObjectOfType<Data>();
            if (mInstance == null)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>(PREFAB_PATH)) as GameObject;
                mInstance = go.GetComponent<Data>();
                go.transform.localPosition = new Vector3(0, 0, 0);
            }
            return mInstance;
        }
    }
    public void LoadLevel(string aLevelName)
    {        
        SocialEvents.OnMetricState(aLevelName);
        lastScene = SceneManager.GetActiveScene().name;
		music.MusicChange (aLevelName);
        SceneManager.LoadScene(aLevelName);
        actualScene = aLevelName;
        GetComponent<Loading>().SceneChanged();
    }
    void Awake()
    {
        
        if (!mInstance)
            mInstance = this;
        //otherwise, if we do, kill this thing
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        Fabric.Runtime.Fabric.Initialize();
        SocialEvents.OnUserReady += OnUserReady;
        fightersManager = GetComponent<FightersManager>();
        peleasManager = GetComponent<PeleasManager>();
        hostorialManager = GetComponent<HistorialManager>();
        customizerData = GetComponent<CustomizerData>();
        settings = GetComponent<Settings>();
        Invoke("StartLoadingFromServer", 1);
    }
    void StartLoadingFromServer()
    {
        fightersManager.LoadFighters(0, 100);
    }
    void OnUserReady(string a, string b, string c)
    {
        if (playerSettings.heroData.nick == null || playerSettings.heroData.nick == "" || playerSettings.heroData.nick == "Anónimo")
            LoadLevel("04_Names");
        else if (SceneManager.GetActiveScene().name != "0_Splash")
            LoadLevel("03_Home");
    }

}
