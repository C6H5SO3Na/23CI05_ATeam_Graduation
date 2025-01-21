using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushOutFromWall : MonoBehaviour
{
    ConfirmBuriedWall wallChecker;

    // Start is called before the first frame update
    void Start()
    {
        //子オブジェクトにWallCheckerがあるか確認する(WallCheckerが無いとき使用不可)
        wallChecker = GetComponentInChildren<ConfirmBuriedWall>();
        if(!wallChecker)
        {
            Debug.LogWarning("WallCheckerが子オブジェクトにありません");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(wallChecker)
        {
            //壁に埋まっていたら、壁から押し出す
            if(wallChecker.isTouchingWall)
            {
                //衝突位置の取得
                //Vector3 hitPosition = 
            }
        }
    }
}
