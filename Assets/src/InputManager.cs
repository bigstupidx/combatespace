using UnityEngine;
using System.Collections;

public class InputManager:MonoBehaviour  {

    public bool defense_L;
    public bool defense_R;

    public bool gyro_enable;
    public Vector3 gyro_rotation;

    public float heroRotation;
    public float heroRotationVertical;

    private float timeToDefense = 0.1f;

    private float minSwipeDistY = 15;
    private float minSwipeDistX = 20;

    private Vector2 startPos;


    // public Text debugText;

    public directions direction;

    public enum directions
    {
        NONE,
        UP,
        DOWN,
        DEFENSE,
        DEFENSE_L,
        DEFENSE_R,
    }

    private float newTime;
    private bool touched;

    private bool movedByTime;
    private float timeSinceTouch;
    private PlayerSettings.controls controls;

    void Start()
    {
        controls = Data.Instance.playerSettings.control;
#if UNITY_EDITOR
        gyro_enable = false;
#elif  UNITY_IPHONE || UNITY_ANDROID
         if (Input.gyro.enabled)
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
            Events.OnHeroAction(HeroActions.actions.GANCHO_DOWN_L);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Events.OnHeroAction(HeroActions.actions.GANCHO_DOWN_R);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetDefense(true, true);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SetDefense(false, true);
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            SetDefense(true, false);
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            SetDefense(false, false);
        }
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



        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:

                    startPos = touch.position;
                    touched = true;
                    Invoke("CheckIfStillTouchungDefense", timeToDefense);  
              
                    break;
                case TouchPhase.Ended:
                    touched = false;
                    if (Vector2.Distance(touch.position, startPos) > minSwipeDistX)
                    {
                        Move(touch.position.x, touch.position.y);
                    } else
                    {
                        Events.OnHeroAction(HeroActions.actions.IDLE);
                    }
                    break;
            }
        }
#endif
    }

    void Move(float touchFinalPositionX, float touchFinalPositionY)
    {
        if (startPos.y > (Screen.height / 2))
            Swipe(directions.UP);
        else
            Swipe(directions.DOWN);
    }
    void Swipe(directions direction)
    {
        UI.Instance.debugField.text = "startPosX: " + startPos.x + "     screenWidth: " + (Screen.width/2);

        if (startPos.x < (Screen.width / 2))
        {
            switch (direction)
            {
                case directions.UP:
                    Events.OnHeroAction(HeroActions.actions.GANCHO_UP_L); break;
                case directions.DOWN:
                    Events.OnHeroAction(HeroActions.actions.GANCHO_DOWN_L);break;
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
        if (touched)
        {
            Events.OnHeroAction(HeroActions.actions.DEFENSE);
        }
    }
}
