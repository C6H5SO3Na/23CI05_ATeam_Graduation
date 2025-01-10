using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    TextMeshProUGUI text;
    Image arrow;
    [SerializeField] GameObject playerInstance;
    PlayerController player;
    void Start()
    {
        text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        arrow = transform.GetChild(0).GetComponent<Image>();
        player = playerInstance.GetComponent<PlayerController>();
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
}
