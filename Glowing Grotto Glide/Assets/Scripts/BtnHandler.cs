using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject[] Item;

    public Sprite[] SprItem;

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (name)
        {
            case "Play":
                SceneManager.LoadScene(1);
                break;

            case "Sound":
                if (!PlayerPrefs.HasKey("Sound") || PlayerPrefs.GetString("Sound") == "False")
                {
                    SetOnorOff(true);

                    GameObject.FindWithTag("Sound").GetComponent<AudioSource>().mute = false;

                    PlayerPrefs.SetString("Sound", "True");
                }
                else
                {
                    SetOnorOff(false);

                    GameObject.FindWithTag("Sound").GetComponent<AudioSource>().mute = true;

                    PlayerPrefs.SetString("Sound", "False");
                }
                break;

            case "Music":
                if (!PlayerPrefs.HasKey("Music") || PlayerPrefs.GetString("Music") == "False")
                {
                    SetOnorOff(true);

                    PlayerPrefs.SetString("Music", "True");
                }
                else
                {
                    SetOnorOff(false);

                    PlayerPrefs.SetString("Music", "False");
                }
                break;

            case "Vibro":
                if (!PlayerPrefs.HasKey("Vibro") || PlayerPrefs.GetString("Vibro") == "False")
                {
                    SetOnorOff(true);

                    PlayerPrefs.SetString("Vibro", "True");
                }
                else
                {
                    SetOnorOff(false);

                    PlayerPrefs.SetString("Vibro", "False");
                }
                break;

            case "Recs":
                Item[0].SetActive(true);

                if (PlayerPrefs.HasKey("1"))
                    GameObject.FindWithTag("1").GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("1").ToString();
                break;

            case "Sett":
                Item[0].SetActive(true);

                if (PlayerPrefs.GetString("Vibro") == "True" && Item[3].GetComponent<Image>().sprite != SprItem[0])
                {
                    Item[3].GetComponent<Image>().sprite = SprItem[0];

                    Item[3].transform.GetChild(0).gameObject.SetActive(false);
                    Item[3].transform.GetChild(1).gameObject.SetActive(true);
                }

                if (PlayerPrefs.GetString("Music") == "True" && Item[2].GetComponent<Image>().sprite != SprItem[0])
                {
                    Item[2].GetComponent<Image>().sprite = SprItem[0];

                    Item[2].transform.GetChild(0).gameObject.SetActive(false);
                    Item[2].transform.GetChild(1).gameObject.SetActive(true);
                }

                if (PlayerPrefs.GetString("Sound") == "True" && Item[1].GetComponent<Image>().sprite != SprItem[0])
                {
                    Item[1].GetComponent<Image>().sprite = SprItem[0];

                    Item[1].transform.GetChild(0).gameObject.SetActive(false);
                    Item[1].transform.GetChild(1).gameObject.SetActive(true);
                }
                break;

            case "Quit":
                Application.Quit();
                break;

            case "Back":
                transform.parent.gameObject.SetActive(false);
                break;

            case "Left":
                Item[0].GetComponent<Rigidbody2D>().angularVelocity = 0;
                break;

            case "Right":
                Item[0].GetComponent<Rigidbody2D>().angularVelocity = 0;
                break;

            case "Main":
                SceneManager.LoadScene(0);
                break;

            default: break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (name)
        {
            case "Left":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                if(Item[0].GetComponent<Rigidbody2D>().angularVelocity == 0)
                    Item[0].GetComponent<Rigidbody2D>().angularVelocity += 150;
                break;

            case "Right":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                if (Item[0].GetComponent<Rigidbody2D>().angularVelocity == 0)
                    Item[0].GetComponent<Rigidbody2D>().angularVelocity -= 150;
                break;

            case "Play":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Sound":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Music":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Vibro":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Recs":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Sett":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Quit":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Back":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Main":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            default: break;
        }
    }

    void SetOnorOff(bool set)
    {
        if (set)
        {
            GetComponent<Image>().sprite = SprItem[1];
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else {
            GetComponent<Image>().sprite = SprItem[0];
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
