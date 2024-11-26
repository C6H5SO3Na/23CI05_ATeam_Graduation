using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    //プレイヤー、もしくは敵にぶつかった場合壊れる
    void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
