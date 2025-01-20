using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToPause : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] Canvas canvas;
    void Update()
    {
        if (PauseScreenManager.IsPause) { return; }
        if (Input.GetButtonDown("Pause_P1"))
        {
            GameObject instance = Instantiate(pauseScreen, canvas.transform);
            instance.transform.GetChild(0).GetComponent<PauseScreenManager>().SetPause(1);
        }

        if (Input.GetButtonDown("Pause_P2"))
        {
            GameObject instance = Instantiate(pauseScreen, canvas.transform);
            instance.transform.GetChild(0).GetComponent<PauseScreenManager>().SetPause(2);
        }
    }
}
