using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isMultiPlay = false;
    public bool isClear { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// �N���A���������󂯎��
    /// </summary>
    public void ReceiveClearInformation()
    {
        isClear = true;
        //Debug.Log("�Q�[���N���A�I");
    }
}
