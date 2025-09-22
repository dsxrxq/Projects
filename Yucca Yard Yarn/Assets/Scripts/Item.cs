using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
{
    Vector2 pos;

    string direction = "";
    public string reverse = "";

    static bool canTouch = true;

    public GameObject LineHorizontal, LineVertical;

    float speed = 800;
    GameObject objclick;

    void Start()
    {
        canTouch = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canTouch && Input.touchCount > 0)
        {
            GameObject.FindWithTag("Sound").GetComponent<AudioSource>().Play();
            objclick = gameObject;
            Touch touch = Input.GetTouch(0);
            pos = touch.position;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameObject == objclick && canTouch && Input.touchCount > 0)
        {
            Vector2 touchPos = Input.GetTouch(0).position;

            RectTransform rectTransform = GetComponent<RectTransform>();
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            float minX = Mathf.Min(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
            float maxX = Mathf.Max(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
            float minY = Mathf.Min(corners[0].y, corners[1].y, corners[2].y, corners[3].y);
            float maxY = Mathf.Max(corners[0].y, corners[1].y, corners[2].y, corners[3].y);

            if (touchPos.x >= minX && touchPos.x <= maxX && touchPos.y >= minY && touchPos.y <= maxY)
                return;

            canTouch = false;
            StartScene.CanCheck = false;
            objclick = null;
            Touch touch = Input.GetTouch(0);
            Vector2 swipe = touch.position - pos;

            if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
            {
                if (swipe.x > 0)
                    direction = "right";
                else
                    direction = "left";
            }
            else
            {
                if (swipe.y > 0)
                    direction = "up";
                else
                    direction = "down";
            }

            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Item").Length; i++)
            {
                if ((direction == "left" && transform.position.x > GameObject.FindGameObjectsWithTag("Item")[i].transform.position.x) || (direction == "right" && GameObject.FindGameObjectsWithTag("Item")[i].transform.transform.position.x > transform.position.x) || (direction == "up" && GameObject.FindGameObjectsWithTag("Item")[i].transform.transform.position.y > transform.position.y) || (direction == "down" && GameObject.FindGameObjectsWithTag("Item")[i].transform.transform.position.y < transform.position.y))
                    break;
                else if (i == GameObject.FindGameObjectsWithTag("Item").Length - 1)
                {
                    direction = "";
                    canTouch = true;
                    StartScene.CanCheck = true;
                    return;
                }
            }

            foreach (var box in GetComponents<BoxCollider2D>())
                box.enabled = true;
        }
        else
            objclick = null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (boxCheck() && collision.name == "GameObject" && direction != "" && coroutine == null)
            if ((direction == "left" && collision.transform.parent.gameObject.transform.position.x < transform.position.x) || (direction == "right" && collision.transform.parent.gameObject.transform.position.x > transform.position.x) || (direction == "up" && collision.transform.parent.gameObject.transform.position.y > transform.position.y) || (direction == "down" && collision.transform.parent.gameObject.transform.position.y < transform.position.y))
                coroutine = StartCoroutine(disable(collision.transform.parent.gameObject));
    }

    bool boxCheck()
    {
        if (GetComponent<BoxCollider2D>().enabled)
            return true;
        else return false;
    }

    public bool Check()
    {
        if (canTouch)
            return true;
        else return false;
    }

    static Coroutine enable;

    Coroutine coroutine;

    public void EnableCanTouch()
    {
        if (enable != null)
        {
            StopCoroutine(enable);
            enable = null;
        }
        canTouch = true;

        StartScene.TMPobj.Clear();
        StartScene.TMPspr.Clear();
    }

    public void Enable()
    {
        if (enable == null)
            enable = StartCoroutine(enableCanTouch());
    }

    IEnumerator enableCanTouch()
    {
        yield return new WaitForSeconds(0.5f);
        if (!StartScene.Destroying)
        {
            StartScene.CanCheck = false;

            Vector3 tmppos = new Vector3(0, 0, 0);
            Vector3 startpos = new Vector3(0, 0, 0);

            tmppos = StartScene.TMPobj[1].transform.position;
            startpos = transform.position;

            foreach (GameObject objs in GameObject.FindGameObjectsWithTag("Item"))
                foreach (var boxes in objs.GetComponents<BoxCollider2D>())
                    if (boxes.enabled)
                        boxes.enabled = false;

            CheckAndMove(gameObject, StartScene.TMPobj[1], reverse);

            while ((reverse == "left" && transform.position.x >= tmppos.x) || (reverse == "right" && transform.position.x <= tmppos.x) || (reverse == "up" && transform.position.y <= tmppos.y) || (reverse == "down" && transform.position.y >= tmppos.y))
                yield return new WaitForFixedUpdate();

            GetComponent<Item>().reverse = "";

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StartScene.TMPobj[1].GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            transform.position = startpos;
            StartScene.TMPobj[1].transform.position = tmppos;

            GetComponent<Image>().sprite = StartScene.TMPspr[0];
            StartScene.TMPobj[1].GetComponent<Image>().sprite = StartScene.TMPspr[1];

            StartScene.TMPobj.Clear();
            StartScene.TMPspr.Clear();

            canTouch = true;
            StartScene.CanCheck = true;
        }
        enable = null;
    }

    void CheckAndMove(GameObject obj, GameObject sobj, string direct)
    {
        if (direct == "left")
        {
            obj.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
            sobj.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        }
        else if (direct == "right")
        {
            obj.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            sobj.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
        }
        else if (direct == "up")
        {
            obj.GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            sobj.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
        }
        else if (direct == "down")
        {
            obj.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
            sobj.GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        }
    }

    IEnumerator disable(GameObject obj)
    {
        foreach (var box in GetComponents<BoxCollider2D>())
            box.enabled = false;

        Vector3 tmppos = new Vector3(0, 0, 0);
        Vector3 startpos = new Vector3(0, 0, 0);

        tmppos = obj.transform.position;
        startpos = transform.position;

        CheckAndMove(gameObject, obj, direction);

        foreach (var box in GetComponents<BoxCollider2D>())
            box.enabled = false;

        reverse = direction;

        while ((direction == "left" && transform.position.x >= tmppos.x) || (direction == "right" && transform.position.x <= tmppos.x) || (direction == "up" && transform.position.y <= tmppos.y) || (direction == "down" && transform.position.y >= tmppos.y))
            yield return new WaitForFixedUpdate();

        direction = "";

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if (!canTouch)
        {
            transform.position = startpos;
            obj.transform.position = tmppos;

            StartScene.TMPspr.Add(GetComponent<Image>().sprite);
            StartScene.TMPspr.Add(obj.GetComponent<Image>().sprite);

            GetComponent<Image>().sprite = StartScene.TMPspr[1];
            obj.GetComponent<Image>().sprite = StartScene.TMPspr[0];
        }

        StartScene.TMPobj.Add(gameObject);
        StartScene.TMPobj.Add(obj);

        StartScene.CanCheck = true;
        StartScene.CanAddScore = true;

        if (GetComponent<Image>().sprite.name.Contains("bomb") || obj.GetComponent<Image>().sprite.name.Contains("bomb"))
        {
            if (reverse == "left" || reverse == "right")
                LineHorizontal.GetComponent<Line>().AddAllObject();
            else
                LineVertical.GetComponent<Line>().AddAllObject();

            StartScene.TMPobj.Clear();
            StartScene.TMPspr.Clear();

            canTouch = true;

            enable = null;

            reverse = "";
        }

        coroutine = null;

        StartScene.Destroying = false;
    }
}
