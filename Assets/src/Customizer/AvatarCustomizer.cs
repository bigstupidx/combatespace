using UnityEngine;
using System.Collections;
using System;

public class AvatarCustomizer : MonoBehaviour {

    public string style;
    public Parts parts;
    [Serializable]
    public class Parts
    {
        public int cabezas;
        public int peinados;
        public int pelos;
        public int piel;
        public int narices;
        public int barbas;
        public int cejas;        
        public int guantes;
        public int pantalon;
        public int botas;
        public int tatoo;
    }

    public AvatarCustomizerPart[] avatarCustomizerParts;
    public CharacterHead head;

    void Start()
    {
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
            case "cabezas":     parts.cabezas = partID; break;
            case "piel":        parts.piel = partID; break;
            case "tatoo":       parts.tatoo = partID; break;
            case "pelos":       parts.pelos = partID; break;
            case "peinados":    parts.peinados = partID; break;
            case "cejas":       parts.cejas = partID; break;
            case "narices":     parts.narices = partID; break;
            case "barbas":      parts.barbas = partID; break;
            case "pantalon":    parts.pantalon = partID; break;
            case "botas":       parts.botas = partID; break;
        }
    }
}
