using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectImageManager : MonoBehaviour
{
    RectTransform rectTransform;
    Vector2 prePosition;//初期位置

    //プロパティ
    public int SelectNum
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

    int selectNum;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        selectNum = 0;
        prePosition = rectTransform.localPosition;
    }

    int Wrap(int value, int low, int high)
    {
        int n = (value - low) % (high - low);
        return (n >= 0) ? (n + low) : (n + high);
    }

    public void Move(float sign)
    {
        selectNum -= (int)Mathf.Sign(sign);
        selectNum = Wrap(selectNum, 0, maxSelect);
        rectTransform.DOAnchorPosY(selectNum * -120f + prePosition.y, 0.5f);//.SetEase(Ease.InOutSine);
    }

    public void ChangeBlinkSpeed(float speed)
    {
        GetComponent<Blinking>().blinkSpeed = speed;
    }
}
