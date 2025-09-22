using TMPro;
using UnityEngine;

public class Road : MonoBehaviour
{
    public TMP_Text score;

    public Car car;

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "MyCar")
        {
            StartScene.Touched = false;

            score.text = (int.Parse(score.text) + 1).ToString();

            car.reset();
        }
    }
}
