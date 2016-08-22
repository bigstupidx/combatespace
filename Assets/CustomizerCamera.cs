using UnityEngine;
using System.Collections;
using System;

public class CustomizerCamera : MonoBehaviour {

    public Camera camera;

    public CameraSet defaultSet;
    public CameraSet pelo;
    public CameraSet cabeza;
    public CameraSet peinados;
    public CameraSet narices;
    public CameraSet cejas;
    public CameraSet pantalones;
    public CameraSet botas;
    public CameraSet barbas;

    [Serializable]
    public class CameraSet
    {
        public Vector3 cam_pos;
        public Vector3 cam_rot;
    }
    private CameraSet activeSet;

    public Vector3 cam_pos;
    public Vector3 cam_rot;

    void Start()
    {
        activeSet = defaultSet;
        Events.OnCustomizerChangeParts += OnCustomizerChangeParts;
    }
    void OnDestroy()
    {
        Events.OnCustomizerChangeParts -= OnCustomizerChangeParts;
    }
    void OnCustomizerChangeParts(string part)
    {
        switch (part)
        {
            case "cabezas":
                activeSet = cabeza;
                break;
            case "pelos":
                activeSet = pelo; 
                break;
            case "peinados":
                activeSet = peinados; 
                break;
            case "barbas":
                activeSet = barbas; 
                break;
            case "cejas":
                activeSet = cejas; 
                break;
            case "narices":
                activeSet = pelo; 
                break;
            case "pantalon":
                activeSet = pantalones;
                break;
            case "botas": 
                activeSet = botas; 
                break;
            default: 
                activeSet = defaultSet; 
                break;
        }
    }
	void Update () {
        camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, activeSet.cam_pos, 0.1f);
        // camera.transform.localEulerAngles = Vector3.Lerp(camera.transform.localEulerAngles, activeSet.cam_rot, 0.1f);
	}
}
