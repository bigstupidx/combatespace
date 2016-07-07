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
        //speed = 30;
        RotationArea = Data.Instance.settings.defaultSpeed.rotationArea;
        speed = 20 + (Data.Instance.playerSettings.characterData.stats.Speed / 2);
        Invoke("Loop", 3);
    }
    void Loop()
    {
        if (Game.Instance.fightStatus.state == FightStatus.states.KO) return;

        int rand = Random.Range(0, RotationArea);
        rand += (Data.Instance.playerSettings.characterData.stats.Speed / 5);
        if(Random.Range(0,100)<50) rand *= -1;
        MoveTo += rand;

        Invoke("Loop", 2);
    }
	void Update () {
        if (Game.Instance.fightStatus.state == FightStatus.states.KO) return;
        if (MoveTo > Mathf.Floor(actualZ))
            actualZ += Time.deltaTime*speed;
        else if (MoveTo < Mathf.Floor(actualZ))
            actualZ -= Time.deltaTime * speed;

        pivot.transform.localEulerAngles = new Vector3(0, actualZ,0);
	}
}
