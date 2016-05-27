using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    public float speed;
    public int RotationArea;
    public GameObject pivot;
    public Character character;
    public int MoveTo;
    public float actualZ;

    public void Init()
    {
        Loop();
    }
    void Loop()
    {
        if (character.characterActions.state == CharacterActions.states.KO) return;
        int rand = Random.Range(0, RotationArea);
        if(Random.Range(0,100)<50) rand *= -1;
        MoveTo += rand;

        Invoke("Loop", 4);
    }
	void Update () {
        if (MoveTo > Mathf.Floor(actualZ))
            actualZ += Time.deltaTime*speed;
        else if (MoveTo < Mathf.Floor(actualZ))
            actualZ -= Time.deltaTime * speed;

        pivot.transform.localEulerAngles = new Vector3(0, actualZ,0);
	}
}
