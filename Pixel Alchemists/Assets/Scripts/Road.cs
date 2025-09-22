using UnityEngine;

public class Road : MonoBehaviour
{
    void FixedUpdate()
    {
        if (StartScene.End)
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        else if (StartScene.SpeedUp)
            GetComponent<Rigidbody2D>().velocity = Vector2.left * (500 * 1.5f);
        else GetComponent<Rigidbody2D>().velocity = Vector2.left * 500;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Destroy")
            Destroy(gameObject);
    }
}
