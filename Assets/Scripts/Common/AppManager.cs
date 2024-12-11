using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static bool isCreated = false;
    void Awake()
    {
        //シングルトンパターン適用
        if (isCreated)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        isCreated = true;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
