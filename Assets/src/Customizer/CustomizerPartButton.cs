using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CustomizerPartButton : MonoBehaviour {

    private int id;
   // public CustomizerPartData data;
    private string part;
    private CustomizerView view;
    public Image thumb;

    public void Init(int id, string part, string thumbName, CustomizerView view)
    {
        this.view = view;
        this.id = id;
        this.part = part;

        Texture2D texture = Resources.Load("Customizer/icons/" + thumbName) as Texture2D;
        thumb.sprite = Sprite.Create(texture, new Rect(0, 0, 512, 512), Vector2.zero);
    }
	public void OnClicked () {
        view.Selected(part, id);
	}
}
