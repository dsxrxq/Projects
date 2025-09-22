using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BtnHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Car car;

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (name)
        {
            case "Play":
                if (SceneManager.GetActiveScene().buildIndex == 1)
                    StartScene.seconds = GameObject.FindWithTag("Music").GetComponent<AudioSource>().time;

                SceneManager.LoadScene(1);
                break;

            case "Menu":
                SceneManager.LoadScene(0);
                break;

            case "Pause":
                if (!Paused)
                {
                    Time.timeScale = 1;
                }
                else Paused = false;
                break;

            case "Quit":
                Application.Quit();
                break;

            default: break;
        }
    }

    bool Paused = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (name)
        {
            case "Touch":
                if (!StartScene.Touched)
                    StartScene.Touched = true;
                break;

            case "Pause":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                if (Time.timeScale == 1)
                {
                    Paused = true;
                    Time.timeScale = 0;
                }
                break;

            case "Quit":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Play":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            case "Menu":
                GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
                break;

            default: break;
        }
    }
}
