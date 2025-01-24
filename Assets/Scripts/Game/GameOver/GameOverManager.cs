using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] SelectImageManager selectImage;
    AudioSource bgm;
    AudioSource se;
    GameOverSound sound;
    [SerializeField] GameObject fade;
    Fade fadeInstance;

    float tookTime = 0f;//経過時間
    enum Select
    {
        Retry, ToSelect, ToTitle
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
        sound = GetComponent<GameOverSound>();
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
        if (Mathf.Abs(Input.GetAxis("Vertical_P1")) > 0f && !DOTween.IsTweening(selectImage.transform))
        {
            se.PlayOneShot(sound.selectSE);
            selectImage.Move(Input.GetAxis("Vertical_P1"));
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
            case (int)Select.Retry:
                selectImage.ChangeBlinkSpeed(5f);
                phase = Phase.PreFadeOut;
                break;

            case (int)Select.ToSelect:
                selectImage.ChangeBlinkSpeed(5f);
                phase = Phase.PreFadeOut;
                break;

            case (int)Select.ToTitle:
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
        bgm.Stop();
        switch (selectImage.SelectNum)
        {
            case (int)Select.Retry:
                SceneManager.LoadScene("GameScene");
                break;

            case (int)Select.ToSelect:
                SceneManager.LoadScene("StageSelectScene");
                break;

            case (int)Select.ToTitle:
                SceneManager.LoadScene("TitleScene");
                break;
        }
    }
}
