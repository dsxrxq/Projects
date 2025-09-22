using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float start = 0;

    Rigidbody2D rb2;

    RectTransform rt;

    float temp = 0;

    bool OnRoad = true;

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (LayerMask.LayerToName(coll.gameObject.layer).Contains("Road"))
            OnRoad = true;
    }

    public void OnCollisionExit2D(Collision2D coll)
    {
        if (LayerMask.LayerToName(coll.gameObject.layer).Contains("Road"))
            OnRoad = false;
    }

    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        rt = GetComponent<RectTransform>();
        start = rt.anchoredPosition.x;

        StartCoroutine(SpeedControl());
        StartCoroutine(SpeedUp());
    }

    IEnumerator SpeedControl()
    {
        yield return new WaitForSeconds(2.5f);

        if (!StartScene.End && !StartScene.SpeedUp && OnRoad)
        {
            if (Random.Range(0, 101) > 50)
            {
                temp = start + Random.Range(-150, -25);
                if (temp > rt.anchoredPosition.x)
                {
                    rb2.constraints = RigidbodyConstraints2D.None;
                    while (rt.anchoredPosition.x < temp)
                    {
                        yield return new WaitForSeconds(0.01f);
                        if (StartScene.End)
                            yield break;
                        else if (StartScene.SpeedUp)
                        {
                            StartCoroutine(SpeedControl());
                            yield break;
                        }
                        if (rb2.velocity.x < 200)
                            rb2.velocity = new Vector2(rb2.velocity.x + 10, rb2.velocity.y);
                        else rb2.velocity = new Vector2(200, rb2.velocity.y);
                    }
                }
                else if (temp < rt.anchoredPosition.x)
                {
                    rb2.constraints = RigidbodyConstraints2D.None;
                    while (rt.anchoredPosition.x > temp)
                    {
                        yield return new WaitForSeconds(0.01f);
                        if (StartScene.End)
                            yield break;
                        else if (StartScene.SpeedUp)
                        {
                            StartCoroutine(SpeedControl());
                            yield break;
                        }
                        if (rb2.velocity.x > -200)
                            rb2.velocity = new Vector2(rb2.velocity.x - 10, rb2.velocity.y);
                        else rb2.velocity = new Vector2(-200, rb2.velocity.y);
                    }
                }
                temp = 0;
                if (rb2.velocity.x > 0)
                    while (rb2.velocity.x > 3)
                    {
                        yield return new WaitForSeconds(0.01f);
                        if (StartScene.End)
                            yield break;
                        else if (StartScene.SpeedUp)
                        {
                            StartCoroutine(SpeedControl());
                            yield break;
                        }
                        rb2.velocity = new Vector2(rb2.velocity.x - 5f, rb2.velocity.y);
                    }
                else if (rb2.velocity.x < 0)
                    while (rb2.velocity.x < 3)
                    {
                        yield return new WaitForSeconds(0.01f);
                        if (StartScene.End)
                            yield break;
                        else if (StartScene.SpeedUp)
                        {
                            StartCoroutine(SpeedControl());
                            yield break;
                        }
                        rb2.velocity = new Vector2(rb2.velocity.x + 5f, rb2.velocity.y);
                    }

                rb2.constraints = RigidbodyConstraints2D.FreezePositionX;
            }
        }
        StartCoroutine(SpeedControl());
    }

    IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(0.1f);
        if (StartScene.SpeedUp)
        {
            temp = start - 150;

            rb2.constraints = RigidbodyConstraints2D.None;
            while (rt.anchoredPosition.x > temp)
            {
                yield return new WaitForSeconds(0.01f);
                if (StartScene.End)
                    yield break;
                if (rb2.velocity.x > -300)
                    rb2.velocity = new Vector2(rb2.velocity.x - 15, rb2.velocity.y);
                else rb2.velocity = new Vector2(-300, rb2.velocity.y);
            }

            temp = 0;

            if (rb2.velocity.x < 0)
                while (rb2.velocity.x < 0)
                {
                    yield return new WaitForSeconds(0.01f);
                    if (StartScene.End)
                        yield break;
                    rb2.velocity = new Vector2(rb2.velocity.x + 10, rb2.velocity.y);
                }

            rb2.constraints = RigidbodyConstraints2D.FreezePositionX;

            StartScene.BuildFaster = true;

            yield return new WaitForSeconds(3);

            StartScene.SpeedUp = false;

            yield return new WaitForSeconds(0.5f);

            StartScene.BuildFaster = false;
        }
        StartCoroutine(SpeedUp());
    }

    void FixedUpdate()
    {
        if (transform.rotation.eulerAngles.z < (360 - 20) && transform.rotation.eulerAngles.z > 50)
            transform.rotation = Quaternion.Euler(0, 0, 360 - 20);
        else if (transform.rotation.eulerAngles.z < (360 - 50) && transform.rotation.eulerAngles.z > 35)
            transform.rotation = Quaternion.Euler(0, 0, 35);
    }
}
