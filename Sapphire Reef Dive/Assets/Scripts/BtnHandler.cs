using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnHandler : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public GameObject[] Item;

    public Sprite[] SprItem;
    
    public void OnPointerUp(PointerEventData eventData)
    {
        switch (name)
        {
            case "Bgs":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();

                Item[0].SetActive(true);

                if (PlayerPrefs.HasKey("Bg2"))
                {
                    Item[4].SetActive(false);
                    if (!Item[3].GetComponent<Button>())
                        Item[3].AddComponent<Button>();
                }

                if (PlayerPrefs.HasKey("Bg"))
                {
                    Item[3].transform.SetParent(Item[2].transform, false);
                    Item[3].transform.position = Item[2].transform.position;
                }
                break;

            case "Exit":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();

                if (transform.parent.name == "Lvls")
                    transform.parent.gameObject.SetActive(false);
                else transform.parent.parent.gameObject.SetActive(false);
                break;

            case "Lvl":
                if (transform.childCount > 0 && transform.GetChild(0).gameObject.activeSelf)
                    return;

                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();

                StartScene.lvl = int.Parse(GetComponent<Image>().sprite.name.Replace("level_", ""));

                SceneManager.LoadScene(1);
                break;

            case "Restart":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();

                SceneManager.LoadScene(1);
                break;

            case "Main":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();

                SceneManager.LoadScene(0);
                break;

            case "Start":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();

                Item[0].SetActive(true);

                for (int i = 1; i < 10; i++)
                {
                    if (PlayerPrefs.HasKey("Lvl"+i.ToString()))
                    {
                        Item[i].transform.GetChild(0).gameObject.SetActive(false);
                        Item[i].AddComponent<Button>();
                    }
                }
                break;

            case "Sett":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();

                Item[0].SetActive(true);

                if (PlayerPrefs.HasKey("Sound") && PlayerPrefs.GetString("Sound") == "True")
                    Item[1].GetComponent<Image>().sprite = SprItem[0];

                break;

            case "Resume":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();

                Item[0].SetActive(false);

                Time.timeScale = 1;
                break;

            default:break;
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        switch (name)
        {
            case "Bg":
                if(transform.childCount > 0 && transform.GetChild(0).name == "Buy" && transform.GetChild(0).gameObject.activeSelf)
                {
                    if (PlayerPrefs.GetInt("Balance", 0) < 700)
                        return;

                    GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();

                    PlayerPrefs.SetInt("Balance", (PlayerPrefs.GetInt("Balance") - 700));

                    PlayerPrefs.SetInt("Bg2", 1);

                    gameObject.AddComponent<Button>();

                    transform.GetChild(0).gameObject.SetActive(false);

                    return;
                }

                if ((transform.childCount == 1 && transform.GetChild(0).name == "Image") || (transform.childCount == 2 && transform.GetChild(1).name == "Image"))
                    return;

                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();

                Item[0].transform.SetParent(transform, false);

                Item[0].transform.position = transform.position;

                if (GetComponent<Image>().sprite.name.Contains("2"))
                    PlayerPrefs.SetInt("Bg", 1);
                else PlayerPrefs.DeleteKey("Bg");

                for (int i = 1; i < Item.Length; i++)
                    Item[i].GetComponent<Image>().sprite = SprItem[0];
                break;

            case "Sound":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                if (!PlayerPrefs.HasKey("Sound") || PlayerPrefs.GetString("Sound") == "False")
                {
                    GetComponent<Image>().sprite = SprItem[1];

                    GameObject.FindWithTag("Sound").GetComponent<AudioSource>().mute = false;

                    PlayerPrefs.SetString("Sound", "True");
                }
                else
                {
                    GetComponent<Image>().sprite = SprItem[0];

                    GameObject.FindWithTag("Sound").GetComponent<AudioSource>().mute = true;

                    PlayerPrefs.SetString("Sound", "False");
                }
                break;

            case "Pause":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();

                Time.timeScale = 0;

                Item[0].SetActive(true);
                break;

            default: break;
        }
    }
}
