using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectImageManager : MonoBehaviour
{
    RectTransform rectTransform;

    //プロパティ
    public Select SelectNum
    {
        private set
        {
            selectNum = value;
        }
        get
        {
            return selectNum;
        }
    }
    [SerializeField] int maxSelect;

    public enum Select
    {
        SinglePlay = 0, MultiPlay, Option, Credit, Exit
    }

    Select selectNum;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        selectNum = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    int Wrap(int value, int low, int high)
    {
        int n = (value - low) % (high - low);
        return (n >= 0) ? (n + low) : (n + high);
    }

    public void Move(float sign)
    {
        selectNum -= (int)Mathf.Sign(sign);
        selectNum = (Select)Wrap((int)selectNum, 0, maxSelect);
        rectTransform.DOAnchorPosY((int)selectNum * -120f + 60f, 0.5f);//.SetEase(Ease.InOutSine);
    }
}
