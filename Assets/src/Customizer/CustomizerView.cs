using UnityEngine;
using System.Collections;

public class CustomizerView : MonoBehaviour {

    public Transform partsContainer;
    public Transform partContainer;

    public CustomizerPartsButton partsButton;
    public CustomizerPartButton partbutton;

	void Start () {
        Events.OnCustomizerRefresh += OnCustomizerRefresh;
        LoppUntikLoad();
        Invoke("Delay", 0.2f);
	}
    void Delay()
    {
        OnCustomizerRefresh("cabezas");
    }
    void OnDestroy()
    {
        Events.OnCustomizerRefresh -= OnCustomizerRefresh;
        Resources.UnloadUnusedAssets();
    }
    void LoppUntikLoad()
    {
        if (Data.Instance.customizerData.data.Count > 0)
            DataLoaded();
        else
            Invoke("LoppUntikLoad", 0.2f);
    }
    void DataLoaded()
    {
        foreach (string part in Data.Instance.customizerData.parts)
        {
            CustomizerPartsButton newPartsButton = Instantiate(partsButton);
            newPartsButton.transform.SetParent(partsContainer);
            newPartsButton.Init(part);
            newPartsButton.transform.localScale = Vector3.one;
            newPartsButton.SetOff();
        }
    }
    string newPart;
    void OnCustomizerRefresh(string part)
    {
        this.newPart = part;
        foreach (CustomizerPartsButton button in partsContainer.GetComponentsInChildren<CustomizerPartsButton>())
        {
            if (button.partName == part)
                button.SetOn();
            else
                button.SetOff();
        }
        Events.OnCustomizerChangeParts(newPart);
        Utils.RemoveAllChildsIn(partContainer);
       
        Invoke("LoadNewButtons", 0.5f);
    }
    void LoadNewButtons()
    {
        int id = 0;
        Resources.UnloadUnusedAssets();
        foreach (CustomizerPartData data in Data.Instance.customizerData.data)
        {
            if (data.name == newPart)
            {
                CustomizerPartButton newPartButton = Instantiate(partbutton);
                newPartButton.transform.SetParent(partContainer);
                // newPartButton.data = data;
                newPartButton.transform.localScale = Vector3.one;
                newPartButton.Init(id, newPart, data.thumb, this);
                id++;
            }
        }
    }
    public void Selected(string part, int partID)
    {
        Events.OnCustomizerChangePart(part, partID);       
    }
}
