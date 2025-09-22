using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
{
    public Rigidbody2D Bike;

    bool pressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!pressed)
            pressed = true;
    }

    void FixedUpdate()
    {
        if (pressed)
        {
            if (name == "Left")
            {
                Bike.angularVelocity = 100;
            }
            if (name == "Right")
            {
                Bike.angularVelocity = -100;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (pressed)
            pressed = false;
    }
}
