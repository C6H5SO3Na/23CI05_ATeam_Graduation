using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    TextMeshProUGUI text;
    Image arrow;
    PlayerController player;
    void Start()
    {
        text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        arrow = transform.GetChild(0).GetComponent<Image>();

        if (!GameManager.isMultiPlay && player.OriginalPlayerNum == 2)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        text.text = player.NowPlayerNum.ToString() + "P";
        ChangeColor(player.NowPlayerNum);
    }

    void LateUpdate()
    {
        //固定
        transform.rotation = Quaternion.identity;
    }

    void ChangeColor(int playerNum)
    {
        Color color = Color.white;
        //プレイヤー番号によって色を変える
        switch (playerNum)
        {
            case 1:
                color = Color.red;
                break;

            case 2:
                color = Color.blue;
                break;
        }
        text.color = color;
        arrow.color = color;
    }

    /// <summary>
    /// プレイヤーのインスタンスを設定
    /// </summary>
    /// <param name="player">プレイヤー</param>
    public void SetInstance(PlayerController player)
    {
        this.player = player;
    }

    /// <summary>
    /// プレイヤーを取得
    /// </summary>
    /// <param name="player">プレイヤー</param>
    public PlayerController GetInstance()
    {
        return player;
    }
}
