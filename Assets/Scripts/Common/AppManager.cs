using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static bool isCreated = false;
    static string stageName = "1-1";
    public static string StageName
    {
        set { stageName = value; }
        get { return stageName; }
    }
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
