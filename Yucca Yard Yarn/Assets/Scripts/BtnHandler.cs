using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject[] Item;

    public Sprite[] SprItem;

    public Image[] Bg;

    public Sprite SprBg;

    bool touched = false;

    static int lvl = 1;

    public void OnPointerUp(PointerEventData eventData)
    {
        switch(name)
        {
            case "Set":
                if(transform.GetChild(0).GetComponent<TMP_Text>().text != "is set")
                {
                    foreach (var i in GameObject.FindGameObjectsWithTag("text"))
                        i.GetComponent<TMP_Text>().text = "set";

                    transform.GetChild(0).GetComponent<TMP_Text>().text = "is set";

                    if (int.Parse(transform.parent.GetComponent<Image>().sprite.name.Replace("bg_s_", "")) == 1)
                        PlayerPrefs.DeleteKey("bg");
                    else PlayerPrefs.SetInt("Bg", 1);

                    foreach (var bg in Bg)
                        bg.sprite = SprBg;
                }
                break;

            case "Start":
                Item[0].SetActive(true);

                for (int i = 1; i < 10; i++)
                {
                    if (PlayerPrefs.HasKey("Lvl" + i.ToString()))
                        Item[i].GetComponent<Image>().color = Color.white;
                    else break;
                }
                break;

            case "Lvl":
                if (GetComponent<Image>().color != Color.white)
                    break;

                StartScene.lvl = int.Parse(transform.GetChild(0).GetComponent<TMP_Text>().text.Replace("level ", ""));

                StartScene.target = 400 + (100 * StartScene.lvl);

                SceneManager.LoadScene(1);
                break;

            case "Sett":
                Item[0].SetActive(true);

                if (PlayerPrefs.HasKey("Bg"))
                {
                    foreach (var i in GameObject.FindGameObjectsWithTag("text"))
                        i.GetComponent<TMP_Text>().text = "set";

                    Item[1].GetComponent<TMP_Text>().text = "is set";
                }
                break;

            case "Back":
                transform.parent.gameObject.SetActive(false);
                break;

            case "Main":
                SceneManager.LoadScene(0);
                break;

            case "Restart":
                SceneManager.LoadScene(1);
                break;

            case "Resume":
                Item[0].SetActive(false);
                Time.timeScale = 1;
                break;

            case "Recs":
                Item[0].SetActive(true);
                if (!touched)
                {
                    touched = true;

                    for (int i = 1; i < 6; i++)
                        if (PlayerPrefs.HasKey("Time" + i.ToString() + lvl.ToString()))
                            GameObject.FindWithTag(i.ToString()).GetComponent<TMP_Text>().text = i.ToString() + ". " + PlayerPrefs.GetString("Time" + i.ToString() + lvl.ToString());
                        else break;
                }
                break;

            case "Right":
                if (lvl == 10)
                    break;

                lvl++;

                Item[0].GetComponent<TMP_Text>().text = "Level " + lvl.ToString();

                for (int i = 1; i < 4; i++)
                    if (PlayerPrefs.HasKey("Time" + i.ToString() + lvl.ToString()))
                        GameObject.FindWithTag(i.ToString()).GetComponent<TMP_Text>().text = i.ToString() + ". " + PlayerPrefs.GetString("Time" + i.ToString() + lvl.ToString());
                    else GameObject.FindWithTag(i.ToString()).GetComponent<TMP_Text>().text = i.ToString() + ". " + "0:00";
                break;

            case "Left":
                if (lvl == 1)
                    break;

                lvl--;

                Item[0].GetComponent<TMP_Text>().text = "Level " + lvl.ToString();

                for (int i = 1; i < 4; i++)
                    if (PlayerPrefs.HasKey("Time" + i.ToString() + lvl.ToString()))
                        GameObject.FindWithTag(i.ToString()).GetComponent<TMP_Text>().text = i.ToString() + ". " + PlayerPrefs.GetString("Time" + i.ToString() + lvl.ToString());
                    else GameObject.FindWithTag(i.ToString()).GetComponent<TMP_Text>().text = i.ToString() + ". " + "0:00";
                break;

            case "Next":
                StartScene.lvl += 1;

                StartScene.target = 400 + (100 * StartScene.lvl);

                SceneManager.LoadScene(1);
                break;

            default: break;
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        switch(name)
        {
            case "Pause":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                Time.timeScale = 0;
                Item[0].SetActive(true);
                break;

            case "Set":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Start":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Lvl":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Sett":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Back":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Main":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Restart":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Resume":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Recs":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Right":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Left":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Next":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            default: break;
        }
    }
}
