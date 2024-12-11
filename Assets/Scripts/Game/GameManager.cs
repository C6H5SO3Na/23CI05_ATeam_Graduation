using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int gameFPS { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //�l�̏�����
        gameFPS = 60;

        //�Q�[���t���[�����[�g�̌Œ�
        Application.targetFrameRate = gameFPS;
    }

    // Update is called once per frame
    void Update()
    {
        //Esc�L�[�Ńv���O�����I��
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
