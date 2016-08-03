using UnityEngine;
using System.Collections;

public class CustomizerPartsButton : MonoBehaviour
{
    private string partName;

    public void Init(string partName)
    {
        this.partName = partName;
    }
    public void OnClicked()
    {
        Events.OnCustomizerRefresh(partName);
    }
}
