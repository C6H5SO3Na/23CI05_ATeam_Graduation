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
    /// �N���A���������󂯎��
    /// </summary>
    public void ReceiveClearInformation()
    {
        if (isClear) { return; }
        isClear = true;
        ui.ShowClear();
        //Debug.Log("�Q�[���N���A�I");
    }

    /// <summary>
    /// �Q�[���I�[�o�[���������󂯎��
    /// </summary>
    public void ReceiveGameOverInformation()
    {
        if (isGameOver) { return; }
        isGameOver = true;
        Debug.Log("Game Over");
        ui.GameOver();
    }

    /// <summary>
    /// �_���[�W���������󂯎��
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
