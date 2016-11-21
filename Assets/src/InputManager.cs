using UnityEngine;
using System.Collections;

public class InputManager:MonoBehaviour  {

    public bool defense_L;
    public bool defense_R;

    public bool gyro_enable;
    public Vector3 gyro_rotation;

    public float heroRotation;
    public float heroRotationVertical;

    private float timeToDefense = 0.06f;

    private float minSwipeDistY = 15;
    private float minSwipeDistX = 20;

    private Vector2 startPos;


    // public Text debugText;

    public directions direction;

    public enum directions
    {
        NONE,
        UP,
        MID,
        DOWN,
        DEFENSE,
        DEFENSE_L,
        DEFENSE_R,
    }

    private float newTime;

    private bool movedByTime;
    private float timeSinceTouch;
    private PlayerSettings.controls controls;

    void Start()
    {
        controls = Data.Instance.playerSettings.control;
#if UNITY_EDITOR
        gyro_enable = false;
#elif  UNITY_IPHONE || UNITY_ANDROID
        Input.gyro.enabled = true;
        if (SystemInfo.supportsGyroscope)
            gyro_enable = true;
#endif
    }


	void Update ()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Events.OnHeroAction(HeroActions.actions.GANCHO_UP_L);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Events.OnHeroAction(HeroActions.actions.GANCHO_UP_R);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Events.OnHeroAction(HeroActions.actions.CORTITO_L);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Events.OnHeroAction(HeroActions.actions.CORTITO_R);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Events.OnHeroAction(HeroActions.actions.GANCHO_DOWN_L);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Events.OnHeroAction(HeroActions.actions.GANCHO_DOWN_R);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetDefense(true, true);
            SetDefense(false, true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SetDefense(true, false);
            SetDefense(false, false);
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SetDefense(false, true);
        //}
        //if (Input.GetKeyUp(KeyCode.Z))
        //{
        //    SetDefense(true, false);
        //}
        //if (Input.GetKeyUp(KeyCode.X))
        //{
        //    SetDefense(false, false);
        //}
        float mouseRot = (Input.mousePosition.x / Screen.width) - 0.5f;
        heroRotationVertical = (Input.mousePosition.y / Screen.height) - 0.5f;
        heroRotation = mouseRot;



#elif UNITY_IPHONE || UNITY_ANDROID

        gyro_enable = true;
        if (controls == PlayerSettings.controls.CONTROL_360)
        {
            gyro_rotation = Input.gyro.rotationRateUnbiased;
        }
        else
        {
            gyro_rotation = new Vector3(0, -Input.acceleration.x*4, 0);
        }
        heroRotationVertical = -Input.acceleration.y;

        if(Input.touchCount == 0 && defending)
        {
            defending = false;
            Events.OnHeroAction(HeroActions.actions.IDLE);
        }
        else if (Input.touchCount > 1)
        {
            Invoke("CheckIfStillTouchungDefense", timeToDefense); 
        }
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:

                    startPos = touch.position;
                   // Invoke("CheckIfStillTouchungDefense", timeToDefense);  
              
                    break;
                case TouchPhase.Ended:    
        ////cortito
                    if (Input.touchCount == 1 && Vector2.Distance(touch.position, startPos) < minSwipeDistX)
                        Swipe(directions.MID);
                    else if (Input.touchCount == 1 && Vector2.Distance(touch.position, startPos) > minSwipeDistX)
                        Move(touch.position.x, touch.position.y);                        
                    break;
            }
        }
#endif
    }
    bool defending;
    void Move(float touchFinalPositionX, float touchFinalPositionY)
    {
        if (Mathf.Abs(startPos.x - touchFinalPositionX) > Mathf.Abs(startPos.y - touchFinalPositionY))
            Swipe(directions.MID);
        else if (touchFinalPositionY < startPos.y)
            Swipe(directions.UP);
        else
            Swipe(directions.DOWN);
        //if (startPos.y > (Screen.height / 2))
        //    Swipe(directions.UP);
        //else
        //    Swipe(directions.DOWN);
    }
    void Swipe(directions direction)
    {
        if (startPos.x < (Screen.width / 2))
        {
            switch (direction)
            {
                case directions.UP:
                    Events.OnHeroAction(HeroActions.actions.GANCHO_UP_L); break;
                case directions.DOWN:
                    Events.OnHeroAction(HeroActions.actions.GANCHO_DOWN_L);break;
                case directions.MID:
                    Events.OnHeroAction(HeroActions.actions.CORTITO_L); break;
                case directions.DEFENSE:
                    Events.OnHeroAction(HeroActions.actions.DEFENSE);break;
            }
        }
        else
        {
            switch (direction)
            {
                case directions.UP:
                    Events.OnHeroAction(HeroActions.actions.GANCHO_UP_R);break;
                case directions.DOWN:
                    Events.OnHeroAction(HeroActions.actions.GANCHO_DOWN_R);break;
                case directions.MID:
                    Events.OnHeroAction(HeroActions.actions.CORTITO_R); break;
                case directions.DEFENSE:
                    Events.OnHeroAction(HeroActions.actions.DEFENSE); break;
            }
        }
    }
    void SetDefense(bool isLeft, bool state)
    {
        if (isLeft)
            defense_L = state;
        else
            defense_R = state;

        if(defense_L && defense_R)
            Events.OnHeroAction(HeroActions.actions.DEFENSE);
        else if (defense_L && !defense_R)
            Events.OnHeroAction(HeroActions.actions.DEFENSE);
        else if (!defense_L && defense_R)
            Events.OnHeroAction(HeroActions.actions.DEFENSE);
        else if (!defense_L && !defense_R)
            Events.OnHeroAction(HeroActions.actions.IDLE);
    }
    void CheckIfStillTouchungDefense()
    {
        if (Input.touchCount > 1)
        {
            defending = true;
            Events.OnHeroAction(HeroActions.actions.DEFENSE);
        }
    }
}
