using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public static bool isCreated = false;
    public AudioSource player;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //シングルトンパターン適用
        if (isCreated)
        {
            Destroy(gameObject);
        }
        else
        {
            isCreated = true;
        }
    }

    void Play(AudioClip audioClip)
    {
        
    }
}
