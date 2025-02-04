using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGenerator : MonoBehaviour
{
    [SerializeField] GameObject normalCamera;
    [SerializeField] GameObject topViewCamera;

    // Start is called before the first frame update
    void Start()
    {
        switch (AppManager.StageName)
        {
            //2-1Å`2-4
            case "2-1":
            case "2-2":
            //case "2-3":
            case "2-4":
                Instantiate(topViewCamera);
                break;

            default:
                Instantiate(normalCamera);
                break;
        }
    }
}
