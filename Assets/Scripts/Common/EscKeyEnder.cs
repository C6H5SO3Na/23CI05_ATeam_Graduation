using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscKeyEnder : MonoBehaviour
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
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
