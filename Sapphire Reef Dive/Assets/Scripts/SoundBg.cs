using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBg : MonoBehaviour
{
    // Start is called before the first frame update
    private static SoundBg instance = null;
    public static SoundBg Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    
}
