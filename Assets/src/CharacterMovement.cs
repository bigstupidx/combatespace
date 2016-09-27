using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    public float speed;
    public int RotationArea;
    public GameObject pivot;
    public Character character;
    public int MoveTo;
    public float actualZ;
    private float _z;

    void Start()
    {
        Events.OnRoundStart += OnRoundStart;
    }
    void OnDestroy()
    {
        Events.OnRoundStart -= OnRoundStart;
    }
    void OnRoundStart()
    {
        _z = -8;
        pivot.transform.localPosition = new Vector3(0, 0, _z);
        MoveTo =  0;
        actualZ = 0;
        pivot.transform.localEulerAngles = Vector3.zero;
        Loop();
    }
    public void Init()
    {
        RotationArea = Data.Instance.settings.defaultSpeed.rotationArea;

        if (Data.Instance.playerSettings.control == PlayerSettings.controls.CONTROL_JOYSTICK)
            RotationArea /= 2;

        speed = 20 + (Data.Instance.playerSettings.characterData.stats.Speed / 2);       
    }
    void Loop()
    {
        //AVANZA:
        if (Game.Instance.fightStatus.state == FightStatus.states.IDLE)
        {
            _z += Time.deltaTime*3;
            if (pivot.transform.localPosition.z >=0)
            {
                _z = 0;
                Events.OnCharactersStartedFight();
            }
            pivot.transform.localPosition = new Vector3(0,0, _z);
            Invoke("Loop", Time.deltaTime);
            return;
        }

        if (Game.Instance.fightStatus.state == FightStatus.states.KO) return;
        int rand = Random.Range(0, RotationArea);
        rand += (Data.Instance.playerSettings.characterData.stats.Speed / 5);
        if(Random.Range(0,100)<50) rand *= -1;
        MoveTo += rand;

        Invoke("Loop", 2);
    }
	void Update () {
        if (Game.Instance.fightStatus.state == FightStatus.states.IDLE)
        {
            Vector3 pos = pivot.transform.localPosition;
            pivot.transform.localPosition = pos;
            return;
        }
        if (Data.Instance.settings.gamePaused) return;
        if (Game.Instance.fightStatus.state == FightStatus.states.KO) return;
        if (Game.Instance.fightStatus.state == FightStatus.states.BETWEEN_ROUNDS) return;

        if (MoveTo > Mathf.Floor(actualZ))
            actualZ += Time.deltaTime*speed;
        else if (MoveTo < Mathf.Floor(actualZ))
            actualZ -= Time.deltaTime * speed;

        pivot.transform.localEulerAngles = new Vector3(0, actualZ,0);
	}
}
