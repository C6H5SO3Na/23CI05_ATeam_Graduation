using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPositionToUIPosition : MonoBehaviour
{
    RectTransform rectTransform;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        player = GetComponent<PlayerUIManager>().GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) { return; }
        //オブジェクトのワールド座標
        Vector3 worldPosition = player.transform.position;

        //ワールド座標をスクリーン座標に変換
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        //UIの位置を更新
        this.rectTransform.position = screenPosition;

    }
}
