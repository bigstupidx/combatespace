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
    void OnCustomizerRefresh(string part)
    {
        foreach (CustomizerPartsButton button in partsContainer.GetComponentsInChildren<CustomizerPartsButton>())
        {
            if (button.partName == part)
                button.SetOn();
            else
                button.SetOff();
        }
        Events.OnCustomizerChangeParts(part);
        Utils.RemoveAllChildsIn(partContainer);
        int id = 0;
        foreach (CustomizerPartData data in Data.Instance.customizerData.data)
        {
            if (data.name == part)
            {
                CustomizerPartButton newPartButton = Instantiate(partbutton);
                newPartButton.transform.SetParent(partContainer);
               // newPartButton.data = data;
                newPartButton.transform.localScale = Vector3.one;
                newPartButton.Init(id, part, data.thumb, this);
                id++;
            }
        }
    }
    public void Selected(string part, int partID)
    {
        Events.OnCustomizerChangePart(part, partID);       
    }
}
