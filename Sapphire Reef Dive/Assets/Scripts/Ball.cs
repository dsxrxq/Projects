using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool startmove = false;

    public GameObject Over;

    public void StartMove()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-20, 20), 30) * 25;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Block")
            collision.GetComponent<Block>().Check();
        else if (collision.name == "Platform" && transform.position.y > collision.transform.position.y)
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x / 25, 30) * 25;
        else if (collision.name == "Destroy")
        {
            if (GameObject.FindGameObjectsWithTag("Ball").Length == 1)
            {
                Time.timeScale = 0;
                Over.SetActive(true);
            }    
            Destroy(gameObject);
        }
    }
}
