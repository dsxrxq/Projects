using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerConroller : MonoBehaviour
{
    private float leftX = -10174f;
    private float centerX = -9591f;
    private float rightX = -9055f;

    private Vector3 targetPosition;
    private bool moveLeft;
    private bool moveRight;
    private bool stop;

    void Start()
    {
        StartCoroutine(LeftRightRotate());

        float y = transform.position.y;
        float z = transform.position.z;

        targetPosition = new Vector3(centerX, y, z);
        transform.position = targetPosition;

        for (int i = 0; i < transform.GetChild(0).childCount; i++)
            transform.GetChild(0).GetChild(i).GetComponent<Rigidbody>().AddTorque(new Vector3(1, 0, 0) * -500000);
    }

    IEnumerator LeftRightRotate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, -10, 0);

            yield return new WaitForSeconds(0.15f);

            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 10, 0);

            yield return new WaitForSeconds(0.15f);

            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, 0);
        }

        yield return new WaitForSeconds(0.001f);

        StartCoroutine(LeftRightRotate());
    }

    public GameObject Pause, Over;
    public TMP_Text MainScore, PauseScore, OverScore;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !Pause.activeSelf && !Over.activeSelf) StartCoroutine(LeftRight(true));
        if (Input.GetKeyDown(KeyCode.D) && !Pause.activeSelf && !Over.activeSelf) StartCoroutine(LeftRight(false));
        if (Input.GetKeyDown(KeyCode.Escape) && !Over.activeSelf)
        {
            if (!Pause.activeSelf)
            {
                Time.timeScale = 0;
                Pause.SetActive(true);
                PauseScore.transform.parent.gameObject.SetActive(true);
                PauseScore.text = MainScore.text;
                MainScore.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 1;
                Pause.SetActive(false);
                PauseScore.transform.parent.gameObject.SetActive(false);
                MainScore.transform.parent.gameObject.SetActive(true);
            }

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Cars")
        {
            Time.timeScale = 0;
            Over.SetActive(true);
            OverScore.transform.parent.gameObject.SetActive(true);
            OverScore.text = MainScore.text;
            MainScore.transform.parent.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        float y = transform.position.y;
        float z = transform.position.z;

        if (moveLeft)
        {
            if (Mathf.Approximately(targetPosition.x, centerX))
                targetPosition = new Vector3(leftX, y, z);
            else if (Mathf.Approximately(targetPosition.x, rightX))
                targetPosition = new Vector3(centerX, y, z);

            moveLeft = false;
        }

        if (moveRight)
        {
            if (Mathf.Approximately(targetPosition.x, centerX))
                targetPosition = new Vector3(rightX, y, z);
            else if (Mathf.Approximately(targetPosition.x, leftX))
                targetPosition = new Vector3(centerX, y, z);

            moveRight = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 100f);
    }

    IEnumerator LeftRight(bool deadinsult)
    {
        yield return new WaitForSeconds(0.1f);

        if (deadinsult)
            moveLeft = true;
        else moveRight = true;
    }
}
