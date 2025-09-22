using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject[] Item;
    public Sprite[] SprItem;

    public TMP_Text score;

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (name)
        {
            case "Play":
                SceneManager.LoadScene(1);
                break;

            case "Exit":
                Application.Quit();
                break;

            case "Sett":
                Item[0].SetActive(true);

                if (PlayerPrefs.HasKey("Sound") && PlayerPrefs.GetString("Sound") == "True")
                {
                    Item[1].GetComponent<Image>().sprite = SprItem[0];
                    if (Item[2].activeSelf)
                    {
                        Item[2].SetActive(false);
                        Item[3].SetActive(true);
                    }
                }

                if (PlayerPrefs.HasKey("Music") && PlayerPrefs.GetString("Music") == "True")
                {
                    Item[4].GetComponent<Image>().sprite = SprItem[0];
                    if (Item[5].activeSelf)
                    {
                        Item[5].SetActive(false);
                        Item[6].SetActive(true);
                    }
                }
                break;

            case "Back":
                transform.parent.gameObject.SetActive(false);
                break;

            case "Recs":
                Item[0].SetActive(true);
                GameObject.FindWithTag("1").GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("1", 0).ToString();
                break;

            case "SpeedUp":
                if (!StartScene.SpeedUp && !GetComponent<Image>().sprite.name.Contains("inactive"))
                {
                    StartScene.SpeedUp = true;
                    GetComponent<Image>().sprite = SprItem[0];
                }
                break;

            case "TryAgain":
                SceneManager.LoadScene(1);
                break;

            case "Continue":
                Item[0].SetActive(false);
                Time.timeScale = 1;
                break;

            case "Main":
                if (PlayerPrefs.GetInt("1", 0) < int.Parse(score.text))
                    PlayerPrefs.SetInt("1", int.Parse(score.text));

                SceneManager.LoadScene(0);
                break;

            default: break;
        }

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        switch (name)
        {
            case "Sound":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                if (!PlayerPrefs.HasKey("Sound") || PlayerPrefs.GetString("Sound") == "False")
                {
                    Item[0].GetComponent<Image>().sprite = SprItem[1];

                    Item[1].SetActive(false);
                    Item[2].SetActive(true);

                    GameObject.FindWithTag("Sound").GetComponent<AudioSource>().mute = false;

                    PlayerPrefs.SetString("Sound", "True");
                }
                else
                {
                    Item[0].GetComponent<Image>().sprite = SprItem[0];

                    Item[2].SetActive(false);
                    Item[1].SetActive(true);

                    GameObject.FindWithTag("Sound").GetComponent<AudioSource>().mute = true;

                    PlayerPrefs.SetString("Sound", "False");
                }
                break;

            case "Pause":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                Time.timeScale = 0;
                Item[0].SetActive(true);
                break;

            case "Music":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                if (!PlayerPrefs.HasKey("Music") || PlayerPrefs.GetString("Music") == "False")
                {
                    Item[0].GetComponent<Image>().sprite = SprItem[1];

                    Item[1].SetActive(false);
                    Item[2].SetActive(true);

                    PlayerPrefs.SetString("Music", "True");
                }
                else
                {
                    Item[0].GetComponent<Image>().sprite = SprItem[0];

                    Item[2].SetActive(false);
                    Item[1].SetActive(true);

                    PlayerPrefs.SetString("Music", "False");
                }
                break;

            case "Play":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Exit":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Sett":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Back":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Recs":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "TryAgain":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Continue":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Main":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;


            default: break;
        }
    }
}
