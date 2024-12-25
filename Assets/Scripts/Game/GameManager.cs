using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isMultiPlay = false;
    public bool isClear { get; private set; }
    int playersHP;
    public int PlayersHP
    {
        set { playersHP = value; }
        get { return playersHP; }
    }
    public bool isGameOver { get; private set; }
    [SerializeField] GameUIManager ui;
    GameBGM sound;
    AudioSource bgm;
    AudioSource se;

    // Start is called before the first frame update
    void Start()
    {
        isClear = false;
        isGameOver = false;
        bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        se = GameObject.FindGameObjectWithTag("SE").GetComponent<AudioSource>();
        sound = GetComponent<GameBGM>();
        bgm.clip = sound.mainBGM;
        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// クリアした情報を受け取る
    /// </summary>
    public void ReceiveClearInformation()
    {
        if (isClear) { return; }
        isClear = true;

        bgm.Stop();
        bgm.PlayOneShot(sound.stageClearBGM);
        ui.ShowClear();
        //Debug.Log("ゲームクリア！");
    }

    /// <summary>
    /// ゲームオーバーした情報を受け取る
    /// </summary>
    public void ReceiveGameOverInformation()
    {
        if (isGameOver) { return; }
        isGameOver = true;

        bgm.Stop();
        bgm.PlayOneShot(sound.gameOverBGM);
        ui.GameOver();
        //Debug.Log("Game Over");
    }

    /// <summary>
    /// ダメージ減少情報を受け取る
    /// </summary>
    public void ReceiveDamageInformation(int damageAmount = 1)
    {
        ui.DecreaseHP(damageAmount);
        PlayersHP -= damageAmount;
        if (PlayersHP <= 0)
        {
            ReceiveGameOverInformation();
        }
    }
}
