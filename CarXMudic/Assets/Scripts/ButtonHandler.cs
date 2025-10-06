using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Rigidbody rb;

    private void Start()
    {
        defaultpos = transform.GetChild(0).transform.position;
        coroutine = null;
        touched = false;
    }

    static bool touched = false;

    public TMP_Text MainScore, PauseScore;

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (name)
        {
            case "Play":
                if (!touched)
                {
                    touched = true;
                    StartCoroutine(ChangeScene());
                }
                break;

            case "Retry":
                SceneManager.LoadScene(1);
                break;

            case "Main":
                SceneManager.LoadScene(0);
                break;

            case "Resume":
                Time.timeScale = 1;
                PauseScore.transform.parent.gameObject.SetActive(false);
                MainScore.transform.parent.gameObject.SetActive(true);
                transform.parent.parent.gameObject.SetActive(false);
                break;

            case "Exit":
                Application.Quit();
                break;

            default:break;
        }
    }

    Vector3 defaultpos;

    bool entered = false;

    Coroutine coroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        entered = true;
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(Entered());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        entered = false;
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(Exit());
    }

    IEnumerator Entered()
    {
        yield return new WaitForSeconds(0.01f);
        if (entered && transform.GetChild(0).transform.position.x > defaultpos.x -20)
            transform.GetChild(0).transform.position = new Vector3(transform.GetChild(0).transform.position.x - 1, transform.GetChild(0).transform.position.y, transform.GetChild(0).transform.position.z);

        coroutine = StartCoroutine(Entered());
    }

    IEnumerator Exit()
    {
        yield return new WaitForSeconds(0.01f);

        if (!entered && transform.GetChild(0).transform.position.x < defaultpos.x)
        {
            transform.GetChild(0).transform.position = new Vector3(transform.GetChild(0).transform.position.x + 1, transform.GetChild(0).transform.position.y, transform.GetChild(0).transform.position.z);
            coroutine = StartCoroutine(Exit());
        }
        else
            coroutine = null;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    IEnumerator ChangeScene()
    {
        for (int i = 0; i < rb.transform.GetChild(0).childCount; i++)
        {
            rb.transform.GetChild(0).GetChild(i).GetComponent<Rigidbody>().AddTorque(new Vector3(1, 0, 0) * -1000);
        }

        for (int i = 200; i < 1000; i += 200)
        {
            rb.AddForce(-rb.transform.forward * i);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < rb.transform.GetChild(0).childCount; i++)
        {
            rb.transform.GetChild(0).GetChild(i).GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        rb.linearVelocity = Vector3.zero;

        for (int i = 0; i < 2; i++)
        {
            rb.transform.GetChild(0).GetChild(i).GetComponent<Rigidbody>().AddTorque(new Vector3(1, 0, 0) * 10000);
        }

        StartCoroutine(FrontWheels());

        StartCoroutine(StartCar());

        for (int i = 2500; i < 100000; i += 2500)
        {
            rb.AddForce(rb.transform.forward * i);
            yield return new WaitForSeconds(0.1f);
        }

        SceneManager.LoadScene(1);

        Destroy(rb.gameObject);
    }

    IEnumerator FrontWheels()
    {
        for (int i = 1000; i < 10000; i+= 1000)
        {
            for (int f = 2; f < 4; f++)
                rb.transform.GetChild(0).GetChild(f).GetComponent<Rigidbody>().AddTorque(new Vector3(1, 0, 0) * i);

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator StartCar()
    {

        rb.AddTorque(new Vector3(0, 1, 0) * -500000);

        yield return new WaitForSeconds(0.75f);

        rb.angularVelocity = new Vector3(0, 0, 0);

        rb.AddTorque(new Vector3(0, 1, 0) * 500000);

        yield return new WaitForSeconds(1f);

        rb.angularVelocity = new Vector3(0, 0, 0);

        rb.AddTorque(new Vector3(0, 1, 0) * -500000);

        yield return new WaitForSeconds(0.25f);

        rb.angularVelocity = new Vector3(0, 0, 0);
    }
}
