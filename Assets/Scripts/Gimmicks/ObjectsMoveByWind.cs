using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsMoveByWind : MonoBehaviour
{
    //-------------------------------------------------------------------------------
    // 変数
    //-------------------------------------------------------------------------------
    Rigidbody rb;                               // このスクリプトをアタッチしているオブジェクトのRigidbody
    private float receivingWindPower = 0f;      // 受けている風の力
    public float ReceivingWindPower
    {
        set { receivingWindPower = value; }
        get { return receivingWindPower; }
    }
    private Vector3 windForce = Vector3.zero;   // 受けている風の強さ
    public Vector3 WindForce
    {
        set { windForce = value; }
        get { return windForce; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //このスクリプトをアタッチしているRigidbodyを取得
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(rb)
        {
            //風に当たっていたら動く
            rb.MovePosition(rb.position + WindForce * Time.fixedDeltaTime);
        }
    }
}
