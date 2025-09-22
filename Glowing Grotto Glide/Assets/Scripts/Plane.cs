using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{


    private void FixedUpdate()
    {
        if(!StartScene.Ending)
            foreach (var obj in GameObject.FindGameObjectsWithTag("Item"))
                obj.GetComponent<Rigidbody2D>().velocity = -transform.right * 500;
        else
            foreach (var obj in GameObject.FindGameObjectsWithTag("Item"))
                obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
