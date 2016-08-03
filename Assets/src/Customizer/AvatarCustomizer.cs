using UnityEngine;
using System.Collections;

public class AvatarCustomizer : MonoBehaviour {

    public AvatarCustomizerPart[] parts;

    void Start()
    {
        Events.OnCustomizerChangePart += OnCustomizerChangePart;
    }
    void OnDestroy()
    {
        Events.OnCustomizerChangePart -= OnCustomizerChangePart;
    }
    public void OnCustomizerChangePart(string partName, string partType, int materialID)
    {
        int partNum = materialID+1;
        foreach (AvatarCustomizerPart part in parts)
        {            
            if (part.data.name == partName + partNum)
                part.ChangeTexture(partName, partType, materialID);
        }
	}
}
