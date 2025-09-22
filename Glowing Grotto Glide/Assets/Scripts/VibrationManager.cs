using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public static void Vibrate(long milliseconds)
    {
        Handheld.Vibrate();
    }
}
