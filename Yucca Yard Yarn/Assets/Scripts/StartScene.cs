using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public Image[] Bg;

    public Sprite SprBg;

    public static bool CanAddScore = false, Destroying = false, CanCheck = false;

    public static List<GameObject> TMPobj = new List<GameObject>();

    public static List<Sprite> TMPspr = new List<Sprite>();

    public static int lvl = 1, target = 500, counter = 0;

    public Image[] Items;

    public Sprite[] SprItem;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        if (PlayerPrefs.HasKey("Bg"))
            foreach (var bg in Bg)
                bg.sprite = SprBg;

        Destroying = false;
        CanCheck = false;
        CanAddScore = false;

        counter = 0;

        TMPobj.Clear();
        TMPspr.Clear();

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            foreach (var it in Items)
            {
                if(StartScene.counter < 3 &&UnityEngine.Random.Range(0,500) > 490)
                {
                    it.GetComponent<Image>().sprite = SprItem[SprItem.Length - 1];
                    counter+=1;
                }
                else it.GetComponent<Image>().sprite = SprItem[UnityEngine.Random.Range(0, SprItem.Length-1)];
            }

            Item[1].GetComponent<TMP_Text>().text = "0/"+target.ToString();

            CanCheck = true;

            StartCoroutine(AddScore());
            StartCoroutine(Timer());
        }

        Time.timeScale = 1;
    }

    IEnumerator AddScore()
    {
        yield return new WaitForSeconds(0.5f);
        CanAddScore = true;
    }

    float seconds = 120;

    public TMP_Text timer;

    public GameObject[] Item;

    IEnumerator Timer()
    {
        seconds--;
        timer.text = TimeSpan.FromSeconds(seconds).ToString(@"m\:ss");

        yield return new WaitForSeconds(1);

        if (seconds <= 0)
        {
            Time.timeScale = 0;
            Item[0].SetActive(true);
        }
        else StartCoroutine(Timer());
    }
}
