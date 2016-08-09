using UnityEngine;
using System.Collections;
using System;

public class AvatarCustomizer : MonoBehaviour {

    public Styles style;

    public AvatarCustomizerPart[] avatarCustomizerParts;
    public CharacterHead head;

    void Start()
    {
        style = Data.Instance.playerSettings.heroData.styles;
        head.SetCabeza(1);
        Events.OnCustomizerChangePart += OnCustomizerChangePart;
    }
    void OnDestroy()
    {
        Events.OnCustomizerChangePart -= OnCustomizerChangePart;
    }
    public void OnCustomizerChangePart(string partName, int partID)
    {
        SetStyle(partName, partID);
        int id = 0;
        foreach (CustomizerPartData data in Data.Instance.customizerData.data)
        {
            if (data.name == partName)
            {
                if (id == partID)
                {
                    if (data.color.a > 0)
                        ChangePart(data.name, "", data.color, 0);
                    for (int a = 0; a < data.url.Count; a++)
                        ChangePart(data.name, data.url[a], data.color, a);
                    return;
                }
                id++;
            }
        }
	}
    public void ChangePart(string partName, string partType, Color color, int materialID)
    {
        if (partName == "peinados" || partName == "cejas" || partName == "narices" || partName == "barbas")
        {
            head.SetMeshPart(int.Parse(partType), partName);
        }
        else
        if (partName == "cabezas")
        {
            head.SetCabeza(int.Parse(partType));
        }
        else if (partName == "piel")
            head.ChangePiel(partType, color);
        else if (partName == "pelos")
            head.ChangePelos(partType, color);

        int partNum = materialID + 1;
        foreach (AvatarCustomizerPart part in avatarCustomizerParts)
        {
            if (part.data.name == partName + partNum)
                part.ChangeTexture(partName, partType, color);
        }
    }
    void SetStyle(string partName, int partID)
    {
        switch (partName)
        {
            case "cabezas": style.cabezas = partID; break;
            case "piel": style.piel = partID; break;
            case "tatoo": style.tatoo = partID; break;
            case "pelos": style.pelos = partID; break;
            case "peinados": style.peinados = partID; break;
            case "cejas": style.cejas = partID; break;
            case "narices": style.narices = partID; break;
            case "barbas": style.barbas = partID; break;
            case "pantalon": style.pantalon = partID; break;
            case "botas": style.botas = partID; break;
        }
    }
}
