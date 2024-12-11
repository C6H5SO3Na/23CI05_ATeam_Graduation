using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int gameFPS { get; private set; }
    public static bool isMultiPlay = false;

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

    }
}
