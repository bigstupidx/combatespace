using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CustomizerPartButton : MonoBehaviour {

    private int id;
   // public CustomizerPartData data;
    private string part;
    private CustomizerView view;
    public Image thumb;
    private Texture2D texture;

    public void Init(int id, string part, string thumbName, CustomizerView view)
    {
        this.view = view;
        this.id = id;
        this.part = part;

        texture = Resources.Load("Customizer/icons/" + thumbName) as Texture2D;
        thumb.sprite = Sprite.Create(texture, new Rect(0, 0, 511, 511), Vector2.zero);
    }
	public void OnClicked () {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        view.Selected(part, id);
	}
    void OnDestroy()
    {
        texture = null;
    }
}
