using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bike : MonoBehaviour
{
    Coroutine coroutine;
    Coroutine coroutine2;

    public Image Btn, Win;

    public Sprite SprBtn;

    public TMP_Text score, Text;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Finish")
        {
            StartScene.End = true;

            if (PlayerPrefs.GetInt("1", 0) < int.Parse(score.text))
                PlayerPrefs.SetInt("1", int.Parse(score.text));

            foreach (var gmb in GameObject.FindGameObjectsWithTag("Objects"))
                gmb.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            Win.gameObject.SetActive(true);
            Text.gameObject.SetActive(true);

            StartCoroutine(ShowWin());
            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowWin()
    {
        while (Win.color.a < 0.5f)
        {
            yield return new WaitForSeconds(0.01f);

            Win.color = new Color(0, 0, 0, Win.color.a + 0.01f);
        }

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(0);
    }

    IEnumerator ShowText()
    {
        while (Text.color.a < 1f)
        {
            yield return new WaitForSeconds(0.005f);
            Text.color = new Color(1, 1, 1, Text.color.a + 0.01f);
        }
    }

    IEnumerator AddScore(int add)
    {
        yield return new WaitForSeconds(1f);

        if ((transform.rotation.eulerAngles.z > 30 && transform.rotation.eulerAngles.z < 90) || (transform.rotation.eulerAngles.z < 330 && transform.rotation.eulerAngles.z > 90))
            score.text = (int.Parse(score.text) + add).ToString();

        coroutine = null;
    }

    void FixedUpdate()
    {
        if (!StartScene.End)
        {
            float zRotation = transform.rotation.eulerAngles.z;

            if (coroutine2 == null)
                coroutine2 = StartCoroutine(CheckFlip());

            if ((zRotation > 30 && zRotation < 90) || (zRotation < 330 && zRotation > 90))
            {
                if (coroutine == null)
                {
                    if (zRotation > 30 && zRotation < 90)
                        coroutine = StartCoroutine(AddScore(1));
                    else if (zRotation < 330 && zRotation > 90)
                        coroutine = StartCoroutine(AddScore(1));
                }
            }
        }
    }
    IEnumerator CheckFlip()
    {
        float totalRotation = 0f;
        float previousRotation = transform.rotation.eulerAngles.z;

        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            float currentRotation = transform.rotation.eulerAngles.z;
            float rotationDelta = currentRotation - previousRotation;

            if (rotationDelta < -180f)
                rotationDelta += 360f;
            else if (rotationDelta > 180f)
                rotationDelta -= 360f;

            totalRotation += rotationDelta;
            previousRotation = currentRotation;

            if (Mathf.Abs(totalRotation) >= 330f)
            {
                if (Btn.sprite != SprBtn)
                    Btn.sprite = SprBtn;
                score.text = (int.Parse(score.text) + 1).ToString();
                totalRotation = 0f;
            }

            if (coroutine == null && ((previousRotation > 345 && previousRotation < 360) || (previousRotation > 0 && previousRotation < 15)))
            {
                coroutine2 = null;
                yield break;
            }
        }
    }
}