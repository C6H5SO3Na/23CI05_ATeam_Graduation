using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEPlayer : MonoBehaviour
{
    static SEPlayer instance;
    public AudioSource player;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static SEPlayer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SEPlayer>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<SEPlayer>();
                    singletonObject.name = typeof(SEPlayer).ToString() + " (Singleton)"; DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }
}
