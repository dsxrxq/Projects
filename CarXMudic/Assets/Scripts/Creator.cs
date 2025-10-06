using System.Collections;
using UnityEngine;

public class Creator : MonoBehaviour
{
    public GameObject[] cars;

    private void Start()
    {
        StartCoroutine(creation());
    }

    IEnumerator creation()
    {
        CreateCar(Random.Range(0, 6));

        yield return new WaitForSeconds(Random.Range(0.7f,1.5f));

        StartCoroutine(creation());
    }

    void CreateCar(int rand)
    {
        if (rand < 4)
        {
            GameObject car = Instantiate(cars[Random.Range(0, cars.Length)], new Vector3(0, 0, 0), Quaternion.identity);

            car.transform.SetParent(cars[0].transform.parent);

            car.transform.rotation = cars[0].transform.rotation;

            if (rand == 0)
                car.transform.position = cars[0].transform.position;
            else if(rand == 1)
                car.transform.position = cars[1].transform.position;
            else
                car.transform.position = cars[2].transform.position;

            car.SetActive(true);
        }
        else
        {
            GameObject car1= Instantiate(cars[Random.Range(0, cars.Length)], new Vector3(0, 0, 0), Quaternion.identity);
            GameObject car2= Instantiate(cars[Random.Range(0, cars.Length)], new Vector3(0, 0, 0), Quaternion.identity);

            car1.transform.SetParent(cars[0].transform.parent);
            car2.transform.SetParent(cars[0].transform.parent);

            car1.transform.rotation = cars[0].transform.rotation;
            car2.transform.rotation = cars[0].transform.rotation;

            if (rand == 4)
            {
                car1.transform.position = cars[0].transform.position;
                car2.transform.position = cars[1].transform.position;
            }
            else
            {
                car1.transform.position = cars[1].transform.position;
                car2.transform.position = cars[2].transform.position;
            }

            car1.SetActive(true);
            car2.SetActive(true);
        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Cars").Length; i++) 
            GameObject.FindGameObjectsWithTag("Cars")[i].GetComponent<Rigidbody>().linearVelocity = -transform.forward * speed;
    }

    public static float speed = 6000f;
}
