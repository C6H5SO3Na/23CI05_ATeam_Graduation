using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmBuriedWall : MonoBehaviour
{
    public bool isTouchingWall { private set; get; }    // 壁に触れているかどうか
    public Vector3 hitPosition { private set; get; }    // 衝突した場所

    // Start is called before the first frame update
    void Start()
    {
        isTouchingWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        //壁に触れていることを記憶する
        isTouchingWall = true;

        //衝突位置の取得
        //hitPosition = other.
    }

    void OnTriggerExit(Collider other)
    {
        //壁から離れたことを記憶する
        isTouchingWall = false;
    }


}
