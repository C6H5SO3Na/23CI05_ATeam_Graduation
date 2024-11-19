using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    //プレイヤー、もしくは敵にぶつかった場合壊れる
    void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
