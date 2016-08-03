using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class AvatarCustomizerPart {

    public CustomizerPartData data;
    public SkinnedMeshRenderer[] skinnedMeshRenderers;

    public void ChangeTexture(string parts, string part, int materialID)
    {
        Debug.Log(parts + " " + part + " " + materialID);
        switch (parts)
        {
            case "guantes": if(materialID == 0) materialID = 2; break;
            case "pantalon": materialID = 1; break;
        }
        foreach (SkinnedMeshRenderer smr in skinnedMeshRenderers)
            smr.materials[materialID].mainTexture = GetTexture(parts + "/" + part);
	}
    Texture GetTexture(string textureURL)
    {
        Texture texture = Resources.Load("Customizer/" + textureURL) as Texture;
        return texture;
    }
}
