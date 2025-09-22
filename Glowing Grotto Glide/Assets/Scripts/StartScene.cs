using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public static bool CanVibro;

    public static bool Ending = false;

    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;


        Ending = false;
        
        if (PlayerPrefs.HasKey("Sound") && PlayerPrefs.GetString("Sound") == "True")
            GameObject.FindWithTag("Sound").GetComponent<AudioSource>().mute = false;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (PlayerPrefs.HasKey("Music") && PlayerPrefs.GetString("Music") == "True")
                GameObject.FindWithTag("Music").GetComponent<AudioSource>().Play();

            if (PlayerPrefs.HasKey("Vibro") && PlayerPrefs.GetString("Vibro") == "True")
                CanVibro = true;
            else CanVibro = false;

            StartCoroutine(CreateObj());
        }

        Time.timeScale = 1;
    }

    public GameObject[] objs;

    IEnumerator CreateObj()
    {
        if (!Ending)
        {
            int index = Random.Range(1, objs.Length);

            GameObject clone = Instantiate(objs[index], new Vector3(0, 0, 0), Quaternion.identity);

            if (index == 1)
                clone.name = "bomb";
            else
                clone.name = "star";

            clone.transform.SetParent(objs[0].transform.parent, false);

            if (Random.Range(0, 101) > 75)
                clone.transform.position = new Vector3(objs[0].transform.position.x, Random.Range(objs[0].transform.position.y, objs[1].transform.position.y), objs[0].transform.position.z);
            else if (Random.Range(0, 101) > 50)
                clone.transform.position = new Vector3(objs[2].transform.position.x, Random.Range(objs[0].transform.position.y, objs[1].transform.position.y), objs[0].transform.position.z);
            else if (Random.Range(0, 101) > 25)
                clone.transform.position = new Vector3(Random.Range(objs[0].transform.position.x, objs[2].transform.position.x), objs[0].transform.position.y, objs[0].transform.position.z);
            else
                clone.transform.position = new Vector3(Random.Range(objs[0].transform.position.x, objs[2].transform.position.x), objs[1].transform.position.y, objs[0].transform.position.z);

            clone.SetActive(true);

            yield return new WaitForSeconds(Random.Range(0.1f, 1.5f));

            StartCoroutine(CreateObj());
        }
    }
}
