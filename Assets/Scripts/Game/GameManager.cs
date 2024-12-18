using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isMultiPlay = false;
    public bool isClear { get; private set; }
    int playersHP;
    public int PlayersHP
    {
        set { playersHP = value; }
        get { return playersHP; }
    }
    public bool isGameOver { get; private set; }
    [SerializeField] GameUIManager ui;

    // Start is called before the first frame update
    void Start()
    {
        isClear = false;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// クリアした情報を受け取る
    /// </summary>
    public void ReceiveClearInformation()
    {
        isClear = true;
        //Debug.Log("ゲームクリア！");
    }

    /// <summary>
    /// ゲームオーバーした情報を受け取る
    /// </summary>
    public void ReceiveGameOverInformation()
    {
        isGameOver = true;
        Debug.Log("Game Over");
    }

    /// <summary>
    /// ゲームオーバーした情報を受け取る
    /// </summary>
    public void ReceiveDamageInformation(int damageAmount = 1)
    {
        ui.DecreaseHP();
        PlayersHP -= damageAmount;
        if (PlayersHP <= 0)
        {
            ReceiveGameOverInformation();
        }
    }
}
