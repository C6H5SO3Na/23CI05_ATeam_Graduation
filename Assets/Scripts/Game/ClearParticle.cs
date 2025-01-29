using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearParticle : MonoBehaviour
{
    void Start()
    {
        //âúÇ…çsÇ≠ÇÊÇ§Ç…Ç∑ÇÈ
        transform.position = transform.parent.position + transform.parent.forward * 10f;
    }
}
