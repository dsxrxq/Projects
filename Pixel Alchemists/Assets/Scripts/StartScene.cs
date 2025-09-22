using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{

    public static bool SpeedUp = false, BuildFaster = false, End = false;

    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (PlayerPrefs.HasKey("Music") && PlayerPrefs.GetString("Music") == "True")
                GameObject.FindWithTag("Music").GetComponent<AudioSource>().Play();
        }
        else
        {
            if (PlayerPrefs.HasKey("Sound") && PlayerPrefs.GetString("Sound") == "True")
            {
                if (GameObject.FindWithTag("Sound").GetComponent<AudioSource>().mute)
                    GameObject.FindWithTag("Sound").GetComponent<AudioSource>().mute = false;
            }
        }

        SpeedUp = false;
        BuildFaster = false;
        End = false;

        Time.timeScale = 1;
    }
}
