using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public CharacterMovement characterMovement;
    public InputManager inputManager;
    public FightStatus fightStatus;

    static Game mInstance = null;
    

	public static Game Instance
    {
        get
        {
            return mInstance;
        }
    }
    void Awake()
    {
        if (!mInstance)
            mInstance = this;
    }
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        characterMovement = GetComponent<CharacterMovement>();

        fightStatus = GetComponent<FightStatus>();
        characterMovement.Init();
    }
}
