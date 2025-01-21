using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToPause : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    Canvas canvas;

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }
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
