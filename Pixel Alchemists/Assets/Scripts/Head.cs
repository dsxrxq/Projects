using TMPro;
using UnityEngine;

public class Head : MonoBehaviour
{
    public GameObject Over;

    public TMP_Text score;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Top" || collision.name == "Down" || collision.name == "Up" || collision.name == "Bot")
        {
            Time.timeScale = 0;

            if (PlayerPrefs.GetInt("1", 0) < int.Parse(score.text))
                PlayerPrefs.SetInt("1", int.Parse(score.text));

            Over.SetActive(true);
        }
    }
}
