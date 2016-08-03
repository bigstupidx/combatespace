using UnityEngine;
using System.Collections;

public class CustomizerPartButton : MonoBehaviour {

    public CustomizerPartData data;

	public void OnClicked () {
        for (int a= 0; a<data.url.Count; a++)
            Events.OnCustomizerChangePart(data.name, data.url[a], a);
	}
}
