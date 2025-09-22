using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    public Sprite[] SprCar;

    public List<GameObject> Bl = new List<GameObject>();

    void Start()
    {
        GetComponent<Image>().sprite = SprCar[Random.Range(0, SprCar.Length)];
        end = transform.eulerAngles.z - 90;
        start = transform.rotation;
        startPos = transform.position;
    }

    float speed = 500;

    float end = 0;

    Quaternion start = Quaternion.identity;

    Vector3 startPos;

    public void reset()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Image>().sprite = SprCar[Random.Range(0, SprCar.Length)];
        transform.rotation = start;
        transform.position = startPos;
        float speed = 500;
        foreach (var cars in GameObject.FindGameObjectsWithTag("Car"))
            Destroy(cars);
        Bl.Clear();
        ss.FastCreater();
    }

    void FixedUpdate()
    {
        if (StartScene.Touched)
        {
            GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        }
    }

    public float rotationSpeed = 2f;

    public TMP_Text score, scoreEnd;

    public GameObject over;

    public StartScene ss;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Car")
        {
            Time.timeScale = 0;
            over.SetActive(true);
            scoreEnd.text = score.text;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Point" && !Bl.Contains(collision.gameObject))
        {
            Bl.Add(collision.gameObject);
            StartCoroutine(Canceling(StartCoroutine(RotateToPoint(collision.transform))));
        }
    }

    Coroutine coroutine;

    IEnumerator Canceling(Coroutine coroutine)
    {
        yield return new WaitForSeconds(0.3f);

        StopCoroutine(coroutine);
    }

    private IEnumerator RotateToPoint(Transform targetTransform)
    {
        while (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(Vector3.forward, targetTransform.position - transform.position)) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward, targetTransform.position - transform.position), rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
