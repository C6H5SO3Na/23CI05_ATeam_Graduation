using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPositionToUIPosition : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] GameObject playerInstance;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // オブジェクトのワールド座標
        Vector3 worldPosition = playerInstance.transform.position;


        // ワールド座標をスクリーン座標に変換
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // UIエレメントの位置を更新
        this.rectTransform.position = screenPosition;

    }
}
