using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToPause : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameManager gameManager;
    void Update()
    {
        //ポーズに遷移しない条件を列挙
        if (PauseScreenManager.IsPause) { return; }//ポーズ画面が既に表示されている
        if (gameManager.isGameOver) { return; }//ゲームオーバー画面が出ている
        if (gameManager.isClear) { return; }//ゲームクリア画面が出ている

        //1コン
        if (Input.GetButtonDown("Pause_P1"))
        {
            GameObject instance = Instantiate(pauseScreen, transform.parent);
            instance.transform.GetChild(0).GetComponent<PauseScreenManager>().SetPause(1);
        }

        //2コン
        if (Input.GetButtonDown("Pause_P2"))
        {
            GameObject instance = Instantiate(pauseScreen, transform.parent);
            instance.transform.GetChild(0).GetComponent<PauseScreenManager>().SetPause(2);
        }
    }
}
