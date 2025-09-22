using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public GameObject[] Item;

    public Sprite[] SprItem;

    public static bool Touched = false;

    public static float seconds;

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            GameObject.FindWithTag("Music").GetComponent<AudioSource>().time = seconds;
            index = UnityEngine.Random.Range(0, 2);
            StartCoroutine(Creater());
        }
        else seconds = 0;

        Touched = false;

        Time.timeScale = 1;
    }

    int index = 0;

    public void FastCreater()
    {
        if (index == 0)
            index = 1;
        else index = 0;

        GameObject clone = GameObject.Instantiate(Item[index], new Vector3(0, 0, 0), Quaternion.identity);

        clone.transform.SetParent(Item[index].transform.parent, false);

        clone.transform.position = Item[index].transform.position;

        clone.transform.rotation = Item[index].transform.rotation;

        clone.GetComponent<Image>().sprite = SprItem[UnityEngine.Random.Range(0, SprItem.Length)];

        clone.name = "Car";

        clone.tag = "Car";

        clone.SetActive(true);

        if (index == 0)
            clone.GetComponent<Rigidbody2D>().velocity = Vector2.up * 500;
        else clone.GetComponent<Rigidbody2D>().velocity = Vector2.down * 500;
    }

    IEnumerator Creater()
    {
        if (index == 0)
            index = 1;
        else index = 0;

        GameObject clone = GameObject.Instantiate(Item[index], new Vector3(0,0,0), Quaternion.identity);

        clone.transform.SetParent(Item[index].transform.parent,false);

        clone.transform.position = Item[index].transform.position;

        clone.transform.rotation = Item[index].transform.rotation;

        clone.GetComponent<Image>().sprite = SprItem[UnityEngine.Random.Range(0, SprItem.Length)];

        clone.name = "Car";

        clone.tag = "Car";

        clone.SetActive(true);

        if (index == 0)
            clone.GetComponent<Rigidbody2D>().velocity = Vector2.up * 500;
        else clone.GetComponent<Rigidbody2D>().velocity = Vector2.down * 500;

        yield return new WaitForSeconds(UnityEngine.Random.Range(1, 5));

        StartCoroutine(Creater());
    }
}
