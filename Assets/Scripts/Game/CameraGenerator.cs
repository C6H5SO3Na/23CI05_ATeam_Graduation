using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGenerator : MonoBehaviour
{
    [SerializeField] GameObject normalCamera;
    [SerializeField] GameObject ueCamera;

    // Start is called before the first frame update
    void Start()
    {
        switch (AppManager.StageName)
        {
            case "2-1":
            case "2-2":
                Instantiate(ueCamera);
                break;

            default:
                Instantiate(normalCamera);
                break;
        }
    }
}
