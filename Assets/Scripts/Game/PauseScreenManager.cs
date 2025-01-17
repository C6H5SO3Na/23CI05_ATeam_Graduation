using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreenManager : MonoBehaviour
{
    static bool isPause;
    int playerNum = 0;
    public static bool IsPause
    {
        private set { isPause = value; }
        get { return isPause; }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 1f;
            isPause = false;
        }
    }

    void SetPause(int playerNum)
    {
        this.playerNum = playerNum;
        Time.timeScale = 0f;
        isPause = true;
    }
}
