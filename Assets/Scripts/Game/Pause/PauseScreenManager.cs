using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.SceneManagement;

public class PauseScreenManager : MonoBehaviour
{
    static bool isPause;
    int playerNum = 0;
    [SerializeField] SelectImageManager selectImage;
    AudioSource bgm;
    AudioSource se;
    PauseSound sound;
    [SerializeField] GameObject fade;
    Fade fadeInstance;
    float tookTime = 0f;//経過時間
    public static bool IsPause
    {
        private set { isPause = value; }
        get { return isPause; }
    }
    enum Select
    {
        ToGame, Retry, ToSelect
    }
    enum Phase
    {
        Select, PreFadeOut, FadeOut
    }

    Phase phase;
    // Start is called before the first frame update
    void Start()
    {
        se = GameObject.FindGameObjectWithTag("SE").GetComponent<AudioSource>();
        bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        sound = GetComponent<PauseSound>();
        phase = Phase.Select;
    }

    // Update is called once per frame
    void Update()
    {
        switch (phase)
        {
            case Phase.Select:
                Selecting();
                break;

            case Phase.PreFadeOut:
                tookTime += Time.unscaledDeltaTime;
                if (tookTime >= 1f)
                {
                    //フェードアウト
                    fadeInstance = Instantiate(fade, transform.parent.parent).GetComponent<Fade>();
                    fadeInstance.StartFadeOut(1f);
                    phase = Phase.FadeOut;
                }
                break;

            case Phase.FadeOut:

                if (!fadeInstance.DoFade())
                {
                    AfterFadeOut();
                }
                break;
        }
    }

    void Selecting()
    {
        //イージング中は動作しない
        if (DOTween.IsTweening(selectImage.transform)) { return; }
        //項目選択
        if (Mathf.Abs(Input.GetAxis("Vertical_P" + playerNum.ToString())) > 0f && !DOTween.IsTweening(selectImage.transform))
        {
            se.PlayOneShot(sound.selectSE);
            selectImage.Move(Input.GetAxis("Vertical_P" + playerNum.ToString()));
        }

        //Bで決定
        if (Input.GetButtonDown("Submit"))
        {
            Choice();
        }
    }

    /// <summary>
    /// 決定したときの処理
    /// </summary>
    void Choice()
    {
        se.PlayOneShot(sound.choiceSE);
        switch (selectImage.SelectNum)
        {
            case (int)Select.ToGame:
                Time.timeScale = 1f;
                isPause = false;
                Destroy(transform.parent.gameObject);//すぐにゲーム本編へ復帰
                break;

            case (int)Select.Retry:
                selectImage.ChangeBlinkSpeed(5f);
                phase = Phase.PreFadeOut;
                break;

            case (int)Select.ToSelect:
                selectImage.ChangeBlinkSpeed(5f);
                phase = Phase.PreFadeOut;
                break;
        }
    }

    /// <summary>
    /// フェードアウト後の処理
    /// </summary>
    void AfterFadeOut()
    {
        Time.timeScale = 1f;
        IsPause = false;
        switch (selectImage.SelectNum)
        {
            case (int)Select.ToGame:
                break;//既にDestroyされている

            case (int)Select.Retry:
                bgm.Stop();
                SceneManager.LoadScene("GameScene");
                break;

            case (int)Select.ToSelect:
                bgm.Stop();
                SceneManager.LoadScene("StageSelectScene");
                break;
        }
    }

    public void SetPause(int playerNum)
    {
        this.playerNum = playerNum;
        Time.timeScale = 0f;
        isPause = true;
    }
}
