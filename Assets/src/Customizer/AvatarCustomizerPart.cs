using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class AvatarCustomizerPart {

    public CustomizerPartData data;
    public SkinnedMeshRenderer[] skinnedMeshRenderers;

    public void ChangeTexture(string parts, string part, Color color)
    {
        int materialID = 0;
        switch (parts)
        {
            case "guantes": materialID = 2; break;
            case "pantalon": materialID = 1; break;
        }
        if (color != null)
        {
            foreach (SkinnedMeshRenderer smr in skinnedMeshRenderers)
            {
                smr.materials[materialID].color = color;
            }
            return;
        }
        foreach (SkinnedMeshRenderer smr in skinnedMeshRenderers)
        {
            smr.materials[materialID].mainTexture = GetTexture(parts + "/" + part);
        }
	}
    Texture GetTexture(string textureURL)
    {
        Texture texture = Resources.Load("Customizer/" + textureURL) as Texture;
        return texture;
    }
}
