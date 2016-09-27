using UnityEngine;
using System.Collections;

public class AvatarExpresiones : MonoBehaviour {

    public Renderer idle;
    public Renderer o;
    public Renderer cerrada;
    public types type;
    public GameObject ojos;

    private bool colorSetted;

    public enum types
    {
        IDLE,
        O,
        CERRADA
    }
	void Start () {
        SetIdle();
        Events.OnAvatarExpresion += OnAvatarExpresion;
        SetEyesClosed(false);
        StartCoroutine(SetPestaneaLoop());
	}
    IEnumerator SetPestaneaLoop()
    {
        SetEyesClosed(true);
        yield return new WaitForSeconds(0.2f);
        SetEyesClosed(false);
        yield return new WaitForSeconds(Random.Range(4, 10));
        StartCoroutine(SetPestaneaLoop());
    }
    void SetEyesClosed(bool opened)
    {
        if (!colorSetted && GetComponentInParent<CharacterHead>().color != null)
        {
            foreach (MeshRenderer mr in ojos.GetComponentsInChildren<MeshRenderer>())
            {
                mr.material.color = GetComponentInParent<CharacterHead>().color;                
            }
            colorSetted = true;
        }
        ojos.SetActive(opened);
    }
    void OnDestroy()
    {
        Events.OnAvatarExpresion -= OnAvatarExpresion;
    }
    void OnAvatarExpresion(types type, bool isMyAvatar)
    {
        //if (!isMyAvatar && GetComponentInParent<AvatarCustomizer>().isMyAvatar || isMyAvatar && !GetComponentInParent<AvatarCustomizer>().isMyAvatar) return;
        switch (type)
        {
            case types.IDLE: SetIdle();  break;
            case types.O: SetO(); break;
            case types.CERRADA: SetCerrada(); break;
        }
    }
    void SetIdle()
    {
        SetEyesClosed(false);
        type = types.IDLE;
        idle.enabled = true;
        o.enabled = false;
        cerrada.enabled = false;
    }
    void SetO()
    {
        SetEyesClosed(true);
        idle.enabled = false;
        o.enabled = true;
        cerrada.enabled = false;
        Invoke("SetCerrada", 1);
        type = types.O;
    }
    void SetCerrada()
    {
        if (type == types.O) 
            Invoke("SetIdle", Random.Range(10,20)/10);
        else 
            Invoke("SetIdle", Random.Range(1, 3));

        idle.enabled = false;
        o.enabled = false;
        cerrada.enabled = true;
        type = types.CERRADA;
    }

}
