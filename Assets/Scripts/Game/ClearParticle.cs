using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearParticle : MonoBehaviour
{
    void Start()
    {
        //奥に行くようにする
        transform.position = transform.parent.position + transform.parent.forward * 10f;
    }
}
