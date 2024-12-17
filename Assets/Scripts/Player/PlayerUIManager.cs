using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    TextMeshPro text;
    SpriteRenderer arrow;
    PlayerController player;
    void Start()
    {
        text = transform.GetChild(1).GetComponent<TextMeshPro>();
        arrow = transform.GetChild(0).GetComponent<SpriteRenderer>();
        player = transform.parent.GetComponent<PlayerController>();
        if (player.PlayerNum == 2)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        text.text = player.PlayerNum.ToString() + "P";
        ChangeColor(player.PlayerNum);
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
}
