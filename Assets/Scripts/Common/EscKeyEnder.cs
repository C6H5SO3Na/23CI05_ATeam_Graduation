using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscKeyEnder : MonoBehaviour
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
