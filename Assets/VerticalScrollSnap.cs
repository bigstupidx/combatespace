using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VerticalScrollSnap : MonoBehaviour
{
    public GameObject container;

    public Movement movement;
    public enum Movement
    {
        NONE,
        MOVING
    }

    ScrollRect scrollRect;

    public int active;

    public int totalItems;

    public float itemHeight;
    public float snapSpeed;

    public float actual_Y;
    public float new_Y;

    bool started;

    void Start()
    {
        Events.OnVerticalScrollSnapChanged += OnVerticalScrollSnapChanged;
    }
    void OnDestroy()
    {
        Events.OnVerticalScrollSnapChanged -= OnVerticalScrollSnapChanged;
    }
    void OnVerticalScrollSnapChanged(int activeID)
    {
        ChangeValue(activeID);
    }
    public void Init(int activeID)
    {
        scrollRect = GetComponent<ScrollRect>();
        active = activeID;

        started = true;
        totalItems = container.GetComponentsInChildren<FighterSelectorButton>().Length;

        ChangeValue(activeID);
    }
    public int GetActive()
    {
        return active;
    }
    public void ChangeValue(int activeID)
    {
        this.active = activeID;
        new_Y = itemHeight * active;
        movement = Movement.MOVING;
        Events.SetFighter(activeID);
    }
    void Update()
    {
        if (movement == Movement.NONE)
        {
            scrollRect.inertia = true;
            return;
        }
        scrollRect.inertia = false;

        float newY = Mathf.Lerp(container.transform.localPosition.y, new_Y, snapSpeed * Time.deltaTime);
        Repositionate(newY);
    }
    void Repositionate(float newY)
    {
        container.transform.localPosition = new Vector2(0, newY);
    }
    public void DragEnd()
    {
        if (actual_Y < container.transform.localPosition.y)
            active++;   
        else
            active--;

        if (active <= 0) active = 0;
        if (active >= totalItems - 1) active = totalItems-1;
        ChangeValue(active);
    }

    public void OnDrag()
    {
        movement = Movement.NONE;
        actual_Y = itemHeight * active;
    }
}
