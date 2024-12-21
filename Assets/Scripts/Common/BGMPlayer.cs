using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    static BGMPlayer instance;
    public AudioSource player;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static BGMPlayer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BGMPlayer>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<BGMPlayer>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }
}
