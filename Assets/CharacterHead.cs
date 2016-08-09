using UnityEngine;
using System.Collections;
using System;

public class CharacterHead : MonoBehaviour {
    
    public GameObject[] cabezas;

    public GameObject orejas;
    public GameObject cejas;
    public GameObject narices;
    public GameObject pelos;
    public GameObject barbas;

    private int cabezaID;

    public void SetCabeza(int cabezaID)
    {
        print("cabezaID " + cabezaID);
        this.cabezaID = cabezaID;
        foreach (GameObject cabeza in cabezas)
                cabeza.SetActive(false);

        GetCabeza(cabezaID).SetActive(true);
        print("GetCabeza " + GetCabeza(cabezaID).name);

        SetContainer("nariz", narices);
        SetContainer("cejas", cejas);
        SetContainer("orejas", orejas);
        SetContainer("pelos", pelos);
    }
    public void SetMeshPart(int itemId, string part)
    {
        GameObject container = null;
        if(part == "peinados")
            container = pelos;
        else if (part == "cejas")
            container = cejas;
        else if (part == "narices")
            container = narices;
        else if (part == "barbas")
            container = barbas;

        int id = 0;
        foreach (MeshRenderer go in container.GetComponentsInChildren<MeshRenderer>())
        {
            id++;
            if (id == itemId)
                go.enabled = true;
            else
                go.enabled = false;
        }
    }

    void SetContainer(string partName, GameObject part)
    {
        Transform container = GetContainerFor(partName);
        part.transform.SetParent(container);
        part.transform.localPosition = Vector3.zero;
        part.transform.localEulerAngles = Vector3.zero;
    }
    GameObject GetCabeza(int cabezaID)
    {
        foreach (GameObject cabeza in cabezas)
            if (cabeza.name == "cabeza" + cabezaID)
                return cabeza;
        return null;
    }
    public void ChangePiel(string partType, Color color)
    {
       // print(partType + " - " + color);
        foreach (GameObject go in cabezas)
            Change(go.GetComponent<SkinnedMeshRenderer>().material, partType, color);
        foreach (MeshRenderer mr in orejas.GetComponentsInChildren<MeshRenderer>())
            Change(mr.material, partType, color);
        foreach (MeshRenderer mr in narices.GetComponentsInChildren<MeshRenderer>())
            Change(mr.material, partType, color);
    }
    public void ChangePelos(string partType, Color color)
    {
        foreach (MeshRenderer mr in pelos.GetComponentsInChildren<MeshRenderer>())
            Change(mr.material, partType, color);
        foreach (MeshRenderer mr in cejas.GetComponentsInChildren<MeshRenderer>())
            Change(mr.material, partType, color);
    }
    void Change(Material material, string partType, Color color)
    {
        if (color != null)
            material.color = color;
        else
            material.mainTexture = GetTexture(partType);
    }
    Texture GetTexture(string textureURL)
    {
        Texture texture = Resources.Load("Customizer/" + textureURL) as Texture;
        return texture;
    }
    Transform GetContainerFor(string partName)
    {
        return GetCabeza(cabezaID).transform.Find(partName);
    }
}
