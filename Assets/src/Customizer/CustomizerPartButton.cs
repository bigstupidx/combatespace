using UnityEngine;
using System.Collections;

public class CustomizerPartButton : MonoBehaviour {

    private int id;
   // public CustomizerPartData data;
    private string part;
    private CustomizerView view;

    public void Init(int id, string part, CustomizerView view)
    {
        this.view = view;
        this.id = id;
        this.part = part;
    }
	public void OnClicked () {
        view.Selected(part, id);
	}
}
