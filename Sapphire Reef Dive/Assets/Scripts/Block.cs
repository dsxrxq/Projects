using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public int fordestroying = 1;

    public TMP_Text balance;

    public GameObject Ball;

    public GameObject Over;

    void Start()
    {
        if (GetComponent<Image>().sprite.name.Contains("glass"))
            fordestroying = 2;
        else fordestroying = 1;
    }
    public void Check()
    {
        if (GetComponent<Image>().sprite.name.Contains("golden"))
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject clone = Instantiate(Ball, new Vector3(0, 0, 0), Quaternion.identity);
                clone.transform.SetParent(transform.parent.parent, false);
                clone.transform.position = transform.position;
                clone.transform.SetSiblingIndex(1);

                clone.name = "Ball";

                clone.SetActive(true);

                clone.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-20, 20), 30) * -25;
            }

            if (transform.parent.childCount == 1)
            {
                Time.timeScale = 0;
                Over.SetActive(true);
                PlayerPrefs.SetInt("Lvl" + StartScene.lvl.ToString(), 1);
            }

            Destroy(gameObject);
        }
        else
        {
            fordestroying-=1;

            if(fordestroying == 0)
            {
                if (GetComponent<Image>().sprite.name.Contains("glass"))
                    PlayerPrefs.SetInt("Balance", int.Parse(balance.text = (int.Parse(balance.text) + 25).ToString()));
                else PlayerPrefs.SetInt("Balance", int.Parse(balance.text = (int.Parse(balance.text) + 10).ToString()));

                if (transform.parent.childCount == 1)
                {
                    Time.timeScale = 0;
                    Over.SetActive(true);
                    PlayerPrefs.SetInt("Lvl" + StartScene.lvl.ToString(), 1);
                }
                Destroy(gameObject);
            }
        }
    }
}
