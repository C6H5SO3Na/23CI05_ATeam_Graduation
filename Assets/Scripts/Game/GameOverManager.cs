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
    enum Select
    {
        Retry, ToSelect, ToTitle
    }
    // Start is called before the first frame update
    void Start()
    {
        //bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        //se = GameObject.FindGameObjectWithTag("SE").GetComponent<AudioSource>();

        //bgm.clip = sound.titleBGM;
        //bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //€–Ú‘I‘ð
        if (Mathf.Abs(Input.GetAxis("Vertical_P1")) > 0f && !DOTween.IsTweening(selectImage.transform))
        {
            //se.PlayOneShot(sound.selectSE);
            selectImage.Move(Input.GetAxis("Vertical_P1"));
        }

        //B‚ÅŒˆ’è
        if (Input.GetButtonDown("Submit"))
        {
            //se.PlayOneShot(sound.choiceSE);
            switch (selectImage.SelectNum)
            {
                case (int)Select.Retry:
                    //bgm.Stop();
                    SceneManager.LoadScene("GameScene");
                    break;

                case (int)Select.ToSelect:
                    //bgm.Stop();
                    SceneManager.LoadScene("StageSelectScene");
                    break;

                case (int)Select.ToTitle:
                    //bgm.Stop();
                    SceneManager.LoadScene("TitleScene");
                    break;
            }
        }
    }
}
