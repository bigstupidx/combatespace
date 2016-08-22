using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class AvatarCustomizerPart {

    public CustomizerPartData data;
    public SkinnedMeshRenderer[] skinnedMeshRenderers;

    public void ChangeTexture(string parts, string part, Color color)
    {
        //  Debug.Log("__________parts: " + parts + " part: " + part + " color: " + color);

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
    }
    public void ChangeTexture(string parts, string part)
    {
        Debug.Log("ChangeTexture __________parts: " + parts + " part: " + part );

        int materialID = 0;
        switch (parts)
        {
            case "guantes_tela": materialID = 2; break;
            case "pantalon_tela": materialID = 1; break;
            case "tatoo": materialID = 0; break;
        }       
        foreach (SkinnedMeshRenderer smr in skinnedMeshRenderers)
        {
            smr.materials[materialID].mainTexture = GetTexture(part);
        }
    }
    Texture GetTexture(string textureURL)
    {
        Texture texture = Resources.Load("Customizer/textures/" + textureURL) as Texture;
        return texture;
    }
}
