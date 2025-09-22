using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Line : MonoBehaviour
{
    public List<GameObject> objs = new List<GameObject>();

    List<GameObject> forDestroy = new List<GameObject>();

    public GameObject[] Items;

    bool Deleteall = false;

    private void Start()
    {
        StartCoroutine(Retring());
    }

    public void AddAllObject()
    {
        foreach(var obj in objs)
            forDestroy.Add(obj);
        Deleteall = true;
    }

    public TMP_Text timer;

    IEnumerator Retring()
    {
        forDestroy.Clear();

        yield return new WaitForSeconds(0.01f);

        if (StartScene.CanCheck)
        {
            if (!Deleteall)
            {
                for (int i = 0; i < objs.Count; i++)
                {
                    if (i == 0 && objs[i].GetComponent<Image>().sprite == objs[i + 1].GetComponent<Image>().sprite && objs[i + 1].GetComponent<Image>().sprite == objs[i + 2].GetComponent<Image>().sprite && objs[i + 2].GetComponent<Image>().sprite == objs[i + 3].GetComponent<Image>().sprite && objs[i + 3].GetComponent<Image>().sprite == objs[i + 4].GetComponent<Image>().sprite && objs[i + 4].GetComponent<Image>().sprite == objs[i + 5].GetComponent<Image>().sprite && objs[i + 5].GetComponent<Image>().sprite == objs[i + 6].GetComponent<Image>().sprite && objs[i + 6].GetComponent<Image>().sprite == objs[i + 7].GetComponent<Image>().sprite)
                    {
                        forDestroy.Add(objs[i]);
                        forDestroy.Add(objs[i + 1]);
                        forDestroy.Add(objs[i + 2]);
                        forDestroy.Add(objs[i + 3]);
                        forDestroy.Add(objs[i + 4]);
                        forDestroy.Add(objs[i + 5]);
                        forDestroy.Add(objs[i + 6]);
                        forDestroy.Add(objs[i + 7]);
                        break;
                    }
                    else if (i <= 1 && objs[i].GetComponent<Image>().sprite == objs[i + 1].GetComponent<Image>().sprite && objs[i + 1].GetComponent<Image>().sprite == objs[i + 2].GetComponent<Image>().sprite && objs[i + 2].GetComponent<Image>().sprite == objs[i + 3].GetComponent<Image>().sprite && objs[i + 3].GetComponent<Image>().sprite == objs[i + 4].GetComponent<Image>().sprite && objs[i + 4].GetComponent<Image>().sprite == objs[i + 5].GetComponent<Image>().sprite && objs[i + 5].GetComponent<Image>().sprite == objs[i + 6].GetComponent<Image>().sprite)
                    {
                        forDestroy.Add(objs[i]);
                        forDestroy.Add(objs[i + 1]);
                        forDestroy.Add(objs[i + 2]);
                        forDestroy.Add(objs[i + 3]);
                        forDestroy.Add(objs[i + 4]);
                        forDestroy.Add(objs[i + 5]);
                        forDestroy.Add(objs[i + 6]);
                        break;
                    }
                    else if (i <= 2 && objs[i].GetComponent<Image>().sprite == objs[i + 1].GetComponent<Image>().sprite && objs[i + 1].GetComponent<Image>().sprite == objs[i + 2].GetComponent<Image>().sprite && objs[i + 2].GetComponent<Image>().sprite == objs[i + 3].GetComponent<Image>().sprite && objs[i + 3].GetComponent<Image>().sprite == objs[i + 4].GetComponent<Image>().sprite && objs[i + 4].GetComponent<Image>().sprite == objs[i + 5].GetComponent<Image>().sprite)
                    {
                        forDestroy.Add(objs[i]);
                        forDestroy.Add(objs[i + 1]);
                        forDestroy.Add(objs[i + 2]);
                        forDestroy.Add(objs[i + 3]);
                        forDestroy.Add(objs[i + 4]);
                        forDestroy.Add(objs[i + 5]);
                        break;
                    }
                    else if (i <= 3 && objs[i].GetComponent<Image>().sprite == objs[i + 1].GetComponent<Image>().sprite && objs[i + 1].GetComponent<Image>().sprite == objs[i + 2].GetComponent<Image>().sprite && objs[i + 2].GetComponent<Image>().sprite == objs[i + 3].GetComponent<Image>().sprite && objs[i + 3].GetComponent<Image>().sprite == objs[i + 4].GetComponent<Image>().sprite)
                    {
                        forDestroy.Add(objs[i]);
                        forDestroy.Add(objs[i + 1]);
                        forDestroy.Add(objs[i + 2]);
                        forDestroy.Add(objs[i + 3]);
                        forDestroy.Add(objs[i + 4]);
                        break;
                    }
                    else if (i <= 4 && objs[i].GetComponent<Image>().sprite == objs[i + 1].GetComponent<Image>().sprite && objs[i + 1].GetComponent<Image>().sprite == objs[i + 2].GetComponent<Image>().sprite && objs[i + 2].GetComponent<Image>().sprite == objs[i + 3].GetComponent<Image>().sprite)
                    {
                        forDestroy.Add(objs[i]);
                        forDestroy.Add(objs[i + 1]);
                        forDestroy.Add(objs[i + 2]);
                        forDestroy.Add(objs[i + 3]);
                        break;
                    }
                    else if (i <= 5 && objs[i].GetComponent<Image>().sprite == objs[i + 1].GetComponent<Image>().sprite && objs[i + 1].GetComponent<Image>().sprite == objs[i + 2].GetComponent<Image>().sprite)
                    {
                        forDestroy.Add(objs[i]);
                        forDestroy.Add(objs[i + 1]);
                        forDestroy.Add(objs[i + 2]);
                        break;
                    }
                }
            }

            Deleteall = false;

            if (forDestroy.Count > 2)
            {
                StartScene.Destroying = true;
                if (StartScene.TMPobj.Count > 0)
                    StartScene.TMPobj[0].GetComponent<Item>().EnableCanTouch();

                foreach (var obj in forDestroy)
                    ChangeSpr(obj.GetComponent<Image>());

                string[] parts = score.text.Split('/');

                if (StartScene.CanAddScore)
                {
                    StartScene.CanAddScore = false;


                    if (parts.Length == 2)
                    {
                        parts[0] = (int.Parse(parts[0]) + (10 * forDestroy.Count)).ToString();
                        score.text = string.Join("/", parts);
                    }
                }

                if (int.Parse(parts[0]) >= StartScene.target)
                {
                    PlayerPrefs.SetString("Lvl" + StartScene.lvl.ToString(), "Claimed");
                    Time.timeScale = 0;
                    Items[0].SetActive(true);

                    Items[1].GetComponent<TMP_Text>().text = timer.text;

                    for (int i = 1; i < 4; i++)
                    {
                        if (PlayerPrefs.GetString("Time" + i.ToString() + StartScene.lvl.ToString(), "00:00") == "00:00")
                        {
                            PlayerPrefs.SetString("Time" + i.ToString() + StartScene.lvl.ToString(), timer.text);
                            break;
                        }
                        else
                        {
                            string time1 = timer.text;
                            string time2 = PlayerPrefs.GetString("Time" + i.ToString() + StartScene.lvl.ToString(), "00:00");

                            if (time1 != time2)
                            {
                                int result = string.Compare(time1, time2);

                                if (result > 0)
                                {
                                    if (i == 1)
                                    {
                                        PlayerPrefs.SetString("Time" + (i+2).ToString() + StartScene.lvl.ToString(), PlayerPrefs.GetString("Time" + (i+1).ToString() + StartScene.lvl.ToString(), "0:00"));
                                        PlayerPrefs.SetString("Time" + (i+1).ToString() + StartScene.lvl.ToString(), PlayerPrefs.GetString("Time" + i.ToString() + StartScene.lvl.ToString(), "0:00"));
                                    }
                                    else PlayerPrefs.SetString("Time" + (i + 1).ToString() + StartScene.lvl.ToString(), PlayerPrefs.GetString("Time" + i.ToString() + StartScene.lvl.ToString(), "0:00"));

                                    PlayerPrefs.SetString("Time" + i.ToString() + StartScene.lvl.ToString(), timer.text);

                                    break;
                                }
                            }
                            else break;
                        }
                    }

                    if (StartScene.lvl == 10)
                        Items[2].SetActive(false);
                }

                forDestroy.Clear();

                StartScene.TMPobj.Clear();
                StartScene.TMPspr.Clear();

            }
            else if (!StartScene.Destroying && StartScene.TMPobj.Count > 0 && !StartScene.TMPobj[0].GetComponent<Item>().Check())
                StartScene.TMPobj[0].GetComponent<Item>().Enable();
        }
        StartCoroutine(Retring());
    }

    void ChangeSpr(Image img)
    {
        Sprite TMP = img.sprite;

        if (StartScene.counter < 3 && UnityEngine.Random.Range(0, 500) > 490)
        {
            img.GetComponent<Image>().sprite = SprItem[SprItem.Length - 1];
            StartScene.counter += 1;
        }
        else img.GetComponent<Image>().sprite = SprItem[UnityEngine.Random.Range(0, SprItem.Length - 1)];

        if (img.sprite == TMP)
            ChangeSpr(img);
    }

    public Sprite[] SprItem;

    public TMP_Text score;
}
