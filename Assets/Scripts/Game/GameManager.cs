using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int gameFPS { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //値の初期化
        gameFPS = 60;

        //ゲームフレームレートの固定
        Application.targetFrameRate = gameFPS;
    }

    // Update is called once per frame
    void Update()
    {
        //Escキーでプログラム終了
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
