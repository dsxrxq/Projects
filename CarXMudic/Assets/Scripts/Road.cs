using System.Collections;
using TMPro;
using UnityEngine;

public class Road : MonoBehaviour
{

    public Material material;

    public TMP_Text text;

    float m;

    public float speed = 0.007f;

    public void Start()
    {
        material.mainTextureOffset = Vector2.zero;

        StartCoroutine(road());
    }

    int limiter = 500;

    IEnumerator road()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.001f);

            material.mainTextureOffset = new Vector2(material.mainTextureOffset.x, material.mainTextureOffset.y + speed);

            m += 0.1f;
            float temp = m;

            if (temp >= 1000)
            {
                temp /= 1000;
                text.text = temp.ToString("F2") + "km";
            }
            else
                text.text = temp.ToString("F0") + "m";



            if(temp >= limiter)
            {
                limiter += 500;
                GetComponent<ChangerSpeed>().Changer();
            }
        }
    }
}
