using System.Collections;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public GameObject[] Example; // 0-bot 1-top 2-up 3-down 4-botfinish 5-topfinish

    bool canFinish = false, finishCreated = false;

    void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(Random.Range(60, 120));

        canFinish = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Bot")
        {
            if (canFinish)
            {
                if (!finishCreated)
                {
                    finishCreated = true;
                    Create(Example[4], "Bot");
                }
                else
                    Create(Example[0], "Bot");
            }
            else
            {
                if (Random.Range(0, 101) > 50)
                    Create(Example[0], "Bot");

                else
                    Create(Example[2], "Up");
            }
        }
        else if (collision.name == "Top")
        {
            if (canFinish)
            {
                if (!finishCreated)
                {
                    finishCreated = true;
                    Create(Example[5], "Top");
                }
                else Create(Example[1], "Top");
            }
            else
            {
                if (Random.Range(0, 101) > 50)
                    Create(Example[1], "Top");

                else
                    Create(Example[3], "Down");
            }
        }
        else if (collision.name == "Up")
        {
            if (canFinish)
            {
                finishCreated = true;
                Create(Example[5], "Top");
            }
            else
            {
                if (Random.Range(0, 101) > 50)
                    Create(Example[3], "Down");

                else
                    Create(Example[1], "Top");
            }
        }
        else if (collision.name == "Down")
        {
            if (canFinish)
            {
                finishCreated = true;
                Create(Example[4], "Bot");
            }
            else
            {
                if (Random.Range(0, 101) > 50)
                    Create(Example[2], "Up");

                else
                    Create(Example[0], "Bot");
            }
        }
    }

    void Create(GameObject obj, string name)
    {
        GameObject clone = Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);
        clone.transform.SetParent(obj.transform.parent, false);
        if (StartScene.SpeedUp || StartScene.BuildFaster)
            clone.GetComponent<RectTransform>().anchoredPosition = new Vector2(obj.GetComponent<RectTransform>().anchoredPosition.x - 6.9f, obj.GetComponent<RectTransform>().anchoredPosition.y);
        else clone.transform.position = obj.transform.position;
        clone.name = name;
        clone.SetActive(true);
    }
}
