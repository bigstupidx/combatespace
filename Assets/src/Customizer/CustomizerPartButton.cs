using UnityEngine;
using System.Collections;

public class CustomizerPartButton : MonoBehaviour {

    public CustomizerPartData data;

	public void OnClicked () {
        if(data.color.a > 0)
            Events.OnCustomizerChangePart(data.name, "", data.color, 0);
        for (int a= 0; a<data.url.Count; a++)
            Events.OnCustomizerChangePart(data.name, data.url[a], data.color, a);
	}
}
