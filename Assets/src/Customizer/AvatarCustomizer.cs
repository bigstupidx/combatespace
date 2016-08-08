using UnityEngine;
using System.Collections;

public class AvatarCustomizer : MonoBehaviour {

    public int cabezaID;
    public AvatarCustomizerPart[] parts;
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
    public void OnCustomizerChangePart(string partName, string partType, Color color, int materialID)
    {
       // print("partName  : "  + partName + "    partType: " + partType + "  color: " + color);
        if (partName == "narices")
        {
            print(partType);
            head.SetCabeza(int.Parse(partType));
        } else if (partName == "cejas")
        {
            print(partType);
            head.SetCabeza(int.Parse(partType));
        }
        else if (partName == "peinados")
        {
            print(partType);
            head.SetPeinados(int.Parse(partType));
        }
        else
        if (partName == "cabezas")
        {
            print(partType);
            head.SetCabeza(int.Parse(partType));
        }

        if (partName == "piel")
            head.ChangePiel(partType, color);
        else if (partName == "pelos")
            head.ChangePelos(partType, color);

        int partNum = materialID+1;
        foreach (AvatarCustomizerPart part in parts)
        {            
            if (part.data.name == partName + partNum)
                part.ChangeTexture(partName, partType, color);
        }
	}
  
    
}
