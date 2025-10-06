using System.Collections;
using UnityEngine;


public class Construcrions : MonoBehaviour
{

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().linearVelocity = new Vector3(0,0,-1) * (Creator.speed -3500);
    }

    private void Start()
    {
        StartCoroutine(DeleteItem());
    }

    IEnumerator DeleteItem()
    {
        yield return new WaitForSeconds(60f);
        Destroy(gameObject);
    }
}
