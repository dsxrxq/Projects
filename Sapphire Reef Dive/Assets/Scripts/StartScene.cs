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

    public static int lvl;

    public GameObject[] lvls;

    public TMP_Text balance;

    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        if (PlayerPrefs.HasKey("Sound") && PlayerPrefs.GetString("Sound") == "True")
        {
            if (GameObject.FindWithTag("Sound").GetComponent<AudioSource>().mute)
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().mute = false;

            if (SceneManager.GetActiveScene().buildIndex == 1)
                GameObject.FindWithTag("Music").GetComponent<AudioSource>().Play();
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            balance.text = PlayerPrefs.GetInt("Balance").ToString();
            for (int i = 1; i < 11; i++)
            {
                if (lvl == i)
                {
                    lvls[i - 1].SetActive(true);
                    break;
                }
            }
        }

        if(PlayerPrefs.HasKey("Bg"))
            foreach(var bg in Bg)
                bg.sprite = SprBg;

        Time.timeScale = 1;
    }
}
