using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static bool isCreated = false;
    void Awake()
    {
        //�V���O���g���p�^�[���K�p
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
