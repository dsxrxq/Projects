using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public bool Destroying = false;

    public TMP_Text score;

    public GameObject[] button;

    public GameObject over;

    public Sprite SprExp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Item" && !collision.GetComponent<Item>().Destroying)
        {
            Destroying = true;
            Destroy(gameObject);
        }
        if(collision.name == "Plane")
        {
            if (transform.name == "star")
            {
                score.text = (int.Parse(score.text) + 1).ToString();
                if(StartScene.CanVibro)
                    VibrationManager.Vibrate(1);
                Destroy(gameObject);
            }
            else if (transform.name == "bomb")
            {
                StartScene.Ending = true;

                over.SetActive(true);

                if (PlayerPrefs.GetInt("1", 0) < int.Parse(score.text))
                    PlayerPrefs.SetInt("1", int.Parse(score.text));

                collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

                foreach (var obj in button)
                    obj.SetActive(false);

                GetComponent<Image>().sprite = SprExp;

                if (StartScene.CanVibro)
                    VibrationManager.Vibrate(10);

                StartCoroutine(Ending());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Image")
            Destroy(gameObject);
    }

    IEnumerator Ending()
    {
        yield return new WaitForSeconds(0.01f);

        float alpha = over.GetComponent<Image>().color.a;

        alpha += 0.025f;

        over.GetComponent<Image>().color = new Color(0,0,0,alpha);

        if (alpha >= 1)
            SceneManager.LoadScene(0);
        else StartCoroutine(Ending());
    }
}
