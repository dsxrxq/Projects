using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatformController : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    bool Touched = false;

    public Ball ball;
    public void OnPointerUp(PointerEventData eventData) { Touched = false; }

    public void OnPointerDown(PointerEventData eventData) { if (!ball.startmove) { ball.startmove = true; ball.StartMove(); } Touched = true; }

    Vector3 touch = Vector3.zero;

    public float speed;

    void FixedUpdate()
    {
        if (Touched && Input.touchCount > 0)
        {
            if (touch == Vector3.zero)
                touch = Input.GetTouch(0).position;

            if (Input.GetTouch(0).position.x > touch.x + 50 || Input.GetTouch(0).position.x < touch.x - 50)
            {
                if (Input.GetTouch(0).position.x > touch.x + 50)
                    transform.GetChild(0).GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
                else transform.GetChild(0).GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
            }
            else transform.GetChild(0).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else if (touch != null)
        {
            touch = Vector3.zero;
            transform.GetChild(0).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
