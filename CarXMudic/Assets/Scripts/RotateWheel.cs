using System.Collections;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DeleteItem());
    }

    void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
            transform.GetChild(i).GetComponent<Rigidbody>().AddTorque(new Vector3(1, 0, 0) * 500000);
    }

    IEnumerator DeleteItem() 
    { 
        yield return new WaitForSeconds(9f);
        Destroy(gameObject);
    }
}
