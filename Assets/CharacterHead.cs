using UnityEngine;
using System.Collections;

public class CharacterHead : MonoBehaviour {

    public GameObject[] cabezas;
    public GameObject[] orejas;
    public GameObject[] cejas;
    public GameObject[] nariz;

    private int cabezaID;

    public void SetCabeza(int cabezaID)
    {
        this.cabezaID = cabezaID;
        foreach (GameObject cabeza in cabezas)
        {
            if (cabeza.name == "cabeza" + cabezaID)
                cabeza.SetActive(true);
            else
                cabeza.SetActive(false);
        }
    }
    public void ChangePiel(string partType, Color color)
    {
        Texture texture = GetTexture("piel/" + partType);
        foreach (GameObject go in cabezas)
            Change(go.GetComponent<MeshRenderer>().material, partType, color);
        foreach (GameObject go in orejas)
            Change(go.GetComponent<MeshRenderer>().material, partType, color);
        foreach (GameObject go in nariz)
            Change(go.GetComponent<MeshRenderer>().material, partType, color);
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
}
