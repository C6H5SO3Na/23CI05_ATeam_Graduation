using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundaryHandler : MonoBehaviour
{
    Vector3 correctedPosition;
    // Update is called once per frame
    void LateUpdate()
    {
        correctedPosition = transform.position;
        correctedPosition.x = Mathf.Clamp(correctedPosition.x, 0, 21);
        correctedPosition.y = Mathf.Max(correctedPosition.y, 1f);
        correctedPosition.z = Mathf.Clamp(correctedPosition.z, 0, 21);
        transform.position = correctedPosition;
    }
}
