using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsMoveByWind : MonoBehaviour
{
    Rigidbody rb;                // このスクリプトをアタッチしているオブジェクトのRigidbody
    public Vector3 windForce;    // 受けている風の強さ

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
            rb.AddForce(windForce);
        }
    }
}
