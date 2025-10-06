using System.Collections;
using UnityEngine;

public class ChangerSpeed : MonoBehaviour
{
    public void Changer()
    {
        GetComponent<Road>().speed += (GetComponent<Road>().speed * 0.1f);
        Creator.speed += (Creator.speed * 0.1f);
    }
}
