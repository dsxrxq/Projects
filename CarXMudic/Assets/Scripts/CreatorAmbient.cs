using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorAmbient : MonoBehaviour
{
    public GameObject[] constr;

    void Start()
    {
        for (int i = 8; i < transform.childCount; i++)
        {
            poses.Add(transform.GetChild(i).position);

            if (i == transform.childCount - 1)
                for (int j = 8; j < transform.childCount; j++)
                {
                    int rand = Random.Range(0, poses.Count);

                    transform.GetChild(j).transform.position = new Vector3(transform.GetChild(j).transform.position.x, transform.GetChild(j).transform.position.y, poses[rand].z);
                    poses.RemoveAt(rand);
                }
        }

        StartCoroutine(Creation());
    }

    List<Vector3> poses = new List<Vector3>();

    IEnumerator Creation()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 0.6f));

        int rand = Random.Range(0, constr.Length);

        GameObject clone = Instantiate(constr[rand], new Vector3(0, 0, 0), Quaternion.identity);
        clone.transform.position = constr[rand].transform.position;
        clone.transform.rotation = constr[rand].transform.rotation;
        clone.transform.SetParent(transform, false);
        clone.SetActive(true);
        clone.name = "construction";

        StartCoroutine(Creation());
    }
}
